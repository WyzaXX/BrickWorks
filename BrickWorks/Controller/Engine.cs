namespace BrickWorks.Controller
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BrickWorks.Models;
    using System.Text;

    public class Engine
    {
        public Engine()
        {

        }

        public void Run()
        {

            var input = ParseInput();

            var height = input[0];
            var width = input[1];

            //error case
            if (height == 2 && width == 2)
            {
                Console.WriteLine("-1 No solution exists!!!");
                return;
            }

            //push the inputs and validate inside
            var matrixLayer1 = new BrickMatrix(height, width);

            //fill the matrix with the data provided
            matrixLayer1.FillMatrix();

            //validation of brick spanning
            if (matrixLayer1.CheckIfBrickIsSpanningThreeRows() ||
                matrixLayer1.CheckIfBrickIsSpanningThreeColumns())
            {
                return;
            }


            //find and fill the second layer of the brick wall
            var secondLayer = matrixLayer1.FillLayer2Matrix();

            //----------------------------------------------------
            //this array will be used for visualisation later
            var secondLayerWithAsterix = new string[(height * 2) + 1, (width * 2) + 1];

            //prints on the console
            PrintSecondLayer(secondLayer);

            //preparing for visualisation
            secondLayerWithAsterix = matrixLayer1.ConvertIntArrToStringArr();

            //prints on the console with asterixs and dashes
            PrintSecondLayerWithAsterixs(secondLayerWithAsterix);
        }

        //this method is used to parse the input
        private List<int> ParseInput()
        {
            return Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        //print the second layer on the console
        private void PrintSecondLayer(int[,]arr)
        {
            var sb = new StringBuilder();

            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    sb.Append($"{arr[row, col]} ");
                }
                sb.AppendLine();
            }
            sb.ToString().TrimEnd();
            Console.WriteLine(sb);           
        }

        //print the secondlayer and surrond each brick with "*" also between brick put a "-"
        private void PrintSecondLayerWithAsterixs(string[,] arr)
        {
            var sb = new StringBuilder();

            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    sb.Append($"{arr[row, col]} ");
                }
                sb.AppendLine();
            }
            sb.ToString().TrimEnd();
            Console.WriteLine(sb);
        }
    }
}
