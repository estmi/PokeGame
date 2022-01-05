using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class TaulaPersones {
        Dictionary<String, Persona> taula = new();
        /// <summary>
        /// Crea una taula de referències a persones
        /// </summary>
        public TaulaPersones() { }
        /// <summary>
        /// Assigna o obté una persona de la taula
        /// </summary>
        public Persona this[string nom]
        { 
            get => taula[nom]; 
            set 
            {
                if (taula.ContainsKey(nom))
                {
                    taula[nom] = value;
                }
                else
                {
                    taula.Add(nom, value);
                }
               
            }
        }
        /// <summary>
        /// Obtè el número total de persones
        /// </summary>
        public int NumPersones { get=>taula.Count; }
        /// <summary>
        /// Afegeix una persona a la taula
        /// </summary>
        /// <param name="conv">Convidat a afegir</param>
        public void Afegir(Persona pers) => taula.Add(pers.Nom, pers);
        /// <summary>
        /// Eliminar una persona de la taula
        /// </summary>
        /// <param name="conv">Convidat a eliminar</param>
        public void Eliminar(Persona pers) => taula.Remove(pers.Nom);
        /// <summary>
        /// Elimina la persona donat el seu nom
        /// </summary>
        /// <param name="posicio">Posició a eliminar</param>
        public void Eliminar(string nom) => taula.Remove(nom);
        public void Add(Persona pers) => Afegir(pers);
    }
}
