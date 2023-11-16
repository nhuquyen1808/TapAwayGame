using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{
    public GameObject hideSquare;
    // Start is called before the first frame update
    void Start()
    {
        hideSquare.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hideSquare.gameObject.SetActive(true);

        }
    }
}
