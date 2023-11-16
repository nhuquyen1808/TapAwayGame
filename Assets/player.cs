using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject currentTeleport;
    RaycastHit2D[] hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 5 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hit = Physics2D.RaycastAll(transform.position, Vector2.right, 10);
            if (hit[1].collider != null)
            {
                if (hit[1].collider.tag == "Square")
                {
                    transform.DOMove(hit[1].point, 1);

                    transform.DOScale(0, 1).From(1);


                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleport = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleport)
            {
                currentTeleport = null;
            }
        }
    }
}
