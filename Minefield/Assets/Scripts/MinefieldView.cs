using UnityEngine;
using UnityEngine.UI;

public class MinefieldView : MonoBehaviour, IGameView
{
	public const int BOMB_ID = -1;

	public static MinefieldView ME;
	public GridLayoutGroup grid;

	private MinefieldTile[,] mBoard;

	private void Awake()
	{
		ME = this;
	}

	public void OnBoardCreation(int[,] board)
	{      
		grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		grid.constraintCount = board.GetLength(0);

		mBoard = new MinefieldTile[board.GetLength(0), board.GetLength(1)];
		for (var i = 0; i < board.GetLength(0); i++)
		{
			for (var j = 0; j < board.GetLength(1); j++)
			{
				mBoard[i, j] = Instantiate(GameAssets.ME.tile, transform).GetComponent<MinefieldTile>();
				mBoard[i, j].Initiate(i, j, board[i, j]);
			}
		}
	}

	public void Propagate(int i, int j)
	{
		bool mBomb;
		if (!CheckInBoundsBomb(i, j, out mBomb))
			return;            
		else
			if (mBomb || mBoard[i, j].done) 
			    return;

		mBoard[i, j].ChangeSprite();

		if (mBoard[i, j].value > 0)
			return;

		Propagate(i - 1, j - 1);
		Propagate(i,     j - 1);
		Propagate(i + 1, j - 1);
		Propagate(i - 1, j    );
		Propagate(i + 1, j    );
		Propagate(i - 1, j + 1);
		Propagate(i,     j + 1);
		Propagate(i + 1, j + 1);
	}

	private bool CheckInBoundsBomb(int i, int j, out bool bomb)
    {
        bomb = false;
		if (i < 0 || i >= mBoard.GetLength(0) || j < 0 || j >= mBoard.GetLength(1)) return false;
		bomb = mBoard[i, j].value == BOMB_ID;
        return true;
    }
}
