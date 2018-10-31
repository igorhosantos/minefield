using Minefield;
using UnityEngine;

public class Startup : MonoBehaviour
{
	public static Startup ME;
	public MinefieldView minefieldView;
    private MinefieldSession session;

    void Awake()
    {
		ME = this;

		StartGame();      
    }

    public void StartGame()
	{
		session = new MinefieldSession(15, 15, 25, minefieldView);
	}
}
