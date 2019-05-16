using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
   public class OtheloGame
    {
        private readonly eNumGameType k_Type;
        private OtheloBoard m_OtheloBoard;
        private Player[] m_Players; 
        private int m_PlayerTurn;
        
        public OtheloGame(string i_PlayerOneName, string i_PlayerTwoName, int i_SizeOfBoard, eNumGameType i_GameType)
        {
            m_Players = new Player[2];
            m_Players[0] = new Player(i_PlayerOneName, eNumSquare.BlackCell, 0);
            m_Players[1] = new Player(i_PlayerTwoName, eNumSquare.WhiteCell, 0);

            m_OtheloBoard = new OtheloBoard(i_SizeOfBoard);

            initilizePossibleMovesForBothPlayers();
            
            m_PlayerTurn = 0;
            k_Type = i_GameType;
        }

        public eNumSquare[,] OtheloBoard
        {
            get {return m_OtheloBoard.Board;}
        }

        public string PlayerNameTurn
        {
            get {return m_Players[m_PlayerTurn].Name;}
        }

        public bool IsStillPlayable()
        {
            bool canFirstPlayerStillPlay = m_Players[0].PossibleMoves.Count > 0;
            bool canSecondPlayerStillPlay = m_Players[1].PossibleMoves.Count > 0;

            return canFirstPlayerStillPlay || canSecondPlayerStillPlay;
        }
        
        public void MakeMove(Coords i_Coord)
        {
            eNumSquare playerSoldier = m_Players[m_PlayerTurn].SoldierType;

            m_OtheloBoard.SetSquare(i_Coord, playerSoldier);
            m_OtheloBoard.UpdateBoard(i_Coord, playerSoldier);
            SwitchTurn();
            UpdatePossibleMovesForBothPlayers();
            updateScoreForBothPlayer();
        }

        public void MakeCpuMove()
        {
            Random randomMove = new Random();
            int numOfPossibleMoves = m_Players[m_PlayerTurn].PossibleMoves.Count;
            int index = randomMove.Next(numOfPossibleMoves);
        
            MakeMove(m_Players[m_PlayerTurn].PossibleMoves[index]);
        }

        public bool IsLegalMoveTurn(Coords i_Crd)
        { 
            return m_Players[m_PlayerTurn].PossibleMoves.Contains(i_Crd);
        }

        public int Turn
        {
            get { return m_PlayerTurn; }
        }

        public void UpdatePossibleMovesForBothPlayers()
        {
            Player playerOne = m_Players[0];
            Player playerTwo = m_Players[1];
            m_Players[0].PossibleMoves.Clear();
            m_Players[1].PossibleMoves.Clear();

            eNumSquare[,] Board = m_OtheloBoard.Board;

            int sizeOfRowInBoard = Board.GetLength(0);
            int sizeOfColInBoard = sizeOfRowInBoard;
            
            for (int i = 0; i < sizeOfColInBoard; i++)
            {
                for(int j = 0; j < sizeOfRowInBoard; j++)
                {
                    Coords currentCoord = new Coords(i, j);
                    if (m_OtheloBoard.GetSquare(currentCoord) == eNumSquare.EmptyCell)
                    {
                        if (m_OtheloBoard.IsLegalToFill(currentCoord, eNumSquare.BlackCell))
                        {
                            playerOne.PossibleMoves.Add(currentCoord);
                        }

                        if (m_OtheloBoard.IsLegalToFill(currentCoord, eNumSquare.WhiteCell))
                        {
                            playerTwo.PossibleMoves.Add(currentCoord);
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

        public bool IsPlayerHasMove()
        {
            int numOfMovesOfCurrentPlayer = m_Players[m_PlayerTurn].PossibleMoves.Count;
            return numOfMovesOfCurrentPlayer > 0;
        }

        public void SwitchTurn()
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

        public Player[] Players
        {
            get { return m_Players; }
        }

        public string GetWinnerName()
        {
            int firstPlayerScore = m_Players[0].Score;
            int secondPlayerScore = m_Players[1].Score;
            string theWinner = null;

            if(firstPlayerScore > secondPlayerScore)
            {
                theWinner = m_Players[0].Name;
            }
            else if(secondPlayerScore > firstPlayerScore)
            {
                theWinner = m_Players[1].Name;
            }

            return theWinner;
        }

        public eNumSquare GetSquareType(int i_X, int i_Y)
        {
            return m_OtheloBoard.GetSquare(new Coords(i_X, i_Y));
        }

        private void initilizePossibleMovesForBothPlayers()
        {
            initilizePossibleMovesForPlayerOne();
            initilizePossibleMovesForPlayerTwo();
        }

        private void initilizePossibleMovesForPlayerOne()
        {
            int sizeOfBoard = m_OtheloBoard.Board.GetLength(0);
            int middlePosition = sizeOfBoard / 2;

            List<Coords> possibleMovesForPlayerOne = m_Players[0].PossibleMoves;
            possibleMovesForPlayerOne.Add(new Coords(middlePosition - 1, middlePosition - 2));
            possibleMovesForPlayerOne.Add(new Coords(middlePosition - 2, middlePosition - 1));
            possibleMovesForPlayerOne.Add(new Coords(middlePosition + 1, middlePosition));
            possibleMovesForPlayerOne.Add(new Coords(middlePosition, middlePosition + 1));
        }

        private void initilizePossibleMovesForPlayerTwo()
        {
            int sizeOfBoard = m_OtheloBoard.Board.GetLength(0);
            int middlePosition = sizeOfBoard / 2;

            List<Coords> possibleMovesForPlayerTwo = m_Players[1].PossibleMoves;
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition, middlePosition - 2));
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition + 1, middlePosition - 1));
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition - 2, middlePosition));
            possibleMovesForPlayerTwo.Add(new Coords(middlePosition - 1, middlePosition + 1));
        }

        private void updateScoreForBothPlayer()
        {
            eNumSquare[,] Board = m_OtheloBoard.Board;
       
            int scoreForPlayerOne = 0, scoreForPlayerTwo = 0;
            int sizeOfRowInBoard = Board.GetLength(0);
            int sizeOfColInBoard = sizeOfRowInBoard;

            for (int i = 0; i < sizeOfColInBoard; i++)
            {
                for (int j = 0; j < sizeOfRowInBoard; j++)
                {
                    if (Board[i, j] == eNumSquare.BlackCell)
                    {
                        scoreForPlayerOne++;
                    }
                    else if (Board[i, j] == eNumSquare.WhiteCell)
                    {
                        scoreForPlayerTwo++;
                    }
                }
            }

            m_Players[0].Score = scoreForPlayerOne;
            m_Players[1].Score = scoreForPlayerTwo; 
        }
    }
}
