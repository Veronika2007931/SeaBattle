using System;
using System.Text;

Console.OutputEncoding = UTF8Encoding.UTF8;

Random random = new Random();

char[,] playerBoard = new char[9, 11];
char[,] computerBoard = new char[9, 11];
char[,] playerHits = new char[9, 11];
char[,] computerHits = new char[9, 11];

InitializeBoard(playerBoard);
InitializeBoard(computerBoard);
InitializeBoard(playerHits);
InitializeBoard(computerHits);


Console.WriteLine("Привіт! Як твій настрій? Хочеш зіграти в морський бій?");

string title = "Ваша дошка!";


Console.WriteLine("Розсташуйте ваші кораблі: ");
DisplayBoard(playerBoard,title);


static void InitializeBoard(char[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            board[i, j] = '.';
        }
    }
}

static void DisplayBoard(char[,] board, string title)
{
    Console.WriteLine(title);
    Console.WriteLine("  0 1 2 3 4 5 6 7 8 9 X");
    for (int i = 0; i < board.GetLength(0); i++)
    {
        Console.Write(i + " ");
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write(board[i, j] + " ");
        }
        Console.WriteLine();
    }
}
