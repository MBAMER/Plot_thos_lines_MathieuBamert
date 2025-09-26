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
    // Create an instance of a FormsPlot like this
    readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

    public Form1()
    {
        InitializeComponent();

        // Add the FormsPlot to the panel
        panel1.Controls.Add(FormsPlot1);

       
    }

    private void ImporterFichierCSV(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Title = "Sélectionnez un fichier",
            Filter = "TFichiers texte (*.csv)|*.csv",
            InitialDirectory = @"C:\"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string selectedFile = openFileDialog.FileName;

            // Lire toutes les lignes du CSV
            var lignes = File.ReadAllLines(selectedFile);

            // Listes pour stocker les colonnes 3, 4 et 5
            List<double> colonne3 = new List<double>();
            List<double> colonne4 = new List<double>();
            List<double> colonne5 = new List<double>();

            foreach (string ligne in lignes.Skip(1)) // Skip(1) pour ignorer la ligne d’entête
            {
                string[] valeurs = ligne.Split(';'); // ajuste le séparateur si nécessaire

                if (valeurs.Length >= 5) // On vérifie qu’il y a au moins 5 colonnes
                {
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

            // Axe X = indices des lignes
            double[] dataX = Enumerable.Range(0, colonne3.Count).Select(i => (double)i).ToArray();

            // Récupérer les Y
            double[] dataY0 = colonne3.ToArray(); // courbe colonne 3
            double[] dataY1 = colonne4.ToArray(); // courbe colonne 4
            double[] dataY2 = colonne5.ToArray(); // courbe colonne 5

            // Affichage
            FormsPlot1.Plot.Clear();
            FormsPlot1.Plot.Add.Scatter(dataX, dataY0);
            FormsPlot1.Plot.Add.Scatter(dataX, dataY1);
            FormsPlot1.Plot.Add.Scatter(dataX, dataY2);
        
            FormsPlot1.Refresh();
        }
    }
}
