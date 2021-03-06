namespace Class
{
    public class Escenari
    {
        
        /// <summary>
        /// Crea un escenari donades unes mides
        /// </summary>
        /// <param name="files">Número de files de l'escenari</param>
        /// <param name="columnes">Número de columnes de l'escenari</param>
        public Escenari(int files, int columnes) 
        { 
            Files = files; 
            Columnes = columnes; 
            Tauler = new Posicio[Files, Columnes];
        }
        /// <summary>
        /// Retorna el número de files de l'escenari
        /// </summary>
        public int Files { get; }
        /// <summary>
        /// Retorna el número de columnes de l'escenari
        /// </summary>
        public int Columnes { get; }
        /// <summary>
        /// Retorna el número de homes que hi ha dins de l'escenari
        /// </summary>
        public int Homes
        {
            get
            {
                int nCambrers = 0;
                foreach (var item in Tauler)
                {
                    if (item is Home)
                        nCambrers++;
                }
                return nCambrers;
            }
        }
        /// <summary>
        /// Retorna el número de dones que hi ha dins de l'escenari
        /// </summary>
        public int Dones
        {
            get
            {
                int nCambrers = 0;
                foreach (var item in Tauler)
                {
                    if (item is Dona)
                        nCambrers++;
                }
                return nCambrers;
            }
        }
        /// <summary>
        /// Retorna el número de Cambrers que hi ha dins de l'escenari
        /// </summary>
        public int Cambrers
        {
            get
            {
                int nCambrers = 0;
                foreach (var item in Tauler)
                {
                    if (item is Cambrer)
                        nCambrers++;
                }
                return nCambrers;
            }
        }
        /// <summary>
        /// Obtè la TaulaPersones que guarda l'escenari
        /// </summary>
        public TaulaPersones TaulaDePersones { get; } = new();
        public Posicio[,] Tauler { get; }
        /// <summary>
        /// Es suposa que les coordenades són vàlides, que hi ha una persona a l'origen i que
        /// el destí està buit.
        /// </summary>
        /// <param name="filOrig">Fila de la coordenada d'origen</param>
        /// <param name="colOrig">Columna de la coordenada d'origen</param>
        /// <param name="filDesti">Fila de la coordenada de destí</param>
        /// <param name="colDesti">Columna de la coordenada de destí</param>
        private void Moure(int filOrig, int colOrig, int filDesti, int colDesti) 
        {
            string nom = (Tauler[filOrig, colOrig] as Persona).Nom;
            TaulaDePersones[nom].Fila = filDesti;
            TaulaDePersones[nom].Columna = colDesti;
            Tauler[filDesti, colDesti] = TaulaDePersones[nom];
            Tauler[filOrig, colOrig] = new();
        }
        /// <summary>
        /// Retorna la Posició que hi ha en una coordenada donada
        /// </summary>
        public Posicio this[int fila, int col] => Tauler[fila,col];
        /// <summary>
        /// Mira si una coordenada es correcte per ser destí d'una persona
        /// </summary>
        /// <param name="fil">fila de la coordenada</param>
        /// <param name="col">columna de la coordenada</param>
        /// <returns>retorna si la coordenada és vàlida i està buida</returns>
        public bool DestiValid(int fil, int col) 
        {
            return Tauler[fil, col] is Persona;
        }
        /// <summary>
        /// Retorna el contingut del escenari en forma de matriu d'strings,
        /// representant cada persona amb el seu nom
        /// </summary>
        /// <returns>Matriu de caràcters</returns>
        public string[,] ContingutNoms() 
        {
            string[,] taula = new string[Files,Columnes];
            for (int i = 0; i < Files; i++)
            {
                for (int j = 0; j < Columnes; j++)
                {
                    if (Tauler[i, j] is Persona p)
                        taula[i, j] = p.Nom;
                    else taula[i, j] = "";
                }
            }
            return taula;
        }
        /// <summary>
        /// Elimina una persona de l'escenari i de la taula de persones
        /// </summary>
        /// <param name="fil">Fila on està la persona</param>
        /// <param name="col">Columna on està la persona</param>
        public void Buidar(int fil, int col) 
        {
            string nom = (Tauler[fil, col] as Persona).Nom;



        }
        /// <summary>
        /// Posa una Persona dins de l'escenari i a la taula de persones
        /// Si la posició de la persona ja està ocupada, genera una excepció
        /// </summary>
        /// <param name="pers">Persona a afegir</param>
        public void Posar(Persona pers) { }
        /// <summary>
        /// Mira si en el tauler hi ha alguna persona amb aquest nom
        /// </summary>
        /// <param name="nom">Nom a cercar</param>
        /// <returns>Si hi ha coincidència</returns>
        public bool NomRepetit(string nom) 
        {
            try
            {
                _ = TaulaDePersones[nom];
                return true;
            }
            catch 
            {
                return false;
            }
            
        }
        /// <summary>
        /// Fa que totes les persones facin un moviment
        /// </summary>
        public void Cicle() 
        {
         
            
        }
    }
}