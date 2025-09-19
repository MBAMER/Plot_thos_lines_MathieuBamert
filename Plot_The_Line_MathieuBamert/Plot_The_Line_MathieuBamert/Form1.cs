using ScottPlot.WinForms;
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

        double[] dataX = { 0, 1, 2, 3, 4, 5, 6,7 };
        double[] dataY = { 0, 10, 4, 9, 16, 25, 36, 6 };

        FormsPlot1.Plot.Add.Scatter(dataX, dataY);
        FormsPlot1.Refresh();
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
            Console.WriteLine("Fichier sélectionné : " + selectedFile);
        }
    }
}
