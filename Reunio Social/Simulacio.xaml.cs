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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Class;

namespace ReunioSocial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Simulacio : Window
    {
        //Settings s = new();
        Escenari Escenari;
        Point posInicial;
        public Simulacio()
        {
            InitializeComponent();
        }
        public Simulacio(Settings settings) : this()
        {
            GC.Collect();
            // Creem un Escenari amb la configuracio donada per settings
            Escenari = new(settings.nFiles, settings.nColumnes, settings.nHomes, settings.nDones, settings.nCambrers);
            Escenari.ShowGridLines = true;
            // Li afegim un Drop per quan moguem persones poder posar-les a lloc
            Escenari.Drop += Escenari_Drop;
            // Posicionem l'Escenari al Grid Principal i l'afegim al Grid
            Grid.SetColumn(Escenari, 1);
            Grid.SetRow(Escenari, 0);
            grd.Children.Add(Escenari);
            // Li assignem un Drop per poder fer "Sortir" a les persones
            Drop += Simulacio_Drop;
        }

        private void Escenari_Drop(object sender, DragEventArgs e)
        {
            // Prevenim Bubbling
            e.Handled = true;
            // Si es una persona la posarem al lloc designat
            if (e.Data.GetDataPresent("PERSONA"))
            {
                Escenari Escenari = sender as Escenari;
                Posicio pos = e.Source as Posicio;
                Persona p = e.Data.GetData("PERSONA") as Persona;
                // Buidem la posicio d'on prove la persona
                Escenari.Buida(p.Fila, p.Columna);
                // Li canviem les propietats de posicio
                p.Fila = pos.Fila;
                p.Columna = pos.Columna;
                // La posem a la seva nova posicio
                Escenari.Posa(p);
            }
           
        }
        // Nomes pot activar-se amb AllowDrop de el senyal de Exit ja que tots els altres estan handled
        private void Simulacio_Drop(object sender, DragEventArgs e)
        {
            // Si es una persona la eliminara del Taulell i de la llista de persones
            if (e.Data.GetDataPresent("PERSONA"))
            {
                Simulacio s = sender as Simulacio;
                Image pos = e.Source as Image;
                Persona p = e.Data.GetData("PERSONA") as Persona;
                
                s.Escenari.Elimina(p);
            }
        }

        private void btnCicle_Click(object sender, RoutedEventArgs e)
        {
            Escenari.Cicle();
        }

        

        private void btnSurt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Preview");
            posInicial = e.GetPosition(null);
        }

        

        private void btnAdeu_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("PERSONA"))
            {

            }
            else e.Effects = DragDropEffects.None;
        }
    }
}
