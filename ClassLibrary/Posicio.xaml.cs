using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Class
{
    /// <summary>Classe base per a totes les caselles de l'escenari.</summary>
    public partial class Posicio : UserControl
    {
        public static Random random = new Random();
        private Image imgIcona = new();
        private TextBlock tbNom = new();
        /// <summary>
        /// Crea una nova posició
        /// </summary>
        /// <param name="fil">Fila de la Posició</param>
        /// <param name="col">Columna de la Posició</param>
        public Posicio(string nom = "", int fil = 0, int col = 0)
        {
            InitializeComponent();
            Nom = nom;
            Columna = col;
            Fila = fil;
            tbNom.Text = Nom;
            DockPanel.SetDock(tbNom, Dock.Bottom);
            dock.Children.Add(tbNom);
            dock.Children.Add(imgIcona);
            tbNom.TextAlignment = System.Windows.TextAlignment.Center;
            tbNom.FontSize = 20;
            AllowDrop = true;
            DragEnter += Posicio_DragEnter;
            
        }


        private void Posicio_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent("PERSONA"))
            {
                
            }
            else if (e.Data.GetDataPresent("PERSONATIPUS"))
            {

            }
            else e.Effects = DragDropEffects.None;
        }

        /// <summary>
        /// Obté o assigna el nom de la posició
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Assigna o obté la columna de la posicio
        /// </summary>
        public int Columna { get; set; }
        /// <summary>
        /// Assigna o obté la fila de la posicio
        /// </summary>
        public int Fila { get; set; }
        /// <summary>
        /// Retorna si la posició està o no buida
        /// </summary>
        public virtual bool EsBuida => true;
        /// <summary>
        /// Dóna accés al control Image incrustat
        /// </summary>
        public ImageSource Icona
        {
            get { return imgIcona.Source; }
            set { imgIcona.Source = value; }

        }
        /// <summary>
        /// Retorna la distància entre dues posicions
        /// </summary>
        /// <param name="pos1">Primera posició</param>
        /// <param name="pos2">Segona posició</param>
        /// <returns>Distància entre les dues posicions</returns>
        public static double Distancia(Posicio pos1, Posicio pos2) =>
            Math.Sqrt(
                Math.Pow(
                    Math.Abs(pos1.Columna - pos2.Columna),
                    2) +
                Math.Pow(
                    Math.Abs(pos1.Fila - pos2.Fila),
                    2)
                );



    }
}
