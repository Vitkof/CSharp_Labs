using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Boing
{ 
    class Program
    {
        static object locker = new object();


        public static int counter;
        //public static Barrier barr;
        public static void Main(string[] args)
        {
            Airport Moscow = new Airport();
            Moscow.Planes = new Samolet[] { new Samolet(900, 11, "Ufa"), new Samolet(900, 11, "Berlin"), new Samolet(820, 12, "Belgorod") };
            

            Console.WriteLine("Для запуска авиарейса нажмите соответствующую клавишу:\n\t№1 Ufa \n\t№2 Berlin \n\t№3 Belgorod \n\t*Esc - Выход.");
            Console.WriteLine("_____________________________________");


            ConsoleKeyInfo k;
            do
            {
                k = Console.ReadKey(true);

                if (k.Key == ConsoleKey.D1 || k.Key == ConsoleKey.NumPad1)
                {
                    Thread th1 = new Thread(delegate () { RunX(Moscow); });  //создаем анонимную функцию и передаем параметры уже в делегат
                    th1.Start();
                }
                else if (k.Key == ConsoleKey.D2 || k.Key == ConsoleKey.NumPad2)
                {
                    Thread th2 = new Thread(delegate () { RunY(Moscow); });
                    th2.Start();
                }
                else if (k.Key == ConsoleKey.D3 || k.Key == ConsoleKey.NumPad3)
                {
                    Thread th3 = new Thread(delegate () { RunZ(Moscow); });
                    th3.Start();
                }
            }
            while (k.Key != ConsoleKey.Escape);
            Environment.Exit(0);

            Console.ReadKey();            
        }

        static void RunX(Airport air)
        {
            counter++;
            //barr = new Barrier(counter);           
            air.PuskSamolet(1);
            counter--;
        }
        static void RunY(Airport air)
        {
            counter++;
            //barr = new Barrier(counter);            
            air.PuskSamolet(2);
            counter--;
        }
        static void RunZ(Airport air)
        {
            counter++;            
            air.PuskSamolet(3);
            counter--;
        }
    }
}

