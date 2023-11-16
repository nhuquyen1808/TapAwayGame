using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip bombUsed, click, burst, celebration, collectCoin, collision, lose, collectBonusCoin, buyBomb, clickButton, rewardBonusSFX;
    public AudioSource aus;
    public AudioSource backGroundMusic;

    public static AudioController ins;

    private void Awake()
    {
        if (ins != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            ins = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {

    }

    //Call Audioclip when you need
    public void UpdateSoundAndMusic(AudioSource audioSource,AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
        if (PlayerPrefs.GetInt("isSound") == 0)
        {
            audioSource.volume = 1;
        }
        else if (PlayerPrefs.GetInt("isSound") == 1)
        {
            audioSource.volume = 0;
        }

    }

    public void BackgroundMusic()
    {
        if (PlayerPrefs.GetInt("isMusic") == 0)
        {
            backGroundMusic.UnPause();
           // Debug.Log("Turn on BGM");
        }
        else if (PlayerPrefs.GetInt("isMusic") == 1)
        {
            backGroundMusic.Pause();
            //Debug.Log("Turn OFf BGM");
        }
    }
}
