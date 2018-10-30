using System;
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

            CreateField(bombs);
            
            InsertNumbers();

            LogPrinter.LogList(board, line);
        }

        private void CreateField(int bombs)
        {
            Random rnd = new System.Random();
            var currentBombs = 0;
            for (var i = 0; i < line; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    if ((rnd.NextDouble() < ((double) line * column) / bombs) && currentBombs < bombs)
                    {
                        currentBombs++;
                        board[i, j] = BOMB_ID;
                    }
                    else
                        board[i, j] = EMPTY_ID;
                }
            }
        }

        private void InsertBombs(int bombs)
        {
            System.Random rnd = new System.Random();

            var lines = Enumerable.Range(0, line).ToList();
            var columns = Enumerable.Range(0, column).ToList();

            for (var i = 0; i < bombs; i++)
            {
                var currentLine = rnd.Next(0, lines.Count - 1);
                var currentCollunm = rnd.Next(0, columns.Count - 1);

                board[lines[currentLine], columns[currentCollunm]] = BOMB_ID;

                lines.RemoveAt(currentLine);
                columns.RemoveAt(currentCollunm);
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

        private void InsertNumber(int bL, int bC)
        {
            if (bL - 1 < line && bL - 1 > 0 && bC < column && bC > 0) board[bL - 1, bC] += 1;
            if (bL - 1 < line && bL - 1 > 0 && bC - 1 < column && bC - 1 > 0) board[bL - 1, bC - 1] += 1;
            if (bL < line && bL > 0 && bC - 1 < column && bC - 1 > 0) board[bL, bC - 1] += 1;

            if (bL + 1 < line && bL + 1 > 0 && bC < column && bC > 0) board[bL + 1, bC] += 1;
            if (bL + 1 < line && bL + 1 > 0 && bC + 1 < column && bC + 1 > 0) board[bL + 1, bC + 1] += 1;
            if (bL < line && bL > 0 && bC + 1 < column && bC + 1 > 0) board[bL, bC + 1] += 1;
        }
    }
}