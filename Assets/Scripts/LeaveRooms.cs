using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class LeaveRooms : MonoBehaviourPunCallbacks
{
    public string toLoad;
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        Time.timeScale = 1;
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(toLoad);
    }
}
