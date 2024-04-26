using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class SceneLoadingManager : MonoBehaviourPunCallbacks
{
    public string SceneToLoad;
    public int LastPos;
    void Start()
    {
        StartCoroutine(waitforload());
    }

    IEnumerator waitforload()
    {
        yield return new WaitForSeconds(2f);
        SceneToLoad = PlayerPrefs.GetString("scenetoload");
        SceneManager.LoadSceneAsync(SceneToLoad);
       
    }
   
    
}
