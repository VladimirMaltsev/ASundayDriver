using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenuControls : MonoBehaviour {

    public GameObject lockLevel2;
    public GameObject lockLevel3;
    public GameObject lockLevel4;


	public void SetLevel1()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
    }

    public void SetLevel2()
    {
        if (lockLevel2.activeSelf)
            if(CheckAndDrop(200)){
                lockLevel2.SetActive(false);
            }
        //if (!lockLevel2.activeSelf)
            PlayerPrefs.SetInt("CurrentLevel", 1);
    }

    public void SetLevel3()
    {
        if (lockLevel3.activeSelf)
            if (CheckAndDrop(300))
            {
                lockLevel3.SetActive(false);
            }
        //if (!lockLevel3.activeSelf)
            PlayerPrefs.SetInt("CurrentLevel", 2);
    }

    public void SetLevel4()
    {
        if (lockLevel4.activeSelf)
            if (CheckAndDrop(500))
            {
                lockLevel4.SetActive(false);
            }
        //if (!lockLevel4.activeSelf)
            PlayerPrefs.SetInt("CurrentLevel", 3);
    }

    public bool CheckAndDrop(int payment)
    {
        int currentFireflies = PlayerPrefs.GetInt("Fireflies");
        if (currentFireflies > payment)
        {
            PlayerPrefs.SetInt("Fireflies", currentFireflies - payment);
            return true;
        }
        return false;
    }

}
