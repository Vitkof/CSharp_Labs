using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Facultet
{
    class Student
    {

        private string name;
        private string surname;
        private int age;
        private Collection<int> marks = new Collection<int>();        //not error Reading file
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Недопустимая длина имени")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentNullException("Name не может быть пустым");
                    }
                    if (value.Length < 2 || value == null)
                    {
                        throw new ArgumentOutOfRangeException($"Name '{value}' слишком короткое");
                    }
                    char ch;
                    for (int i = 0; i < value.Length; i++)
                    {
                        ch = value[i];
                        if (!char.IsLetter(ch)) throw new FormatException($"Name '{value}' должно содержать только буквы!");
                    }

                    name = value;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        [StringLength(30, MinimumLength = 6, ErrorMessage = "Недопустимая длина SURNAME")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentNullException("Surname не может быть пустым");
                    }
                    if (value.Length < 2 || value == null)
                    {
                        throw new ArgumentOutOfRangeException($"Surname '{value}' слишком короткое");
                    }
                    char ch;
                    for (int i = 0; i < value.Length; i++)
                    {
                        ch = value[i];
                        if (!char.IsLetter(ch)) throw new FormatException($"Surname '{value}' должно содержать только буквы!");
                    }
                    surname = value;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public int Age
        {
            get { return age; }
            private set
            {
                if (value < 16) age = 16;
                else age = value;
            }
        }
        public Collection<int> Marks
        {
            [DebuggerStepThrough]
            get
            {
                return marks;
            }
        }

        public void SetMarks(Collection<int> coll)
        {
            foreach (int m in coll)
            {
                if (m > 0 && m <= 10) Marks.Add(m);
            }
        }

        public void SetOneMark(int m)
        {
            if (m > 0 && m <= 10) Marks.Add(m);
        }

        public Group GroupIN { get; set; }
        public float GPA
        {
            get
            {
                int sum = 0;
                int kol = 0;
                foreach (int m in Marks)
                { sum += m; kol++; }
                return (float)sum / (float)kol;
            }
        }
        public string Specialty { get; set; }

        public Student() { }
        public Student(string n, string surn, int a, Collection<int> m)
        {
            Name = n;
            Surname = surn;
            Age = a;
            SetMarks(m);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Name, 80);
            sb.Append(" " + Surname + " ");
            sb.Append(Age + " years, ");
            sb.Append($"Group_№{GroupIN.GetID()}");
            sb.Append(" «" + Specialty + "». Marks: ");
            foreach (int m in Marks) sb.Append(m + " ");
            return sb.ToString();
        }



        internal void SaveBinary(Stream st)
        {
            using (BinaryWriter bw = new BinaryWriter(st, Encoding.Default, leaveOpen:true))
            {
                bw.Write(Name);
                bw.Write(Age);
                bw.Write(Surname);
                bw.Write(Specialty);

                foreach (int m in Marks)
                {
                    bw.Write(m);
                }
                
                bw.Flush();
                bw.Dispose();
            } 
        }

        internal void ReadBinary(Stream st)
        {
            try
            {
                using (BinaryReader br = new BinaryReader(st, Encoding.Default, leaveOpen: true))
                {
                    Console.OutputEncoding = Encoding.Default;
                    Name = br.ReadString();
                    Age = br.ReadInt32();
                    Surname = br.ReadString();
                    Specialty = br.ReadString();

                    int i;
                    do
                    {
                        i = br.ReadInt32();
                        SetOneMark(i);
                        Console.WriteLine($"{i} and {br.PeekChar()}");
                        if (br.PeekChar() == -1) break;         //конец потока - заканчиваем читать
                    }
                    while (i > 0 && i <= 10);

                    if(br.PeekChar()!= -1) st.Position -= 4;   //возвращаем на 4 байта назад(int), если это не последний студент
                    br.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }



        internal void SaveStream(Stream st)
        {
            using (StreamWriter sw = new StreamWriter(st, Encoding.Default, 1, true))
            {
                sw.Write(Name);
                sw.Write(" " + Surname + " ");
                sw.Write(Age + " years; " + Specialty + " Marks: ");
                for (int i = 0; i < Marks.Count; i++)
                {
                    sw.Write(Marks[i] + " ");
                }

                sw.WriteLine();
                sw.Flush();
                sw.Dispose();
            }
        }

        internal void ReadStream(Stream st)
        {
                using (StreamReader sr = new StreamReader(st, Encoding.Default, true, 1, leaveOpen: true))
                {                   
                    long pos0 = st.Position;               //начальная позиция SR
                 
                    var line = sr.ReadLine();              //st.Position += 128, что нас не устраивает
                try
                {
                    if (string.IsNullOrWhiteSpace(line) || line.Contains("===")) throw new Exception();  //пропуск пустых строк и 'шапок' групп
                   
                    string pattern = @"^[a-zA-Zа-яА-Я0-9;:«» ]+$";                                    //формат строки
                    if (!Regex.IsMatch(line, pattern, RegexOptions.IgnoreCase)) throw new FormatException("Строка не соответствует формату");
                
                    string[] str = line.Split();
                    Name = str[0];
                    Surname = str[1];
                    Age = Convert.ToInt16(str[2]);
                    Specialty = str[4];

                    for (int i = 6; i < str.Length - 1; i++)    //i=3й - "лет;"   5й- "Оценки:"
                    {
                        SetOneMark(Convert.ToByte(str[i]));
                    }
                }
                catch (Exception)
                {
                    
                }

                    st.Position = pos0 + line.Length + 2;      //ставим указатель на начало след.строки, перепрыгивая +2(перевод строки и возврат каретки)                  
                    sr.Dispose();                 
                }
        }
    }
}
