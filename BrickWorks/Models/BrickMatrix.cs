namespace BrickWorks.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    //this class is used to create and validate the matrix
    public class BrickMatrix
    {
        public string errorMessage = "-1 No solution exists!!!";

        private int[,] answer;
        private int[,] matrixLayer1;
        private string[,] answerWithAsterixs;
        private int height;
        private int width;

        public BrickMatrix(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            matrixLayer1 = new int[this.Height, this.Width];
            answer = new int[(this.Height * 2) + 1, (this.Width * 2) + 1];
            answerWithAsterixs = new string[(this.Height * 2) + 1, (this.Width * 2) + 1];
        }

        //set and check for bad input
        public int Height
        {
            get => height;
            set
            {
                height = value;

                //throw exceptions if validation is wrong
                if (height >= 100)
                {
                    throw new ArgumentException("Height cannot be over/or 100!!!");
                }
                if (height % 2 == 1)
                {
                    throw new ArgumentException("Height cannot be an odd number!!!");
                }
            }

        }
        //set and check for bad input
        public int Width
        {
            get => width;
            set
            {
                width = value;

                if (width >= 100)
                {
                    throw new ArgumentException("Width cannot be over/or 100!!!");
                }
                if (width % 2 == 1)
                {
                    throw new ArgumentException("Width cannot be an odd number!!!");
                }
            }
        }

        //this method is used to fill the matrix with the input provided
        public int[,] FillMatrix()
        {
            for (int row = 0; row < this.Height; row++)
            {
                var input = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                for (int col = 0; col < this.Width; col++)
                {
                    var current = input[col];
                    matrixLayer1[row, col] = current;
                }
            }
            return this.matrixLayer1;
        }

        //this method is used to check the brick measurements (row)
        public bool CheckIfBrickIsSpanningThreeRows()
        {
            for (int row = 0; row < matrixLayer1.GetLength(0) - 2; row++)
            {
                for (int col = 0; col < matrixLayer1.GetLength(1); col++)
                {
                    if (matrixLayer1[row, col] == matrixLayer1[row + 1, col] &&
                        matrixLayer1[row + 1, col] == matrixLayer1[row + 2, col])
                    {
                        Console.WriteLine("There is a brick on 3 rows!");
                        return true;
                    }
                }
            }
            return false;
        }
        //this method is used to check the brick measurements (column)
        public bool CheckIfBrickIsSpanningThreeColumns()
        {
            for (int row = 0; row < matrixLayer1.GetLength(0); row++)
            {
                for (int col = 0; col < matrixLayer1.GetLength(1) - 2; col++)
                {
                    if (matrixLayer1[row, col] == matrixLayer1[row, col + 1] &&
                        matrixLayer1[row, col + 1] == matrixLayer1[row, col + 2])
                    {
                        Console.WriteLine("There is a brick on 3 columns!");
                        return true;
                    }
                }
            }
            return false;
        }

        //this method is used to find and fill the 2nd layer of the brick wall
        public int[,] FillLayer2Matrix()
        {
            var matrixLayer2 = new int[height, width];
            var listWithUsedBricks = new List<int>();

            var brickNumber = 0;
            var brickSeparator = -2;

            //fill the second layer with the solution and prepare for the asterix surrounding
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    //check if this is the last column and if it is add a vertical brick
                    if (matrixLayer2[row, col] == 0 && col == width - 1)
                    {
                        brickNumber++;
                        matrixLayer2[row, col] = answer[(row * 2) + 1, (col * 2) + 1] = brickNumber;
                        matrixLayer2[row + 1, col] = answer[(row * 2) + 3, (col * 2) + 1] = brickNumber;
                        answer[(row * 2) + 2, (col * 2) + 1] = brickSeparator;

                        //check if brick from secondLayer is laying on brick from firstLayer
                        if (matrixLayer1[row, col] - matrixLayer1[row + 1, col] == 0)
                        {
                            //if it is then no solution is possible
                            throw new Exception(errorMessage);
                        }
                    }
                    //check if position is free
                    else if (matrixLayer2[row, col] == 0 && col < width - 1)
                    {
                        brickNumber++;

                        //check if brick on layer 1 is horizontal
                        if (matrixLayer1[row, col] - matrixLayer1[row, col + 1] == 0)
                        {
                            //if brick is horizontal place a vertical brick on layer2
                            matrixLayer2[row, col] = answer[(row * 2) + 1, (col * 2) + 1] = brickNumber;
                            matrixLayer2[row + 1, col] = answer[(row * 2) + 3, (col * 2) + 1] = brickNumber;
                            answer[(row * 2) + 2, (col * 2) + 1] = brickSeparator;
                        }
                        else
                        {
                            //else place the brick horizontally
                            matrixLayer2[row, col] = answer[(row * 2) + 1, (col * 2) + 1] = brickNumber;
                            matrixLayer2[row, col + 1] = answer[(row * 2) + 1, (col * 2) + 3] = brickNumber;
                            answer[(row * 2) + 1, (col * 2) + 2] = brickSeparator;
                        }
                    }
                }
                Console.WriteLine();
            }
            return matrixLayer2;
        }

        //this method is converting the secondLayer into a string [,]
        public string[,] ConvertIntArrToStringArr()
        {

            for (int row = 0; row < answerWithAsterixs.GetLength(0); row++)
            {
                for (int col = 0; col < answerWithAsterixs.GetLength(1); col++)
                {
                    if (answer[row, col] == 0)
                    {
                        answerWithAsterixs[row, col] = "*";
                    }
                    else if (answer[row, col] == -2)
                    {
                        answerWithAsterixs[row, col] = "-";
                    }
                    else
                    {
                        answerWithAsterixs[row, col] = string.Format("{0}", answer[row, col]);
                    }
                }
            }
            return answerWithAsterixs;
        }
    }
}