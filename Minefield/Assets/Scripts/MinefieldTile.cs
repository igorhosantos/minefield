using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinefieldTile : MonoBehaviour, IPointerClickHandler
{
	public int value;
	public bool done;  

	private Image mImage;
	private bool mFlag; 

	private int mI;
	private int mJ;

	private void Awake()
	{
		mImage = gameObject.GetComponent<Image>();
		mImage.sprite = GameAssets.ME.defaultSprite;
	}

    public void Initiate(int i, int j, int value)
	{
		mI = i;
		mJ = j;

		this.value = value;
		mImage.sprite = GameAssets.ME.defaultSprite;
	}   

	public void OnPointerClick(PointerEventData eventData)
    {
		if (eventData.button == PointerEventData.InputButton.Left)
			Click();
		else if (eventData.button == PointerEventData.InputButton.Right)
			SetFlag();
    }

    private void Click()
	{
		if(value == 0)
			MinefieldView.ME.Propagate(mI, mJ);
		ChangeSprite();
	}

    public void ChangeSprite()
	{
		if (value == -1)
			mImage.sprite = GameAssets.ME.bombSprite;
		else
			mImage.sprite = GameAssets.ME.numbersSprite[value];

		done = true;
	}

    private void SetFlag()
	{
		if (done) return;

		mFlag = !mFlag;
		mImage.sprite = mFlag ? GameAssets.ME.flagSprite : GameAssets.ME.defaultSprite;
	}
}
