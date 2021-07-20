using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVeia
{
    class Program
    {
        static bool player1Turn = true;
        static Player player1;
        static Player player2;
        static List<List<char>> gameBoard;
        static void Main(string[] args)
        {
            RunGame();
        }

        private static void TryWinConditions(List<List<char>> gameBoard, bool player1)
        {
            char x = ' ';
            if (player1)
            {
                x = 'X';
            }
            else
            {
                x = 'O';
            }

            GetCurrentBoard(gameBoard);

            List<string> WinConditions = new List<string>()
            {
                "012","345","678","036","147","258","048","246"
            };
            //bool win = winConditions.Any(board => board == gameBoard);
        }

        private static List<string> GetCurrentBoard(List<List<char>> gameBoard)
        {
            List<string> playerMoves = new List<string>();
            for (int row = 0; row < gameBoard.Count; row++)
            {
                for (int collumn = 0; collumn < gameBoard[row].Count; collumn++)
                {
                    Console.Write("| " + gameBoard[row][collumn] + " |");
                }
            }
            return playerMoves;
        }

        static void RunGame()
        {
            Program program = new Program();
            List<string> usedCoordinates = new List<string>();
            //create new gameboard at game start
            gameBoard = new List<List<char>> {
                new List<char>{' ',' ',' '},
                new List<char>{' ',' ',' '},
                new List<char>{' ',' ',' '}
            };
            //create player 1 and 2
            Console.Write("Jogador 1\nNome:");
            player1 = new Player(Console.ReadLine());
            Console.Clear();
            Console.Write("Jogador 2\nNome:");
            player2 = new Player(Console.ReadLine());
            do
            {
                program.Render();
                Turn(gameBoard, usedCoordinates);
                TryWinConditions(gameBoard);
                player1Turn = !player1Turn;
            } while (true);
        }

        void Render()
        {
            Console.Clear();
            Console.WriteLine($"    {player1.Name} x {player2.Name}\n");
            Console.WriteLine("   0    1    2");
            for (int row = 0; row < gameBoard.Count; row++)
            {
                Console.Write($"{row}");
                for (int collumn = 0; collumn < gameBoard[row].Count; collumn++)
                {
                    Console.Write("| " + gameBoard[row][collumn] + " |");
                }
                Console.WriteLine("\n");
            }
        }

        static void Turn(List<List<char>> gameBoard, List<string> usedCoordinates)
        {
            char mark;
            do
            {
                if (player1Turn)
                {
                    Console.WriteLine($"turno do jogador: {player1.Name}\nDigite sua jogada");
                    mark = 'X';
                }
                else
                {
                    Console.WriteLine($"turno do jogador: {player2.Name}\nDigite sua jogada");
                    mark = 'O';
                }
                string resposta = Console.ReadLine().Trim();
                try
                {
                    Int32.Parse(resposta);
                    if (!usedCoordinates.Any(x => x == resposta))
                    {
                        int a = Int32.Parse(resposta.Substring(0,1));
                        int b = Int32.Parse(resposta.Substring(1,1));
                        gameBoard[a][b] = mark;
                        usedCoordinates.Add(resposta);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Jogada proibida, não é possivel jogar em cima de uma casa ja ocupada");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Valor inválido, utilize o formato   ' numeronumero '");
                }
            } while (true);
        }
    }

    class Player
    {
        public string Name { get; set; }
        public Player(string name)
        {
            this.Name = name;
        }
    }
}
