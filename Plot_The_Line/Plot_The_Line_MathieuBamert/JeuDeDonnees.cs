using System;
using System.Collections.Generic;
using System.Drawing;

namespace PlotTheLine
{
    /// <summary>
    /// Représente un jeu de données importé depuis un fichier CSV.
    /// </summary>
    public class JeuDeDonnees
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
}
