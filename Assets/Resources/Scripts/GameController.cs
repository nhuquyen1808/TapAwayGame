using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public int goldsBuyMovement;

    //public Text goldText;
    public static GameController instance;
    [SerializeField] private GameObject bomb;

    public int golds;

    [SerializeField] public Text goldtext;
    [SerializeField] Text bombTextQuantity;
    [SerializeField] public RectTransform goldDestination;
    [SerializeField] public RectTransform goldStart1;
    [SerializeField] public RectTransform goldStart2;
    [SerializeField] public RectTransform goldStart3;

    public int bombQuantity;
    public int totalCoinGain = 10;

    public GameObject bombMerchandising;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] int coinReward;
    public List<RectTransform> imageCoin;
    //   [HideInInspector] public bool checkVirator;
    [HideInInspector] public bool checkSettingClick;
    // [HideInInspector] public int saveStateVibrator;
    [HideInInspector] public bool audioCheck;
    [HideInInspector] public bool musicCheck;
    [HideInInspector] public bool startCoinPos;
    [HideInInspector] public bool startCoinPosReward;
    [HideInInspector] public bool checkGameWin;
    [HideInInspector] public int saveSound;
    [HideInInspector] public bool checkBtnBombUse;
    public Sprite turnOnSoundImg;
    public Sprite turnOnmusicImg;
    public Sprite muteSoundImg;
    public Sprite muteMusicImg;

    [Header("Buttons : ")]
    public Button settingBtn;
    public Button soundBtn;
    // public Button vibrategBtn;
    public Button recallBtn;
    public Button ShowBtn;
    public Button musicBtn;

    public Button buyBomb;
    public Button buyTap;
    [Header("Positions of button : ")]
    public GameObject settingBtnPos;
    public GameObject soundBtnPos;
    public GameObject recallBtnPos;
    public GameObject musicBtnPos;



    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Init();
    }

    private void Init()
    {
        initialPos = new Vector2[coinReward];
        SavePosAndRotation();
        golds = PlayerPrefs.GetInt("Gold");
        bombQuantity = PlayerPrefs.GetInt("Bomb");
        bombTextQuantity.text = bombQuantity.ToString();
        goldtext.text = golds.ToString();
    }

    private void Update()
    {
        bombTextQuantity.text = bombQuantity.ToString();
        if (bombQuantity > 0)
        {
            bombMerchandising.SetActive(true);
        }
        else
        {
            bombMerchandising.SetActive(false);
        }
    }
    void SavePosAndRotation()
    {
        for (int i = 0; i < imageCoin.Count; i++)
        {
            initialPos[i] = imageCoin[i].position;
        }
    }
    private void Reset()
    {
        for (int i = 0; i < imageCoin.Count; i++)
        {
            imageCoin[i].position = initialPos[i];
            //imageCoin[i].rotation = initialRotation[i];
        }
    }

    public void RewardPileOfCoin(int coin)
    {
        Reset();
        var delay = 0f;
        for (int i = 0; i < imageCoin.Count; i++)
        {
            if (startCoinPos && !startCoinPosReward)
            {
                imageCoin[i].transform.position = goldStart1.transform.position;
            }
            else if (!startCoinPos && !startCoinPosReward)
            {
                imageCoin[i].transform.position = goldStart2.transform.position;
            }
            /*  else if(startCoinPosReward && !startCoinPos)
              {
                  imageCoin[i].transform.position = goldStart3.transform.position;
                  Debug.Log("1");
              }*/
            imageCoin[i].gameObject.SetActive(true);
            imageCoin[i].DOMove(goldDestination.transform.position, 0.5f).SetDelay(delay + 0.06f).OnComplete(() => CountCoinByComplete());
            delay += 0.07f;
            //startCoinPosReward = false;
        }
    }
    void CountCoinByComplete()
    {
        PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 1);
        goldtext.text = PlayerPrefs.GetInt("Gold").ToString();
        golds = (int)PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", golds);
    }
    IEnumerator CountCoins(int coin)
    {
        yield return new WaitForSecondsRealtime(0.8f);
        var timer = 0f;
        for (int i = 0; i < coin; i++)
        {
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 1);
            goldtext.text = PlayerPrefs.GetInt("Gold").ToString();
            timer += 0.05f;
            yield return new WaitForSecondsRealtime(timer);
        }
    }
    public void Replay()
    {
        if (checkGameWin)
            return;

        SceneManager.LoadScene("TapAway");
        PlayerPrefs.GetInt("CheckVibrator");
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.clickButton);
        //UiMusicController.ins.OnclickMusicOff();
    }
    public void BuyMoveAndContinue()
    {
        UiManager.ins.HideGameOverPanel();

        ManageSquare.ins.numberOfTap += 10;
        CameraControl.ins.m_isGameOver = false;
        UiManager.ins.movesText.text = ManageSquare.ins.numberOfTap.ToString() + " moves";
        ManageSquare.ins.ChangeColorOfNumberOfTap();
    }
    public void GetReward()
    {
        startCoinPos = true;
        RewardPileOfCoin(10);
        golds = PlayerPrefs.GetInt("Gold");
        goldtext.text = golds.ToString();
        StartCoroutine(NextLevel());
        UiManager.ins.ShowPreventSpamPanel();
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.collectCoin);
    }
    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3f);
        LevelManagement.ins.i++;
        PlayerPrefs.SetInt("indexLevel", LevelManagement.ins.i);
        PlayerPrefs.Save();
        ResetGame();
        SceneManager.LoadScene("TapAway");
        PlayerPrefs.GetInt("CheckVibrator");
        goldtext.text = golds.ToString();

        if (PlayerPrefs.GetInt("isMusic") == 0)
        {
            UiMusicController.ins.OnclickMusicOn();
        }
        else
        {
            UiMusicController.ins.OnclickMusicOff();
        }

    }
    void ResetGame()
    {
        if (LevelManagement.ins.i == 14)
        {
            LevelManagement.ins.i = 0;
            golds = 0;
            PlayerPrefs.SetInt("Gold", golds);
            PlayerPrefs.SetInt("indexLevel", LevelManagement.ins.i);
        }
    }
    public bool checkBomb;
    public bool checkBuyBomb;
    public void DestroySquare()
    {
        BombButtonAction();
    }
    public bool checkBuyTap;
    public void BuyMovement()
    {
        checkBuyTap = true;
        if (golds >= 5)
        {

            ManageSquare.ins.numberOfTap += 10;
            golds -= 5;
            UiManager.ins.SetMovesText(ManageSquare.ins.numberOfTap.ToString() + " moves");
            goldtext.text = golds.ToString();
            PlayerPrefs.SetInt("Gold", golds);
            PlayerPrefs.Save();
            AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.buyBomb);
            UiManager.ins.PopUpQuantityBonus();
        }
        Tutorial.ins.hand.gameObject.SetActive(false);
        ManageSquare.ins.TutLv4();
    }

    public void ShowButtons()
    {
        //vibrategBtn.transform.DOMove(vibratorgBtnPos.transform.position, 0.5f);
        //vibrategBtn.image.DOFade(1, 0.2f).From(0);
        recallBtn.transform.DOMove(recallBtnPos.transform.position, 0.5f);
        recallBtn.image.DOFade(1, 0.2f).From(0);
        ShowBtn.gameObject.SetActive(false);
        UiMusicController.ins.musicOnBtn.transform.DOMove(musicBtnPos.transform.position, 0.5f);
        UiMusicController.ins.musicOnBtn.image.DOFade(1, 0.2f).From(0);
        UiMusicController.ins.soundOnBtn.transform.DOMove(soundBtnPos.transform.position, 0.5f);
        UiMusicController.ins.soundOnBtn.image.DOFade(1, 0.2f).From(0);
        UiMusicController.ins.musicOffBtn.transform.DOMove(musicBtnPos.transform.position, 0.5f);
        UiMusicController.ins.musicOffBtn.image.DOFade(1, 0.2f).From(0);
        UiMusicController.ins.soundOffBtn.transform.DOMove(soundBtnPos.transform.position, 0.5f);
        UiMusicController.ins.soundOffBtn.image.DOFade(1, 0.2f).From(0);

    }

    public void HideButtons()
    {

        //vibrategBtn.transform.DOMove(settingBtnPos.transform.position, 0.5f);
        //vibrategBtn.image.DOFade(0, 0.2f);
        recallBtn.transform.DOMove(settingBtnPos.transform.position, 0.5f);
        recallBtn.image.DOFade(0, 0.2f);
        ShowBtn.gameObject.SetActive(true);
        UiMusicController.ins.musicOnBtn.transform.DOMove(settingBtnPos.transform.position, 0.5f);
        UiMusicController.ins.musicOnBtn.image.DOFade(0, 0.2f);
        UiMusicController.ins.soundOnBtn.transform.DOMove(settingBtnPos.transform.position, 0.5f);
        UiMusicController.ins.soundOnBtn.image.DOFade(0, 0.2f);
        UiMusicController.ins.musicOffBtn.transform.DOMove(settingBtnPos.transform.position, 0.5f);
        UiMusicController.ins.musicOffBtn.image.DOFade(0, 0.2f);
        UiMusicController.ins.soundOffBtn.transform.DOMove(settingBtnPos.transform.position, 0.5f);
        UiMusicController.ins.soundOffBtn.image.DOFade(0, 0.2f);
    }

    public void SettingButtonClicked()
    {
        if (!checkSettingClick)
        {
            ShowButtons();
            checkSettingClick = true;
        }
        else
        {
            HideButtons();
            checkSettingClick = false;
        }
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.clickButton);
    }
    public void RecallButtonClicked()
    {
        HideButtons();
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.clickButton);
    }
    /*    public void VibratorButtonClicked()
        {
            AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.clickButton);
            if (!checkVirator && saveStateVibrator == 0)
            {
                Debug.Log("On");
                checkVirator = true;
                saveStateVibrator = 1;
                PlayerPrefs.SetInt("CheckVibrator", saveStateVibrator);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Off");
                checkVirator = false;
                saveStateVibrator = 0;
                PlayerPrefs.SetInt("CheckVibrator", saveStateVibrator);
                PlayerPrefs.Save();
            }
        }*/

    void BombButtonAction()
    {
        if (!checkBtnBombUse)
        {
            if (golds >= 5 && bombQuantity == 0)
            {
                checkBuyBomb = true;
                UiManager.ins.bombBtnUsing();
                golds -= 5;
                bombQuantity += 1;
                goldtext.text = golds.ToString();
                PlayerPrefs.SetInt("Gold", golds);
                PlayerPrefs.SetInt("Bomb", bombQuantity);
                PlayerPrefs.Save();
                checkBomb = true;

                AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.buyBomb);
            }

            if (bombQuantity > 0)
            {
                checkBomb = true;
                checkBuyBomb = false;
                UiManager.ins.bombBtnUsing();
                checkBuyBomb = false;
                AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.clickButton);

            }
            checkBtnBombUse = true;
            //ManageSquare.ins.UseTutorial();
        }
        else
        {
            checkBomb = false;
            checkBuyBomb = false;
            checkBtnBombUse = false;
            UiManager.ins.bombBtnUsing();
        }

    }

}
