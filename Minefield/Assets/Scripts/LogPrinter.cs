using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogPrinter
{

	public static void LogList(int[,] rawData, int breakLine)
	{
	    string str = "";
	    int countBreak = 0;

	    for (int i = 0; i < rawData.GetLength(0); i++)
	    {
	        for (int j = 0; j < rawData.GetLength(1); j++)
	        {
	            countBreak++;

                if (countBreak == breakLine)
	            {
	                str += rawData[i, j];
                    str += '\n';
	                countBreak = 0;
	            }
	            else
	            {
	                str += rawData[i,j] + ",";
	            }
            }
        }

        Debug.Log(str);

	}
	
}
