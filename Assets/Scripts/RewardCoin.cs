using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCoin : MonoBehaviour
{
    public int amount;
    public void CallAddCoin()
    {
        StartCoroutine(AddCoin());
    }
    IEnumerator AddCoin()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("coin", amount.ToString());
        WWW www = new WWW("https://havenverse.world/Middleware/purchasecoin.php", form);
        yield return www;
        if (www.text[0] == '1')
        {
            
            Debug.Log("Coins Added sucessfully");
        }
        else
        {
            Debug.Log("Coins Addition failed");

        }
    }
}
