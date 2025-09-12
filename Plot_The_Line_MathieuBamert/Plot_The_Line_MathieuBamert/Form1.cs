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

        double[] dataX = { 0, 1, 2, 3, 4, 5 };
        double[] dataY = { 0, 10, 4, 9, 16, 25 };

        FormsPlot1.Plot.Add.Scatter(dataX, dataY);
        FormsPlot1.Refresh();
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }
}
