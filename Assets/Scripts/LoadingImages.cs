using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingImages : MonoBehaviour
{
    public Image Img;
    public Sprite[] images;
    void Start()
    {
        StartCoroutine(ChangeImage());
    }

   IEnumerator ChangeImage()
    {
        ChooseRandom();
        yield return new WaitForSeconds(3f);
        StartCoroutine(ChangeImage());
    }
    void ChooseRandom()
    {
        Img.sprite = images[Random.Range(0, images.Length - 1)];
    }
}
