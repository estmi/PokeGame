
using System;
using System.Collections.Generic;
using Class;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicio vermell = new(0, 4);
            Posicio rosa = new(2, 0);
            Posicio act = new(2, 2);
            Posicio actual;
            Console.WriteLine("Estatic");
            ferCrida(act, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Nord");
            actual = new(act.Fila-1, act.Columna);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Nordest");
            actual = new(act.Fila-1, act.Columna+1);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Est");
            actual = new(act.Fila, act.Columna+1);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Sudest");
            actual = new(act.Fila + 1, act.Columna + 1);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Sud");
            actual = new(act.Fila + 1, act.Columna);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Sudoest");
            actual = new(act.Fila + 1, act.Columna - 1);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Oest");
            actual = new(act.Fila,act.Columna-1);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
            Console.WriteLine("Noroest");
            actual = new(act.Fila - 1, act.Columna - 1);
            ferCrida(actual, vermell, rosa);
            Console.WriteLine("----------------------");
        }
        
        private static void ferCrida(Posicio actual, Posicio vermell,Posicio rosa)
        {
            //Console.WriteLine(Distancia(actual, vermell));
            //Console.WriteLine(Distancia(actual, vermell) * 8);
            //Console.WriteLine(Distancia(actual, rosa));
            //Console.WriteLine(Distancia(actual, rosa) * 2);
            Console.WriteLine(8/Distancia(actual, vermell)+2/ Distancia(actual, rosa) );
        }

        public static double Distancia(Posicio pos1, Posicio pos2) =>
           Math.Sqrt(
               Math.Pow(
                   Math.Abs(pos1.Columna - pos2.Columna),
                   2) +
               Math.Pow(
                   Math.Abs(pos1.Fila - pos2.Fila),
                   2)
               );

    }
}
