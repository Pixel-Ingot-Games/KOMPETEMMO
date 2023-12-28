using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Gaia;
public class PlayerInstantiate : MonoBehaviourPunCallbacks
{
    public Transform SpawnPosition;
    public GameObject PlayerPrefab;
    public CameraController target;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
      temp=  PhotonNetwork.Instantiate(PlayerPrefab.name, SpawnPosition.position, Quaternion.identity);
        StartCoroutine(setTarget());
    }
    IEnumerator setTarget()
    {
        yield return new WaitForSeconds(2f);
        target.target = temp;
    }
}
