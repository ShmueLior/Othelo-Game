using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class OtheloGame
    {
        private OtheloBoard m_OtheloBoard;
        private Player[] m_Players = new Player[2];
        private int m_Turn;
        private readonly eNumGameType k_Type;
        
        public OtheloGame (string i_PlayerOneName, string i_PlayerTwoName, int i_SizeOfBoard, eNumGameType i_GameType)
        {
            m_OtheloBoard = new OtheloBoard(i_SizeOfBoard);
            m_Players[0] = new Player(i_PlayerOneName);

            initilizePossibeMovesForPlayerOne();
            m_Players[1] = new Player(i_PlayerTwoName);
            initilizePossibeMovesForPlayerTwo();
            m_Turn = 0;
            k_Type = i_GameType;
        }
        public eNumSquare [,] GetOtheloBoard()
        {
            return m_OtheloBoard.GetBoard();
        }
        public string GetPlayerName()
        {
            return m_Players[m_Turn].GetPlayerName();
        }

        public bool IsStillPlayable()//לממש
        {
            return true; // לממש
        }

        public void MakeMove(Coords i_Crd)
        {
            
        }

        public void MakeCpuMove()
        {
            return; // לממש
        }

        public bool IsLegalMoveTurn(Coords i_Crd) // logic
        {
            return true;
        }

        public int GetTurn ()
        {
            return m_Turn;
        }

        public void UpdatePossibleMovesForBothPlayers()
        {
            return; // לממש
        }
        public bool IsMoveExceedBoard(string i_Str) // valid str
        {
           return m_OtheloBoard.IsExceedBoard(i_Str);
        }
        private void initilizePossibeMovesForPlayerOne()
        {
            int position = (m_OtheloBoard.GetBoard().GetLength(0)) / 2;
            m_Players[0].m_PossibleMoves.Add(new Coords(position, position - 2));
            m_Players[0].m_PossibleMoves.Add(new Coords(position + 1, position - 1));
            m_Players[0].m_PossibleMoves.Add(new Coords(position - 2, position));
            m_Players[0].m_PossibleMoves.Add(new Coords(position - 1, position + 1));
        }

        private void initilizePossibeMovesForPlayerTwo()
        {
            int position = m_OtheloBoard.GetBoard().GetLength(0) / 2;
            m_Players[1].m_PossibleMoves.Add(new Coords(position - 1, position - 2));
            m_Players[1].m_PossibleMoves.Add(new Coords(position - 2, position - 1));
            m_Players[1].m_PossibleMoves.Add(new Coords(position + 1, position));
            m_Players[1].m_PossibleMoves.Add(new Coords(position, position + 1));
        }

        public bool IsPlayerHasMove()
        {
            return m_Players[m_Turn].m_PossibleMoves.Count > 0;
        }
    }
}
