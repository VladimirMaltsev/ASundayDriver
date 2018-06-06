using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

    public GameObject[] startPositions;
    public BezierCurve[] bcurves;
    public AudioClip[] audios;
    public AudioSource ac;
    public CarControllerGyro ccg;
    public GameObject mainCamera;
    private BezierCurve bc;
    public GameObject firefly;

    private int loadedLevel;

    public SpriteRenderer mainLight;
    public SpriteRenderer headLightRight;
    public SpriteRenderer headLightLeft;
    public SpriteRenderer car;

    private System.Random rand;

    private int countFireflies;


    // Use this for initialization
    void Start() {
        rand = new System.Random();
        loadedLevel = PlayerPrefs.GetInt("CurrentLevel");
        switch (loadedLevel)
        {
            case 0: //blue theme
                ccg.transform.position = startPositions[0].transform.position;
                car.color = new Color32(200, 200, 200, 255);
                mainLight.color = new Color32(60, 120, 255, 187);
                headLightLeft.color = new Color32(50, 120, 255, 140);
                headLightRight.color = headLightLeft.color;

                bc = bcurves[0];
                countFireflies = 30;

                ac.clip = audios[0];
                ac.Play();
                break;

            case 1: //pink theme
                ccg.transform.position = startPositions[1].transform.position;
                car.color = new Color32(141, 236, 169, 255);
                mainLight.color = new Color32(255, 60, 180, 187);
                headLightLeft.color = new Color32(230, 150, 255, 140);
                headLightRight.color = headLightLeft.color;

                bc = bcurves[1];
                countFireflies = 30;

                ac.clip = audios[1];
                ac.Play();
                break;

            case 2: //green theme
                ccg.GetComponent<Rigidbody2D>().angularDrag = 10;

                ccg.transform.position = startPositions[2].transform.position;
                car.color = new Color32(255, 156, 156, 255);
                mainLight.color = new Color32(120, 255, 60, 187);
                headLightLeft.color = new Color32(20, 255, 100, 140);
                headLightRight.color = headLightLeft.color;

                bc = bcurves[2];
                countFireflies = 100;

                ac.clip = audios[2];
                ac.Play();
                break;

            default:
                ccg.transform.position = startPositions[1].transform.position;
                break;
        }
        mainCamera.transform.position = new Vector3(ccg.transform.position.x, ccg.transform.position.y - 40, -1);

        for (int i = 0; i < countFireflies; i++)
        {
            float ii = i;
            float randomPostion = (float)rand.NextDouble();
            Instantiate(firefly, bc.GetPointAt(randomPostion), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
