using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Facultet
{
    class Faculty : IEnumerable<Group>
    {
        private Group[] _groups;
        public Faculty(Group[] gArray)
        {
            _groups = new Group[gArray.Length];
            for (int i = 0; i < gArray.Length; i++)
            {
                _groups[i] = gArray[i];
            }
        }
        internal void Clear()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                _groups[i].Clear();
            }
            
        }
        public Group this[int index]        //index'ator
        {
            get
            {
                return _groups[index];
            }
            set
            {
                _groups[index] = value;
            }
        }

        //***********methods****************
        internal IEnumerable<Student> Filtration(int ageIn, int ageOut)
        {
            IEnumerable<Student> filtr = from gr in this
                                            from s in gr
                                            where s.Age >= ageIn && s.Age <= ageOut
                                            orderby s.Age
                                            select s;
            return filtr;
        }

        internal IEnumerable<IGrouping<int,Student>> Grouping()
        {
            var result = from gr in this
                      from s in gr
                      orderby s.Age
                      group s by s.Age;
            return result;           
        }

        internal IEnumerable<Student> SearchSurname(string surn)
        {
            IEnumerable<Student> searched = from gr in this
                         from s in gr
                         where s.Surname == surn
                         select s;
            return searched;
        }

        internal IEnumerable<Student> SelectGPA()
        {
            IEnumerable<Student> selected = from gr in this
                                            from s in gr
                                            orderby s.GPA descending
                                            select s;

            return selected;
        }



        internal void SaveAllStudents(string path)
        {
            File.Delete(path);
            foreach (Group gr in this)
            {
                gr.SaveGroupTxtFile(path);
            }

            path = path.Remove(path.IndexOf('.') + 1, 3) + "bin";
            using (FileStream bfs = new FileStream(path, FileMode.OpenOrCreate))
            {
                foreach (Group gr in this)
                {
                    gr.SaveBinFile(bfs);
                }
                bfs.Flush();
                bfs.Dispose();
            }
        }

        internal void ReadAllStudentsTxt(string path)
        {
            try
            {
                foreach (Group gr in this)
                {
                    gr.ReadGroupTxtFile(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        IEnumerator IEnumerable.GetEnumerator()   //реализация явная интерфейса
        {
            return GetEnumerator();
        }

        public IEnumerator<Group> GetEnumerator()
        {
            return new FacultyEnum(_groups);
        }


        protected class FacultyEnum : IEnumerator<Group>
        {
            private Group[] _groups;
            int position = -1;       //указатель

            public FacultyEnum(Group[] list)
            {
                _groups = new Group[list.Length];
                Array.Copy(list, _groups, list.Length);
            }

            public Group Current        //свойство-читка текущ. группы
            {
                get
                {
                    return _groups[position];
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                if (position >= _groups.Length - 1)
                {
                    return false;
                }
                else
                {
                    position++;
                    return true;
                }
            }

            public void Reset()
            {
                position = -1;
            }

        }
    }
}
