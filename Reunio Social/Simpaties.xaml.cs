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
using Xceed.Wpf.Toolkit;

namespace ReunioSocial
{
    /// <summary>
    /// Interaction logic for Simpaties.xaml
    /// </summary>
    public partial class Simpaties : Window
    {
        public Simpaties()
        {
            InitializeComponent();
            grd.ShowGridLines = true;
        }
        public Simpaties(TaulaPersones tp) : this()
        {
            // Amplada predefinida per les columnes amb nom de convidats
            int ampladaColumna = 60;
            // Recompte de convidats afegits a la taula
            int nConvidats = 0;
            // Estableix si esta a la primera volta del bucle
            bool primer = true;
            int nFila = 1;
            foreach (KeyValuePair<String, Persona> kvp in tp)
            {
                // Recollim el valor del kvp 
                Persona persona = kvp.Value;
                // Comprovem si es un convidat
                // Si es convidat continuem, si no ho es, no fem res
                if (persona.EsConvidat())
                {
                    // El transformem per comoditat
                    Convidat convidat = persona as Convidat;


                    RowDefinition rd = new();
                    rd.Height = new GridLength(30);
                    // Afegim una fila nova al Grid
                    grd.RowDefinitions.Add(rd);
                    // Creem un textblock que contindra el nom del convidat de la fila
                    TextBlock tb = new();
                    // Assignem propietats al textblock
                    tb.Text = persona.Nom;
                    tb.TextAlignment = TextAlignment.Center;
                    // Li assignem les propietats del Grid per poder-lo posicionar
                    Grid.SetColumn(tb, 0);
                    Grid.SetRow(tb, nFila);
                    // L'afegim al Grid un cop esta ben posicionat i amb totes les propietats
                    grd.Children.Add(tb);
                    // Creem un IntegerUpDown el qual mostrara l'atraccio per el sexe oposat
                    IntegerUpDown iUpDownSexe = new();
                    // Li direm que el seu rang sera [0,3] i el valor per defecte sera el PlusSexe del convidat
                    iUpDownSexe.Minimum = 0;
                    iUpDownSexe.Maximum = 3;
                    iUpDownSexe.Value = convidat.PlusSexe;
                    // En el tag li posarem el convidat al qual referencia per poder-lo manipular des de l'event ValueChanged
                    iUpDownSexe.Tag = convidat;
                    // Li assignem un event que actualitzara el valor del convidat de la taula de persones
                    iUpDownSexe.ValueChanged += IUpDownSexe_ValueChanged;
                    // Assignacio de propietats per poder-lo posicionar en el Grid
                    Grid.SetColumn(iUpDownSexe, 1);
                    Grid.SetRow(iUpDownSexe, nFila);
                    // L'afegim i posicionem dins el grid
                    grd.Children.Add(iUpDownSexe);

                    int nColumna = 2;
                    foreach (KeyValuePair<String, Persona> kvpSimpaties in tp)
                    {
                        // Recollim el valor del kvp 
                        Persona person = kvpSimpaties.Value;
                        // Comprovem si es un convidat
                        // Si es convidat continuem, si no ho es, no fem res
                        if (person.EsConvidat())
                        {
                            // El transformem per comoditat
                            Convidat c = person as Convidat;
                            // Si es la primera volta crearem la primera fila que conte tots els noms de convidats
                            if (primer)
                            {
                                // Fem recompte de convidats
                                nConvidats++;
                                // Creem una columna
                                ColumnDefinition cd = new();
                                // Definim l'amplada de la columna
                                cd.Width = new GridLength(ampladaColumna);
                                // Afegim la columna al Grid
                                grd.ColumnDefinitions.Add(cd);
                                // Creem un textblock que contindra el nom del convidat a la primera linia del Grid
                                TextBlock tb2 = new();
                                // Li assignem propietats
                                tb2.Text = person.Nom;
                                tb2.TextAlignment = TextAlignment.Center;
                                // Li assignem les propietats del Grid per poder-lo posicionar
                                Grid.SetColumn(tb2, nColumna);
                                Grid.SetRow(tb2, 0);
                                // L'afegim al Grid un cop esta ben posicionat i amb totes les propietats
                                grd.Children.Add(tb2);
                            }
                            // Aquest integerUpDown contindra la relacio de simpatia entre el convidat de la Fila amb el convidat de la Columma
                            IntegerUpDown iUpDown = new();
                            // El rang de l'IntegerUpDown es el que tenen les simapties [-5,5]
                            iUpDown.Minimum = -5;
                            iUpDown.Maximum = 5;
                            // El valor per defecte sera la relacio actual
                            iUpDown.Value = convidat[c.Nom];
                            // Aquest integerUpDown en el Tag contindra un Objecte amb from i to 
                            //     from i to son dos propietats de la classe SimpatiaIntegerUpDown
                            iUpDown.Tag = new SimpatiaIntegerUpDown
                            {
                                from = convidat,
                                to = c
                            };
                            // Li assignem un event que actualitzara el valor de l'atraccio del convidat de la taula de persones
                            iUpDown.ValueChanged += IUpDown_ValueChanged;
                            // Si la Fila i la Columna contenen el mateix Convidat despres sera anulat el IntegerUpDown suposant de que no pots interessar-te mes o menys per tu mateix
                            if (c.Nom == persona.Nom)
                                iUpDown.IsEnabled = false;
                            // Posicionem l'objecte en el Grid
                            Grid.SetColumn(iUpDown, nColumna++);
                            Grid.SetRow(iUpDown, nFila);
                            // L'afegim al Grid un cop esta ben posicionat i amb totes les propietats
                            grd.Children.Add(iUpDown);
                        }
                    }
                    nFila++;
                    primer = false;
                }
            }
            // Assignem un mida per defecte a la finestra
            Width = nConvidats * ampladaColumna + 165;
            Height = nConvidats * 30 + 80;
        }
        /// <summary>
        /// Aquest event agafara el nou valor del IntegerUpDown i l'assignara al convidat especificat en el seu tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IUpDownSexe_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown iUpDown = sender as IntegerUpDown;
            Convidat c = iUpDown.Tag as Convidat;
            c.PlusSexe = (int)e.NewValue;
        }
        /// <summary>
        /// Aquest event agafara el nou valor del IntegerUpDown i l'assignara a la relacio de simpaties entre el convidat from del tag de l'IntegerUpDown i el to 
        ///     from i to son dos propietats de la classe SimpatiaIntegerUpDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown iUpDown = sender as IntegerUpDown;
            SimpatiaIntegerUpDown sIUD = iUpDown.Tag as SimpatiaIntegerUpDown;
            sIUD.from[sIUD.to.Nom] = (int)e.NewValue;
        }
    }
}
