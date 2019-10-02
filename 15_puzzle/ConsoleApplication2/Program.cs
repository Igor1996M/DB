using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleApplication1
{
    public class GameOfFifteen
    {
        public void View(int[,] array, int row, int col)
        {
            StreamWriter str = new StreamWriter("Check_1.txt");

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write("{0}\t", array[i, j]);
                    str.Write("{0}\t", array[i, j]);
                }
                Console.WriteLine();
                str.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("[Result recorded in Check_1.txt]");
            Console.WriteLine();
            str.Close();  
        }

        public void Shuffler(int[,] array, int row, int col, string Choose)
        {
            int Number;
            
            switch (Choose) {
                case "1":
                    Console.WriteLine("You move Up!");
                    
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (array[i, j] == 0)
                            {
                                Number = array[i, j];
                                array[i, j] = array[i - 1, j];
                                array[i - 1, j] = Number;
                            }
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine("You move Down!");
                    for (int i = row - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (array[i, j] == 0)
                            {
                                Number = array[i, j];
                                array[i, j] = array[i + 1, j];
                                array[i + 1, j] = Number;
                            }
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("You move Left!");
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (array[i, j] == 0)
                            {
                                Number = array[i, j];
                                array[i, j] = array[i, j - 1];
                                array[i, j - 1] = Number;
                            }
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine("You move Right!");
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = col - 1; j >= 0; j--)
                        {
                            if (array[i, j] == 0)
                            {
                                Number = array[i, j];
                                array[i, j] = array[i, j + 1];
                                array[i, j + 1] = Number;
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Error, only 4 numbers!");
                    break;
            }

            View(array, row, col);
            
        }

        public void Solver(int[,] array, int row, int col, string Choose, StreamWriter Write)
        {
            int New_number;
            switch (Choose)
            {
                case "1":
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (array[i, j] == 0)
                            {
                                New_number = array[i + 1, j];
                                Write.WriteLine(New_number + "↑");
                            }
                        }
                    }
                    break;
                case "2":
                    for (int i = row - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (array[i, j] == 0)
                            {
                                New_number = array[i - 1, j];
                                Write.WriteLine(New_number + "↓");

                            }
                        }
                    }
                    break;
                case "3":
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (array[i, j] == 0)
                            {
                                New_number = array[i, j + 1];
                                Write.WriteLine(New_number + "←");
                            }
                        }
                    }
                    break;
                case "4":
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = col - 1; j >= 0; j--)
                        {
                            if (array[i, j] == 0)
                            {
                                New_number = array[i, j - 1];
                                Write.WriteLine(New_number + "→");
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Error, only 4 numbers!");
                    
                    break;
            }
            Console.WriteLine("===============================================================================");
        }

    }

    class Program
    {

        static int count = 0;
        
        static void Player(object obj)
        {
            count++;
        }

        static void Main(string[] args)
        {
            const int Row = 4;
            const int Col = 4;
            GameOfFifteen Game = new GameOfFifteen();
            
                  int[,] Mass =  
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, 0}
            };

            //Тестовый вариант
           /* int[,] Mass =  
            {
                {1, 2, 0, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, 16}
            };*/

            
                Game.View(Mass, Row, Col);
                Console.WriteLine("===============================================================================");
                Console.WriteLine("Choose One:");
                Console.WriteLine("1: Up");
                Console.WriteLine("2: Down");
                Console.WriteLine("3: Left");
                Console.WriteLine("4: Right");
                Console.WriteLine("===============================================================================");
                
                StreamWriter Write = new StreamWriter("Check_2.txt");
                
                bool Right = false;
                long interval = 1000;
                int d = 0;
                try
                {
                    while (Right == false)
                    {
                        Timer timer = new Timer(Player, null, 0, interval);

                        Console.Write("Your choose: ");
                        string Action = Console.ReadLine();
                        Game.Shuffler(Mass, Row, Col, Action);
                        Game.Solver(Mass, Row, Col, Action, Write);
                        bool Right1 = true;
                        for (int i = 0; i < Row; i++)
                        {
                            for (int j = 0; j < Col - 1; j++)
                            {
                                if (Mass[i, j] > Mass[i, j + 1])
                                {
                                    Right1 = false;
                                    break;
                                }
                            }
                        }
                        Right = Right1;

                        Console.WriteLine("Your time: " + (count - d) + " sec.");
                        d = count;
                    }

                    Console.WriteLine();
                    Console.WriteLine("[Result recorded in Check_2.txt]");
                    Console.WriteLine();

                    Write.Close();
                }
                catch (Exception ex)
                {
                    Console.Write("Error");
                    Console.Write(ex.ToString());
                }
                Console.ReadLine();
                Console.Clear();
        }
    }
}

