using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CheckRayCast : MonoBehaviour
{
    int index;
    public GameObject blanKet;
    public bool check;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (check)
        {
            blanKet.gameObject.SetActive(false);
        }
        else { blanKet.gameObject.SetActive(true);}
    }
}
