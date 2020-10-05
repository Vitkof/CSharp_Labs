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
    class Group : Collection<Student>
    {
        private static byte id = 1;
        private byte id_group = id;

        public static void Update_id()
        { id++; }
        public byte GetID()
        { return id_group; }

        public Group(string Spec)
        {
            Update_id();
            Specialty = Spec;
            foreach (Student s in this)
            {
                s.Specialty = Specialty;
            }
        }
        public Group()
        { }


        public string Specialty { get; set; }

        protected override void InsertItem(int index, Student s)
        {
            base.InsertItem(index, s);
            s.GroupIN = this;
            s.Specialty = Specialty;
        }
        protected new void Add(Student s)
        {
            base.Add(s);
            s.GroupIN = this;
        }
        protected override void SetItem(int index, Student s)
        {
            base.SetItem(index, s);
            s.GroupIN = this;
            s.Specialty = Specialty;
        }
        protected override void RemoveItem(int index)
        {
            this[index].GroupIN = null;
            this[index].Specialty = null;
            base.RemoveItem(index);
        }
        protected override void ClearItems()
        {
            foreach (Student s in this)
            {
                s.GroupIN = null;
                s.Specialty = null;
            }
            base.ClearItems();
        }

        internal IOrderedEnumerable<Student> AgeFilter(int age1, int age2)
        {
            var r = this.Where(s => s.Age >= age1 && s.Age <= age2).OrderBy(s => s.Age);            
            return r;
        }
        internal List<string> AgeGrouping()
        {
            List<string> strLst = new List<string>();
            var r = this.GroupBy(s => s.Age);
            foreach (IGrouping<int, Student> group in r)
            {
                strLst.Add(("\n" + group.Key));
                foreach (Student s in group)
                {
                    s.GroupIN = this;
                    s.Specialty = Specialty;
                    strLst.Add(s.ToString());
                }
            }
            return strLst;
        }


        protected Student[] ToArray()
        {
            var array = this.ToArray<Student>();
            return array;
        }
        protected List<Student> ToList()
        {
            var list = this.ToList<Student>();
            return list;
        }
        protected Dictionary<string, Student> ToDictionary()
        {
            var dict = this.ToDictionary(s => s.Surname);
            return dict;
        }




        internal void SaveBinFile(Stream st)
        {
            foreach (Student s in this)
            {
                s.SaveBinary(st);
            }
        }

        internal void ReadBinFile(Stream st)
        {
            using (BufferedStream bs = new BufferedStream(st, 500))
            {
                while (bs.Position < bs.Length)
                {
                    Student s = new Student();
                    s.ReadBinary(bs);             
                    Add(s);
                }                
            }
        }

        internal void SaveGroupTxtFile(string path)
        {

            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default, 100, true))
                {
                    sw.WriteLine();
                    sw.Write("================== ");
                    sw.Write(Specialty.Substring(1, Specialty.Length - 2));
                    sw.WriteLine(" =============");
                    sw.Flush();
                    sw.Dispose();
                }

                foreach (Student s in this)
                {
                    s.SaveStream(fs);
                }
                fs.Flush();
                fs.Dispose();
            }
        }

        internal void ReadGroupTxtFile(string path)
        {

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                while (fs.Position < fs.Length)
                {
                    Student s = new Student();
                    try
                    {
                        s.ReadStream(fs);
                        if (s.Specialty == Specialty) this.InsertItem(0,s);
                    }
                    catch
                    {
                        continue;
                    }
                }
                fs.Flush();
                fs.Dispose();
            }
        }       
    }
}
