using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{ 
    class OtheloBoard
    {
        private EnumSquare[,] m_Board;
    
        public OtheloBoard (int i_Size)
        {
          m_Board = new EnumSquare[i_Size,i_Size];
          initilizeBoard();
        }

        public EnumSquare[,] GetBoard()
        {
            return m_Board;
        }

        public EnumSquare GetSquare (Coords i_Crd)
        {
            return m_Board[i_Crd.x, i_Crd.y];
        }

        public void SetSquare(Coords i_Crd, EnumSquare i_Sqr)
        {
            m_Board[i_Crd.y, i_Crd.x] = i_Sqr;
        }

        public bool IsSquareFilled(Coords i_Crd)
        {
            return m_Board[i_Crd.x, i_Crd.y] != EnumSquare.EmptyCell;
        }

        public bool IsSquareExceedBoard(int i_CoordX, int i_CoordY) // valid str
        {
            bool isCoordXexceedBoard = i_CoordX < 0 || i_CoordX >= m_Board.GetLength(0);
            bool isCoordYexceedBoard = i_CoordY < 0 || i_CoordY >= m_Board.GetLength(0);
            return (isCoordXexceedBoard || isCoordYexceedBoard);
        }

        public bool IsLegalToFill(Coords i_Crd, EnumSquare i_Sqr) // logic
        {
            return true;
            // check the row, check the coll, check the diagonal
        }// neccesary?!

        private void initilizeBoard()
        {
            int middlePosition = m_Board.GetLength(0) / 2;

            for(int i = 0; i < m_Board.GetLength(0); i++)
            {
                for(int j = 0; j < m_Board.GetLength(0); j++)
                {
                    m_Board[i,j] = EnumSquare.EmptyCell;
                }
            }

            m_Board[middlePosition, middlePosition] = EnumSquare.WhiteCell;
            m_Board[middlePosition - 1, middlePosition - 1] = EnumSquare.WhiteCell;
            m_Board[middlePosition - 1, middlePosition] = EnumSquare.BlackCell;
            m_Board[middlePosition, middlePosition - 1] = EnumSquare.BlackCell;
        }

        
        public void UpdateBoard(Coords i_Coord, EnumSquare i_SoldierType) 
        {
            bool neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x - 1, i_Coord.y, -1, 0, i_SoldierType, ref neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x - 1, i_Coord.y + 1, -1, 1, i_SoldierType,ref  neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x , i_Coord.y + 1, 0, 1, i_SoldierType,ref  neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x + 1, i_Coord.y + 1, 1, 1, i_SoldierType,ref neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x + 1, i_Coord.y, 1, 0, i_SoldierType,ref neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x + 1, i_Coord.y - 1, 1, -1, i_SoldierType,ref neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x , i_Coord.y - 1, 0, -1, i_SoldierType, ref neededToUpdateSquare);
            neededToUpdateSquare = false;
            updateDirctionPathDueToMove(i_Coord.x - 1, i_Coord.y - 1, -1, -1, i_SoldierType,ref neededToUpdateSquare);
            neededToUpdateSquare = false;
        }

        private void updateDirctionPathDueToMove(int i_CordX, int i_CordY, int i_DirctionX, int i_DirctionY, EnumSquare i_SoldierType , ref bool  neededToUpdateSquare)
        {
            if(m_Board[i_CordY, i_CordX] == EnumSquare.EmptyCell || IsSquareExceedBoard(i_CordX,i_CordY))
            {
                neededToUpdateSquare = false;
            }
            else if(m_Board[i_CordY,i_CordX] == i_SoldierType)
            {
                neededToUpdateSquare = true;
            }
            else
            {
                updateDirctionPathDueToMove(i_CordX + i_DirctionX, i_CordY + i_DirctionY, i_DirctionX, i_DirctionY, i_SoldierType, ref neededToUpdateSquare);
                if(neededToUpdateSquare)
                {
                    m_Board[i_CordY, i_CordX] = i_SoldierType;
                }
            }
        }
        
    }
}
