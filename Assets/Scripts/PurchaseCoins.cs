using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System.Numerics;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Thirdweb;
using Nethereum.ABI.FunctionEncoding.Attributes;
public class PurchaseCoins : MonoBehaviour
{
    ThirdwebSDK sdk;
    Contract contract;
    public TMP_Text _status;
    public TMP_InputField amount;
    public const string _contractAddress = "0x49B90Afd282E98587b5Af6F39a76E1693a2BAAFe";
    string abi = "[{\"type\":\"constructor\",\"name\":\"\",\"inputs\":[],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"event\",\"name\":\"TokensReceived\",\"inputs\":[{\"type\":\"address\",\"name\":\"sender\",\"indexed\":false,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"amount\",\"indexed\":false,\"internalType\":\"uint256\"},{\"type\":\"string\",\"name\":\"message\",\"indexed\":false,\"internalType\":\"string\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"balance\",\"inputs\":[],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"depositTokens\",\"inputs\":[{\"type\":\"uint256\",\"name\":\"amount\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"owner\",\"inputs\":[],\"outputs\":[{\"type\":\"address\",\"name\":\"\",\"internalType\":\"address\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"tokenAddress\",\"inputs\":[],\"outputs\":[{\"type\":\"address\",\"name\":\"\",\"internalType\":\"address\"}],\"stateMutability\":\"view\"}]";
    BigInteger amtpurchased;
    public CoinCheck cc;
    // Start is called before the first frame update
    void Start()
    {
        sdk = ThirdwebManager.Instance.SDK;
        contract = sdk.GetContract(_contractAddress, abi);
    }

    public async void ReceiveToken()
    {
        var res = await sdk.Wallet.GetAddress();
        _status.text = res.ToString();
        try
        {
            Transaction transaction = await contract.Prepare(
            functionName: "depositTokens",
            args: new object[] { long.Parse(amount.text) }
        );
            transaction.SetGasLimit("100000");
            try
            {


                var transactionResult = await transaction.SendAndWaitForTransactionResult(gasless: false);

                //var transactionResult = await contract.Write("depositTokens", 1000000000000000000);
                _status.text = transactionResult.ToString();
                var allEvent = await contract.GetEventLogs<TokensReceived>();

                var currentUserEvent = allEvent.Where(item => item.Event.Sender.ToString() == res).ToList();
                int lastItem = currentUserEvent.Count - 1;



                Debug.Log("AMOUNT RECEIVED: " + currentUserEvent[lastItem].Event.Amount);

                amtpurchased = currentUserEvent[lastItem].Event.Amount;
                StartCoroutine(AddCoin());
            }
            catch (System.Exception e)
            {
                Debug.Log("[Custom Call]" + e);
            }

        }
        catch (Exception e)
        {
            Debug.LogError("[Custom Call]Error " + e);
        }
    }
    public void CallAddCoin()
    {
        StartCoroutine(AddCoin());
    }
    IEnumerator AddCoin()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("coin", amtpurchased.ToString());
        WWW www = new WWW("https://havenverse.world/Middleware/purchasecoin.php", form);
        yield return www;
        if (www.text[0] == '1')
        {
            cc.CallCheck();
            Debug.Log("Coins Added sucessfully");          
        } 
        else
        {
            Debug.Log("Coins Addition failed");
           
        }
    }

}


[Event("TokensReceived")]
public class TokensReceived : IEventDTO
{
    [Parameter("address", "sender", 1)]
    public string Sender { get; set; }

    [Parameter("uint256", "amount", 2)]
    public BigInteger Amount { get; set; }

    [Parameter("string", "message", 3)]
    public string Message { get; set; }
}

