using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLibrary1
{
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
        /// Crea una nova posició amb fila i columna igual a 0
        /// </summary>
        public Posicio() { }
        /// <summary>
        /// Assigna o obté la columna de la posicio
        /// </summary>
        public int Columna{ get; set; }
        /// <summary>
        /// Assigna o obté la fila de la posicio
        /// </summary>
        public int Fila
        { get; set;}
    }
}
