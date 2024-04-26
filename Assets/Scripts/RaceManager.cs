using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Aeroplane;
using UnityEngine.SceneManagement;
public class RaceManager : MonoBehaviourPunCallbacks
{
    public GameObject[] Checkpoints;
    public int Laps;
    public TMP_Text FinishLineText;
    public int currentLap;
    public GameObject FinishPanel;
    public GameObject[] JetPrefabs;
    public GameObject Player;
    int PlayerJet;
    public GameObject[] OtherPlayers;
    public TMP_Text LapsTest;
    public GameObject WaitingForPlayers;
    public GameObject Timer;
    public int MaxPlayers;
    public Transform[] InstanPos;
    bool wait;
    PhotonView photonView;
    public RewardCoin rc;
    void Start()
    {  
        photonView = PhotonView.Get(this);
        PlayerJet = PlayerPrefs.GetInt("PlayerJet");
        GameObject Pl = PhotonNetwork.Instantiate(JetPrefabs[PlayerJet].name, InstanPos[PhotonNetwork.LocalPlayer.ActorNumber].position, InstanPos[PhotonNetwork.LocalPlayer.ActorNumber].rotation);
        Player = GameObject.FindGameObjectWithTag("Player");
        OtherPlayers = GameObject.FindGameObjectsWithTag("Jet");
        LapCompleted();
        currentLap = 1;
       
        if (PhotonNetwork.CurrentRoom.PlayerCount < MaxPlayers)
        {
            //WaitingForPlayers.SetActive(true);
           // wait = true;
        }
        
    }
    void Update()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            //OtherPlayers = GameObject.FindGameObjectsWithTag("Jet");
        }
        if (wait)
        {
            Player.GetComponent<AeroplaneUserControl2Axis>().enabled = false;
        }
        LapsTest.text = "LAP: " + currentLap + "/" + (Laps - 1);
        if (Checkpoints[Checkpoints.Length-1].GetComponent<Checkpoint>().passed)
        {
            Debug.Log("Lap finished");
            if (currentLap<Laps)
            {
                FinishLineText.text = "LAP";
                LapCompleted();
            }
            else
            {
                FinishPanel.SetActive(true);
                Time.timeScale = 0;
              //  Player.GetComponent<AeroplaneUserControl2Axis>().enabled = false;
            }
        }
        if (currentLap == Laps - 1)
        {
            FinishLineText.text = "FINISH";

        }
       
    }
    public void LapCompleted()
    {
        for (int i = 0; i < Checkpoints.Length; i++)
        {
            Checkpoints[i].SetActive(false);
            Checkpoints[i].GetComponent<Checkpoint>().passed = false;
        }
        Checkpoints[0].SetActive(true);
        Checkpoints[1].SetActive(true);
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadSceneAsync("Airportmenu");
    }
}
