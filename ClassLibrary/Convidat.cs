using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public abstract class Convidat : Persona
    {
        SortedDictionary<String, Int32> Simpaties = new();
        /// <summary>
        /// Crea un convidat
        /// </summary>
        /// <param name="nom">Caràcter que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia sobre el sexe contrari</param>
        public Convidat(string nom, int sexe, SortedDictionary<String, Int32> simpaties = null) :base(nom)
        {
            if (simpaties!= null)
                Simpaties = simpaties;
            PlusSexe = sexe;
        }
        /// <summary>
        /// Retorna o estableix la simpaties envers a algú
        /// </summary>
        public int this[string nom] 
        { 
            get =>Simpaties[nom];
            set 
            {
                if (Simpaties.ContainsKey(nom))
                    Simpaties[nom] = value;
                else
                    Simpaties.Add(nom, value);
              
            } 
        }
        /// <summary>
        /// Elimina la simpatia sobre la persona referenciada
        /// </summary>
        /// <param name="p"></param>
        public void EliminaSimpatia(Persona p)
        {
            Simpaties.Remove(p.Nom);
        }
        /// <summary>
        /// Retorna un booleà indicant si és home
        /// </summary>
        /// <value> True si es home, false en cas contrari. </value>
        public abstract bool EsHome
        {
            get;
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
        /// <summary>
        /// ToString per ajudar a Debugar
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Nom + base.ToString();
        }
    }
}
