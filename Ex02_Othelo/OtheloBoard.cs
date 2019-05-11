using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{ 
    class OtheloBoard
    {

        private eNumSquare[,] m_Board;
    
        public OtheloBoard (int i_Size)
        {
          m_Board = new eNumSquare[i_Size,i_Size];
          initilizeBoard();
        }
        public eNumSquare[,] GetBoard()
        {
            return m_Board;
        }
        public eNumSquare GetSquare (Coords i_Crd)
        {
            return m_Board[i_Crd.x, i_Crd.y];
        }

        public void SetSquare(Coords i_Crd, eNumSquare i_Sqr)
        {
            m_Board[i_Crd.x, i_Crd.y] = i_Sqr;
        }

        public bool IsSquareFilled(Coords i_Crd)
        {
            return m_Board[i_Crd.x, i_Crd.y] != eNumSquare.EmptyCell;
        }

        public bool IsExceedBoard(string i_Str) // valid str
        {
            int x = i_Str[0] - 'A';
            int y = (i_Str[1] - '0');

            return !(x >= 0 && x < m_Board.GetLength(0) && y >= 0 && y < m_Board.GetLength(0));
        }
        public bool IsLegalToFill(Coords i_Crd, eNumSquare i_Sqr) // logic
        {
            return true;
            // check the row, check the coll, check the diagonal
        }// neccesary?!

        private void initilizeBoard()
        {
            int position = m_Board.GetLength(0) / 2;
            for(int i = 0; i< m_Board.GetLength(0); i++)
            {
                for(int j = 0; j < m_Board.GetLength(0); j++)
                {
                    m_Board[i,j] = eNumSquare.EmptyCell;
                }
            }
            m_Board[position, position] = eNumSquare.WhiteCell;
            m_Board[position - 1, position - 1] = eNumSquare.WhiteCell;
            m_Board[position - 1,position] = eNumSquare.BlackCell;
            m_Board[position, position - 1] = eNumSquare.BlackCell;
        }
        /*
        void UpdateBoard() // necceccary?
        {
 
        }
        */
    }
}
