using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinefieldSession : MonoBehaviour
{
    
    private int line;
    private int collunm;
    private int bombs;
    private int[,] board;

    private const int BOMB_ID = -1;
    private const int EMPTY_ID = 0;

    public MinefieldSession(int line, int collunm, int bombs)
    {
        this.line = line;
        this.collunm = collunm;
        this.bombs = bombs;
        board = new int[line,collunm];
        
        InsertEmpty();
        InsertBombs(bombs);
        InsertNumbers();

        LogPrinter.LogList(board,line);
    }

    private void InsertEmpty()
    {
        for (int i = 0; i < board.GetLength(0); i++)
            for (int j = 0; j < board.GetLength(1); j++)
               board[i, j] = EMPTY_ID;
    }

    private void InsertBombs(int bombs)
    {
        int countBombs = 0;
        System.Random rnd = new System.Random();

        while (countBombs < bombs)
        {
            int currentLine = rnd.Next(0, line);
            int currentCollunm = rnd.Next(0, collunm);

            if (board[currentLine, currentCollunm] != BOMB_ID)
            {
                board[currentLine, currentCollunm] = BOMB_ID;
                countBombs++;
            }
        }
    }

    private void InsertNumbers()
    {
        for (int i = 0; i < board.GetLength(0); i++)
         for (int j = 0; j < board.GetLength(1); j++)
             if (board[i, j] == BOMB_ID) InsertNumber(i, j);
    }


    private void InsertNumber(int bL, int bC)
    {
        if (bL-1 < line && bL-1 > 0 && bC < collunm && bC > 0)  board[bL - 1, bC] += 1;
        if (bL-1 < line && bL-1 > 0 && bC-1 < collunm && bC-1 > 0)  board[bL - 1, bC - 1] += 1;
        if (bL < line && bL > 0 && bC-1 < collunm && bC-1 > 0)  board[bL, bC - 1] += 1;


        if (bL+1 < line && bL+1 > 0 && bC < collunm && bC > 0) board[bL + 1, bC] += 1;
        if (bL+1 < line && bL+1 > 0 && bC+1 < collunm && bC+1 > 0) board[bL + 1, bC + 1] += 1;
        if (bL < line && bL > 0 && bC + 1 < collunm && bC + 1 > 0) board[bL, bC + 1] += 1;

    }
}
