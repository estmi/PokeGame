using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    /// <summary>Classe base per a totes les caselles de l'escenari.</summary>
    public class Posicio
    {
        /// <summary>
        /// Crea una nova posició
        /// </summary>
        /// <param name="fil">Fila de la Posició</param>
        /// <param name="col">Columna de la Posició</param>
        public Posicio(int fil = 0, int col = 0)
        {
            Columna = col;
            Fila = fil;
        }
        /// <summary>
        /// Assigna o obté la columna de la posicio
        /// </summary>
        public int Columna { get; set; }
        /// <summary>
        /// Assigna o obté la fila de la posicio
        /// </summary>
        public int Fila { get; set; }
        /// <summary>
        /// Retorna si la posició està o no buida
        /// </summary>
        public virtual bool Buida =>true;
        /// <summary>
        /// Retorna la distància entre dues posicions
        /// </summary>
        /// <param name="pos1">Primera posició</param>
        /// <param name="pos2">Segona posició</param>
        /// <returns>Distància entre les dues posicions</returns>
        public static double Distancia(Posicio pos1, Posicio pos2) => 
            Math.Sqrt(
                Math.Abs(pos1.Columna - pos2.Columna) * 2 + 
                Math.Abs(pos1.Fila - pos2.Fila) * 2);

    }
}
