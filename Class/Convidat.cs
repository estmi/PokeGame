using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public abstract class Convidat : Persona
    {
        Dictionary<String, int> simpaties = new();
        /// <summary>
        /// Crea un convidat
        /// </summary>
        /// <param name="nom">Caràcter que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia sobre el sexe contrari</param>
        public Convidat(string nom, int sexe):base(nom)
        {
            PlusSexe = sexe;
        }
        /// <summary>
        /// Retorna o estableix la simpaties envers a algú
        /// </summary>
        public int this[string nom] 
        { 
            get =>simpaties[nom];
            set 
            {
                try
                {
                    simpaties[nom] = value;
                }
                catch
                {
                    simpaties.Add(nom, value);
                }
            } 
        }

        /// <summary>
        /// Retorna o estableix el plus de simpatia envers del sexe contrari
        /// </summary>
        public int PlusSexe { get; set; }
        /// <summary>
        /// Retorna que si és un convidat
        /// </summary>
        /// <returns>Cert</returns>
        public override bool EsConvidat() => true;
    }
}
