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
        private double Atraccio(int fil, int col, Escenari esc)
        {
            double atraccio = 0;
            foreach (KeyValuePair<String,Persona> kvp in esc.Gent)
            {
                Persona persona = kvp.Value;
                if (persona.Columna != col && persona.Fila != fil)
                    atraccio+= Interes(persona) / Distancia(esc[fil, col], persona );
            }
            //return Interes(esc[fil, col]) / Distancia(this, new("", fil, col));
            return atraccio;
        }
        /// <summary>
        /// Decideix quin serà el següent moviment que farà la persona
        /// </summary>
        /// <param name="esc">Escenari on esta situada la persona</param>
        /// <returns>Una de les 5 possibles direccions (Quiet, Amunt, Avall, Dreta, Esquerra</returns>
        public Direccio OnVaig(Escenari esc)
        {
            List<Direccio> llista = new() { Direccio.Quiet };
            double gran = Atraccio(Fila, Columna, esc);
            Dictionary<Direccio,Double > onAnire = new();
            if (esc.DestiValid(Fila-1,Columna))
            {   
                //Nord
                double atracAct = Atraccio(Fila - 1, Columna, esc);

                if (atracAct > gran) llista = new() { Direccio.Nord };
                else if (atracAct == gran) llista.Add(Direccio.Nord);
            }    

            if (esc.DestiValid(Fila - 1, Columna+1))
            {
                double atracAct = Atraccio(Fila - 1, Columna+1, esc);

                if (atracAct > gran) llista = new() { Direccio.Nordest};
                else if (atracAct == gran) llista.Add(Direccio.Nordest);
            }

            if (esc.DestiValid(Fila, Columna+1))
            {
                double atracAct = Atraccio(Fila, Columna + 1, esc);

                if (atracAct > gran) llista = new() { Direccio.Est };
                else if (atracAct == gran) llista.Add(Direccio.Est);
            }

            if (esc.DestiValid(Fila + 1, Columna+1))
            {
                double atracAct = Atraccio(Fila + 1, Columna + 1, esc);

                if (atracAct > gran) llista = new() { Direccio.Sudest };
                else if (atracAct == gran) llista.Add(Direccio.Sudest);
            }
            

            if (esc.DestiValid(Fila + 1, Columna))
            {
                double atracAct = Atraccio(Fila + 1, Columna, esc);

                if (atracAct > gran) llista = new() { Direccio.Sud };
                else if (atracAct == gran) llista.Add(Direccio.Sud);
            }
            

            if (esc.DestiValid(Fila + 1, Columna-1))
            {
                double atracAct = Atraccio(Fila + 1, Columna - 1, esc);

                if (atracAct > gran) llista = new() { Direccio.Sudoest };
                else if (atracAct == gran) llista.Add(Direccio.Sudoest);
            }
            

            if (esc.DestiValid(Fila , Columna-1))
            {
                double atracAct = Atraccio(Fila , Columna - 1, esc);

                if (atracAct > gran) llista = new() { Direccio.Oest };
                else if (atracAct == gran) llista.Add(Direccio.Oest);
            }
            

            if (esc.DestiValid(Fila -1, Columna-1))
            {
                double atracAct = Atraccio(Fila - 1, Columna - 1, esc);

                if (atracAct > gran) llista = new() { Direccio.Noroest };
                else if (atracAct == gran) llista.Add(Direccio.Noroest);
            }
            
            return llista[random.Next(llista.Count)];
        }
        /// <summary>
        /// Interès de la persona sobre una determinada posició
        /// </summary>
        /// <param name="pos">Posició</param>
        /// <returns>Interès quantificat</returns>
        public abstract int Interes(Posicio pos);
        /// <summary>
        /// Determina si la persona es un convidat (home o dona) o un cambrer
        /// </summary>
        /// <returns>Retorna si és convidat</returns>
        public abstract bool EsConvidat();
    }

}
