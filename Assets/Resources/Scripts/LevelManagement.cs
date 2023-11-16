using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement ins;
    public List<ManageSquare> listLevel = new List<ManageSquare>();
    public ManageSquare manageSuqare;
    public int i;
    public Transform parent;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        increaseLevel();
        UiManager.ins.SetLevelText("Level " + (i+1).ToString());
    }
    void increaseLevel()
    {
        i = PlayerPrefs.GetInt("indexLevel");
        foreach (ManageSquare levelid in listLevel)
        {
            if (levelid.indexLevel == (i + 1))
            {
                manageSuqare = Instantiate(levelid, parent);
                manageSuqare.transform.position = Vector3.zero;
               
            }
            
        }
    }
}

