using System;
using System.Threading;
namespace Kolo_i_Krzyz
{
    interface IMoving
    {
        bool MakeMove(char[,] startBoard, char[,] gameBoard);
    }
    /**********************************************************************/
    abstract class Players
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public bool CheckIfPlayersWon(char[,] gameBoard)
        {
            int height = gameBoard.GetLength(0);
            int width = gameBoard.GetLength(1);
            if (height != width)
                throw new Exception("The board is not square!");
            // Check rows
            for (int i = 0; i < height; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < width; j++)
                {
                    if (gameBoard[i, j] == Symbol)
                        rowSum++;
                }
                if (rowSum == width)
                    return true;
            }
            // Check columns
            for (int j = 0; j < width; j++)
            {
                int colSum = 0;
                for (int i = 0; i < height; i++)
                {
                    if (gameBoard[i, j] == Symbol)
                        colSum++;
                }
                if (colSum == height)
                    return true;
            }
            // Check diagonals
            int diagSumA = 0;
            int diagSumB = 0;
            for (int k = 0; k < width; k++)
            {
                if (gameBoard[k, k] == Symbol)
                    diagSumA++;
                if (gameBoard[k, width - 1 - k] == Symbol)
                    diagSumB++;
            }
            if (diagSumA == width || diagSumB == width)
                return true;
            return false;
        }
        public bool PlaceSymbol(char c, char[,] startBoard, char[,] gameBoard)
        {
            int height = gameBoard.GetLength(0);
            int width = gameBoard.GetLength(1);
            if (height != startBoard.GetLength(0) || width != startBoard.GetLength(1))
                throw new Exception("The boards have different sizes!");
            // Try to put player's symbol at a given place, if the place is available
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (gameBoard[i, j] == c && gameBoard[i, j] == startBoard[i, j])
                    {
                        gameBoard[i, j] = Symbol;
                        return true;
                    }
            // Otherwise, return without success
            return false;
        }

     }
    /**********************************************************************/
    class HumanPlayer : Players, IMoving
    {
        public bool MakeMove(char[,] startBoard, char[,] gameBoard)
        {
            // Ask human player to enter a place until (s)he picks an available one
            char chosenPlace;
            do
            {
                Console.Write("Choose an empty place: ");
                chosenPlace = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            while (!PlaceSymbol(chosenPlace, startBoard, gameBoard));

            // TODO: human move
            return CheckIfPlayersWon(gameBoard);
        }
    }
    /**********************************************************************/
    class ComputerPlayer : Players, IMoving
    {
        public bool MakeMove(char[,] startBoard, char[,] gameBoard)
        {
            // Draw random numbers until AI player picks an available one
            Random rnd = new Random();
            char chosenPlace;
            do
            {
                int p = rnd.Next(1, gameBoard.Length + 1); // random 1-9
                chosenPlace = p.ToString()[0]; // convert digit to char
            }
            while (!PlaceSymbol(chosenPlace, startBoard, gameBoard));
            Thread.Sleep(2000); // wait 2 seconds

            // TODO: computer move
            return CheckIfPlayersWon(gameBoard);
        }
    }
}