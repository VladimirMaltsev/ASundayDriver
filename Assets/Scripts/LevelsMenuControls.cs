using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GoogleMobileAds.Api;

public class LevelsMenuControls : MonoBehaviour {

    public GameObject lockLevel2;
    public GameObject lockLevel3;
    public GameObject lockLevel4;

    public GameObject playButtonLevel1;
    public GameObject playButtonLevel2;
    public GameObject playButtonLevel3;

    public GameObject loadingPage;
    public GameObject levelMenu;

    public TextMeshProUGUI countFireflies;

    private BannerView banner;
    private string bannerId = "ca-app-pub-7280831525899952/1262335776";

    void Start()
    {
        countFireflies.SetText("" + PlayerPrefs.GetInt("Fireflies"));
        lockLevel2.SetActive (PlayerPrefs.GetInt("IsLock2") == 1 ? true : false);
        lockLevel3.SetActive (PlayerPrefs.GetInt("IsLock3") == 1 ? true : false);

        playButtonLevel2.SetActive (PlayerPrefs.GetInt("IsLock2") == 1 ? false : true);
        playButtonLevel3.SetActive (PlayerPrefs.GetInt("IsLock3") == 1 ? false : true);
        //lockLevel4.SetActive (PlayerPrefs.GetInt("IsLock1") == 1 ? true : false);

        this.requestBanner();
    }

    private void requestBanner()
    {
        banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        banner.LoadAd(request);
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
            if (CheckAndDrop(30))
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
            if (CheckAndDrop(500))
            {
                lockLevel4.SetActive(false);
                playButtonLevel3.SetActive(true);
            }
        }
        else
        {
            PlayButtonLevel(4);
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
        loadingPage.SetActive(true);
        levelMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }

}
