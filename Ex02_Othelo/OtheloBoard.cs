﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{ 
    public class OtheloBoard
    {
        private eNumSquare[,] m_Board;
    
        public OtheloBoard(int i_Size)
        {
          m_Board = new eNumSquare[i_Size, i_Size];
          initilizeBoard();
        }

        public eNumSquare[,] Board
        {
            get { return m_Board; }
        }

        public eNumSquare GetSquare(Coords i_Crd)
        {
            return m_Board[i_Crd.X, i_Crd.Y];
        }

        public void SetSquare(Coords i_Coord, eNumSquare i_Square)
        {
            m_Board[i_Coord.X, i_Coord.Y] = i_Square;
        }

        public bool IsSquareFilled(Coords i_Coord)
        {
            return m_Board[i_Coord.X, i_Coord.Y] != eNumSquare.EmptyCell;
        }

        public bool IsSquareExceedBoard(int i_CoordX, int i_CoordY) 
        {
            bool isCoordXexceedBoard = i_CoordX < 0 || i_CoordX >= m_Board.GetLength(0);
            bool isCoordYexceedBoard = i_CoordY < 0 || i_CoordY >= m_Board.GetLength(0);
            return isCoordXexceedBoard || isCoordYexceedBoard;
        }

        public bool IsLegalToFill(Coords i_Coord, eNumSquare i_SoldierType) // logic
        {
            bool[] dirctionPath = new bool[8]; 
            bool neededToUpdateSquare = false;
            bool paintTheSquare = false;
            bool isFirstOcurrance = true;
            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y, -1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // בדיקת עמודה למעלה
            dirctionPath[0] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y + 1, -1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון ימין למעלה
            dirctionPath[1] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X, i_Coord.Y + 1, 0, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // שורה ימינה
            dirctionPath[2] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y + 1, 1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון ימין למטה
            dirctionPath[3] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y, 1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // עמודה למטה
            dirctionPath[4] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y - 1, 1, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון שמאלה למטה
            dirctionPath[5] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X, i_Coord.Y - 1, 0, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // שורה שמאלה
            dirctionPath[6] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y - 1, -1, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון שמאלה למעלה
            dirctionPath[7] = neededToUpdateSquare;

            bool isLegalSquare = false;
            for (int i = 0; i < 8; i++)
            {
                if (dirctionPath[i] == true)
                {
                    isLegalSquare = true;
                    break;
                }
            }

            return isLegalSquare;
        }
  
        public void UpdateBoard(Coords i_Coord, eNumSquare i_SoldierType) 
        {
            bool neededToUpdateSquare = false;
            bool paintTheSquare = true;
            bool isFirstOcurrance = true;

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y, -1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // בדיקת עמודה למעלה
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y + 1, -1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון ימין למעלה
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X , i_Coord.Y + 1, 0, 1, i_SoldierType, ref  neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // שורה ימינה
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y + 1, 1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון ימין למטה
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y, 1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // עמודה למטה
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y - 1, 1, -1, i_SoldierType,ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון שמאלה למטה

            updateOrCheckDirctionPathDueToMove(i_Coord.X , i_Coord.Y - 1, 0, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // שורה שמאלה

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y - 1, -1, -1, i_SoldierType,ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); // אלכסון שמאלה למעלה     
        }

        private void initilizeBoard()
        {
            int middlePosition = m_Board.GetLength(0) / 2;

            for(int i = 0; i < m_Board.GetLength(0); i++)
            {
                for(int j = 0; j < m_Board.GetLength(0); j++)
                {
                    m_Board[i,j] = eNumSquare.EmptyCell;
                }
            }

            m_Board[middlePosition, middlePosition] = eNumSquare.WhiteCell;
            m_Board[middlePosition - 1, middlePosition - 1] = eNumSquare.WhiteCell;
            m_Board[middlePosition - 1, middlePosition] = eNumSquare.BlackCell;
            m_Board[middlePosition, middlePosition - 1] = eNumSquare.BlackCell;
        }

        private void updateOrCheckDirctionPathDueToMove(int i_CordX, int i_CordY, int i_DirctionX, int i_DirctionY, eNumSquare i_SoldierType , ref bool io_NeededToUpdateSquare, bool paintTheSquare, bool isFirstOcurrance)
        {
            if(IsSquareExceedBoard(i_CordX, i_CordY) || m_Board[i_CordX, i_CordY] == eNumSquare.EmptyCell)
            {
                io_NeededToUpdateSquare = false;
            }
            else if(m_Board[i_CordX, i_CordY] == i_SoldierType)
            {
                if (!isFirstOcurrance)
                {
                    io_NeededToUpdateSquare = true;
                }
                else
                {
                    io_NeededToUpdateSquare = false;
                }
            }
            else
            {
                isFirstOcurrance = false;
                updateOrCheckDirctionPathDueToMove(i_CordX + i_DirctionX, i_CordY + i_DirctionY, i_DirctionX, i_DirctionY, i_SoldierType, ref io_NeededToUpdateSquare, paintTheSquare, isFirstOcurrance);
                if(io_NeededToUpdateSquare && paintTheSquare)
                {
                    m_Board[i_CordX, i_CordY] = i_SoldierType;
                }
            }
        } 
    }
}
