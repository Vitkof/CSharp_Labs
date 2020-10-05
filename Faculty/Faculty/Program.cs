using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.ComponentModel.DataAnnotations;


namespace Facultet
{
    class Program
    {
        private static void ClearAll(Faculty f)
        {
            f.Clear();
        }
        private static void PokazGroup(Group g)
        {            
            foreach (Student s in g)
            {
                Console.WriteLine(s.ToString());      
            }
        }
        private static void PokazFac(Faculty f)
        {
            foreach (Group gr in f)
            {
                PokazGroup(gr);
                Console.WriteLine("___________________________________________________");
            }
        }
        private static void Information()
        {
            Console.WriteLine("\n~ ФАКУЛЬТЕТ ~ \n  Введите № операции и Enter \n   1-Показать весь факультет\n   2-Добавить студентов из txt-файла\n   " +
                "3-Добавить студентов из bin-файла (двоичного) \n   4-Отфильтровать всех студентов по возрасту N \n   5-Сгруппировать по возрасту \n   " +
                "6-Показать Ср.Балл студентов \n   7-Показать данные по фамилии \n   8-Сохранить всех в файлы ***.txt и ***.bin \n   9-Удалить всех студентов");
            Console.Write("  Exit - для выхода из программы\n");
        }



        private static void Main()
        {
            var gr1 = new Group("«Cybersecurity»")
            {
                new Student ( "Kira","Smirnova", 18, new Collection<int>{13, 9, 10}),
                new Student ("Lena","Iva", 20, new Collection<int> {5, 6, 99}),
                new Student ("Oleg","Kuznetsov", 18, new Collection<int> {7, 7, 4}),
                new Student ("Alex","Sokolov", 22, new Collection<int> {7, 8, 8}),
                new Student ("Kate","Lebedeva", 21, new Collection<int> {9, 9, 9}),
                new Student ("Sabrina","Cherkalova",19,new Collection<int>{3, 4, 6}),
                new Student ("Victor","Zabolotni",19,new Collection<int>{ 6,6,6}),
                new Student ("Nikolai","Lavrenkov",18,new Collection<int>{ 3,4,4}),
                new Student ("Alexandr", "Erohin",18,new Collection<int>{ 7,7,7}),
                new Student("Alex","Rubec",23,new Collection<int>{ 5,9,10}),
            };
            
            var gr2 = new Group("«Informatics»")
            {
                new Student("Dmitry","Palashenko",22,new Collection<int>{ 8,8,8,6}),
                new Student("Svetlana","Loboda",21,new Collection<int>{ 6,6,7,3}),
                new Student("Ilka","Kostuk",20,new Collection<int>{ 9,9,9,8})
            };

            var gr3 = new Group("«EconomyCybernetics»")
            {
                new Student("Albert","Strelkovski", 17,new Collection<int>{ 7,5,7}),
                new Student("Vadim","Kachan",18,new Collection<int>{ 8,6,8}),
                new Student("Anton","Rubec",21,new Collection<int>{ 9,8,9}),
                new Student("Nikita","Shevchik",17,new Collection<int>{ 2,3,3})
            };

            Group[] garray = new Group[] { gr1, gr2, gr3 };
            Faculty mathfack = new Faculty(garray);

            Information();
            string v;

            do
            {                               
                v = Console.ReadLine();
                Console.Clear();
                
                switch (v)
                {
                    case "1":
                        {

                            PokazFac(mathfack);
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Имя файла: ");
                            string path = Console.ReadLine();

                            mathfack.ReadAllStudentsTxt(path+".txt");
                            PokazFac(mathfack);

                            break;
                        }
                    case "3":
                        {
                            Console.Write("Имя файла: ");
                            string path = Console.ReadLine();

                            using (FileStream fs = new FileStream(path+".bin", FileMode.OpenOrCreate))
                            {
                                Console.Write("в № группы: ");
                                string g = Console.ReadLine();
                                switch (g)
                                {
                                    case "1": { gr1.ReadBinFile(fs); PokazGroup(gr1); break; }
                                    case "2": { gr2.ReadBinFile(fs); PokazGroup(gr2); break; }
                                    case "3": { gr3.ReadBinFile(fs); PokazGroup(gr3); break; }
                                    default: { Console.WriteLine("Такая группа еще не появилась"); break; }
                                }
                                fs.Flush();
                                fs.Dispose();
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.Write("\nFILTRATION,Age. From: ");
                            int ageIn = int.Parse(Console.ReadLine());
                            Console.Write("\t\t  To: ");
                            int ageOut = int.Parse(Console.ReadLine());

                            var r = mathfack.Filtration(ageIn, ageOut);
                            foreach (Student s in r)
                            {
                                Console.WriteLine(s.ToString());
                            }
                            break;
                        }
                    case "5":
                        {
                            var r = mathfack.Grouping();
                            foreach (var g in r)
                            {
                                Console.WriteLine("\n{0}", g.Key);
                                foreach (Student s in g)
                                {
                                    Console.WriteLine(s.ToString());
                                }
                            }
                            break;
                        }
                    case "6":
                        {
                            var r = mathfack.SelectGPA();
                            foreach (var s in r) Console.WriteLine(s.Surname + ", GPA- " + String.Format("{0:0.00}", s.GPA));
                            break;
                        }
                    case "7":
                        {
                            Console.Write("\nВведите фамилию: ");
                            string surname = Console.ReadLine();

                            var r = mathfack.SearchSurname(surname);
                            if (!r.Any()) Console.WriteLine("С такой фамилией нет!");
                            foreach (var s in r) Console.WriteLine(s.ToString());
                            break;
                        }
                    case "8":
                        {
                            Console.Write("Имя файла: ");
                            string path = Console.ReadLine();
                            mathfack.SaveAllStudents(path + ".txt");
                            Console.WriteLine("Сохранено!");
                            break;
                        }
                    case "9":
                        {                            
                            ClearAll(mathfack);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Введенное значение не соответствует ни одной из команд, попробуйте еще раз");
                            break;
                        }

                }
                Information();
            }
            while (v != "Exit");

            Console.ReadKey();
        }       
    }
}
