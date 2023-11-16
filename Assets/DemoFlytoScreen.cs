using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemoFlytoScreen : MonoBehaviour
{
    public GameObject square;
    public Vector2 originPos;
    public Vector2 squarePos;

    // Start is called before the first frame update
    void Start()
    {
        squarePos = square.transform.position;
        originPos = (Vector2)square.transform.position + Random.insideUnitCircle * 20;

        square.transform.position = originPos;
        square.transform.DOMove(squarePos, 2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
