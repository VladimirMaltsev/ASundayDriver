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


    // Use this for initialization
    void Start() {
        rand = new System.Random();
        loadedLevel = PlayerPrefs.GetInt("CurrentLevel");
        switch (loadedLevel)
        {
            case 0: //blue theme
                ccg.transform.position = startPositions[0].transform.position;
                car.color = carColor1;
                mainLight.color = mainColor1;
                headLightLeft.color = headLightColor1;
                headLightRight.color = headLightColor1;

                bc = bcurves[0];
                countFireflies = 30;

                ac.clip = audios[0];
                ac.Play();
                break;

            case 1: //pink theme
                ccg.transform.position = startPositions[1].transform.position;
                car.color = carColor2;
                mainLight.color = mainColor2;
                headLightLeft.color = headLightColor2;
                headLightRight.color = headLightColor2;

                bc = bcurves[1];
                countFireflies = 30;

                ac.clip = audios[1];
                ac.Play();
                break;

            case 2: //green theme
                ccg.GetComponent<Rigidbody2D>().angularDrag = 10;

                ccg.transform.position = startPositions[2].transform.position;
                car.color = carColor3;
                mainLight.color = mainColor3;
                headLightLeft.color = headLightColor3;
                headLightRight.color = headLightColor3;

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
