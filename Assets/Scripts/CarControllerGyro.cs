using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class CarControllerGyro : MonoBehaviour {

    // Editor
    [Header("Скорость движения машины")]
    public float car_speed;
    public float mouseSensitivityX = 3.5f;
    public float mouseSensitivityY = 3.5f;
    public bool mbLockNHideCursor = false;
    private float cameraRotationX, cameraRotationY;

    public Camera mainCamera;

    public Gyroscope gyro;
    private Quaternion phone_rotation;
    private float acceleration;
    public float current_angle_rotation;
    private float angle_range = 90;

    public Rigidbody2D car_body;
    public GameManage gm;
    public ParticleSystem ps_BOOOM;
    public Light headLight;
    //public GameObject saveMenuUI;


    private bool hasProtection;
    public bool mayBeDestroyed;
    public bool wasCrash;

    private float previous_gyro_rotation_z;
    public float current_gyro_rotation_z;
    private float inital_phone_rotation_z;

    private float screenWidth;
    
    


    public SpriteRenderer headlightRight;
    public SpriteRenderer headlightLeft;
    public SpriteRenderer headlightLeftBack;
    public SpriteRenderer headlightRightBack;

   // public Transform wheel;

    void Awake()
    {
        //saveMenuUI.SetActive(false);
        //controlType = ControlType.ButtonsControls;
       
        hasProtection = false;
        mayBeDestroyed = true;
        wasCrash = false;
        

        gyro = Input.gyro;
        gyro.enabled = true;
        
        car_body = GetComponent<Rigidbody2D>();
        car_body.centerOfMass -= new Vector2(0f,1.5f);
        inital_phone_rotation_z = GetGyroRotationZ();
        current_angle_rotation = 0;
        previous_gyro_rotation_z = 0;
    }

    void Start()
    {
        screenWidth = Screen.width;
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Controls") == ControlType.GyroControls.ToString())
            UpdateCurrentAngle();

        if (PlayerPrefs.GetString("Controls") == ControlType.ButtonsControls.ToString())
        {
            int i = 0;
            float acceleration = 1;
            while (Input.touchCount > i && car_speed > 0)
            {
                if (Input.GetTouch(i).position.x > screenWidth / 2)
                {
                    car_body.AddTorque(-car_speed* 6f * Time.deltaTime * acceleration, ForceMode2D.Impulse);
                }
                if (Input.GetTouch(i).position.x < screenWidth / 2)
                {
                    car_body.AddTorque(car_speed * 6f * Time.deltaTime * acceleration, ForceMode2D.Impulse);
                }
                ++i;
                acceleration += 0.1f;
            }
        }

#if UNITY_EDITOR
        cameraRotationX += UnityEngine.Input.GetAxis("Mouse Y") * mouseSensitivityY;
        cameraRotationY += UnityEngine.Input.GetAxis("Mouse X") * mouseSensitivityX;
        current_angle_rotation = cameraRotationY;
#endif

        if (car_speed > 0)
        {
            car_body.AddForce(transform.up * car_speed, ForceMode2D.Impulse);
            car_body.AddTorque(Mathf.Clamp(current_angle_rotation, -angle_range / 2, angle_range / 2), ForceMode2D.Impulse);
        }

        Vector3 newPos = transform.position;
        newPos += transform.right * current_angle_rotation / 10;
        newPos.z = mainCamera.transform.position.z;

        Vector3 velocity = Vector3.zero;
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, newPos, ref velocity, 0.15f);
        mainCamera.transform.rotation = transform.rotation;

    }


    public void UpdateCurrentAngle()
    {
        acceleration = gyro.rotationRate.z;
        if (WasRotation())
            current_angle_rotation += acceleration * Time.deltaTime * 360 / Mathf.PI;
    }

    public void ButtonLeftWheel()
    {
        current_angle_rotation += 55f;
    }

    public void ButtonRightWheel()
    {
        current_angle_rotation -= 55f;
    }

    public bool WasRotation()
    {
        bool was_rotatiton = false;
        current_gyro_rotation_z = GetGyroRotationZ() - inital_phone_rotation_z;
        if (current_gyro_rotation_z > 180)
            current_gyro_rotation_z -= 360;

        if (Mathf.Abs(previous_gyro_rotation_z - current_gyro_rotation_z) > 0.1)
            was_rotatiton = true;

        previous_gyro_rotation_z = current_gyro_rotation_z;

        return was_rotatiton;
    }


    

    

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Puddle"))
        {
            gm.PlusFirefly();
            Object.Destroy(coll.gameObject);
        }
    }

    public void ActivateProtection()
    {
        headLight.enabled = true;
        mayBeDestroyed = false;
        gm.saveMenuUI.SetActive(false);
        hasProtection = true;
        StartCoroutine(Destroy());
        car_speed = 400;
    }

    IEnumerator Destroy()
    {
        if (!hasProtection)
        {
            yield break;
        }
        
        var sprite = this.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 20; i++)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        sprite.enabled = true;
        headlightRight.enabled = true;
        headlightLeft.enabled = true;
        headlightRightBack.enabled = true;
        headlightLeftBack.enabled = true;
        mayBeDestroyed = true;
    }

    public float GetGyroRotationZ()
    {
        return (Quaternion.Euler(90f, 0f, 0f) * new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w)).eulerAngles.z;
    }
   
    
}
