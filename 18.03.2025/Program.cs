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
DisplayBoard(playerBoard, title);


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

class Program
{
    static int ROWS = 9, COLS = 11;
    static char[,] playerBoard = new char[ROWS, COLS];
    static char[,] botBoard = new char[ROWS, COLS];
    static char[,] botHiddenBoard = new char[ROWS, COLS];
    static Random rand = new Random();

    static Dictionary<int, int> ships = new Dictionary<int, int>
    {
        { 1, 4 },
        { 2, 3 },
        { 3, 2 },
        { 4, 1 }
    };

    static void Main()
    {
        InitializeBoard(playerBoard);
        InitializeBoard(botBoard);
        InitializeBoard(botHiddenBoard);
        PlacePlayerShips();
        GenerateBotShips();
        PlayGame();
    }

    static void InitializeBoard(char[,] board)
    {
        for (int i = 0; i < ROWS; i++)
            for (int j = 0; j < COLS; j++)
                board[i, j] = '.';
    }

    static void PlacePlayerShips()
    {
        Console.WriteLine("Розставте свої кораблі:");
        foreach (var ship in ships)
        {
            int size = ship.Key;
            int count = ship.Value;

            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    PrintBoard(playerBoard, "Ваше поле");
                    Console.WriteLine($"Розмістіть корабель розміром {size}. Введіть рядок і стовпець (0-{ROWS - 1} 0-{COLS - 1}):");
                    int x = int.Parse(Console.ReadLine());
                    int y = int.Parse(Console.ReadLine());
                    if (IsValidPosition(x, y, playerBoard))
                    {
                        playerBoard[x, y] = 'S';
                        break;
                    }
                    Console.WriteLine("Неправильна позиція, спробуйте ще раз.");
                }
            }
        }
    }

    static bool IsValidPosition(int x, int y, char[,] board)
    {
        return x >= 0 && x < ROWS && y >= 0 && y < COLS && board[x, y] == '.';
    }

    static void GenerateBotShips()
    {
        foreach (var ship in ships)
        {
            int size = ship.Key;
            int count = ship.Value;
            for (int i = 0; i < count; i++)
            {
                PlaceShip(size, botBoard);
            }
        }
    }

    static void PlaceShip(int size, char[,] board)
    {
        while (true)
        {
            int x = rand.Next(ROWS);
            int y = rand.Next(COLS);
            if (IsValidPosition(x, y, board))
            {
                board[x, y] = 'S';
                break;
            }
        }

        static void PlayerTurn()
        {
            int x, y;
            Console.Write("Введіть координати пострілу (x y): ");
            x = int.Parse(Console.ReadLine());
            y = int.Parse(Console.ReadLine());
            if (computerBoard[x, y] == 'S')
            {
                Console.WriteLine("Влучання!");
                computerBoard[x, y] = 'X';
            }
            else
            {
                Console.WriteLine("Мимо!");
                computerBoard[x, y] = 'O';
            }
        }

        static void ComputerTurn()
        {
            int x, y;
            Random rand = new Random();
            x = rand.Next(1, 12);
            y = rand.Next(1, 10);

            if (playerBoard[x, y] == 'S')
            {
                Console.WriteLine($"Бот влучив у ({x}, {y})!");
                playerBoard[x, y] = 'X';
            }
            else
            {
                Console.WriteLine($"Бот промахнувся в ({x}, {y}).");
                playerBoard[x, y] = 'O';
            }
        }

        static void PlayGame()
        {
            while (true)
            {
                PrintBoard(playerBoard, "Ваше поле");
                PrintBoard(botHiddenBoard, "Поле ворога");
                Console.WriteLine("Ваш хід! Введіть координати для атаки (рядок і стовпець):");
                int x = int.Parse(Console.ReadLine());
                int y = int.Parse(Console.ReadLine());

                if (botBoard[x, y] == 'S')
                {
                    Console.WriteLine("Попадання!");
                    botHiddenBoard[x, y] = 'X';
                    botBoard[x, y] = 'X';
                }
                else
                {
                    Console.WriteLine("Промах.");
                    botHiddenBoard[x, y] = 'O';
                }

                BotTurn();
            }
        }