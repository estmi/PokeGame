using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Class
{
    /// <summary>
    /// Classe abstracta que defineix una persona.
    /// </summary>
    public abstract class Persona:Posicio
    {
        Point posInicial;
        Direccio direccio;
        public Direccio Direccio {  get { return direccio; } set { direccio = value; } }
        /// <summary>
        /// Crea una persona
        /// </summary>
        /// <param name="nom">Strng que identifica la persona</param>
        /// <param name="fil">Fila on està localitzada</param>
        /// <param name="col">Columna on està localitzada</param>
        public Persona(string nom = "", int fil = 0, int col = 0) : base(nom,fil, col) 
        { 
            AllowDrop = false;
            PreviewMouseLeftButtonDown += Persona_PreviewMouseLeftButtonDown;
            MouseMove += Persona_MouseMove;
        }

        private void Persona_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point posicioActual = e.GetPosition(null);
            Vector desplacament = posInicial - posicioActual;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(desplacament.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(desplacament.Y) > SystemParameters.MinimumVerticalDragDistance
               ))
            {
                Persona p = sender as Persona;
                DataObject dadesArrosegades = new DataObject("PERSONA", p);
                DragDrop.DoDragDrop(p, dadesArrosegades, DragDropEffects.Move);
            }
        }

        private void Persona_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            posInicial = e.GetPosition(null);
        }

        /// <summary>
        /// Retorna que la posició ocupada per aquesta persona no està buida
        /// </summary>
        public override bool EsBuida => false;
        /// <summary>
        /// Atraccio de persona sobre una determinada posicio
        /// </summary>
        /// <param name="fil">Fila de la posició</param>
        /// <param name="col">Columan de la posició</param>
        /// <param name="esc">Escenari on estan situats</param>
        /// <returns>Atracció quantificada</returns>
        
        /// <summary>
        /// Decideix quin serà el següent moviment que farà la persona
        /// </summary>
        /// <param name="esc">Escenari on esta situada la persona</param>
        /// <returns>Una de les 5 possibles direccions (Quiet, Amunt, Avall, Dreta, Esquerra</returns>
        public Direccio OnVaig(Escenari esc)
        {
            List<Direccio> llista = new() { Direccio };
            if (esc.DestiValid(Fila-1,Columna)&&Direccio!=Direccio.Sud)
            {
                //Nord
                llista.Add(Direccio.Nord);
            }    

            if (esc.DestiValid(Fila, Columna+1) && Direccio != Direccio.Oest)
            {
                llista.Add(Direccio.Est);
            }

            if (esc.DestiValid(Fila + 1, Columna) && Direccio != Direccio.Nord)
            {
                llista.Add(Direccio.Sud);
            }

            if (esc.DestiValid(Fila , Columna-1) && Direccio != Direccio.Est)
            {
                llista.Add(Direccio.Oest);
            }
            
            Direccio onVaig = llista[random.Next(llista.Count)];
            
            if (onVaig != Direccio)
            {
                Direccio = onVaig;
                Nom = Direccio.ToString();
                return Direccio.Quiet;
            }
            else
            {
                return Direccio;
            }
        }
    }

}
