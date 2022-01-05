using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace Class
{
    public class Escenari : Grid
    {
        
        Random _random = new();
        StreamReader nomsNois = new("Nois.txt");
        StreamReader nomsNoies = new("Noies.txt");
        /// <summary>
        /// Crea un escenari donades unes mides
        /// </summary>
        /// <param name="files">Número de files de l'escenari</param>
        /// <param name="columnes">Número de columnes de l'escenari</param>
        public Escenari(int files, int columnes,int nHomes = 0, int nDones = 0,int nCambrers = 0) : base()
        {
            for (int i = 0; i < files; i++)
            {
                RowDefinitions.Add(new());
            }
            for (int i = 0; i < columnes; i++)
            {
                ColumnDefinitions.Add(new());
            }

            Files = files; 
            Columnes = columnes; 
            Tauler = new Posicio[Files, Columnes];
            for (int i = 0; i < Tauler.GetLength(0); i++)
            {
                for (int j = 0; j < Tauler.GetLength(1); j++)
                {
                    Tauler[i, j] = new("", i, j);
                    PosaPosicio(Tauler[i, j]);
                }
            }
            for (int i = 0; i < nHomes; i++)
            {
                CreaHome();
            }
            for (int i = 0; i < nDones; i++)
            {
                CreaDona();
            }
            for (int i = 0; i < nCambrers; i++)
            {
                CreaCambrer();
            }
            
        }
        /// <summary>
        /// Tanca l'escenari
        /// </summary>
        public void Close()
        {
            nomsNoies.Close();
            nomsNois.Close();
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
        public int NumHomes { get; private set; } = 0;
        /// <summary>
        /// Retorna el número de dones que hi ha dins de l'escenari
        /// </summary>
        public int NumDones { get; private set; } = 0;
        /// <summary>
        /// Retorna el número de Cambrers que hi ha dins de l'escenari
        /// </summary>
        public int NumCambrers { get; private set; } = 0;
        /// <summary>
        /// Obtè la TaulaPersones que guarda l'escenari
        /// </summary>
        public TaulaPersones Gent { get; } = new();
        /// <summary>
        /// Obte una matriu de tot l'escenari
        /// </summary>
        public Posicio[,] Tauler { get; }
        /// <summary>
        /// Mou una persona de (filOrig, colOrig) fins a la posicio (filDesti, colDesti)
        /// Es suposa que les coordenades són vàlides, que hi ha una persona a l'origen i que
        /// el destí està buit.
        /// </summary>
        /// <param name="filOrig">Fila de la coordenada d'origen</param>
        /// <param name="colOrig">Columna de la coordenada d'origen</param>
        /// <param name="filDesti">Fila de la coordenada de destí</param>
        /// <param name="colDesti">Columna de la coordenada de destí</param>
        private void Mou(int filOrig, int colOrig, int filDesti, int colDesti) 
        {
            string nom = (Tauler[filOrig, colOrig] as Persona).Nom;
            int idx = Children.IndexOf(Gent[nom]);
            Gent[nom].Fila = filDesti;
            Gent[nom].Columna = colDesti;
            Tauler[filDesti, colDesti] = Gent[nom];
            Tauler[filOrig, colOrig] = new("",filOrig,colOrig);
            SetRow(Children[idx], filDesti);
            SetColumn(Children[idx], colDesti);
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
            if (0 <= fil && fil < Files && 0 <= col && col < Columnes)
            {
                return Tauler[fil, col].EsBuida;
            }
            return false;
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
        public void Buida(int fil, int col) 
        {
            if (!Tauler[fil, col].EsBuida)
            {
                Persona p = Tauler[fil, col] as Persona;
                if (p.EsConvidat())
                {
                    Convidat c = p as Convidat;
                    if (c.EsHome)
                        NumHomes--;
                    else
                        NumDones--;
                }
                else
                    NumCambrers--;

                Children.Remove(Tauler[fil, col]);
                Tauler[fil, col] = new("",fil,col);
                Gent.EliminaPersona(p);
            }
        }
        /// <summary>
        /// Obte una llista amb posicions buides
        /// </summary>
        public List<Posicio> LlistaPosicionsBuides
        {
            get
            {
                List<Posicio> llista = new();
                for (int i = 0; i < Tauler.GetLength(0); i++)
                {
                    for (int j = 0; j < Tauler.GetLength(1); j++)
                    {
                        Posicio p = Tauler[i, j];
                        if (p.EsBuida)
                            llista.Add(p);
                    }
                }
                return llista;
            }
            
        }
        #region Creació i posicionament de persones
        /// <summary>
        /// Crea Home a una posicio aleatoria
        /// </summary>
        public void CreaHome()
        {
            List<Posicio> l = LlistaPosicionsBuides;
            Posicio p = l[_random.Next(l.Count)];
            CreaHome(p.Fila, p.Columna);
        }
        /// <summary>
        /// Crea Home a una posicio donada
        /// </summary>
        public void CreaHome(int fila, int columna)
        {
            Home h = new(nomsNois.ReadLine(), _random.Next(4));
            
            h.Fila = fila;
            h.Columna = columna;
            ObteImatge(h);
            Posa(h);
        }
        /// <summary>
        /// Crea Dona a una posicio aleatoria
        /// </summary>
        public void CreaDona()
        {
            List<Posicio> l = LlistaPosicionsBuides;
            Posicio p = l[_random.Next(l.Count)];
            CreaDona(p.Fila, p.Columna);
        }
        /// <summary>
        /// Crea Dona a una posicio donada
        /// </summary>
        public void CreaDona(int fila, int columna)
        {
            Dona d = new(nomsNoies.ReadLine(), _random.Next(4));
            
            d.Fila = fila;
            d.Columna = columna;
            ObteImatge(d);
            Posa(d);
        }
        /// <summary>
        /// Crea Cambrer a una posicio aleatoria
        /// </summary>
        public void CreaCambrer()
        {
            List<Posicio> l = LlistaPosicionsBuides;
            Posicio p = l[_random.Next(l.Count)];
            CreaCambrer(p.Fila, p.Columna);
        }
        /// <summary>
        /// Crea Cambrer a una posicio donada
        /// </summary>
        public void CreaCambrer(int fila, int columna)
        {
            Cambrer c = new();
            c.Fila = fila;
            c.Columna = columna;
            ObteImatge(c);
            Posa(c);
        }
        /// <summary>
        /// Posa una Persona dins de l'escenari i a la taula de persones
        /// Si la posició de la persona ja està ocupada, genera una excepció
        /// </summary>
        /// <param name="pers">Persona a afegir</param>
        public void Posa(Persona pers)
        {
            if (!DestiValid(pers.Fila, pers.Columna))
                throw new ArgumentException("Posicio No valida");
            SetColumn(pers, pers.Columna);
            SetRow(pers, pers.Fila);
            Children.Add(pers);
            Gent.AfegeixPersona(pers);
            Tauler[pers.Fila, pers.Columna] = pers;
            if (pers.EsConvidat())
            {
                Convidat c = pers as Convidat;
                if (c.EsHome)
                {
                    NumHomes++;
                }
                else
                    NumDones++;
            }
            else
                NumCambrers++;
        }
        /// <summary>
        /// Coloca una posicio dins del grid, fent les assignacions que pertoquen
        /// </summary>
        /// <param name="p"></param>
        private void PosaPosicio(Posicio p)
        {
            SetColumn(p, p.Columna);
            SetRow(p, p.Fila);
            Children.Add(p);
        }
        /// <summary>
        /// Elimina una persona del taulell
        /// </summary>
        /// <param name="pers">Persona a eliminar</param>
        public void Elimina(Persona pers)
        {
            Buida(pers.Fila, pers.Columna);
        }
        /// <summary>
        /// Obté un nom d'un fitxer de noms que no s'estigui fent servir
        /// </summary>
        /// <param name="fitxer">Fitxer de noms</param>
        /// <returns>El nom triat</returns>
        private String ObteNom(String fitxer)
        {
            //No s'utilitza perque ja assigno els noms automaticament de l'arxiu
            return null;
        }
        /// <summary>
        /// Assigna una Imatge aleatòria a la persona segons sigui
        /// Home, dona o cambrer
        /// </summary>
        /// <param name="p"></param>
        private void ObteImatge(Persona p)
        {
            String prefix;
            if (p.EsConvidat())
            {
                Convidat c = p as Convidat;
                if (c.EsHome)
                {
                    prefix = "Home";
                }
                else
                    prefix = "Dona";
                p.Icona = (ImageSource)FindResource(prefix + _random.Next(1,4) + "Key");
            }
            else
                p.Icona = (ImageSource)FindResource("CambrerKey");

        }
        #endregion

        /// <summary>
        /// Mira si en el tauler hi ha alguna persona amb aquest nom
        /// </summary>
        /// <param name="nom">Nom a cercar</param>
        /// <returns>Si hi ha coincidència</returns>
        public bool NomRepetit(string nom) 
        {
            try
            {
                _ = Gent[nom];
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
            foreach (KeyValuePair<String,Persona> kvp in Gent)
            {
                Persona persona = kvp.Value;
                Direccio direccio = persona.OnVaig(this);
                switch (direccio)
                {
                    case Direccio.Nord:
                        Mou(persona.Fila, persona.Columna, persona.Fila - 1, persona.Columna);
                        break;
                    case Direccio.Nordest:
                        Mou(persona.Fila, persona.Columna, persona.Fila - 1, persona.Columna + 1);
                        break;
                    case Direccio.Est:
                        Mou(persona.Fila, persona.Columna, persona.Fila, persona.Columna + 1);
                        break;
                    case Direccio.Sudest:
                        Mou(persona.Fila, persona.Columna, persona.Fila + 1, persona.Columna + 1);
                        break;
                    case Direccio.Sud:
                        Mou(persona.Fila, persona.Columna, persona.Fila + 1, persona.Columna);
                        break;
                    case Direccio.Sudoest:
                        Mou(persona.Fila, persona.Columna, persona.Fila + 1, persona.Columna - 1);
                        break;
                    case Direccio.Oest:
                        Mou(persona.Fila, persona.Columna, persona.Fila, persona.Columna - 1);
                        break;
                    case Direccio.Noroest:
                        Mou(persona.Fila, persona.Columna, persona.Fila - 1, persona.Columna - 1);
                        break;
                    default:
                        break;
                }
            }
            
        }
    }
}