using UnityEngine;

public class GameAssets : MonoBehaviour
{
	public static GameAssets ME;

	public GameObject tile;
	public Sprite[] numbersSprite;
	public Sprite defaultSprite;
	public Sprite flagSprite;
	public Sprite bombSprite;

	public Sprite defaultSmile;
	public Sprite deadSmile;

	private void Awake()
	{
		ME = this;
	}
}
