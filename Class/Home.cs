using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class Home : Convidat
    {
        /// <summary>
        /// Crea un Home
        /// </summary>
        /// <param name="nom">String que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia envers del sexe contrari</param>
        public Home(string nom, int sexe) : base(nom, sexe) { }
        /// <summary>
        /// Interès d'aquest home per una posició
        /// </summary>
        /// <param name="pos">Posició per la qual s'interessa</param>
        /// <returns>Interès quantificat</returns>
        public override int Interes(Posicio pos) 
        { 
            if (pos is Persona p)
            {
                if (p is Convidat c)
                {
                    if (c is Home h)
                    {
                        return h[Nom];
                    }
                    else if (c is Dona d)
                    {
                        return d[Nom] + PlusSexe;
                    }
                }
                else if (p is Cambrer)
                {
                    return 1;
                }
            }
            return 0;
            
        }

    }
}
