using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Square : MonoBehaviour
{
    public bool checkClicked;
    public Vector2 direction;
    public Vector2 stopPoint;
    public Vector2 offset;
    public Vector2 posInfrontOfSaw;
    public int index;
    public LayerMask Layer;
    public Sprite squareMesh1, squareMesh2, squareMesh3, squareMesh4;
    public GameObject Circle;
    public SpriteRenderer rend;
    public List<GameObject> SquareHit;
    public int id;
    public bool canMove;

    public Vector2 startPos;
    public bool checkChangeColor;
    public bool canClick;
    public bool checkBlockPos;
    public GameObject particleSystemBlock;

    public GameObject trailsEffect;
    public GameObject currentTeleport;

    public List<GameObject> explosions = new List<GameObject>();
    public List<GameObject> trails = new List<GameObject>();
    Color colorMain;

    private void Awake()
    {
        //HideParticle();
    }
    void Start()
    {
        //startPos = transform.position;
        CheckDirection();
        id = transform.GetSiblingIndex();
        if (ManageSquare.ins.indexLevel == 2 || ManageSquare.ins.indexLevel == 3)
        {
            canClick = false;
            ManageSquare.ins.ChangeColorTutorial();
        }
        else
        {
            canClick = true;
        }

    }
    private void Update()
    {
    }
    void CheckDirection()
    {
        if (index == 1)
        {
            direction = transform.up;
            stopPoint = new Vector2(0, 1f);
            posInfrontOfSaw = new Vector2(0, 1.7f);
            rend.sprite = squareMesh1;
            offset = new Vector2(0, 0.82f);
            particleSystemBlock = explosions[0];
            trailsEffect = trails[0];

        }
        if (index == 2)
        {
            direction = -transform.up;
            stopPoint = new Vector2(0, -1f);
            posInfrontOfSaw = new Vector2(0, -1.7f);
            rend.sprite = squareMesh2;
            offset = new Vector2(0, -0.82f);
            particleSystemBlock = explosions[1];
            trailsEffect = trails[1];
        }
        if (index == 3)
        {
            direction = transform.right;
            stopPoint = new Vector2(1f, 0);
            posInfrontOfSaw = new Vector2(1.7f, 0);
            rend.sprite = squareMesh3;
            offset = new Vector2(0.82f, 0);
            particleSystemBlock = explosions[2];
            trailsEffect = trails[2];

        }
        if (index == 4)
        {
            direction = -transform.right;
            stopPoint = new Vector2(-1f, 0);
            posInfrontOfSaw = new Vector2(-1.7f, 0);
            rend.sprite = squareMesh4;
            offset = new Vector2(-0.82f, 0);
            particleSystemBlock = explosions[3];
            trailsEffect = trails[3];
        }
    }
    public void CheckSquareClicked()
    {
        if (checkClicked)
        {

            SquareMovement();
        }
    }
    public void SquareMovement()
    {
        RaycastHit2D[] squareHit = Physics2D.RaycastAll(transform.position, direction, 15, Layer);
        if (checkClicked)
        {
            if (squareHit.Length >= 2)
            {
                Vector2 distance = squareHit[1].collider.transform.position;
                Vector2 a = distance - direction;
                Vector2 v = a - offset;
                if (squareHit[1].collider.tag == "Square")
                {
                    if (squareHit[1].distance > 1)
                    {
                        transform.DOMove(v, 0.3f).OnComplete(() =>
                        {
                            if (UiMusicController.ins.checkVirator || UiMusicController.ins.saveStateVibrator == 1)
                            {
                                Handheld.Vibrate();
                            }
                            AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.collision);
                            //CameraControl.ins.isCheck = true;
                            ShakeAllTheSquareInFrontOfObject(direction, squareHit[1].distance);
                            ChangeColorOfSquare(squareHit[1].transform.GetComponent<Square>());
                        });
                    }
                    else
                    {
                        //CameraControl.ins.isCheck = true;
                        transform.DOScale(0.8f, 0.1f).OnComplete(() =>
                        {
                            transform.DOScale(1, 0.1f);
                        });
                        if (UiMusicController.ins.checkVirator || UiMusicController.ins.saveStateVibrator == 1)
                        {
                            Handheld.Vibrate();
                        }
                        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.collision);

                        ShakeAllTheSquareInFrontOfObject(direction, squareHit[1].distance);
                        ChangeColorOfSquare(squareHit[1].transform.GetComponent<Square>());
                    }
                }
                if (squareHit[1].collider.tag == "Gear")
                {
                    CameraControl.ins.isCheck = false;
                    transform.DOMove(v, 0.25f).OnComplete(() =>
                    {
                        destroySquareByGear();
                    });
                    ManageSquare.ins.dem--;

                }

                if (squareHit[1].collider.tag == "TelePorter")
                {
                    Vector2 b = (Vector2)squareHit[1].collider.transform.position - direction - offset;
                    canClick = false;
                    transform.DOMove(b, 0.5f)
                        .OnComplete(() =>
                        {
                            if (currentTeleport != null)
                            {
                                //Debug.Log(currentTeleport.GetComponent<BlackHole>().GetDestination().transform.name);
                                RaycastHit2D[] blackHoleHit = Physics2D.RaycastAll(currentTeleport.GetComponent<BlackHole>().GetDestination().position, direction, 10);
                                if (blackHoleHit.Length >= 2)
                                {
                                    if (blackHoleHit[1].collider != null)
                                    {
                                        if (blackHoleHit[1].collider.tag == "Square")
                                        {
                                            if (blackHoleHit[1].distance > 1)
                                            {
                                                transform.DOMove(squareHit[1].collider.transform.position, 0.07f);
                                                Vector2 c = (Vector2)currentTeleport.GetComponent<BlackHole>().GetDestination().position /*+ posBlackHole*/;

                                                transform.DOScale(0, 0.25f).OnComplete(() =>
                                                {
                                                    transform.position = c;
                                                    transform.DOScale(1, 0.25f).From(0);
                                                    Vector2 des = (Vector2)blackHoleHit[1].transform.position - offset - direction;
                                                    transform.DOMove(des, 0.3f);
                                                    canClick = true;
                                                    CameraControl.ins.isCheck = false;
                                                });
                                            }
                                            else
                                            {
                                                ChangeColorOfSquare(blackHoleHit[1].transform.GetComponent<Square>());
                                                canClick = true;
                                                CameraControl.ins.isCheck = false;

                                            }
                                        }
                                        if (blackHoleHit[1].collider.tag == "Gear")
                                        {
                                            Vector2 g = (Vector2)currentTeleport.GetComponent<BlackHole>().GetDestination().position;

                                            transform.DOMove(squareHit[1].collider.transform.position, 0.07f).OnComplete(()=>
                                            {
                                                transform.DOScale(0, 0.25f).OnComplete(() => 
                                                {
                                                    transform.position = g;
                                                    transform.DOScale(1, 0.25f).OnComplete(()=> 
                                                    {
                                                        Vector2 postoDestroy = (Vector2)blackHoleHit[1].collider.transform.position - posInfrontOfSaw;
                                                        transform.DOMove(postoDestroy, 0.5f).OnComplete(() =>
                                                        {
                                                            destroySquareByGear();
                                                            canClick = true;
                                                            CameraControl.ins.isCheck = false;
                                                        });
                                                        ManageSquare.ins.dem--;
                                                    });
                                                });
                                            });
                                        }
                                    }
                                }
                                else
                                {
              
                                    Vector2  f = (Vector2)currentTeleport.GetComponent<BlackHole>().GetDestination().position /*+ posBlackHole*/;
                                    transform.DOMove(squareHit[1].collider.transform.position, 0.07f).OnComplete(() =>
                                    {
                                        //rend.material.DOFade(0, 0.5f);
                                        transform.DOScale(0, 0.25f).From(1) .OnComplete(()=> 
                                        {
                                            transform.position = f;
                                           // rend.material.DOFade(1, 0.25f);
                                            transform.DOScale(1, 0.25f).OnComplete(()=> {
                                                MoveAndHideSquare();
                                                canClick = true;
                                                CameraControl.ins.isCheck = false;

                                            });
                                            
                                        });

                                   
                                    });
                                }
                            }  
                            
                        });
                  
                }
            }
            // Square move and destroy
            else
            {
                /*trailsEffect.gameObject.SetActive(true);
                CameraControl.ins.isCheck = false;
                Vector2 move = transform.position;
                GetComponent<BoxCollider2D>().enabled = false;
                rend.transform.DOMove(move + direction * 20, 1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    LevelManagement.ins.manageSuqare.squares.Remove(this);
                });
                rend.transform.DOScale(0.7f, 0.08f).OnComplete(() =>
                 {
                     rend.transform.DOScale(1f, 0.08f);
                 });
                ManageSquare.ins.dem--;
                rend.material.DOFade(0, 1f);*/
                MoveAndHideSquare();
            }
        }
        LevelManagement.ins.manageSuqare.CheckAllSquare();
    }

    public void MoveAndHideSquare()
    {
        trailsEffect.gameObject.SetActive(true);
        CameraControl.ins.isCheck = false;
        Vector2 move = transform.position;
        GetComponent<BoxCollider2D>().enabled = false;
        rend.transform.DOMove(move + direction * 15, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.SetActive(false);
            LevelManagement.ins.manageSuqare.squares.Remove(this);
        });
        rend.transform.DOScale(0.7f, 0.08f).OnComplete(() =>
        {
            rend.transform.DOScale(1f, 0.08f);
        });
        ManageSquare.ins.dem--;
        rend.material.DOFade(0, 1f);
        ManageSquare.ins.CheckAllSquare();
    }
    void destroySquareByGear()
    {
       particleSystemBlock.gameObject.SetActive(true);
        GameObject explosion = Instantiate(particleSystemBlock, transform.position, Quaternion.identity);
        Destroy(explosion, 1);
        gameObject.SetActive(false);
        LevelManagement.ins.manageSuqare.squares.Remove(this);
        AudioController.ins.aus.PlayOneShot(AudioController.ins.burst);
    }

    void ShakeAllTheSquareInFrontOfObject(Vector3 dir, float distance)
    {
        Vector2 shakeStrength = (Vector2)transform.position + (Vector2)dir * 0.3f;
        startPos = transform.position;
        RaycastHit2D[] hitToShake = Physics2D.RaycastAll(transform.position, dir, 1, Layer);
        for (int i = 0; i < hitToShake.Length; i++)
        {
            Square hitSquare = hitToShake[i].transform.GetComponent<Square>();
            if (hitSquare.id != this.id)
            {
                hitSquare.ShakeAllTheSquareInFrontOfObject(dir, Mathf.Infinity);
            }
        }
        if (hitToShake.Length >= 2)
        {
            if (distance < 1)
            {
                if (checkChangeColor)
                {
                    ChangeColorOfSquare(hitToShake[1].transform.GetComponent<Square>());
                    checkChangeColor = false;
                }
            }
        }
        transform.DOMove(shakeStrength, 0.07f)
        .OnComplete(() =>
        {
            transform.DOMove(startPos, 0.07f).SetEase(Ease.Linear).OnComplete(() =>
            {
                CameraControl.ins.isCheck = false;
            });
        });
    }

    void ChangeColorOfSquare(Square square)
    {
        square.rend.DOColor(new Color(1, 0, 0), 0.07f).OnComplete(() =>
  {
      square.rend.DOColor(new Color(1, 1, 1), 0.07f);
  });
    }

    public bool isRight()
    {
        RaycastHit2D[] squareHit = Physics2D.RaycastAll(transform.position, direction, 5, Layer);
        if (squareHit.Length >= 2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TelePorter"))
        {
            currentTeleport = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TelePorter"))
        {
            if (collision.gameObject == currentTeleport)
            {
                currentTeleport = null;

            }
        }
    }
}
