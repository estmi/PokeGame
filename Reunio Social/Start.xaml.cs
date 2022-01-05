using Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReunioSocial
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            
            InitializeComponent();
            setSliders();
            
        }
        /// <summary>
        /// Posa tots els sliders als defaults utilitzant LimitsEscenari i DefaultsEscenari
        /// </summary>
        private void setSliders()
        {
            sldCambrers.Maximum = (int)LimitsEscenari.Cambrers;
            sldColumnes.Maximum = (int)LimitsEscenari.Columnes;
            sldFiles.Maximum = (int)LimitsEscenari.Files;
            sldDones.Maximum = (int)LimitsEscenari.Dones;
            sldHomes.Maximum = (int)LimitsEscenari.Homes;

            sldColumnes.Minimum = 1;
            sldFiles.Minimum = 1;

            sldCambrers.Value = (int)DefaultsEscenari.Cambrers;
            sldColumnes.Value = (int)DefaultsEscenari.Columnes;
            sldFiles.Value = (int)DefaultsEscenari.Files;
            sldDones.Value = (int)DefaultsEscenari.Dones;
            sldHomes.Value = (int)DefaultsEscenari.Homes;
        }

        private void btnSimulationStart_Click(object sender, RoutedEventArgs e)
        {
            // Creem un objecte de configuracio
            Settings settings = new()
            {
                nHomes = (int)sldHomes.Value,
                nDones = (int)sldDones.Value,
                nCambrers = (int)sldCambrers.Value,
                nFiles = (int)sldFiles.Value,
                nColumnes = (int)sldColumnes.Value
            };
            // inciem una Simulacio amb la configuracio descrita
            Window simulacio = new Simulacio(settings);
            simulacio.Show();

        }
    }
}
