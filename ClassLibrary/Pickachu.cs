using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class Pickachu : Persona
    {
        
        /// <summary>
        /// Crea un convidat
        /// </summary>
        /// <param name="nom">Caràcter que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia sobre el sexe contrari</param>
        public Pickachu() :base(Direccio.Quiet.ToString())
        {
        }
        public override string ToString()
        {
            return Nom + base.ToString();
        }


    }
}
