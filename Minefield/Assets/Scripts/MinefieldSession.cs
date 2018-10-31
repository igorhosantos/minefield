using System;
using System.Collections.Generic;

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
		private const int BOMB_ID = -1;
        private const int EMPTY_ID = 0;

        private int mLine;
		private int mColumn;
		private int mBombs;
		private int[,] mBoard;

		private IGameView mGameView;

		public MinefieldSession(int line, int column, int bombs, IGameView gameView)
        {
			mGameView = gameView;
            mLine = line;
            mColumn = column;
            mBombs = bombs;

            mBoard = new int[line, column];

            CreateField();
			InsertBombs();
            InsertNumbers();

			mGameView.OnBoardCreation(mBoard);

            LogPrinter.LogList(mBoard, line);
        }

        private void CreateField()
        {
			for (var i = 0; i < mLine; i++)
				for (var j = 0; j < mColumn; j++)
					mBoard[i, j] = EMPTY_ID;
        }

        private void InsertBombs()
        {
			var possibilities = new List<MineSpot>();
			for (var i = 0; i < mLine; i++)
				for (var j = 0; j < mColumn; j++)
					possibilities.Add(new MineSpot(i, j));
			
            Random rnd = new Random();         
            for (var i = 0; i < mBombs; i++)
            {
				var r = rnd.Next(0, possibilities.Count - 1);            
				mBoard[possibilities[r].x, possibilities[r].y] = BOMB_ID;
				possibilities.RemoveAt(r);
            }
        }

        private void InsertNumbers()
        {
            for (var i = 0; i < mLine; i++)
            {
                for (var j = 0; j < mColumn; j++)
                {
                    if (mBoard[i, j] == BOMB_ID) continue;

                    int amount = 0;
                    bool isBomb = false;
                    if (CheckInBoundsBomb(i - 1, j - 1  , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i,     j - 1  , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i + 1, j - 1  , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i - 1, j      , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i + 1, j      , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i - 1, j + 1  , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i,     j + 1  , out isBomb) && isBomb)
                        amount++;
                    if (CheckInBoundsBomb(i + 1, j + 1  , out isBomb) && isBomb)
                        amount++;

                    mBoard[i, j] = amount;
                }
            }
        }

        private bool CheckInBoundsBomb(int i, int j, out bool bomb)
        {
            bomb = false;
            if (i < 0 || i >= mLine || j < 0 || j >= mColumn) return false;
            bomb = mBoard[i, j] == BOMB_ID;
            return true;
        }
    }
}