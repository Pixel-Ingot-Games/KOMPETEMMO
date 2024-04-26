using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayCoin : MonoBehaviour
{
    public int amount;
    public bool paid;
    public void CallPay()
    {
        StartCoroutine(Pay());
    }
    IEnumerator Pay()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("coin", amount.ToString());
        WWW www = new WWW("https://havenverse.world/Middleware/usecoin.php", form);
        yield return www;
        if (www.text[0] == '1')
        {
            Debug.Log("Coin paid");
            paid = true;
        }
        else
        {
            Debug.Log("Coin Check Failed");
        }
    }
}
