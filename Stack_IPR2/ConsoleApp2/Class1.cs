using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Exception;

namespace MyNameSpace   //собственное пространство имен
{
    public class ListNode<T>    //класс как универсальный тип
    {
        public T Data { get; set; }
        public ListNode<T> Next { get; set; }
        
        private static T minData;
        public static T MinData
        { get { return minData; }
            set { minData = value; }
        }
       // public ListNode<T>(T dat)
        //    {}




        public static uint LastID = 1;     

        public uint ID = LastID;



        public static void Updade_LastID()
        {
            LastID += 1;
        }
        public static uint GetID()
        {
            return ListNode<T>.LastID;
        }
       
    }


    public class GenericList<T> : StringHelper
    {
        private ListNode<T> _head;   //1е поле унверсального типа =ykazatel
        private ListNode<T> _last;   //2е поле универсального типа =hvost


        private int _count = 0;



        public int Count { get { return _count; } }

        public void Add(T item)
        {
            var node = new ListNode<T> { Data = item };  
            if (Added != null) Added($"Добавлена строка: {item}");
            ListNode<T>.Updade_LastID();      //обновление последнего созданного ID
            if (_count == 0)
            {
                _head = node;
                _last = _head;
            }
            else
            {
                _last = _last.Next = node;
            }
            _count++;
            //if (Added != null) Added($"Добавлена строка: {item}");
        }


        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException();
            }
            if (index > 0)
            {
                var prev = GetNodeByIndex(index - 1);
                var node = prev.Next;
                prev.Next = node.Next;
                if (index == _count - 1)
                {
                    _last = prev;
                }

            }
            else
            {
                _head = _head.Next;
            }
            _count--;
        }

        public bool Remove(T item)
        {
            if (_count == 0) return false;

            var prev = _head;
            var node = _head;

            while (node != null && !node.Data.Equals(item)) //прогонка по стеку
            {
                prev = node;
                node = node.Next;
            }

            if (node != null)  //нашел ли узел
            {
                if (node == _head)
                {
                    _head = node.Next;
                }
                else
                {
                    prev.Next = node.Next;
                    if (node.Next == null)  ////// if (node.Data.Equals(_last.Data))
                    {                       //сравнение удаляемого звена с хвостовым звеном с последующим изменением хвоста        
                        _last = prev;
                    }
                }
                if (Withdraw != null) Withdraw($"Удалена строка: {item}");
                _count--;
                return true;
            }
            return false;
        }

        public void InsertAt(T item, int index)
        {
            if (index < 0 || index > _count)
            {
                throw new IndexOutOfRangeException();
            }

            var newNode = new ListNode<T> { Data = item };
            if (Added != null) Added($"Вставлена строка: {item} в позицию {index+1}");
            ListNode<T>.Updade_LastID();    //UP ID при способе Вставка узла
            if (index > 0)
            {
                var prev = GetNodeByIndex(index - 1);
                newNode.Next = prev.Next;
                prev.Next = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head = newNode;
            }
            _count++;

        }
        public static void Destroy()
        {
            //GenericList<T>.
        }


        public ListNode<T> GetNodeByID(uint ID)
        {
            if (ID <= 0 || ID > ListNode<T>.LastID) { throw new Exception("Недопустимый ID !"); }
            int i;
            for (i =0; i< _count; i++)
            {
                if(GetNodeByIndex(i).ID == ID) return GetNodeByIndex(i);
            }
            return null;
        }
        


        private ListNode<T> GetNodeByIndex(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException();
            }

            int i = 0;
            var iter = _head;
            while (i < index)
            {
                iter = iter.Next;
                i++;
            }
            return iter;
        }

        public T this[int index]  //индексатор
        {
            get
            {
                return GetNodeByIndex(index).Data;
            }
            set
            {
                GetNodeByIndex(index).Data = value;
            }

        }
        public uint GetID(int index)
        {
            
                return GetNodeByIndex(index).ID;
            
        }

        public event ShowMess Added;
        public event ShowMess Withdraw;
        public event EventHandler<TestEvent> onCount;
        public void KeyPress()
        {
            if (onCount != null)
            { onCount(this, new TestEvent(string.Format("Press key"))); }
        }
    }

    public class TestEvent : EventArgs
    {
        public string Message { get; private set; }
        public TestEvent(string message)
        { Message = message; }
    }
    public delegate void ShowMess(string mess);
    public delegate int CountDelegate(string message);
    public delegate int CountDelegateAll(GenericList<string> lst);
    public class StringHelper
    {
        public int GetCount(string inputString)
        { return inputString.Length; }
        public int GetAllCount(GenericList<string> lst)
        {
            int sum = 0;
            for (int i = 0; i < lst.Count; i++)
            { sum += lst[i].Length; }
            return sum;
        }
        public int GetSumbalA(string inputString)
        { return inputString.Count(c => c == 'A'); }
        public int GetSumbal(string inputString, char ch)
        { return inputString.Count(c => c == ch); }

        // public event CountDelegate onCount;
    }
}




