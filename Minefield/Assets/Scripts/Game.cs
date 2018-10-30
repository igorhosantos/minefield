using Minefield;
using UnityEngine;

public class Game : MonoBehaviour
{
    private MinefieldSession session;

    void Awake()
    {
        session = new MinefieldSession(10,10,25);
    }
}
