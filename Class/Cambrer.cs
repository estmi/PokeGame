using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class Cambrer : Persona
    {
        private static int NCambrer = 1;
        /// <summary>
        /// Crea un cambrer (Persona de la que no importa el nom, i es dirà "Cambrer 1",
        /// "Cambrer 2", "Cambrer 3", "Cambrer 4" ... "Cambrer N"/// </summary>
        public Cambrer():base(String.Format("Cambrer {0}",NCambrer++)) { }
        /// <summary>
        /// Interès del cambrer per una posició
        /// </summary>
        /// <param name="pos">posició per la que s'interessa</param>
        /// <returns>Retorna 0 si no hi ha ningú, 1 si hi ha un convidat i -1 si un cambrer</returns>
        public override int Interes(Posicio pos) 
        {
            if (pos is Persona p)
            {
                return p.EsConvidat() ? 1 : -1;
            }
            else return 0;

        }
        /// <summary>
        /// Retorna que el Cambrer no és un convidat
        /// </summary>
        /// <returns>false</returns>
        public override bool EsConvidat() => false;
    }
}
