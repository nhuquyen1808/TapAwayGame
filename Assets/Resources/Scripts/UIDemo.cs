using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIDemo : MonoBehaviour
{
    public GameObject img1, img2, img3, reward,texttap;

    private void Start()
    {
        img3.gameObject.SetActive(false);
        reward.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShowPanel());
        }
    }
    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(0.1f);
        //img1.gameObject.SetActive(true);
        img1.transform.DOScale(1, 2);
        yield return new WaitForSeconds(0.2f);
       // img2.gameObject.SetActive(true);
        img2.transform.DOScale(1, 2);
        yield return new WaitForSeconds(0.2f);
        reward.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        img3.gameObject.SetActive(true);
        //img3.transform.DOScale(1, 2);
        yield return new WaitForSeconds(0.6f);
        //img3.gameObject.SetActive(true);
        texttap.transform.DOScale(1, 2);
    }
}
