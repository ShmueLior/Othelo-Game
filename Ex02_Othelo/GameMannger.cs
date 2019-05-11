using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class GameMannger
    {

        public void StartUI()
        {
            string firstPlayerName, secondPlayerName = null;
            int boardSize = 0 , selcetAnswer;
            eNumGameType gameType = eNumGameType.PlayerVSCpu;
            bool validInput = false;
            bool keepPlaying = true;

            Console.WriteLine("Welcome to Othelo game !{0}Please enter your name:", Environment.NewLine);
            firstPlayerName = Console.ReadLine();

            Console.WriteLine("Please select the game mode you would like to play:");
            while(!validInput)
            {
                Console.WriteLine("Press 1 for playing against the computer Or press 2 for playing against opponent:");
                selcetAnswer = int.Parse(Console.ReadLine());
                if(selcetAnswer == 1 || selcetAnswer == 2)
                {
                    if(selcetAnswer == 1)
                    {
                        gameType = eNumGameType.PlayerVSCpu;
                        secondPlayerName = "CPU";
                    }
                    else
                    {
                        gameType = eNumGameType.PlayerVSPlayer;
                        Console.WriteLine("Please enter your oppenent name:");
                        secondPlayerName = Console.ReadLine();

                    }
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Worng choice!");
                }
            }
            validInput = false;
            Console.WriteLine("Please select the size of the board for the game:");
            while(!validInput)
            {
                Console.WriteLine("Press 6 for 6X6 board size Or press 8 for 8X8 board size");
                boardSize = int.Parse(Console.ReadLine());
                if(boardSize == 6 || boardSize == 8)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Worng choice!");

                }
            }
            validInput = false;

            while (keepPlaying)
            {
                runGame(firstPlayerName, secondPlayerName, boardSize, gameType);
                
                while(!validInput)
                {
                    Console.WriteLine("Would you like to play again?{0}Press 1 for YES Or 0 to quit:");
                    selcetAnswer = int.Parse(Console.ReadLine());
                    if (selcetAnswer == 1 || selcetAnswer == 0)
                    {
                        if(selcetAnswer == 0)
                        {
                            keepPlaying = false;
                            validInput = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Worng choice!");
                    }
                }
            }
            Console.WriteLine("Thank you and GoodBye!");
        }
        private void runGame(string i_FirstName, string i_SecondName, int i_BoardSize, eNumGameType i_GameType)
        {
            OtheloGame game = new OtheloGame(i_FirstName, i_SecondName, i_BoardSize, i_GameType);

            string selectedMove;
            while(game.IsStillPlayable())
            {
                Ex02.ConsoleUtils.Screen.Clear();
                printGameBoard(game.GetOtheloBoard());
                if (!game.IsPlayerHasMove()) // IsPlayerHasMove Should change the turn
                {
                    Console.WriteLine("There are no possible legal moves, Next player turn!");
                }
                else
                {
                    if (i_GameType == eNumGameType.PlayerVSPlayer || game.GetTurn() == 0)//זה לא תור של מחשב
                    {
                        selectedMove = chooseMove(game.GetPlayerName());
                        if (selectedMove != "Q")
                        {
                            if (isValidString(selectedMove))
                            {
                                if (game.IsMoveExceedBoard(selectedMove))
                                {
                                    Console.WriteLine("Your input move is exceed the board size!");
                                }
                                else
                                {
                                    Coords newCoord = new Coords(selectedMove[0] - 'A', selectedMove[1] - '0');
                                    if (!game.IsLegalMoveTurn(newCoord))
                                    {
                                        Console.WriteLine("Your input move is not exceeded the board but illigal!");
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
            Console.WriteLine("{0}! Select your next move: (It is need to be in the following form : (upperCase letter that represents the cols)(digit that represents the rows))",i_PlayerName);
            return Console.ReadLine();
        }
        private void printGameBoard(eNumSquare[,] i_GameBoard)
        {
            int size = i_GameBoard.GetLength(0);
            char c = 'A';
            string line = new String('=', 4 * size + 1);
            //first line
            Console.Write(" ");
            for(int j = 0; j < size; j++)
            {
                Console.Write("   {0}", char.ConvertFromUtf32(c + j));
            }
            Console.WriteLine("");
            Console.Write(" ");
            Console.WriteLine(line);
            //other lines
            for(int i = 1; i <= size; i++)
            {
                Console.Write("{0} |", i);
                for (int j = 1; j <= size; j++)
                {
                    Console.Write(" {0} |", ((char)(i_GameBoard[i - 1, j - 1])));
                }
                Console.WriteLine("");
                Console.Write(" ");
                Console.WriteLine(line);
            }
        }
        
    }
}
