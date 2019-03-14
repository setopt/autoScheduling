using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmSheduleTest
{
    public partial class Form1 : Form
    {
        public List<Room> Rooms = new List<Room>();
        public List<WhenClass> Classes = new List<WhenClass>();
        public List<WhoClass> Orders = new List<WhoClass>();
        public List<int> Limits = new List<int>();
        public List<Matrix[,]> mtx = new List<AlgorithmSheduleTest.Matrix[,]>();
        public Matrix[,] Matrix;

        public Form1()
        {
            InitializeComponent();
            Rooms.Add(new Room { ID_Room = 1, Roominess = 15 });
            Rooms.Add(new Room { ID_Room = 2, Roominess = 30 });
            Rooms.Add(new Room { ID_Room = 3, Roominess = 30 });
            Rooms.Add(new Room { ID_Room = 4, Roominess = 15 });
            Rooms.Add(new Room { ID_Room = 5, Roominess = 30 });
            
            Orders.Add(new WhoClass { Teacher = 1, Group = 1, NumberStud = 15, Number = 4 });
            Orders.Add(new WhoClass { Teacher = 2, Group = 1, NumberStud = 15, Number = 2 });
            Orders.Add(new WhoClass { Teacher = 3, Group = 1, NumberStud = 15, Number = 3 });
            Orders.Add(new WhoClass { Teacher = 1, Group = 2, NumberStud = 30, Number = 3 });
            Orders.Add(new WhoClass { Teacher = 2, Group = 2, NumberStud = 30, Number = 3 });
            Orders.Add(new WhoClass { Teacher = 3, Group = 2, NumberStud = 30, Number = 2 });
            Orders.Add(new WhoClass { Teacher = 4, Group = 2, NumberStud = 30, Number = 1 });
            Orders.Add(new WhoClass { Teacher = 1, Group = 3, NumberStud = 20, Number = 2 });
            Orders.Add(new WhoClass { Teacher = 2, Group = 3, NumberStud = 20, Number = 3 });
            Orders.Add(new WhoClass { Teacher = 3, Group = 3, NumberStud = 20, Number = 4 });

            fillClasses(Rooms);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Matrix = new Matrix[Classes.Count, Orders.Count];
            PreBuild(Matrix);
            Build(Matrix, Limits);
        }

        void fillClasses(List<Room> rooms)
        {
            for (int i = 1; i < 7; i++)
                for (int j = 1; j < 9; j++)
                    for (int r = 0; r < rooms.Count; r++)
                    {
                        Classes.Add(new WhenClass(i, j, false, rooms[r].ID_Room, rooms[r].Roominess));
                    }

            for (int i = 1; i < 7; i++)
                for (int j = 1; j < 9; j++)
                    for (int r = 0; r < rooms.Count; r++)
                    {
                        Classes.Add(new WhenClass(i, j, true, rooms[r].ID_Room, rooms[r].Roominess));
                    }

            for (int i = 0; i < Orders.Count; i++)
            {
                Limits.Add(Orders[i].Number);
            }

        }

        public void PreBuild(Matrix[,] matrix)
        {
            //часть 1: поставить нули, где группы не помещаются в аудитории
            for (int i = 0; i < Classes.Count; i++)
                for (int j = 0; j < Orders.Count; j++)
                    if (Classes[i].Roominess < Orders[j].NumberStud)
                    {
                        matrix[i, j] = new Matrix { m = false };
                    }
        }

        public void Build(Matrix[,] matrix, List<int> limits)
        {
            //часть 2: собственно выполнение алгоритма
            //Matrix[,] cmatrix = new Matrix[Classes.Count, Orders.Count];
            //for (int i = 0; i < Classes.Count; i++)
            //    for (int j = 0; j < Orders.Count; j++)
            //    {
            //        if (matrix[i, j] == null)
            //            cmatrix[i, j] = null;
            //        else
            //            cmatrix[i, j] = (Matrix)matrix[i, j].Clone();
            //    }
            //        List<int> cLimits = new List<int>();
            //for (int i = 0; i < limits.Count; i++)
            //{
            //    cLimits.Add(limits[i]);
            //}

            for (int i = 0; i < Classes.Count; i++)
                for (int j = 0; j < Orders.Count; j++)
                {                     
                    if (matrix[i, j] == null)
                    {
                        for (int m = 0; m < Classes.Count; m++)
                        {
                            if(matrix[m, j] != null)
                                if(!matrix[m, j].m)
                                    matrix[m, j] = new Matrix { m = false };
                        }

                        for (int m = 0; m < Orders.Count; m++)
                        {
                            if (matrix[i, m] != null)
                                if (!matrix[i, m].m)
                                    matrix[i, m] = new Matrix { m = false };
                        }
                        matrix[i, j] = new Matrix { m = true };
                        Limits[j]--;
                        if (Limits[j] != 0)
                        {
                            matrix[i + (Classes.Count / 2), j] = new Matrix { m = true };
                            Limits[j]--;
                        }
                    }
                }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Matrix[,] Matrix = new Matrix[Classes.Count, Orders.Count];
            PreBuild(Matrix);
            Build(Matrix, Limits);
        }
    }

    public class WhenClass
    {
        int couple;
        int day_of_week;
        bool numdel;
        int room;
        int roominess;

        public int Couple
        {
            get { return couple; }
            set { couple = value; }
        }

        public int Day_Of_Week
        {
            get { return day_of_week; }
            set 
            {
                if(value > 0 && value < 7)
                    day_of_week = value; 
            }
        }

        public bool NumDel
        {
            get { return numdel; }
            set { numdel = value; }
        }

        public int Room
        {
            get { return room; }
            set { room = value; }
        }

        public int Roominess
        {
            get { return roominess; }
            set 
            {
                if(value > 0) 
                    roominess = value; 
            }
        }

        public WhenClass(int _c, int _d, bool _nd, int _r, int _rness)
        {
            Couple = _c;
            Day_Of_Week = _d;
            NumDel = _nd;
            Room = _r;
            Roominess = _rness;
        }
    }

    public class WhoClass
    {
        int teacher;
        int group;
        int numberStud;
        int number;

        public int Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }

        public int Group
        {
            get { return group; }
            set { group = value; }
        }

        public int NumberStud
        {
            get { return numberStud; }
            set 
            { 
                if (value > 0)
                    numberStud = value; 
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                if (value > 0)
                    number = value;
            }
        }
    }

    public class Room
    {
        public int ID_Room;
        public int Roominess = 0;

    }

    public class Matrix : ICloneable
    {
        public bool m;

        public object Clone()
        {
            return new Matrix { m = this.m };
        }
    }
}
