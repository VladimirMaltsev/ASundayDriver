using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class CarBorderController : MonoBehaviour {

    public GameManage gm;
    public CarControllerGyro ccg;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnCollisionStay2D(Collision2D coll)
    {
        OnCollisionEnter2D(coll);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Border" && ccg.mayBeDestroyed)
        {
            ccg.mayBeDestroyed = false;
            ccg.ps_BOOOM.Play();
            if (Vibration.HasVibrator())
            {
                Vibration.Vibrate(200);
            }
            ccg.GetComponent<SpriteRenderer>().enabled = false;
            ccg.headlightRight.enabled = false;
            ccg.headlightLeft.enabled = false;
            ccg.headlightRightBack.enabled = false;
            ccg.headlightLeftBack.enabled = false;
            
            //current_angle_rotation = 0;
            if (!ccg.wasCrash && gm.GetScore() > 10)
            {

                ccg.wasCrash = true;
                gm.saveMenuUI.SetActive(true);
                StartCoroutine(gm.Timer(3));
                ccg.car_speed = 0;
            }
            else
            {
                PlayerPrefs.SetInt("Games", PlayerPrefs.GetInt("Games") - 1);
                if (PlayerPrefs.GetInt("Games") == 0)
                {
                    if (Advertisement.IsReady("video"))
                    {
                        Advertisement.Show("adBitweenGames");
                    }
                    PlayerPrefs.SetInt("Games", 4);
                }
                gm.UpdateTextScore();
                
                gm.levelMenuUI.SetActive(true);
                ccg.car_speed = 0;
            }
            ccg.car_body.AddForce(transform.up * -8000, ForceMode2D.Impulse);
        }

        
    }
    
    
}
