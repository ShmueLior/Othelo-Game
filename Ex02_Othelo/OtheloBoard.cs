namespace Ex02_OtheloLogic
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

        public bool IsLegalToFill(Coords i_Coord, eNumSquare i_SoldierType)
        {
            bool[] dirctionPath = new bool[8]; 
            bool neededToUpdateSquare = false;
            bool paintTheSquare = false;
            bool isFirstOcurrance = true;
            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y, -1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            dirctionPath[0] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y + 1, -1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 
            dirctionPath[1] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X, i_Coord.Y + 1, 0, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 
            dirctionPath[2] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y + 1, 1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 
            dirctionPath[3] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y, 1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            dirctionPath[4] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y - 1, 1, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            dirctionPath[5] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X, i_Coord.Y - 1, 0, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 
            dirctionPath[6] = neededToUpdateSquare;

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y - 1, -1, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 
            dirctionPath[7] = neededToUpdateSquare;

            int numOfDirection = 8;
            bool isLegalSquare = false;
            for (int i = 0; i < numOfDirection; i++)
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

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y, -1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y + 1, -1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X, i_Coord.Y + 1, 0, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y + 1, 1, 1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y, 1, 0, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 
            
            updateOrCheckDirctionPathDueToMove(i_Coord.X + 1, i_Coord.Y - 1, 1, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 

            updateOrCheckDirctionPathDueToMove(i_Coord.X, i_Coord.Y - 1, 0, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance); 

            updateOrCheckDirctionPathDueToMove(i_Coord.X - 1, i_Coord.Y - 1, -1, -1, i_SoldierType, ref neededToUpdateSquare, paintTheSquare, isFirstOcurrance);      
        }

        private void initilizeBoard()
        {
            int middlePosition = m_Board.GetLength(0) / 2;

            for(int i = 0; i < m_Board.GetLength(0); i++)
            {
                for(int j = 0; j < m_Board.GetLength(0); j++)
                {
                    m_Board[i, j] = eNumSquare.EmptyCell;
                }
            }

            m_Board[middlePosition, middlePosition] = eNumSquare.WhiteCell;
            m_Board[middlePosition - 1, middlePosition - 1] = eNumSquare.WhiteCell;
            m_Board[middlePosition - 1, middlePosition] = eNumSquare.BlackCell;
            m_Board[middlePosition, middlePosition - 1] = eNumSquare.BlackCell;
        }

        private void updateOrCheckDirctionPathDueToMove(int i_CordX, int i_CordY, int i_DirctionX, int i_DirctionY, eNumSquare i_SoldierType, ref bool io_NeededToUpdateSquare, bool i_PaintTheSquare, bool i_IsFirstOcurrance)
        {
            if(IsSquareExceedBoard(i_CordX, i_CordY) || m_Board[i_CordX, i_CordY] == eNumSquare.EmptyCell)
            {
                io_NeededToUpdateSquare = false;
            }
            else if(m_Board[i_CordX, i_CordY] == i_SoldierType)
            {
                if (!i_IsFirstOcurrance)
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
                i_IsFirstOcurrance = false;
                updateOrCheckDirctionPathDueToMove(i_CordX + i_DirctionX, i_CordY + i_DirctionY, i_DirctionX, i_DirctionY, i_SoldierType, ref io_NeededToUpdateSquare, i_PaintTheSquare, i_IsFirstOcurrance);
                if(io_NeededToUpdateSquare && i_PaintTheSquare)
                {
                    m_Board[i_CordX, i_CordY] = i_SoldierType;
                }
            }
        } 
    }
}
