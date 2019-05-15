using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
   public class OtheloGame
    {
        private readonly EnumGameType k_Type;
        private OtheloBoard m_OtheloBoard;
        private Player[] m_Players; 
        private int m_PlayerTurn;
        
        public OtheloGame(string i_PlayerOneName, string i_PlayerTwoName, int i_SizeOfBoard, EnumGameType i_GameType)
        {
            m_Players = new Player[2];
            m_Players[0] = new Player(i_PlayerOneName, EnumSquare.BlackCell, 0);
            m_Players[1] = new Player(i_PlayerTwoName, EnumSquare.WhiteCell, 0);

            m_OtheloBoard = new OtheloBoard(i_SizeOfBoard);

            initilizePossibleMovesForBothPlayers();
            
            m_PlayerTurn = 0;
            k_Type = i_GameType;
        }

        public EnumSquare[,] GetGameBoard()
        {
            return m_OtheloBoard.GetBoard();
        }

        public string GetPlayerName()
        {
            return m_Players[m_PlayerTurn].GetPlayerName();
        }

        public bool IsStillPlayable()
        {
            bool canFirstPlayerStillPlay = m_Players[0].m_PossibleMoves.Count > 0;
            bool canSecondPlayerStillPlay = m_Players[1].m_PossibleMoves.Count > 0;

            return canFirstPlayerStillPlay || canSecondPlayerStillPlay;
        }
        
        public void MakeMove(Coords i_Coord)
        {
            EnumSquare playerSoldier = m_Players[m_PlayerTurn].GetSoldierType();

            m_OtheloBoard.SetSquare(i_Coord, playerSoldier);
            m_OtheloBoard.UpdateBoard(i_Coord, playerSoldier);
            switchTurn();
            UpdatePossibleMovesForBothPlayers();
            updateScoreForBothPlayer();
        }

        private void updateScoreForBothPlayer()
        {
            EnumSquare[,] Board = m_OtheloBoard.GetBoard();
       
            int scoreForPlayerOne = 0, scoreForPlayerTwo = 0;
            int sizeOfRowInBoard = Board.GetLength(0);
            int sizeOfColInBoard = sizeOfRowInBoard;

            for (int i = 0; i < sizeOfColInBoard; i++)
            {
                for (int j = 0; j < sizeOfRowInBoard; j++)
                {
                    if (Board[i, j] == EnumSquare.BlackCell)
                    {
                        scoreForPlayerOne++;
                    }
                    else if (Board[i, j] == EnumSquare.WhiteCell)
                    {
                        scoreForPlayerTwo++;
                    }
                }
            }

            m_Players[0].SetScore(scoreForPlayerOne);
            m_Players[1].SetScore(scoreForPlayerTwo); 
        }

        public void MakeCpuMove()
        {
            Random randomMove = new Random();
            int numOfPossibleMoves = m_Players[m_PlayerTurn].m_PossibleMoves.Count;
            int index = randomMove.Next(numOfPossibleMoves);
        
            MakeMove(m_Players[m_PlayerTurn].m_PossibleMoves[index]);
            updateScoreForBothPlayer(); // אולי לעשות את מייקמוב תחת אותה מטודה
        }

        public bool IsLegalMoveTurn(Coords i_Crd)
        { 
            return m_Players[m_PlayerTurn].m_PossibleMoves.Contains(i_Crd);
        }

        public int GetTurn()
        {
            return m_PlayerTurn;
        }

        public void UpdatePossibleMovesForBothPlayers()
        {
            Player playerOne = m_Players[0];
            Player playerTwo = m_Players[1];
            m_Players[0].m_PossibleMoves.Clear();
            m_Players[1].m_PossibleMoves.Clear();

            EnumSquare[,] Board = m_OtheloBoard.GetBoard();

            int sizeOfRowInBoard = Board.GetLength(0);
            int sizeOfColInBoard = sizeOfRowInBoard;
            
            for (int i = 0; i < sizeOfColInBoard; i++)
            {
                for(int j = 0; j < sizeOfRowInBoard; j++)
                {
                    Coords currentCoord = new Coords(i, j);
                    if (m_OtheloBoard.GetSquare(currentCoord) == EnumSquare.EmptyCell)
                    {
                        if (m_OtheloBoard.IsLegalToFill(currentCoord, EnumSquare.BlackCell))
                        {
                            playerOne.m_PossibleMoves.Add(currentCoord);
                        }

                        if (m_OtheloBoard.IsLegalToFill(currentCoord, EnumSquare.WhiteCell))
                        {
                            playerTwo.m_PossibleMoves.Add(currentCoord);
                        }
                    }
                }
            }
        }

        public bool IsPlayerMoveExceedBoard(string i_Str)
        {
            int coordX = i_Str[1] - '1';
            int coordY = i_Str[0] - 'A';
            return m_OtheloBoard.IsSquareExceedBoard(coordX, coordY);
        }

        private void initilizePossibleMovesForBothPlayers()
        {
            initilizePossibleMovesForPlayerOne();
            initilizePossibleMovesForPlayerTwo();
        }

        private void initilizePossibleMovesForPlayerTwo()
        {
            int sizeOfBoard = m_OtheloBoard.GetBoard().GetLength(0);
            int middlePosition = sizeOfBoard / 2;
            List<Coords> possibleMovesForPlayerTwo = m_Players[1].m_PossibleMoves;

            possibleMovesForPlayerTwo.Add(new Coords(middlePosition, middlePosition - 2));
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition + 1, middlePosition - 1));
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition - 2, middlePosition));
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition - 1, middlePosition + 1));
        }

        private void initilizePossibleMovesForPlayerOne()
        {
            int sizeOfBoard = m_OtheloBoard.GetBoard().GetLength(0);
            int middlePosition = sizeOfBoard / 2;

            List<Coords> possibleMovesForPlayerOne = m_Players[0].m_PossibleMoves;
            possibleMovesForPlayerOne.Add(new Coords(middlePosition - 1, middlePosition - 2));
            possibleMovesForPlayerOne.Add(new Coords(middlePosition - 2, middlePosition - 1));
            possibleMovesForPlayerOne.Add(new Coords(middlePosition + 1, middlePosition));
            possibleMovesForPlayerOne.Add(new Coords(middlePosition, middlePosition + 1));
        }

        public bool IsPlayerHasMove()
        {
            int numOfMovesOfCurrentPlayer = m_Players[m_PlayerTurn].m_PossibleMoves.Count;
            return numOfMovesOfCurrentPlayer > 0;
        }

        public void switchTurn()
        {
            if (m_PlayerTurn == 1)
            {
                m_PlayerTurn = 0;
            }
            else
            {
                m_PlayerTurn = 1;
            }
        }

        public Player[] getPlayers()
        {
            return m_Players;
        }

        public string getWinnerName()
        {
            int firstPlayerScore = m_Players[0].GetScore();
            int secondPlayerScore = m_Players[1].GetScore();
            string theWinner = null;

            if(firstPlayerScore > secondPlayerScore)
            {
                theWinner = m_Players[0].GetPlayerName();
            }
            else if(secondPlayerScore > firstPlayerScore)
            {
                theWinner = m_Players[1].GetPlayerName();
            }

            return theWinner;
        }

        public EnumSquare getSquare(int i_X, int i_Y)
        {
            return m_OtheloBoard.GetSquare(new Coords(i_X, i_Y));
        }
    }
}
