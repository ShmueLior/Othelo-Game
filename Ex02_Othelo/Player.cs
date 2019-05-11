using System;
using System.Collections.Generic;
using System.Text;



namespace Ex02_Othelo
{
    class Player
    {
        private string m_Name;
        public List<Coords> m_PossibleMoves;

        public Player(string i_Name)
        {
            m_Name = i_Name;
            m_PossibleMoves = new List<Coords>();
        }
        public string GetPlayerName()
        {
            return m_Name;
        }
    }
}
