using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMusicController : MonoBehaviour
{
    public Button musicOffBtn;
    public Button musicOnBtn;
    public Button soundOffBtn;
    public Button soundOnBtn;

    public bool checkVirator ;
    public int saveStateVibrator;
    
    public static UiMusicController ins;
    private void Awake()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        musicOffBtn.onClick.AddListener(OnclickMusicOn);
        musicOnBtn.onClick.AddListener(OnclickMusicOff);
        soundOffBtn.onClick.AddListener(OnclickSoundOn);
        soundOnBtn.onClick.AddListener(OnclickSoundOff);
        soundOnBtn.onClick.AddListener(OnClickVibrateOn);
        soundOnBtn.onClick.AddListener(OnClickVibrateOff);
        saveStateVibrator = PlayerPrefs.GetInt("CheckVibrator");

        SaveSoundAndMusic();
        AudioController.ins.BackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SaveSoundAndMusic()
    {
        if(PlayerPrefs.GetInt("isSound") == 0)
        {
            OnclickSoundOn();
        }
        else if(PlayerPrefs.GetInt("isSound") == 1)
        {
            OnclickSoundOff();
        }
        if (PlayerPrefs.GetInt("isMusic") == 0)
        {
            OnclickMusicOn();
        }
        else if (PlayerPrefs.GetInt("isMusic") == 1)
        {
            OnclickMusicOff();
        }

        if(PlayerPrefs.GetInt("isVibrate" ) == 0)
        {
            OnClickVibrateOn();
        }
        else if (PlayerPrefs.GetInt("isVibrate") == 1)
        {
            OnClickVibrateOff();
        }
    }
    public void OnclickSoundOff()
    {
        soundOnBtn.gameObject.SetActive(false);
        soundOffBtn.gameObject.SetActive(true);
        //  Debug.Log("Sound Off");
        PlayerPrefs.SetInt("isSound", 1);
    }
    public void OnclickSoundOn()
    {
        soundOnBtn.gameObject.SetActive(true);
        soundOffBtn.gameObject.SetActive(false);
        PlayerPrefs.SetInt("isSound", 0);

    }

    public void OnclickMusicOff()
    {
        musicOnBtn.gameObject.SetActive(false);
        musicOffBtn.gameObject.SetActive(true);
        // Debug.Log("Music Off");
        PlayerPrefs.SetInt("isMusic", 1);
        AudioController.ins.BackgroundMusic();
    }
    public void OnclickMusicOn()
    {
        musicOnBtn.gameObject.SetActive(true);
        musicOffBtn.gameObject.SetActive(false);
        PlayerPrefs.SetInt("isMusic", 0);
        AudioController.ins.BackgroundMusic();
    }

    public void OnClickVibrateOn()
    {
        //ON
        soundOnBtn.gameObject.SetActive(true);
        soundOffBtn.gameObject.SetActive(false);
        checkVirator = true;
        saveStateVibrator = 1;
        PlayerPrefs.SetInt("isVibrate", 0);
        PlayerPrefs.Save();
    }
    public void OnClickVibrateOff()
    {
        //Off
        soundOnBtn.gameObject.SetActive(false);
        soundOffBtn.gameObject.SetActive(true);
        checkVirator = false;
        saveStateVibrator = 0;
        PlayerPrefs.SetInt("isVibrate", 1);
        PlayerPrefs.Save();
    }
}
