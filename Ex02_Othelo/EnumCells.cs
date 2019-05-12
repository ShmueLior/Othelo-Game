namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public enum EnumSquare
    {
        WhiteCell = 'O',
        BlackCell = 'X',
        EmptyCell = ' ',
    }

    public enum EnumGameType
    {
        PlayerVSCpu = 'C',
        PlayerVSPlayer = 'P',
    }
}
