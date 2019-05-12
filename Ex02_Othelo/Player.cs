using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
   public class Player
    {
        private readonly EnumSquare m_SoldierType;
        private string m_Name;

        public List<Coords> m_PossibleMoves;

        public Player(string i_Name, EnumSquare i_SquareType)
        {
            m_Name = i_Name;
            m_PossibleMoves = new List<Coords>();
            m_SoldierType = i_SquareType;
        }

        public string GetPlayerName()
        {
            return m_Name;
        }

        public EnumSquare GetSoldierType()
        {
            return m_SoldierType;
        }
    }
}
