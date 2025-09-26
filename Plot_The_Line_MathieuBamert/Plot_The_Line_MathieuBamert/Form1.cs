using ScottPlot.WinForms;
using System.Windows.Forms;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Plot_The_Line_MathieuBamert;
public partial class Form1 : Form
{
    
    readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

    private List<double> colonne3 = new();
    private List<double> colonne4 = new();
    private List<double> colonne5 = new();
    private List<DateTime> dates = new();
    private string acronyme = ""; // Ajoutez ce champ à la classe

    public Form1()
    {
        InitializeComponent();

        
        panel1.Controls.Add(FormsPlot1);


    }

    private void ImporterFichierCSV(object sender, EventArgs e)
    {

        // Ouvrir l'explorateur de fichier pour sélectionner le fichier CSV
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Title = "Sélectionnez un fichier",
            Filter = "TFichiers texte (*.csv)|*.csv",
            InitialDirectory = @"C:\"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string selectedFile = openFileDialog.FileName;
                var lignes = File.ReadAllLines(selectedFile);

                dates.Clear();
                colonne3.Clear();
                colonne4.Clear();
                colonne5.Clear();

                foreach (string ligne in lignes.Skip(1))
                {
                    string[] valeurs = ligne.Split(';');
                    if (valeurs.Length >= 5)
                    {
                        if (DateTime.TryParse(valeurs[1], out DateTime date))
                            dates.Add(date);

                        if (double.TryParse(valeurs[2], out double valCol3) &&
                            double.TryParse(valeurs[3], out double valCol4) &&
                            double.TryParse(valeurs[4], out double valCol5))
                        {
                            colonne3.Add(valCol3);
                            colonne4.Add(valCol4);
                            colonne5.Add(valCol5);
                        }
                    }
                }

                if (dates.Count == 0 || colonne3.Count == 0)
                    throw new Exception("Aucune donnée valide trouvée dans le fichier.");

                // Extraire l'acronyme (caractères 9 à 11 du nom du fichier sans extension)
                string nomFichier = Path.GetFileNameWithoutExtension(selectedFile);
                acronyme = nomFichier.Length >= 11 ? nomFichier.Substring(8, 3) : "";

                // Remplir la CheckedListBox avec le nom + acronyme
                checkedListBoxTemp.Items.Clear();
                checkedListBoxTemp.Items.Add($"Température moyenne ({acronyme})", true);
                checkedListBoxTemp.Items.Add($"Température maximum ({acronyme})", true);
                checkedListBoxTemp.Items.Add($"Température minimum ({acronyme})", true);

                AfficherCourbesSelectionnees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors de l’importation du fichier :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


    }

    private void CheckedListBoxTemp_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        // Attendre la fin de l’événement pour que l’état soit mis à jour
        BeginInvoke((Action)AfficherCourbesSelectionnees);
    }

    private void AfficherCourbesSelectionnees()
    {
        double[] dataX = dates.Select(d => d.ToOADate()).ToArray();

        FormsPlot1.Plot.Clear();

        if (checkedListBoxTemp.CheckedItems.Contains($"Température moyenne ({acronyme})"))
            FormsPlot1.Plot.Add.Scatter(dataX, colonne3.ToArray());
        if (checkedListBoxTemp.CheckedItems.Contains($"Température minimum ({acronyme})"))
            FormsPlot1.Plot.Add.Scatter(dataX, colonne4.ToArray());
        if (checkedListBoxTemp.CheckedItems.Contains($"Température maximum ({acronyme})"))
            FormsPlot1.Plot.Add.Scatter(dataX, colonne5.ToArray());

        FormsPlot1.Plot.Axes.DateTimeTicksBottom();
        FormsPlot1.Refresh();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}
