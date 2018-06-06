using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            if (!ccg.wasCrash)
            {

                ccg.wasCrash = true;
                gm.saveMenuUI.SetActive(true);
                StartCoroutine(gm.Timer(3));
                ccg.car_speed = 0;
            }
            else
            {
                gm.UpdateTextScore();
                
                gm.levelMenuUI.SetActive(true);
                ccg.car_speed = 0;
            }
            ccg.car_body.AddForce(transform.up * -8000, ForceMode2D.Impulse);
        }

        
    }
    
    
}
