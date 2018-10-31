using System;
using System.Collections.Generic;
using System.Linq;

namespace Minefield
{
    internal class MineSpot
    {
        public int x;
        public int y;

        public MineSpot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class MinefieldSession
    {      
        private int line;
        private int column;
        private int bombs;
        private int[,] board;

        private const int BOMB_ID = -1;
        private const int EMPTY_ID = 0;

        public MinefieldSession(int line, int column, int bombs)
        {
            this.line = line;
            this.column = column;
            this.bombs = bombs;

            board = new int[line, column];

            CreateField();
			InsertBombs();
            InsertNumbers();

            LogPrinter.LogList(board, line);
        }

        private void CreateField()
        {
			for (var i = 0; i < line; i++)
				for (var j = 0; j < column; j++)
					board[i, j] = EMPTY_ID;
        }

        private void InsertBombs()
        {
			var possibilities = new List<MineSpot>();
			for (var i = 0; i < line; i++)
				for (var j = 0; j < column; j++)
					possibilities.Add(new MineSpot(i, j));
			
            Random rnd = new Random();         
            for (var i = 0; i < bombs; i++)
            {
				var r = rnd.Next(0, possibilities.Count - 1);            
				board[possibilities[r].x, possibilities[r].y] = BOMB_ID;
				possibilities.RemoveAt(r);
            }
        }

        private void InsertNumbers()
        {
            for (var i = 0; i < line; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    if (board[i, j] == BOMB_ID) continue;

                    int amount = 0;
                    bool isBomb = false;
                    if (CheckInBoundsBomb(i - 1, j - 1, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i, j - 1, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i + 1, j - 1, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i - 1, j, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i + 1, j, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i - 1, j + 1, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i, j + 1, out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i + 1, j + 1, out isBomb) && isBomb)
                        amount++;

                    board[i, j] = amount;
                }
            }
        }

        private bool CheckInBoundsBomb(int i, int j, out bool bomb)
        {
            bomb = false;
            if (i <= 0 || i >= line || j <= 0 || j >= column) return false;
            bomb = board[i, j] == BOMB_ID;
            return true;
        }
    }
}