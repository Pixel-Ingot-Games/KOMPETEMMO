using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject AfterConnected,Loading;
    public GameObject BeforeConnected;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            BeforeConnected.SetActive(true);
            AfterConnected.SetActive(false);
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
        BeforeConnected.SetActive(false);
        AfterConnected.SetActive(true);
        Debug.Log("Connected To Lobby Server");
    }
    public void Play()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinedRoom()
    {
       // PhotonNetwork.LoadLevel("Game");
       // Loading.SetActive(true);
    }
}
