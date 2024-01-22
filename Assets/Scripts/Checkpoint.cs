using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int number;
    public GameObject[] next;
    public AudioSource aus;
    public bool finishcheckpoint;
    public bool passed;
    public RaceManager RM;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          //  aus.PlayOneShot();
            next[0].SetActive(true);
            next[1].SetActive(true);
            passed = true;
            Debug.Log("checkpoint entered/");
            if (finishcheckpoint)
            {
                RM.currentLap += 1;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(turnoff());
        }
    }
    IEnumerator turnoff()
    {

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
