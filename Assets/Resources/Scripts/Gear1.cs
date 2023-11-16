using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear1 : MonoBehaviour
{
    private float rotz;
    [SerializeField] float speed;
    public GameObject imgSaw;
    public GameObject imgShadowSaw;

    void Update()
    {
        rotz += speed * Time.deltaTime;
        imgSaw.transform.rotation = Quaternion.Euler(0, 0, rotz);
        imgShadowSaw.transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
