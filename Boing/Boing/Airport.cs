using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections;

namespace Boing
{
    public class Airport : IEnumerable
    {
        private Samolet[] planes;
        public Samolet[] Planes
        {
            get { return planes; }
            set { planes = value; }
        }

        public Dictionary<int, Samolet> PlanesInAir = new Dictionary<int, Samolet>();  //запущен. самолеты в воздухе

        public delegate void PuskHandler(string msg);
        event PuskHandler Notify;

        private static object locker = new object();

        private static void ClearPrevLineInfo()     //затираем строку неактуальных данных
        {
            int prevLineCursor = Console.CursorTop - 1;
            Console.SetCursorPosition(0, prevLineCursor);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, prevLineCursor);
        }

        public static Barrier barr;
        internal void PuskSamolet(int index)
        {
            string path = index.ToString() + ".txt";
            index--;
            if (planes[index].FlightStatus == false)
            {
                planes[index].FlightStatus = true;

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    {
                        string line;
                        sr.ReadLine();
                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            string[] array = Convert.ToString(line).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ushort time = Convert.ToUInt16(array[0]);
                            ushort hight = (ushort)(Convert.ToUInt16(array[1]) * 0.3048);   //из футов в метры
                            string point = array[2];

                            var tm = DateTime.Now.Millisecond;

                            lock (locker)    //блокируем консоль
                            {

                                Console.WriteLine("Рейс «" + planes[index].Direction + "», Время полета: " + time + " min; Высота: " + hight + "; " + point);

                            }
                            Thread.Sleep(1000 - tm);  //пользователь может запустить поток в любую милисекунду от 0 до 999, то синхронизируем потоки к началу последующих секунд
                                                      //Thread.Sleep(1000);    //доп.1 сек,чтоб рейсы не "мельтешили". Итого: virtual 1 min poleta ~= 2 sec.
                                                      //barr = new Barrier(Program.counter);
                                                      // barr.SignalAndWait();

                            lock (locker)  //блокируем консоль для правильного неспонтанного стирания устаревшего результата
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                ClearPrevLineInfo();
                            }
                            Thread.Sleep(10);

                        }
                    }
                }
                planes[index].FlightStatus = false;
            }
            else
            {
                Console.WriteLine("Samolet " + ++index + " zapus4en!");
                Thread.Sleep(1000);
                Console.BackgroundColor = ConsoleColor.Black;
                ClearPrevLineInfo();
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < planes.Length; i++)
                yield return planes[i];
        }

    }
}
