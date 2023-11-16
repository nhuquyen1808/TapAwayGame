using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR;

public class Tutorial : MonoBehaviour
{
    //public RectTransform rect;
    public Image hand;
    public Image handPr;
    public static Tutorial ins;


    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        hand.gameObject.SetActive(false);
    }

    void Update()
    {

    }
    public void HintManagement()
    {
        StartCoroutine(handHint());

    }
    IEnumerator handHint()
    {
        yield return new WaitForSeconds(1.25f);
        hand.gameObject.SetActive(true);
        hand.DOFade(1f, 0.3f) .From(0);
        ScaleHand();
    }
    void ScaleHand()
    {
        hand.transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            hand.transform.DOScale(1.5f, 0.5f).OnComplete(() =>
            {
                ScaleHand();
            });
        });
    }
    
    public void HandMovement(Vector3 position)
    {

        handPr.rectTransform.DOMove(position, 0.3f);
    }


}
