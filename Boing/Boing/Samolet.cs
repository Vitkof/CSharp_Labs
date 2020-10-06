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
    public class Samolet
    {
        private int speed, hight;
        private string direction;


        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value >= 40) speed = value;     //мин. speed кукурузника
            }
        } 
        public int Hight
        {
            get
            {
                return hight;
            }
            set
            {
                if (value >= 1) hight = value;     //мин. высота ТУ-16
            }
        } 
        public string Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value.Length) > 1) direction = value;
            }
        }


        public bool FlightStatus { get; set; }

        public Samolet(int speed, int hight, string direction)
        {
            Speed = speed;
            Hight = hight;
            Direction = direction;
            FlightStatus = false;
        }
    }
    
}
