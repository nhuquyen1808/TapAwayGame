using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UiManager : MonoBehaviour
{
    [Header("GameObject : ")]
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject gamePlayPanel;
    public GameObject preventSpam;
    public GameObject bombQuantityText;
    public GameObject rewardBonus;
    public GameObject coinBonus;
    public GameObject loadReward;
    public GameObject AdBtn;
    public GameObject getCoin;
    public GameObject coinGain;
    [Header("Text : ")]
    public Text movesText;
    public Text textWin;
    public Text textLvComplete;
    public Text textTapToContinue;
    public Text retryText;
    public Text tapQuantityBonus;
    public TextMeshProUGUI CoinGain;
    public TextMeshProUGUI levelText;
    public ParticleSystem particleFireWorks1;
    public ParticleSystem particleFireWorks2;
    public Image winEmotion;
    public Button bombBtn;
    public static UiManager ins;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        gameWinPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        rewardBonus.gameObject.SetActive(false);
        loadReward.gameObject.SetActive(false);
        AdBtn.gameObject.SetActive(false);
        getCoin.gameObject.SetActive(false);
        coinGain.gameObject.SetActive(false);
    }
    public void SetMovesText(string txt)
    {
        if (movesText)
        {
            movesText.text = txt;
        }
    }

    public void SetLevelText(string leveltxt)
    {
        if (levelText)
        {
            levelText.text = leveltxt;
        }
    }

    public void bombBtnUsing()

    {  if(GameController.instance.checkBomb )
        {
            bombBtn.GetComponent<Image>().color = Color.red;
        }
        else
        {
            bombBtn.GetComponent<Image>().color = Color.white;
        }
    }
    public void bombBtnUsed()
    {
        bombBtn.GetComponent<Image>().color = Color.white;
    }
    public void ShowGameOverPanel()
    {
        StartCoroutine(DelayShowGameOverPanel());

    }

    IEnumerator DelayShowGameOverPanel()
    {
        yield return new WaitForSeconds(1);
        gameWinPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
        retryText.transform.DOScale(1.1f, 1).OnComplete(() =>
        {
            retryText.transform.DOScale(1f, 1).SetLoops(-1,LoopType.Yoyo);
        });
    }
    public void HideGameOverPanel()
    {
        StartCoroutine(DelayHideGameOverPanel());
    }

    IEnumerator DelayHideGameOverPanel()
    {
        yield return new WaitForSeconds(1);
        gameWinPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
    }
    public void ShowPreventSpamPanel()
    {
        preventSpam.gameObject.SetActive(true);
        Destroy(gameObject, 1);
    }

    public void ShowGoldBonus()
    {
        
        if (PlayerPrefs.GetFloat("Reward") == Rewardbar.ins.maxReward - 1)
        {
            GameController.instance.startCoinPosReward = true;
            GameController.instance.totalCoinGain += 10;
            CoinGain.text = GameController.instance.totalCoinGain.ToString();
           // StartCoroutine(delayShowCoin());
        }
    }
    IEnumerator delayShowCoin()
    {
        yield return new WaitForSeconds(2.3f);
        GameController.instance.RewardPileOfCoin(0);
    }
    public void ShowGameWinPanel()
    {
        ShowGoldBonus();
        BonusReward.ins.DoFadeImg();
        BonusReward.ins.DoScaleImg();
        StartCoroutine(DelayShowGameWinPanel());
        StartCoroutine(DelayShowFireWorks());
        CoinGain.text = GameController.instance.totalCoinGain.ToString();

    }

    IEnumerator DelayShowFireWorks()
    {
        yield return new WaitForSeconds(0.9f);
        particleFireWorks1.Play();
        particleFireWorks2.Play();
        AudioController.ins.aus.PlayOneShot(AudioController.ins.celebration);
    }
    IEnumerator DelayShowGameWinPanel()
    {   
        yield return new WaitForSeconds(1.5f);
        gameWinPanel.SetActive(true);

        gameOverPanel.SetActive(false);
        gamePlayPanel.SetActive(false);

        StartCoroutine(DelayElementOfUi());
        //CoinGain.text = GameController.instance.totalCoinGain.ToString();
    }

    IEnumerator DelayElementOfUi()
    {
        yield return new WaitForSeconds(0.07f);
        textLvComplete.transform.DOScale(1f, 1f);

        yield return new WaitForSeconds(0.12f);
        textWin.transform.DOScale(1f, 2f);
       yield return new WaitForSeconds(0.5f);
        loadReward.gameObject.SetActive(true);
        coinGain.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        rewardBonus.gameObject.SetActive(true);
        AdBtn.gameObject.SetActive(true);
        getCoin.gameObject.SetActive(true);
 


    }
    public void PopUpQuantityBonus()
    {
        tapQuantityBonus.gameObject.SetActive(true);
        tapQuantityBonus.DOFade(1, 0.75f).From(0).OnComplete(() =>
        {
            tapQuantityBonus.DOFade(0, 0.75f);
        });
    }
}
