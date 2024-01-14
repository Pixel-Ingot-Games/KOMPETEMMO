using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TagChanger : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    public string Tag;
    // Start is called before the first frame update
    void Start()
    {
        if (view.IsMine)
        {
            transform.tag = Tag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
