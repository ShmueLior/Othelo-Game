using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
   public class OtheloGame
    {
        private readonly EnumGameType k_Type;
        private OtheloBoard           m_OtheloBoard;
        private Player[]              m_Players; 
        private int                   m_PlayerTurn;
        
        public OtheloGame(string i_PlayerOneName, string i_PlayerTwoName, int i_SizeOfBoard, EnumGameType i_GameType)
        {
            m_Players = new Player[2];
            m_Players[0] = new Player(i_PlayerOneName, EnumSquare.BlackCell);
            m_Players[1] = new Player(i_PlayerTwoName, EnumSquare.WhiteCell);

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
        
        public void MakeMove(Coords i_Crd)
        {
            m_OtheloBoard.SetSquare(i_Crd, m_Players[m_PlayerTurn].GetSoldierType());
            m_OtheloBoard.UpdateBoard(i_Crd, m_Players[m_PlayerTurn].GetSoldierType());
            UpdatePossibleMovesForBothPlayers();
            switchTurn();
        }
        
        public void MakeCpuMove()
        {
            Random randomMove = new Random();
            int index = randomMove.Next(m_Players[m_PlayerTurn].m_PossibleMoves.Count);
            MakeMove(m_Players[m_PlayerTurn].m_PossibleMoves[index]);
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
            m_Players[0].m_PossibleMoves.Clear();
            m_Players[1].m_PossibleMoves.Clear();
            for (int i = 0; i < m_OtheloBoard.GetBoard().GetLength(0); i++)
            {
                for(int j = 0; j < m_OtheloBoard.GetBoard().GetLength(0); j++)
                {
                    Coords coord = new Coords(i, j);
                    if (m_OtheloBoard.GetSquare(coord) == EnumSquare.EmptyCell)
                    {
                        if (m_OtheloBoard.IsLegalToFill(coord, EnumSquare.BlackCell))
                        {
                            m_Players[0].m_PossibleMoves.Add(coord);
                        }
                        if (m_OtheloBoard.IsLegalToFill(coord, EnumSquare.WhiteCell))
                        {
                            m_Players[1].m_PossibleMoves.Add(coord);
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
            int middlePosition = m_OtheloBoard.GetBoard().GetLength(0) / 2;
            m_Players[1].m_PossibleMoves.Add(new Coords(middlePosition, middlePosition - 2));
            m_Players[1].m_PossibleMoves.Add(new Coords(middlePosition + 1, middlePosition - 1));
            m_Players[1].m_PossibleMoves.Add(new Coords(middlePosition - 2, middlePosition));
            m_Players[1].m_PossibleMoves.Add(new Coords(middlePosition - 1, middlePosition + 1));
        }

        private void initilizePossibleMovesForPlayerOne()
        {
            int middlePosition = m_OtheloBoard.GetBoard().GetLength(0) / 2;
            m_Players[0].m_PossibleMoves.Add(new Coords(middlePosition - 1, middlePosition - 2));
            m_Players[0].m_PossibleMoves.Add(new Coords(middlePosition - 2, middlePosition - 1));
            m_Players[0].m_PossibleMoves.Add(new Coords(middlePosition + 1, middlePosition));
            m_Players[0].m_PossibleMoves.Add(new Coords(middlePosition, middlePosition + 1));
        }

        public bool IsPlayerHasMove()
        {
            int numOfMovesOfCurrentPlayer = m_Players[m_PlayerTurn].m_PossibleMoves.Count;
            return numOfMovesOfCurrentPlayer > 0;
        }
        private void switchTurn()
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
    }
}
