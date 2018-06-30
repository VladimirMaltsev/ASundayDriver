using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelsMenuControls : MonoBehaviour {

    public GameObject lockLevel2;
    public GameObject lockLevel3;
    public GameObject lockLevel4;

    public GameObject playButtonLevel1;
    public GameObject playButtonLevel2;
    public GameObject playButtonLevel3;
    public GameObject playButtonLevel4;

    public GameObject loadingPage;
    public GameObject levelMenu;
    public MainMenuController mmc;

    public TextMeshProUGUI countFireflies;



    void Start()
    {
        countFireflies.SetText("" + PlayerPrefs.GetInt("Fireflies"));
        lockLevel2.SetActive (PlayerPrefs.GetInt("IsLock2") == 1 ? true : false);
        lockLevel3.SetActive (PlayerPrefs.GetInt("IsLock3") == 1 ? true : false);
        lockLevel4.SetActive(PlayerPrefs.GetInt("IsLock4") == 1 ? true : false);

        playButtonLevel2.SetActive (PlayerPrefs.GetInt("IsLock2") == 1 ? false : true);
        playButtonLevel3.SetActive (PlayerPrefs.GetInt("IsLock3") == 1 ? false : true);
        playButtonLevel4.SetActive (PlayerPrefs.GetInt("IsLock4") == 1 ? false : true);
        
        
    }

   

	public void SetLevel1()
    {
        PlayButtonLevel(0);
    }

    public void SetLevel2()
    {
        if (lockLevel2.activeSelf)
        {
            if (CheckAndDrop(10))
            {

                lockLevel2.SetActive(false);
                PlayerPrefs.SetInt("IsLock2", 0);
                playButtonLevel2.SetActive(true);
            }
        }
        else
        {
            PlayButtonLevel(1);
        }
    }

    public void SetLevel3()
    {
        if (lockLevel3.activeSelf)
        {
            if (CheckAndDrop(20))
            {
                lockLevel3.SetActive(false);
                playButtonLevel3.SetActive(true);
                PlayerPrefs.SetInt("IsLock3", 0);
            }
        }
        else
        {
            PlayButtonLevel(2);
        }
    }

    public void SetLevel4()
    {
        if (lockLevel4.activeSelf)
        {
            if (CheckAndDrop(30))
            {
                lockLevel4.SetActive(false);
                playButtonLevel3.SetActive(true);
                PlayerPrefs.SetInt("IsLock4", 0);
            }
        }
        else
        {
            PlayButtonLevel(3);
        }

    }

    public bool CheckAndDrop(int payment)
    {
        int currentFireflies = PlayerPrefs.GetInt("Fireflies");
        if (currentFireflies > payment)
        {
            PlayerPrefs.SetInt("Fireflies", currentFireflies - payment);
            countFireflies.SetText("" + PlayerPrefs.GetInt("Fireflies"));
            return true;
        }
        return false;
    }

    public void PlayButtonLevel(int i)
    {
        PlayerPrefs.SetInt("CurrentLevel", i);
        levelMenu.SetActive(false);
        mmc.ActivateAsync();
    }

}
