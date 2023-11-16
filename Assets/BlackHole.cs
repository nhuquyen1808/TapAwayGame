using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Transform destination;
    RaycastHit2D[] hit;

    private float rotz;
    public float speed;
    public GameObject imageBlackHole;

    public static BlackHole instance;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        rotz += speed * Time.deltaTime;
        imageBlackHole.transform.rotation = Quaternion.Euler(0, 0, rotz);
    }

    public Transform GetDestination()
    {
        return destination;
    }

}
