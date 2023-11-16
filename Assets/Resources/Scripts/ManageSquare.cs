using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Reflection;
using System.Collections;
using UnityEngine.XR;

public class ManageSquare : MonoBehaviour
{
    public int indexLevel;
    public int numberOfTap;
    public int dem;
    public static ManageSquare ins;
    public List<Square> squares = new List<Square>();
    List<Vector2> squaresPos = new List<Vector2>();

    private void Awake()
    {
        ins = this;

    }

    void Start()
    {
        CameraControl.ins.ChangeSizeCamera();

        ControlNumberOfTap();

        dem = squares.Count;
        UiManager.ins.SetMovesText(numberOfTap.ToString() + " moves");
        SetPositions();
        MoveOutScreen();
        MoveIntoScreen();

    }
    private void Update()
    {
        TutorialLv3();
    }

    public void ControlNumberOfTap()
    {
        if (indexLevel == 1)
        {
            UiManager.ins.movesText.gameObject.SetActive(false);
            numberOfTap = 6;
            GameController.instance.bombQuantity = 5;
            PlayerPrefs.SetInt("Bomb", GameController.instance.bombQuantity);
            PlayerPrefs.Save();
            GameController.instance.buyBomb.gameObject.SetActive(false);
            GameController.instance.buyTap.gameObject.SetActive(false);
            Vector3 pos = Camera.main.WorldToScreenPoint(squares[0].transform.position);
            Tutorial.ins.handPr.rectTransform.position = pos;
            Tutorial.ins.HintManagement();
            // squares[0].GetComponent<SpriteRenderer>().material.color = new Color(142,115,115);

        }
        if (indexLevel == 2)
        {
            UiManager.ins.movesText.gameObject.SetActive(false);
            numberOfTap = 6;
            PlayerPrefs.SetInt("Bomb", GameController.instance.bombQuantity);
            PlayerPrefs.Save();
            GameController.instance.buyBomb.gameObject.SetActive(false);
            GameController.instance.buyTap.gameObject.SetActive(false);
            Vector3 pos1 = Camera.main.WorldToScreenPoint(squares[0].transform.position);

            Tutorial.ins.handPr.rectTransform.position = pos1;
            Tutorial.ins.HintManagement();
        }

        if (indexLevel == 3)
        {
            numberOfTap = 5;
            GameController.instance.buyTap.gameObject.SetActive(false);
            GameController.instance.buyBomb.gameObject.SetActive(true);
            Vector3 pos2 = GameController.instance.buyBomb.transform.position;
            Tutorial.ins.handPr.rectTransform.position = pos2;
            Tutorial.ins.HintManagement();  
        }
        if (indexLevel == 4)
        {
            numberOfTap = 8;
            GameController.instance.buyTap.gameObject.SetActive(false);
            GameController.instance.buyBomb.gameObject.SetActive(false);
        }
        if (indexLevel == 5)
        {
            numberOfTap = 10;
        }
        if (indexLevel == 6)
        {
            numberOfTap = 10;
        }
        if (indexLevel == 7)
        {
            numberOfTap = 15;
        }
        if (indexLevel == 8)
        {
            numberOfTap = 4;
        }
        if (indexLevel == 9)
        {
            numberOfTap = 14;
        }
        if (indexLevel == 10)
        {
            numberOfTap = 13;
        }

        if (indexLevel == 11)
        {
            numberOfTap = 5;
        }
        if (indexLevel == 12)
        {
            numberOfTap = 10;
        }

        if (indexLevel == 13)
        {
            numberOfTap = 15;
        }

        if (indexLevel == 14)
        {
            numberOfTap = 20;
        }
    }
    public void CheckAllSquare()
    {
/*
        for (int i = 0; i < squares.Count; i++)
        {
            RaycastHit2D[] CastLose = Physics2D.RaycastAll(squares[i].transform.position, squares[i].direction, 1);

            if (CastLose.Length == 1)
            {
                break;
            }
            
        }*/
        if (dem == 0 && !GameController.instance.checkGameWin)
        {
            Debug.Log("1");
            GameController.instance.checkGameWin = true;
            UiManager.ins.ShowGameWinPanel();
            Rewardbar.ins.getReward();
            StartCoroutine(DelayCelebrationSFX());
            AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.rewardBonusSFX);
            AudioController.ins.backGroundMusic.Pause();

        }

    }
    IEnumerator DelayCelebrationSFX()
    {
        yield return new WaitForSeconds(1);
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.rewardBonusSFX);

    }
    void SetPositions()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            squaresPos.Add(squares[i].transform.position);
        }
    }
    void MoveIntoScreen()
    {
        for (int i = 0; i < squaresPos.Count; i++)
        {
            for (int j = 0; j < squares.Count; j++)
            {
                if (i == j)
                {
                    CameraControl.ins.isCheck = true;
                    squares[j].transform.DOLocalMove(squaresPos[i], 1f).OnComplete(() =>
                    {
                        CameraControl.ins.isCheck = false;
                    });
                }
            }
        }
    }
    public void MoveOutScreen()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            squares[i].transform.position = (Vector2)squares[i].transform.position + Random.insideUnitCircle * 20;

        }
    }

    public void ChangeColorOfNumberOfTap()
    {
        if (numberOfTap < 5 && numberOfTap >= 0)
        {
            ///   Debug.Log("nub < 5");
            UiManager.ins.movesText.DOColor(Color.red, 0.5f);
            UiManager.ins.movesText.transform.DOScale(1.2f, 1).OnComplete(() =>
            {
                UiManager.ins.movesText.transform.DOScale(1f, 1).OnComplete(() =>
                {
                    ChangeColorOfNumberOfTap();
                });
            });
        }
        else
        {
            UiManager.ins.movesText.color = Color.black;
        }
    }

    public void UseTutorial()
    {

        if (numberOfTap == 1 && indexLevel == 4)
        {
            GameController.instance.buyTap.gameObject.SetActive(true);
            Vector3 pos3 = GameController.instance.buyTap.transform.position;
            Tutorial.ins.handPr.transform.position = pos3;
            Tutorial.ins.HandMovement(pos3);
            Tutorial.ins.HintManagement();
            StartCoroutine(ChangeColorTutorialLV4());
        }


    }
    
    public Square squareTrue()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            if (squares[i].isRight())
            {
                return squares[i];
            }
        }
        return null;
    }

    public void CheckSquareCanClick()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            squares[i].canClick = true;
            ChangeColorTutorialBack();
        }
    }

    IEnumerator ChangeColorTutorialLV4()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < squares.Count; i++)
        {
            squares[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.4f);
            squares[i].canClick = false;
        }
    }
    public void ChangeColorTutorial()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            if (i > 0)
            {
                squares[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.4f);
            }
        }
    }
    public void ChangeColorTutorialBack()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            if (i > 0)
            {
                squares[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }
        }
    }
    public void TutorialLv3()
    {
        if (GameController.instance.checkBomb)
        {
            Vector3 pos4 = Camera.main.WorldToScreenPoint(squares[0].transform.position);
            Tutorial.ins.HandMovement(pos4);
        }
    }
    public void TutLv4()
    {
        if(indexLevel == 4)
        {
            for (int i = 0; i < squares.Count; i++)
            {
                squares[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                squares[i].canClick = true;
            }
        }
      
    }

}
