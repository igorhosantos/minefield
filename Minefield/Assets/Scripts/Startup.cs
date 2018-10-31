using Minefield;
using UnityEngine;

public class Startup : MonoBehaviour
{
	public MinefieldView minefieldView;
    private MinefieldSession session;

    void Awake()
    {
		session = new MinefieldSession(15,15,25, minefieldView);
    }
}
