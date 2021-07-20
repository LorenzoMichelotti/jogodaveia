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

        private static void TryWinConditions(List<List<char>> gameBoard, bool isplayer1)
        {
            char x = ' ';
            if (isplayer1)
            {
                x = 'X';
            }
            else
            {
                x = 'O';
                isplayer1 = false;
            }

            List<string> currentPlayerPositions = GetCurrentBoard(gameBoard, x);
            List<string> WinConditions = new List<string>()
            {
                "012","345","678","036","147","258","048","246"
            };
            foreach (string winCondition in WinConditions)
            {
                string a = winCondition.Substring(0,1);
                string b = winCondition.Substring(1,1);
                string c = winCondition.Substring(2,1);
                if (currentPlayerPositions.Contains(a) &
                    currentPlayerPositions.Contains(b) &
                    currentPlayerPositions.Contains(c))
                {
                    if (isplayer1)
                    {
                        Program program = new Program();
                        program.Render();
                        Victory(player1.Name);
                    } else
                    {
                        Program program = new Program();
                        program.Render();
                        Victory(player2.Name);
                    }
                }
            }
        }

        private static List<string> GetCurrentBoard(List<List<char>> gameBoard, char x)
        {
            List<string> playerMoves = new List<string>();
            for (int row = 0; row < gameBoard.Count; row++)
            {
                for (int collumn = 0; collumn < gameBoard[row].Count; collumn++)
                {
                    if(gameBoard[row][collumn] == x)
                    {
                        string position = row.ToString() + collumn.ToString();
                        switch (position)
                        {
                            case "00":
                                playerMoves.Add("0");
                                break;
                            case "01":
                                playerMoves.Add("1");
                                break;
                            case "02":
                                playerMoves.Add("2");
                                break;
                            case "10":
                                playerMoves.Add("3");
                                break;
                            case "11":
                                playerMoves.Add("4");
                                break;
                            case "12":
                                playerMoves.Add("5");
                                break;
                            case "20":
                                playerMoves.Add("6");
                                break;
                            case "21":
                                playerMoves.Add("7");
                                break;
                            case "22":
                                playerMoves.Add("8");
                                break;
                            default:
                                break;
                        }
                    }
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
            bool ganhou = false;
            do
            {
                program.Render();
                Turn(gameBoard, usedCoordinates);
                TryWinConditions(gameBoard, player1Turn);
                player1Turn = !player1Turn;
            } while (!ganhou);
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

        static void Victory(string winner)
        {
            Console.WriteLine($"*******************{winner} ganhou!******************" +
                $"\n1-Jogar novamente   2-Sair");
            string resposta = Console.ReadLine();
            if (resposta == "1")
            {
                RunGame();
            }
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
