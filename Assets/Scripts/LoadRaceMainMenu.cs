using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LoadRaceMainMenu : MonoBehaviourPunCallbacks
{
    public GameObject Panel,fpsPanel,carPanel,jetPanel;
    public string ToLoad;
    public bool fps, car, jet;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Panel.SetActive(true);
        if (fps)
        {
            fpsPanel.SetActive(true);
        }
        else if (car)
        {
            carPanel.SetActive(true);
        }
        else if (jet)
        {
            jetPanel.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
   public void Leave()
    {
        Time.timeScale = 1;
      //  Panel.SetActive(false);
        PlayerPrefs.SetString("scenetoload", ToLoad);
        PhotonNetwork.LeaveRoom();
        
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Loading");
        Debug.Log("Loading Race Menu");
    }

}
