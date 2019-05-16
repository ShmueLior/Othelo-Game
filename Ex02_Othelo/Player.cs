using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
   public class Player
    {
        private readonly eNumSquare r_SoldierType;
        private readonly string r_Name;
        private int m_Score;
        private List<Coords> m_PossibleMoves;

        public Player(string i_Name, eNumSquare i_SquareType, int i_Score) 
        {
            r_Name = i_Name;
            m_PossibleMoves = new List<Coords>();
            r_SoldierType = i_SquareType;
            m_Score = 2;
        }

        public List<Coords> PossibleMoves
        {
            get { return m_PossibleMoves; }
        }

        public string Name
        {
            get { return r_Name; }
        }

        public eNumSquare SoldierType
        {
            get { return r_SoldierType; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
    }
}
