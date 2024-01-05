using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadRaceMainMenu : MonoBehaviourPunCallbacks
{
    public GameObject Panel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Panel.SetActive(true);
        }
    }
   public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.LogError("Left room");
        PhotonNetwork.LoadLevel("MainMenuScene");
        Debug.LogError("Left room");
    }

}
