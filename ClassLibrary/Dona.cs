using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class Dona : Convidat
    {
        /// <summary>
        /// Crea una Dona
        /// </summary>
        /// <param name="nom"> String que identifica aquesta Dona</param>
        /// <param name="sexe">Plus de simpatia envers convidats del sexe contrari</param>
        public Dona(string nom,  int sexe=0, SortedDictionary<String, Int32> simpaties = null) : base(nom, sexe, simpaties)
        {

        }
        /// <summary>
        /// Interès d'aquest home per una posició
        /// </summary>
        /// <param name="pos">Posició per la qual s'interessa</param>
        /// <returns>Interès quantificat</returns>
        public override int Interes(Posicio pos) 
        {
            if (!pos.EsBuida)
            {
                Persona p = pos as Persona;
                if (p.EsConvidat())
                {
                    Convidat c = p as Convidat;
                    if (c.EsHome)
                    {
                        return c[Nom] + PlusSexe;
                    }
                    else
                    {
                        return c[Nom];
                    }
                }
            }
            return 0;
        }

        public override bool EsHome => false;
    }
}
