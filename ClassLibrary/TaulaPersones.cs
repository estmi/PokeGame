using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class TaulaPersones : Dictionary<String,Persona>
    {
        Random random = new();
        /// <summary>
        /// Crea una taula de referències a persones
        /// </summary>
        public TaulaPersones() { }

        
        /// <summary>
        /// Obtè el número total de persones
        /// </summary>
        public int NumPersones { get=>Count; }
        /// <summary>
        /// Afegeix una persona a la taula
        /// </summary>
        /// <param name="conv">Convidat a afegir</param>
        private void Afegir(Persona pers) => Add(pers.Nom, pers);
        /// <summary>
        /// Eliminar una persona de la taula
        /// </summary>
        /// <param name="conv">Convidat a eliminar</param>
        private void Eliminar(Persona pers) => Remove(pers.Nom);
        /// <summary>
        /// Elimina la persona donat el seu nom
        /// </summary>
        /// <param name="posicio">Posició a eliminar</param>
        public void Eliminar(string nom) => EliminaPersona(this[nom]);
        
        /// <summary>
        /// Afegeix una persona a la taula interna, si es un convidat li crea la taula de simpaties
        /// </summary>
        /// <param name="pers"></param>
        public void AfegeixPersona(Persona pers)
        {
            if (pers.EsConvidat())
            {
                
                CreaSimpaties(pers as Convidat);
            }
            Afegir(pers);
        }
        /// <summary>
        /// Elimina una persona de la taula interna, si es un convidat l'elimina de la taula de simpaties dels altres convidats
        /// </summary>
        /// <param name="pers"></param>
        public void EliminaPersona(Persona pers)
        {
            if (pers.EsConvidat())
            {
                
                EliminaSimpaties(pers as Convidat);
            }
            Eliminar(pers);
        }
        /// <summary>
        /// Elimina la simpatia dels altres convidats sobre el convidat referenciat
        /// </summary>
        /// <param name="pers"></param>
        private void EliminaSimpaties(Convidat c)
        {
            foreach (KeyValuePair<String, Persona> item in this)
            {
                if (item.Value.EsConvidat())
                {
                    (item.Value as Convidat).EliminaSimpatia(c);
                }
            }
        }
        /// <summary>
        /// Crea les simpaties del convidat referenciat i crea la simpatia de els altres convidats sobre el referenciat
        /// </summary>
        /// <param name="nou"></param>
        private void CreaSimpaties(Convidat nou)
        {
            foreach (KeyValuePair<String, Persona> item in this)
            {
                if (item.Value.EsConvidat())
                {
                    nou[item.Key] = random.Next(-5, 6);
                    (item.Value as Convidat)[nou.Nom] = random.Next(-5, 6);
                }
            }
            nou[nou.Nom] = 0;
        }
    }
}
