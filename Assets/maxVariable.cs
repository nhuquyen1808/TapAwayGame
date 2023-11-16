using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class maxVariable : MonoBehaviour
{
   public List<int> list = new List<int>();
    void Start()
    {
        for (int i = 0; i < 20; i++) 
        {

        }
    }
     int temp;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {

                    if (list[i] > list[j])
                    {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
            }
            Debug.Log("variable : " + list[0]);
            Debug.Log("variable : " + list[1]);
            Debug.Log("variable : " + list[2]);
            Debug.Log("variable : " + list[3]);
            Debug.Log("variable : " + list[4]);
        }
    }
}
