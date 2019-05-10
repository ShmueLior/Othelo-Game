using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class GameMannger
    {
        private int m_GameType;
      //  public OtheloGame game;
        public void RunGame()
        {
            OtheloGame game = BulidGame();
        }

        private OtheloGame BuildGame()
        {
            Console.WriteLine("Welcome to othelo game !");
            Console.WriteLine("Please enter your name :");
            string player1Name = Console.ReadLine();
            Console.WriteLine("Please choose which type of game would you like to play :");
            Console.WriteLine("Choose 1 for play againts opponent");
            Console.WriteLine("Choose 2 for play againts the computer");


            while(int.Parse(Console.ReadLine()) != 1 && 
            OtheloGame game = new OtheloGame(,,,,);
            return game;
        }
        private bool isValidChoice(int choise)
        {

        }
}
