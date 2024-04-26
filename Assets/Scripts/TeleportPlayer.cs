using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    public GameObject player;
    public void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void Teleport(GameObject pos)
    {
        player.transform.position = pos.transform.position;
    }
}
