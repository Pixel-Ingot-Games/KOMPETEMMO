using System;
using Thirdweb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CheckNft : MonoBehaviour
{
    ThirdwebSDK sdk;
    Contract contract;
    public const string _contractAddress = "0x1935231Df5364247d033d77acF1d90514347b2E1";

    public int[] carNFT = new int[4];
    public int[] jetNFT = new int[4];
    public int[] characterNFT = new int[2];

    string tokenId;
    async void Start()
    {
        sdk = ThirdwebManager.Instance.SDK;
        contract = sdk.GetContract(_contractAddress);
        FetchAllNFTs();
    }

    async void FetchAllNFTs()
    {
        try
        {
            var res = await sdk.Wallet.GetAddress();

            // Fetch jetNFTs
            for (int i = 0; i < carNFT.Length; i++)
            {
                carNFT[i] = (int)await contract.ERC1155.BalanceOf(res, i.ToString());
                Debug.Log($"CarNFT[{i}] = {carNFT[i]}");
            }

            // Fetch jetNFTs
            for (int i = 0; i < jetNFT.Length; i++)
            {
                jetNFT[i] = (int)await contract.ERC1155.BalanceOf(res, (i + 4).ToString());
                Debug.Log($"jetNFT[{i}] = {jetNFT[i]}");
            }

            // Fetch characterNFTs
            for (int i = 0; i < characterNFT.Length; i++)
            {
                characterNFT[i] = (int)await contract.ERC1155.BalanceOf(res, (i + 8).ToString());
                Debug.Log($"characterNFT[{i}] = {characterNFT[i]}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error fetching NFTs: {e.Message}");
        }
    }

}
