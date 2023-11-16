using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BonusReward : MonoBehaviour
{
    public RectTransform rect;
    public List<Image> bonusImage;
    public List<Image> bgBonusImage;
    public static BonusReward ins;
    int indexBonusImg;
    int indexBgBonusImg;
    int coinBonus;
    [HideInInspector] public bool checkGetCoin;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        /*        rect.transform.DOMove(endPos.transform.position, 4) .OnComplete(()=> 
                rect.transform.DOMove(startPos.transform.position, 4)).SetLoops(-1, LoopType.Yoyo)
             .Play();*/

    }

    public void DoScaleImg()
    {
        if(!checkGetCoin)
        {
            
            bonusImage[indexBonusImg].transform.DOScale(0.6f, 0.4f).OnComplete(() =>
            {
                //bonusImage[index].DOFade(0.5f, 0.2f).From(1);
                bonusImage[indexBonusImg].transform.DOScale(1f, 0.4f).OnComplete(() => {
                    indexBonusImg++;
                    if (indexBonusImg == bonusImage.Count)
                    {
                        indexBonusImg = 0;
                    }
                    DoScaleImg();
                });
            });
        }
       
    }

    public void DoFadeImg()
    {
        if (!checkGetCoin)
        {
            bgBonusImage[indexBgBonusImg].DOFade(0.4f, 0.4f).From(1).OnComplete(() =>
            {
                bgBonusImage[indexBgBonusImg].DOFade(1, 0.4f).OnComplete(() =>
                {
                    indexBgBonusImg++;
                    if (indexBgBonusImg == bgBonusImage.Count)
                    {
                        indexBgBonusImg = 0;
                    }
                    DoFadeImg();
                });
            });
        }
    }

    public void GetPositionsBonusReward()
    {
        checkGetCoin = true;
        GameController.instance.startCoinPos = false;

        if (indexBonusImg == 0 )
        {
            //x2
            bonusImage[0].DOColor(Color.black, 0.3f);
           // Debug.Log("0");
            coinBonus = 0;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        if (indexBonusImg == 6)
        {
            //x2
            bonusImage[6].DOColor(Color.black, 0.3f);
            // Debug.Log("0");
            coinBonus = 10;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        if (indexBonusImg == 1 )
        {
            //x3
            bonusImage[1].DOColor(Color.black, 0.3f);
            //Debug.Log("1");
            coinBonus = 20;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        if (indexBonusImg == 5)
        {
            //x3
            bonusImage[5].DOColor(Color.black, 0.3f);
            //Debug.Log("1");
            coinBonus = 20;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        if (indexBonusImg == 2 )
        {
            //x4
           //Debug.Log("2");
            bonusImage[2].DOColor(Color.black, 0.3f);
            coinBonus = 30;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        if (indexBonusImg == 4)
        {
            //x4
            //Debug.Log("2");
            bonusImage[4].DOColor(Color.black, 0.3f);
            coinBonus = 30;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        if (indexBonusImg == 3)
        {
            //x5
            //Debug.Log("3");
            bonusImage[3].DOColor(Color.black, 0.3f);
            coinBonus = 40;
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", GameController.instance.golds + coinBonus);
            GameController.instance.totalCoinGain += coinBonus;
            UiManager.ins.CoinGain.text = GameController.instance.totalCoinGain.ToString();
        }
        GetCoinFromAd();
    }

    void GetCoinFromAd()
    {
        GameController.instance.RewardPileOfCoin(10);
        GameController.instance .golds = PlayerPrefs.GetInt("Gold");
       GameController.instance.goldtext.text = GameController.instance.golds.ToString();
        GameController.instance.StartCoroutine(GameController.instance.NextLevel());
        UiManager.ins.ShowPreventSpamPanel();
        AudioController.ins.UpdateSoundAndMusic(AudioController.ins.aus, AudioController.ins.collectCoin);
    }
}
