using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AirplaneMainMenu : MonoBehaviourPunCallbacks
{
    public GameObject[] Jets;
    int currentjet;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting To Photon Servers");
        }
        
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }

        Debug.Log("Connected To Master Server");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected To Lobby Server");
    }
    public void Play()
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
        }
    }
    public void SelectJet()
    {
        PlayerPrefs.SetString("PlayerJet", Jets[currentjet].name);
    }

}
