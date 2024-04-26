using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceRatingPlayerUI :MonoBehaviour
{
	[SerializeField] TextMeshProUGUI PlayerNameText;
	[SerializeField] TextMeshProUGUI PositionText;
	[SerializeField] TextMeshProUGUI TimeText;

	public RectTransform Rect { get; private set; }
	public bool first;
	public RewardCoin rc;
	void Awake ()
	{
		Rect = transform as RectTransform;
        
	}

	public void UpdateData (string playerName, int position, string time = "")
	{
		PlayerNameText.text = playerName;
		PositionText.text = position.ToString();
		if (TimeText != null)
		{
			TimeText.text = time;
		}
        if (first)
        {
            if (playerName==PlayerPrefs.GetString("username"))
            {
				Debug.Log("ending log");
				rc.amount = 30;
				rc.CallAddCoin();
			}
			
		}
		
	}
}
