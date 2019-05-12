namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameMannger
    {
        public void StartUI()
        {
            string firstPlayerName, secondPlayerName = null;
            int boardSize = 0, selcetedAnswer;
            EnumGameType gameType = EnumGameType.PlayerVSCpu;
            bool isValidInput = false;
            bool keepPlaying = true;

            Console.WriteLine("Welcome to Othelo game !{0}Please enter your name:", Environment.NewLine);
            firstPlayerName = Console.ReadLine();

            Console.WriteLine("{0} select the game mode you would like to play:", firstPlayerName);
            while (!isValidInput)
            {
                Console.WriteLine("Press 1 for playing against the computer Or press 2 for playing against opponent:");
                selcetedAnswer = int.Parse(Console.ReadLine());

                if (selcetedAnswer == 1)
                {
                    gameType = EnumGameType.PlayerVSCpu;
                    secondPlayerName = "CPU";
                    isValidInput = true;
                }
                else if (selcetedAnswer == 2)
                {
                    gameType = EnumGameType.PlayerVSPlayer;
                    Console.WriteLine("Please enter your oppenent name:");
                    secondPlayerName = Console.ReadLine();
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Wrong choice!");
                }
            }

            isValidInput = false;
            Console.WriteLine("Please select the size of the board for the game:");
            while (!isValidInput)
            {
                Console.WriteLine("Press 6 for 6X6 board size Or press 8 for 8X8 board size");
                boardSize = int.Parse(Console.ReadLine());
                if (boardSize == 6 || boardSize == 8)
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Worng choice!");
                }
            }

            isValidInput = false;

            while (keepPlaying)
            {
                runGame(firstPlayerName, secondPlayerName, boardSize, gameType);

                while (!isValidInput)
                {
                    Console.WriteLine("Would you like to play again?{0}Press 1 for YES Or 0 to quit:");
                    selcetedAnswer = int.Parse(Console.ReadLine());

                    if (selcetedAnswer == 0)
                    {
                        keepPlaying = false;
                        isValidInput = true;
                    }
                    else if (selcetedAnswer == 1)
                    {
                        Console.WriteLine("Loading new game ...");
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Worng choice!");
                    }
                }
            }
            Console.WriteLine("Thank you and GoodBye!");

        }
        private void runGame(string i_FirstName, string i_SecondName, int i_BoardSize, EnumGameType i_GameType)
        {
            OtheloGame game = new OtheloGame(i_FirstName, i_SecondName, i_BoardSize, i_GameType);

            string selectedMove;
            while (game.IsStillPlayable())
            {
                System.Threading.Thread.Sleep(1000);
                Ex02.ConsoleUtils.Screen.Clear();
                printGameBoard(game.GetGameBoard());
                if (!game.IsPlayerHasMove()) // IsPlayerHasMove Should change the turn
                {
                    Console.WriteLine("There are no possible legal moves, Next player turn!");
                }
                else
                {
                    if (i_GameType == EnumGameType.PlayerVSPlayer || game.GetTurn() == 0)
                    {
                        selectedMove = chooseMove(game.GetPlayerName()); 
                        if (selectedMove != "Q")
                        {
                            if (isValidString(selectedMove)) 
                            {
                                if (game.IsPlayerMoveExceedBoard(selectedMove))
                                {
                                    Console.WriteLine("Your input move is exceed the board size!");
                                }
                                else
                                {
                                    Coords newCoord = new Coords(selectedMove[1] - '1', selectedMove[0] - 'A');
                                    if (!game.IsLegalMoveTurn(newCoord)) 
                                    {
                                        Console.WriteLine("Your input move is illigal!");
                                    }
                                    else
                                    {
                                        game.MakeMove(newCoord);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("InValid input!");
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Comupter playing..");
                        game.MakeCpuMove();
                    }

                }

            }
        }

        private bool isValidString(string i_Str)
        {
            return char.IsUpper(i_Str[0]) && char.IsDigit(i_Str[1]) && i_Str.Length == 2;
        }

        private string chooseMove(string i_PlayerName)
        {
            Console.WriteLine("{0}! Select your next move:{1}It is need to be in the following form : (upperCase letter that represents the cols)(digit that represents the rows)", i_PlayerName, Environment.NewLine);
            return Console.ReadLine();
        }

        private void printGameBoard(EnumSquare[,] i_GameBoard)
        {
            int size = i_GameBoard.GetLength(0);
            char c = 'A';
            string line = new String('=', 4 * size + 1);
            ////first line
            Console.Write(" ");
            for (int j = 0; j < size; j++)
            {
                Console.Write("   {0}", char.ConvertFromUtf32(c + j));
            }
            Console.WriteLine("");
            Console.Write(" ");
            Console.WriteLine(line);
            ////other lines
            for (int i = 1; i <= size; i++)
            {
                Console.Write("{0} |", i);
                for (int j = 1; j <= size; j++)
                {
                    Console.Write(" {0} |", (char)(i_GameBoard[i - 1, j - 1]));
                }
                Console.WriteLine("");
                Console.Write(" ");
                Console.WriteLine(line);
            }
        }

    }
}
