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


    //Colors
    //#level1
    public Color32 carColor1;
    public Color32 mainColor1;
    public Color32 headLightColor1;
    public Color32 backLightColor1;
    //#level2
    public Color32 carColor2;
    public Color32 mainColor2;
    public Color32 headLightColor2;
    public Color32 backLightColor2;
    //#level3
    public Color32 carColor3;
    public Color32 mainColor3;
    public Color32 headLightColor3;
    public Color32 backLightColor3;

    //#level4
    public Color32 carColor4;
    public Color32 mainColor4;
    public Color32 headLightColor4;
    public Color32 backLightColor4;


    // Use this for initialization
    void Start() {
        rand = new System.Random();
        loadedLevel = PlayerPrefs.GetInt("CurrentLevel");
        ccg.GetComponent<SpriteRenderer>().sprite = ccg.cars[PlayerPrefs.GetInt("Car")];
        InitLevel(loadedLevel);
    }

    public void InitLevel(int loadedLevel)
    {
        switch (loadedLevel)
        {
            case 0:
                ccg.car_speed_constant = 450;
                ccg.car_speed = ccg.car_speed_constant;
                ccg.GetComponent<Rigidbody2D>().angularDrag = 14;
                ccg.transform.position = startPositions[0].transform.position;
                car.color = carColor1;
                mainLight.color = mainColor1;
                headLightLeft.color = headLightColor1;
                headLightRight.color = headLightColor1;

                bc = bcurves[0];
                countFireflies = 30;

                //ac.clip = audios[0];
                //ac.Play();
                break;

            case 1:
                ccg.transform.position = startPositions[1].transform.position;
                ccg.GetComponent<Rigidbody2D>().angularDrag = 13;
                ccg.car_speed_constant = 500;
                ccg.car_speed = ccg.car_speed_constant;

                car.color = carColor2;
                mainLight.color = mainColor2;
                headLightLeft.color = headLightColor2;
                headLightRight.color = headLightColor2;

                bc = bcurves[1];
                countFireflies = 30;

                //ac.clip = audios[1];
                //ac.Play();
                break;

            case 2:
                ccg.transform.position = startPositions[2].transform.position;
                ccg.GetComponent<Rigidbody2D>().angularDrag = 12;
                ccg.car_speed_constant = 550;
                ccg.car_speed = ccg.car_speed_constant;

                car.color = carColor3;
                mainLight.color = mainColor3;
                headLightLeft.color = headLightColor3;
                headLightRight.color = headLightColor3;

                bc = bcurves[2];
                countFireflies = 50;

                //ac.clip = audios[2];
                //ac.Play();
                break;

            case 3:
                ccg.transform.position = startPositions[3].transform.position;
                ccg.GetComponent<Rigidbody2D>().angularDrag = 11;
                ccg.car_speed_constant = 600;
                ccg.car_speed = ccg.car_speed_constant;

                car.color = carColor4;
                mainLight.color = mainColor4;
                headLightLeft.color = headLightColor4;
                headLightRight.color = headLightColor4;

                bc = bcurves[3];
                countFireflies = 70;

                //ac.clip = audios[2];
                //ac.Play();
                break;

            default:
                ccg.transform.position = startPositions[1].transform.position;
                break;
        }
        mainCamera.transform.position = new Vector3(ccg.transform.position.x, ccg.transform.position.y - 10, -1);

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
