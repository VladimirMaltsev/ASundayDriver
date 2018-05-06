/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HPObject))]

public class Tank2DControl : MonoBehaviour {

	[Header("Скорость движения и вращения:")]
	[SerializeField] private float tankSpeed;
	[SerializeField] private float tankRotationSpeed;
	[Header("Оружие:")]
	[SerializeField] private float reloadTime;
	[SerializeField] private Tank2DShell tankShell;
	[SerializeField] private Transform tankShellPoint; // откуда вылетают снаряды
	[Header("Башня танка:")]
	[SerializeField] private Transform turret;
	[SerializeField] private float turretSpeed;

	private Rigidbody2D body;
	private HPObject HP;
	private float shotTime = Mathf.Infinity;
	private bool canShot;

	void Awake()
	{
		HP = GetComponent<HPObject>();
		body = GetComponent<Rigidbody2D>();
		body.gravityScale = 0;
	}

	void FixedUpdate()
	{
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		body.AddForce(transform.right * tankSpeed * v, ForceMode2D.Impulse);
		body.AddTorque(tankRotationSpeed * h * -Mathf.Sign(v), ForceMode2D.Impulse);
	}

	Quaternion TurretRotation()
	{
		Vector3 mouse = Input.mousePosition;
		mouse.z = Camera.main.transform.position.z;
		Vector3 direction = Camera.main.ScreenToWorldPoint(mouse) - transform.position;
		float angle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void CanShot()
	{
		if(canShot) return;
		shotTime += Time.deltaTime;

		if(shotTime > reloadTime)
		{
			shotTime = 0;
			canShot = true;
		}
	}

	void TankShot()
	{
		if(Input.GetMouseButtonDown(0) && canShot)
		{
			canShot = false;
			float angle  = Mathf.Atan2(tankShellPoint.right.y, tankShellPoint.right.x) * Mathf.Rad2Deg;
			Tank2DShell shell = Instantiate(tankShell, tankShellPoint.position, Quaternion.AngleAxis(angle, Vector3.forward)) as Tank2DShell;
			shell.SetDirection(tankShellPoint.right);
		}
	}

	void Update()
	{
		CanShot();
		TankShot();

		turret.rotation = Quaternion.Lerp(turret.rotation, TurretRotation(), turretSpeed * Time.deltaTime);

		if(HP.currentHP <= 0)
		{
			Destroy(gameObject);
		}
	}
}
