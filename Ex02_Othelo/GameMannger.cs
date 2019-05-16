namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class GameMannger
    {
        public void StartUI()
        {
            // לשפר
            string firstPlayerName, secondPlayerName = null;
            int boardSize = 0, selcetedAnswer;
            eNumGameType gameType = eNumGameType.PlayerVSCpu;
            bool isValidInput = false;
            bool keepPlaying = true;

            Console.WriteLine("Welcome to Othelo game !{0}Please enter your name:", Environment.NewLine);
            firstPlayerName = Console.ReadLine();

            Console.WriteLine("{0} select the game mode you would like to play:", firstPlayerName);
            while (!isValidInput)
            {
                Console.WriteLine("Press 1 for playing against the computer Or press 2 for playing against opponent:");
                isValidInput = int.TryParse(Console.ReadLine(), out selcetedAnswer);
                if (isValidInput && selcetedAnswer == 1)
                {
                    gameType = eNumGameType.PlayerVSCpu;
                    secondPlayerName = "CPU";
                    isValidInput = true;
                }
                else if (isValidInput && selcetedAnswer == 2)
                {
                    gameType = eNumGameType.PlayerVSPlayer;
                    Console.WriteLine("Please enter your oppenent name:");
                    secondPlayerName = Console.ReadLine();
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Wrong choice!");
                    isValidInput = false;
                }
            }

            isValidInput = false;
            Console.WriteLine("Please select the size of the board for the game:");
            while (!isValidInput)
            {
                Console.WriteLine("Press 6 for 6X6 board size Or press 8 for 8X8 board size:");
                isValidInput = int.TryParse(Console.ReadLine(), out boardSize);
                ////Check Check off the invalid
                if (isValidInput && (boardSize == 6 || boardSize == 8 || boardSize == 4))
                {
                    isValidInput = true;
                }
                else
                { 
                    Console.WriteLine("Worng choice!");
                    isValidInput = false;
                }
            }
    
            isValidInput = false;

            while (keepPlaying)
            {
                runGame(firstPlayerName, secondPlayerName, boardSize, gameType);

                while (!isValidInput)
                {
                    Console.WriteLine("Would you like to play again?{0}Press 1 for YES Or 0 to quit:", Environment.NewLine);
                    isValidInput = int.TryParse(Console.ReadLine(), out selcetedAnswer);

                    if (isValidInput && selcetedAnswer == 0)
                    {
                        keepPlaying = false;
                        isValidInput = true;
                    }
                    else if (isValidInput && selcetedAnswer == 1)
                    {
                        Console.WriteLine("Loading new game ...");
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Worng choice!");
                        isValidInput = false;
                    }
                }

                isValidInput = false;
            }

            Console.WriteLine("Thank you and GoodBye!");
        }

        private void runGame(string io_FirstName, string io_SecondName, int io_BoardSize, eNumGameType io_GameType)
        {
            OtheloGame game = new OtheloGame(io_FirstName, io_SecondName, io_BoardSize, io_GameType);
            printIntroduction(io_FirstName, io_SecondName);
            string selectedMove = null;
            while (game.IsStillPlayable())
            {
                System.Threading.Thread.Sleep(1000);
                Ex02.ConsoleUtils.Screen.Clear();
                printGameBoard(game);
                if (!game.IsPlayerHasMove())
                {
                    game.SwitchTurn();
                    Console.WriteLine("There are no possible legal moves, The turn pass to : {0}!", game.PlayerNameTurn);
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    if (io_GameType == eNumGameType.PlayerVSPlayer || game.Turn == 0)
                    {
                        selectedMove = chooseMove(game.PlayerNameTurn); 
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
                                        Console.WriteLine("Your input move is illigal! {0}You must choose a move which blocks an opponent's  sequence", Environment.NewLine);
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

            if (selectedMove != null)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                printGameBoard(game);
                printTheScoreAndTheWinner(game);
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

        private void printGameBoard(OtheloGame i_game) 
        {
            int size = i_game.OtheloBoard.GetLength(0);
            char c = 'A';
            string line = new String('=', (4 * size) + 1);
            string firstPlayerName = i_game.Players[0].Name;
            string secondPlayerName = i_game.Players[1].Name;
            int firstPlayerScore = i_game.Players[0].Score;
            int secondPlayerScore = i_game.Players[1].Score;

            ////first line
            Console.Write(" ");
            for (int j = 0; j < size; j++)
            {
                Console.Write("   {0}", char.ConvertFromUtf32(c + j));
            }

            Console.WriteLine(string.Empty);
            Console.Write(" ");
            Console.WriteLine(line);
            ////other lines
            for (int i = 1; i <= size; i++)
            {
                Console.Write("{0} |", i);
                for (int j = 1; j <= size; j++)
                {
                    Console.Write(" {0} |", (char)i_game.GetSquareType(i - 1, j - 1));
                }

                if (i == size / 2)
                {   
                    if (i_game.Turn == 0)
                    {
                        Console.Write("-->");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.Write("{0} Score: {1} Soldier X", firstPlayerName, firstPlayerScore);              
                }
                else if (i == (size / 2) + 1)
                {
                    if (i_game.Turn == 1)
                    {
                        Console.Write("-->");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.Write("{0} Score: {1} Soldier O", secondPlayerName, secondPlayerScore);                     
                }

                Console.WriteLine(string.Empty);
                Console.Write(" ");
                Console.WriteLine(line);
            }
        }

        private void printIntroduction(string i_FirstName, string i_SecondName)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("The game is about to start!");
            Console.WriteLine("{0} your soldiers are: {1}", i_FirstName, (char)eNumSquare.BlackCell);
            Console.WriteLine("{0} your soldiers are: {1}", i_SecondName, (char)eNumSquare.WhiteCell);
            Console.WriteLine("Press Enter to start the game..");
            Console.ReadLine();
        }

        private void printTheScoreAndTheWinner(OtheloGame i_game)
        {
            Console.WriteLine("The game is over! The scores are:");
            Console.WriteLine("{0} your score is: {1}", i_game.Players[0].Name, i_game.Players[0].Score);
            Console.WriteLine("{0} your score is: {1}", i_game.Players[1].Name, i_game.Players[1].Score);
            string theWinner = i_game.GetWinnerName();

            if(theWinner == null)
            {
                Console.WriteLine("It's a tie!");
            }
            else
            {
                Console.WriteLine("The winner is {0} !!", theWinner);
            }
        }
    }
}
