using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    
    private MinefieldSession session;

    void Awake()
    {
        session = new MinefieldSession(10,10,25);
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
