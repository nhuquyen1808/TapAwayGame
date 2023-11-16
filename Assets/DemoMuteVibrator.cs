using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMuteVibrator : MonoBehaviour
{
    public bool check;
    public static DemoMuteVibrator ins;

    private void Awake()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Btnvibrator()
    {
        if (!check)
        {
            Debug.Log("B");
            check = true;
        }
        else
        {
            Debug.Log("C");
            check = false;
        }
    }
}
