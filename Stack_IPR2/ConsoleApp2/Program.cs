using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MyNameSpace;


namespace ConsoleApp
{

        class Program
        {
            static int TestDelegate(CountDelegate method, string str)
            { return method(str); }

            static int TestDelegate2(CountDelegateAll meth, GenericList<string> lst)
            { return meth(lst); }

            static void ColorDisplay(string mess)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(mess);
                Console.ResetColor();
            }

            static void Main(string[] args)
                {


                var list = new GenericList<string>();  //сконструированный тип

                string a;
                Console.Write("Добавить ");
                a = Console.ReadLine();

            list.Added += new ShowMess(ColorDisplay);   //подписка

                list.Add(a);
            list.Added -= ColorDisplay;      //otpiska
                list.Add("Первый");            
                list.Add("Второй");
            list.Added += ColorDisplay;
            list.Withdraw += ColorDisplay;

            CountDelegate d1 = list.GetCount;
            CountDelegateAll d2 = list.GetAllCount;

                Console.WriteLine($"Добавить {a}, Первый, Второй");
                Console.WriteLine("Всего строк: " + list.Count);
                Console.WriteLine("Элементы: " + list[0] + " " + list[1] + " " + list[2]);
                Console.WriteLine("Кол-во символов в 1й строке: {0}", TestDelegate(d1,list[0]));
                Console.WriteLine("All sumbals in Stack: {0}", TestDelegate2(d2,list));
                Console.WriteLine(ListNode<int>.GetID());


                Console.WriteLine("\n\rУдалить первый элемент");
                list.RemoveAt(0);
                Console.WriteLine(list.Count);
                Console.WriteLine("Элементы: " + list[0] + " " + list[1]);

                Console.WriteLine("\nДобавить 1 в начало списка");
                list.InsertAt("1", 0);
                Console.WriteLine("Всего строк: " + list.Count);
                Console.WriteLine("Элементы: " + list[0] + " " + list[1] + " " + list[2]);

                Console.WriteLine("\nВставить Audi в позицию 2");
                list.InsertAt("Audi", 2);
                Console.WriteLine("Всего строк: " + list.Count);
                Console.WriteLine("Элементы: " + list[0] + " " + list[1] + " " + list[2] + " " + list[3]);


                Console.WriteLine("\nУдалить первый элемент");
                list.Remove(list[0]);
                Console.WriteLine("Всего строк: " + list.Count);
                Console.WriteLine("Элементы: " + list[0] + " " + list[1] + " " + list[2]);

            Console.WriteLine();


          

            Console.WriteLine("СТРОКИ. \n Для выполнения операции нажмите соответствующий № и Enter \n 1-Добавить \n 2-Удалить \n 3-Вставить на место N \n 4-Количество \n 5-Показать \n 6-Поиск по ID");
            Console.WriteLine(" Exit - для выхода из программы");

            string f = "*111*";
            while (f != "Exit")
            {
                string b;
                f = Console.ReadLine();

                switch (f)
                {
                    case "Exit":
                        {
                            Thread.Sleep(1500);
                            //Environment.Exit(0);
                            break; }

                    case "1":
                        {
                            Console.WriteLine("Введите новую строку: ");
                            b = Console.ReadLine();
                            list.Add(b);
                            break;
                        };

                    case "2":
                        {
                            Console.WriteLine("Введите строку, которую нужно удалить (учитывайте регистр):");
                            b = Console.ReadLine();
                            list.Remove(b);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Введите сперва строку, а затем место, куда её вставить:");
                            b = Console.ReadLine();
                            int c = Convert.ToInt16(Console.ReadLine()) - 1;
                            list.InsertAt(b, c);
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Количество строк: " + list.Count);
                            break;
                        }

                    case "5":
                        {
                            for (int i = 0; i <= list.Count - 1; i++)
                            { Console.Write(list[i] + "(#"+list.GetID(i) +") "); }
                            Console.WriteLine();
                            break;
                        }

                    case "6":
                        {
                            uint id;
                            Console.Write("Введите ID элемента: ");
                            id = Convert.ToUInt32(Console.ReadLine());
                            if (list.GetNodeByID(id) == null)
                            {
                                Console.WriteLine("Узел c таким ID удален");
                                break;
                            }
                            Console.WriteLine("#{0}- "+list.GetNodeByID(id).Data, id);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Введенное значение не соответствует ни одной из команд, попробуйте еще раз");
                            break;
                        }
                }
            }
            
            Environment.Exit(0);
            ConsoleKeyInfo pressed;
            pressed = Console.ReadKey();
            //if (pressed.KeyChar == 'y')


            Console.ReadKey();
            }

        }
   
}
