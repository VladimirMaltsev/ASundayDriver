using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerGyro : MonoBehaviour {

    // Editor
    [Header("Скорость движения машины")]
    public float car_speed;
    public float mouseSensitivityX = 3.5f;
    public float mouseSensitivityY = 3.5f;
    public bool mbLockNHideCursor = false;
    private float cameraRotationX, cameraRotationY;
    
    public Gyroscope gyro;
    private Quaternion phone_rotation;
    private float acceleration;
    public float current_angle_rotation;
    private float angle_range = 90;

    Rigidbody2D car_body;
    public GameManage gm;
    public ParticleSystem ps_BOOOM;
    //public GameObject saveMenuUI;

    
    private bool hasProtection;
    private bool mayBeDestroyed;
    private bool wasCrash;

    private float previous_gyro_rotation_z;
    public float current_gyro_rotation_z;
    private float inital_phone_rotation_z;

   // public Transform wheel;

    void Awake()
    {
        //saveMenuUI.SetActive(false);

        hasProtection = false;
        mayBeDestroyed = true;
        wasCrash = false;
        

        gyro = Input.gyro;
        gyro.enabled = true;

        car_body = GetComponent<Rigidbody2D>();
        inital_phone_rotation_z = GetGyroRotationZ();
        current_angle_rotation = 0;
        previous_gyro_rotation_z = 0;
    }

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    void Update()
    {
        UpdateCurrentAngle();

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
        
    }


    public void UpdateCurrentAngle()
    {
        acceleration = gyro.rotationRate.z;
        if (WasRotation())
            current_angle_rotation += acceleration * Time.deltaTime * 360 / Mathf.PI;
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




    void OnCollisionStay2D(Collision2D coll)
    {
        OnCollisionEnter2D(coll);
    }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Border" && mayBeDestroyed)
            {
            mayBeDestroyed = false;
                ps_BOOOM.Play();
                if (Vibration.HasVibrator())
                {
                    Vibration.Vibrate(200);
                }

            //current_angle_rotation = 0;
            if (!wasCrash)
            {
                wasCrash = true;
                gm.saveMenuUI.SetActive(true);
                StartCoroutine(gm.Timer(3));
                car_speed = 0;
            } else
            {
                gm.UpdateTextScore();
                this.GetComponent<SpriteRenderer>().enabled = false;
                gm.levelMenuUI.SetActive(true);
                car_speed = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Puddle")
        {
            Vibration.Vibrate(200);
            car_speed = 200f;
            car_body.angularDrag = 30;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Puddle")
        {
            car_speed = 400f;
            car_body.angularDrag = 12;
        }
    }

    public void ActivateProtection()
    {
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
        mayBeDestroyed = true;
    }

    public float GetGyroRotationZ()
    {
        return (Quaternion.Euler(90f, 0f, 0f) * new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w)).eulerAngles.z;
    }
   
    
}
