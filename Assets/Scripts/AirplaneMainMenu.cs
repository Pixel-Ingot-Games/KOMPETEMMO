using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class AirplaneMainMenu : MonoBehaviourPunCallbacks
{
    TypedLobby Jetracemode = new TypedLobby("jetmode", LobbyType.Default);
    public GameObject[] Jets;
    int currentjet;
    public CoinCheck cc;
    public PayCoin pc;
    bool paid=true;
    public GameObject LessBalance;
    public CheckNft cn;
    public GameObject lockImg;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting To Photon Servers");
        }
        
    }
    private void Update()
    {
        if (pc.paid && paid )
        {
            StartRace();
            paid = false;
        }
    }
    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby(Jetracemode);
        }

        Debug.Log("Connected To Master Server");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected To Lobby Server");
    }
    public void Play()
    {
        if (cc.coins>=10)
        {
            pc.amount = 10;
            pc.CallPay();  
        }
        else
        {
            LessBalance.SetActive(true);
        }
    }
    public void StartRace()
    {
        PlayerPrefs.SetInt("PlayerJet", currentjet);
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Ptrack Island");
    }
    public void Next()
    {
        if (currentjet < Jets.Length - 1)
        {
            for (int i = 0; i < Jets.Length; i++)
        {
            Jets[i].SetActive(false);
        }
        
            currentjet +=1;
            Jets[currentjet].SetActive(true);
            if (cn.jetNFT[currentjet]>=1)
            {
                lockImg.SetActive(false);     
            }
            else
            {
                lockImg.SetActive(true);
            }
            
        }
    }
    public void Previous()
    {
        if (currentjet >0)
        {
            for (int i = Jets.Length-1; i >-1; i--)
            {
                Jets[i].SetActive(false);
            }

            currentjet -= 1;
            Jets[currentjet].SetActive(true);
            if (cn.jetNFT[currentjet] >= 1)
            {
                lockImg.SetActive(false);      
            }
            else
            {
                lockImg.SetActive(true);
            }
        }
    }
    public void SelectJet()
    {
        if (cn.jetNFT[currentjet] >= 1)
        {
            PlayerPrefs.SetString("PlayerJet", Jets[currentjet].name);
        }
        
    }
    public void Leave()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            SceneManager.LoadScene("LoadingGameplay");
        }
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("LoadingGameplay");
        Debug.Log("Loading Race Menu");
    }
}
