using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public abstract class Persona:Posicio
    {
        /// <summary>
        /// Crea una persona
        /// </summary>
        /// <param name="nom">Strng que identifica la persona</param>
        /// <param name="fil">Fila on està localitzada</param>
        /// <param name="col">Columna on està localitzada</param>
        public Persona(string nom = "", int fil = 0, int col = 0) : base(fil, col) { Nom = nom; }
        /// <summary>
        /// Obté el nom de la persona
        /// </summary>
        public string Nom { get; }
        /// <summary>
        /// Retorna que la posició ocupada per aquesta persona no està buida
        /// </summary>
        public override bool Buida => false;
        /// <summary>
        /// Atraccio de persona sobre una determinada posicio
        /// </summary>
        /// <param name="fil">Fila de la posició</param>
        /// <param name="col">Columan de la posició</param>
        /// <param name="esc">Escenari on estan situats</param>
        /// <returns>Atracció quantificada</returns>
        private double Atraccio(int fil, int col, Escenari esc)
        {
            return 0;
        }
        /// <summary>
        /// Decideix quin serà el següent moviment que farà la persona
        /// </summary>
        /// <param name="esc">Escenari on esta situada la persona</param>
        /// <returns>Una de les 5 possibles direccions (Quiet, Amunt, Avall, Dreta, Esquerra</returns>
        public Direccio OnVaig(Escenari esc)
        {
            //TODO OnVaig
            throw new NotImplementedException();
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
