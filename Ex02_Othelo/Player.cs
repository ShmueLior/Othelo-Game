using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
   public class Player
    {
        private string m_Name;
        private int m_Score;
        private readonly EnumSquare m_SoldierType;

        public List<Coords> m_PossibleMoves;

        public Player(string i_Name, EnumSquare i_SquareType,int i_Score)
        {
            m_Name = i_Name;
            m_PossibleMoves = new List<Coords>();
            m_SoldierType = i_SquareType;
            m_Score = 2;
        }

        public string GetPlayerName()
        {
            return m_Name;
        }

        public EnumSquare GetSoldierType()
        {
            return m_SoldierType;
        }

        public void SetScore(int i_Score)
        {
            m_Score = i_Score;
        }

        public int GetScore()
        {
           return m_Score;
        }
    }
}
