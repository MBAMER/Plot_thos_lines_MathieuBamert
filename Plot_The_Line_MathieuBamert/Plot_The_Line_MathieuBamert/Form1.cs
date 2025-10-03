using ScottPlot.WinForms;
using System.Windows.Forms;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace Plot_The_Line_MathieuBamert
{
    public partial class Form1 : Form
    {
        readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        // Classe pour stocker un fichier import�
        private class JeuDeDonnees
        {
            public string Acronyme { get; set; } = "";
            public List<DateTime> Dates { get; set; } = new();
            public List<double> Moyenne { get; set; } = new();
            public List<double> Maximum { get; set; } = new();
            public List<double> Minimum { get; set; } = new();
            public Color CouleurMoyenne { get; set; }
            public Color CouleurMaximum { get; set; }
            public Color CouleurMinimum { get; set; }
        }

        private List<JeuDeDonnees> tousLesJeux = new();

        // Palette de couleurs cyclique
        private Color[][] paletteCouleurs = new Color[][]
        {
            new[] { Color.Blue, Color.Green, Color.Red },
            new[] { Color.Purple, Color.Orange, Color.Cyan },
            new[] { Color.Magenta, Color.Brown, Color.Lime },
            new[] { Color.DarkBlue, Color.DarkGreen, Color.DarkRed }
        };

        public Form1()
        {
            InitializeComponent();
            panel1.Controls.Add(FormsPlot1);
        }

        private void ImporterFichierCSV(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "S�lectionnez un fichier",
                Filter = "Fichiers texte (*.csv)|*.csv",
                InitialDirectory = @"C:\"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                string selectedFile = openFileDialog.FileName;

                // Lecture CSV 
                var lignes = File.ReadAllLines(selectedFile)
                    .Skip(1)
                    .Select(l => l.Split(';'))
                    .Where(v => v.Length >= 5)
                    .ToList();

                var dates = lignes
                    .Select(v => DateTime.TryParse(v[1], out var d) ? d : (DateTime?)null)
                    .Where(d => d.HasValue)
                    .Select(d => d.Value)
                    .ToList();

                var colMoy = lignes
                    .Select(v => double.TryParse(v[2], out var d) ? d : double.NaN)
                    .Where(d => !double.IsNaN(d))
                    .ToList();

                var colMax = lignes
                    .Select(v => double.TryParse(v[3], out var d) ? d : double.NaN)
                    .Where(d => !double.IsNaN(d))
                    .ToList();

                var colMin = lignes
                    .Select(v => double.TryParse(v[4], out var d) ? d : double.NaN)
                    .Where(d => !double.IsNaN(d))
                    .ToList();

                if (!dates.Any() || !colMoy.Any())
                    throw new Exception("Aucune donn�e valide trouv�e dans le fichier.");

                string nomFichier = Path.GetFileNameWithoutExtension(selectedFile);
                string acronyme = nomFichier.Length >= 11 ? nomFichier.Substring(8, 3) : "";

                // Choix des couleurs pour ce fichier
                var couleurs = paletteCouleurs[tousLesJeux.Count % paletteCouleurs.Length];

                var jeu = new JeuDeDonnees
                {
                    Acronyme = acronyme,
                    Dates = dates,
                    Moyenne = colMoy,
                    Maximum = colMax,
                    Minimum = colMin,
                    CouleurMoyenne = couleurs[0],
                    CouleurMaximum = couleurs[1],
                    CouleurMinimum = couleurs[2]
                };

                tousLesJeux.Add(jeu);

                // Ajout dynamique dans CheckedListBox via LINQ lambda
                new[] { "moyenne", "maximum", "minimum" }
                    .ToList()
                    .ForEach(type =>
                    {
                        string nomItem = $"Temp�rature {type} ({acronyme})";
                        checkedListBoxTemp.Items.Add(nomItem, true);
                    });

                MessageBox.Show(
                    $"Le fichier '{Path.GetFileName(selectedFile)}' a �t� import� avec succ�s !",
                    "Importation r�ussie",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                AfficherCourbesSelectionnees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors de l�importation du fichier :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        
        private void AfficherCourbesSelectionnees(Dictionary<string, bool>? etatsCases = null)
        {
            FormsPlot1.Plot.Clear();

            tousLesJeux.ForEach(jeu =>
            {
                double[] dataX = jeu.Dates.Select(d => d.ToOADate()).ToArray();

                new[]
                {
                    new { Nom = $"Temp�rature moyenne ({jeu.Acronyme})", Donnees = jeu.Moyenne, Couleur = jeu.CouleurMoyenne },
                    new { Nom = $"Temp�rature maximum ({jeu.Acronyme})", Donnees = jeu.Maximum, Couleur = jeu.CouleurMaximum },
                    new { Nom = $"Temp�rature minimum ({jeu.Acronyme})", Donnees = jeu.Minimum, Couleur = jeu.CouleurMinimum }
                }
                .ToList()
                .ForEach(c =>
                {
                    bool estCoche = etatsCases != null
                         ?etatsCases.GetValueOrDefault(c.Nom, false)
                        : checkedListBoxTemp.GetItemChecked(checkedListBoxTemp.Items.IndexOf(c.Nom));

                    if (estCoche)
                    {
                        FormsPlot1.Plot.Add.Scatter(
                            dataX,
                            c.Donnees.ToArray(),
                            color: ScottPlot.Color.FromColor(c.Couleur)
                        ).Label = c.Nom;
                    }
                });

                
            });

            FormsPlot1.Plot.Axes.DateTimeTicksBottom();
            FormsPlot1.Plot.Legend.IsVisible = true;
            FormsPlot1.Refresh();
        }
    }
}
