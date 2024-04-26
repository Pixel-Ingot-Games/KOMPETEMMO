using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinCheck : MonoBehaviour
{
    public int coins;
    public TMP_Text CoinsText;
    public void Start()
    {
        CallCheck();
    }
    public void CallCheck()
    {
        StartCoroutine(Check());
    }
    IEnumerator Check()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        WWW www = new WWW("https://havenverse.world/Middleware/coincheck.php", form);
        yield return www;
        if (www.text[0] == '1')
        {
            string[] responseParts = www.text.Split(' ');
            coins = int.Parse(responseParts[1]);
            if (CoinsText)
            {
                CoinsText.text = coins.ToString();
            }   
        } 
        else
        {
            Debug.Log("Coin Check Failed");
        }
    }
}
