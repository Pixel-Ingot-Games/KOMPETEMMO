using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlaneInstance : MonoBehaviourPunCallbacks
{
    public Transform pos1, pos2;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(prefab.name, pos1.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
