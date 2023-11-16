using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    public static CameraControl ins;
    public Camera cam;
    public bool isCheck;
    public bool m_isGameOver;
    public GameObject bomb;
    public Transform selectingObj;
    float timer;
    RaycastHit2D hit;
    Square square;
    Tween scaledown;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {

        cam = Camera.main;

    }
    private void Update()
    {

        if (m_isGameOver)
            return;
        Click();
        checkDragMouse();
    }

    public bool checkTutorial;
    void Click()
    {

        if (Input.GetMouseButtonDown(0) && isCheck == false)
        {
            Vector2 ray = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(ray, Vector2.zero);
            if (hit.collider != null)
            {
                Vector2 obj = hit.collider.transform.position;
                if (GameController.instance.checkBomb == true)
                {
   
                    if (hit.collider.tag == "Square")
                    {
                        Square squarePref = hit.transform.GetComponent<Square>();
                        squarePref.canClick = false;
                        if (ManageSquare.ins.indexLevel == 3)
                        {
                            if (squarePref == ManageSquare.ins.squares[0])
                            {
                                ManageSquare.ins.CheckSquareCanClick();
                                // ManageSquare.ins.squares[0].canClick = false;
                            }
/*                            else
                            {
                                return;
                            }*/

                        }
                       isCheck = true;
                        squarePref.transform.DOScale(1, 0.5f);
                        squarePref.checkClicked = false;
                        GameObject bom = Instantiate(bomb, hit.transform.position, Quaternion.identity);
                        bom.transform.DOScale(1.5f, 0.7f).OnComplete(()=>
                        {
                            squarePref.particleSystemBlock.gameObject.SetActive(true);
                            GameObject explosion = Instantiate(squarePref.particleSystemBlock, transform.position, Quaternion.identity);
                            Destroy(explosion, 2);
                        });
                        Destroy(squarePref.gameObject, 0.8f);
                        StartCoroutine(bombEffect());
                        Destroy(bom, 0.75f);
                        ManageSquare.ins.squares.Remove(squarePref);
                        ManageSquare.ins.dem--;
                        ManageSquare.ins.CheckAllSquare();
                        GameController.instance.checkBomb = false;
                        
                        UiManager.ins.bombBtnUsed();
                        GameController.instance.bombQuantity -= 1;
                        PlayerPrefs.SetInt("Bomb", GameController.instance.bombQuantity);
                        StartCoroutine(bombEffect());
                        Tutorial.ins.handPr.gameObject.SetActive(false);
                        squarePref.canClick = true;

                    }
                    isCheck = false;
                    GameController.instance.checkBtnBombUse = false;
                    return;
                }

                if (hit.collider.tag == "Square")
                {
                    Square squarePref = hit.transform.GetComponent<Square>();
                    scaledown = squarePref.transform.DOScale(0.85f, 0.5f);
                    selectingObj = squarePref.transform;
                    //isCheck = true;
                    if (ManageSquare.ins.indexLevel == 2)
                    {
                        if (squarePref == ManageSquare.ins.squares[0])
                        {
                            ManageSquare.ins.CheckSquareCanClick();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (hit.collider.tag == "Gear")
                {
                    Debug.Log("Hit Gear");
                }
            }

        }

        if (Input.GetMouseButtonUp(0) && isCheck == false)
        {
            Vector2 ray = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(ray, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Square")
                {
                    Square squarePref = hit.transform.GetComponent<Square>();

                    if (squarePref.canClick)
                    {
                        squarePref.transform.DOScale(1, 0.5f);
                        isCheck = true;
                        squarePref.checkClicked = true;
                        squarePref.checkChangeColor = true;
                        squarePref.CheckSquareClicked();
                        ManageSquare.ins.numberOfTap--;
                        Tutorial.ins.hand.gameObject.SetActive(false);
                        ManageSquare.ins.ChangeColorOfNumberOfTap();
                        ManageSquare.ins.CheckAllSquare();
                        Debug.Log("3");

                        ManageSquare.ins.UseTutorial();
                        ManageSquare.ins.CheckSquareCanClick();
                        UiManager.ins.SetMovesText(ManageSquare.ins.numberOfTap.ToString() + " moves");
                        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.click);
                        if (ManageSquare.ins.dem > 0 && ManageSquare.ins.numberOfTap <= 0)
                        {
                            UiManager.ins.ShowGameOverPanel();
                            m_isGameOver = true;
                            StartCoroutine(DelayLoseSoundFX());
                        }
                        Debug.Log("2");
                    }

                }
            }

        }
    }
    IEnumerator DelayLoseSoundFX()
    {
        yield return new WaitForSeconds(0.7f);
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.lose);
    }

    IEnumerator bombEffect()
    {
        yield return new WaitForSeconds(0.7f);
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.bombUsed);
    }
    public void ChangeSizeCamera()
    {
        if (ManageSquare.ins.indexLevel == 1)
        {
            cam.orthographicSize = 11;
        }
        if (ManageSquare.ins.indexLevel == 2)
        {                                                     
            cam.orthographicSize = 12;
        }
        if (ManageSquare.ins.indexLevel == 3)
        {
            cam.orthographicSize = 12;
        }
        if (ManageSquare.ins.indexLevel == 4)
        {
            cam.orthographicSize = 12;
        }
        if (ManageSquare.ins.indexLevel == 5)
        {
            cam.orthographicSize = 11;
        }
    }
    void checkDragMouse()
    {
        if (selectingObj == null)
        {
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            scaledown.Kill();
            selectingObj.transform.localScale = Vector3.one;
            selectingObj = null;
            return;
        }

        else
        {
            // Debug.Log(Vector2.Distance(cam.WorldToScreenPoint(selectingObj.transform.position), Input.mousePosition));
            if (Vector2.Distance(cam.WorldToScreenPoint(selectingObj.transform.position), Input.mousePosition) > 300)
            {
                scaledown.Kill();
                selectingObj.transform.localScale = Vector3.one;
                selectingObj = null;
            }
        }

    }

    
}
