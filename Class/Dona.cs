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
        public Dona(string nom, int sexe) : base(nom, sexe)
        {

        }
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
                        return h[Nom] + PlusSexe;
                    }
                    else if (c is Dona d)
                    {
                        return d[Nom];
                    }
                }
                else if (p is Cambrer)
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
