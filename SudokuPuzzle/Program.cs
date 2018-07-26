using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuPuzzle
{
    class Program
    {
        public static int[,] puzzle = new int[9, 9];

        static void Main(string[] args)
        {

            //Read Sudoku input file
            ReadFile();

            //Print Sudoku
            PrintSudoku(puzzle);

            Console.WriteLine("Is a valid solution to a Sudoku puzzle :{0}", SolveSudoku(puzzle, 0, 0));
            Console.ReadLine();

        }


        private static void ReadFile()
        {
          
            int lineNumber = 0;
            string[] lines = System.IO.File.ReadAllLines(@"C:\Learning\ScotiaBank\SudokuPuzzle\SudokuPuzzle\input_sudoku.txt");

            foreach (string line in lines)
            {
                if (line !="")
                { 
                AssignArray(line, lineNumber);
                lineNumber++;
                }
            }

           

        }


        private static void AssignArray(string val, int lineNumber) 
        {
            int i=0,j = 0;
           
            int count = 0;

            for (i = lineNumber; i < 9; i++)
            {
                count = 0;
                for (j = 0; j < 9; j++)
                {
                    puzzle[i,j] =  Convert.ToInt32(val.Substring(count, 1));
                    count++;

                }

                break;
                
            }

         }



        public static void PrintSudoku(int[,] puzzle)
        {
            Console.WriteLine("+-----+-----+-----+");

            for (int i = 1; i < 10; ++i)
            {
                for (int j = 1; j < 10; ++j)
                    Console.Write("|{0}", puzzle[i - 1, j - 1]);

                Console.WriteLine("|");
                if (i % 3 == 0) Console.WriteLine("+-----+-----+-----+");
            }
        }


        public static bool SolveSudoku(int[,] puzzle, int row, int col)
        {
            if (row < 9 && col < 9)
            {
                if (puzzle[row, col] != 0)
                {
                    if ((col + 1) < 9) return SolveSudoku(puzzle, row, col + 1);
                    else if ((row + 1) < 9) return SolveSudoku(puzzle, row + 1, 0);
                    else return true;
                }
                else
                {
                    for (int i = 0; i < 9; ++i)
                    {
                        if (IsAvailable(puzzle, row, col, i + 1))
                        {
                            puzzle[row, col] = i + 1;

                            if ((col + 1) < 9)
                            {
                                if (SolveSudoku(puzzle, row, col + 1)) return true;
                                else puzzle[row, col] = 0;
                            }
                            else if ((row + 1) < 9)
                            {
                                if (SolveSudoku(puzzle, row + 1, 0)) return true;
                                else puzzle[row, col] = 0;
                            }
                            else return true;
                        }
                    }
                }

                return false;
            }
            else return true;
        }

        private static bool IsAvailable(int[,] puzzle, int row, int col, int num)
        {
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;

            for (int i = 0; i < 9; ++i)
            {
                if (puzzle[row, i] == num) return false;
                if (puzzle[i, col] == num) return false;
                if (puzzle[rowStart + (i % 3), colStart + (i / 3)] == num) return false;
            }

            return true;
        }


    }





}
