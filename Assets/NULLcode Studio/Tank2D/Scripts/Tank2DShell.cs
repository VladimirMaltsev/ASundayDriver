/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Tank2DShell : MonoBehaviour {

	[SerializeField] private float speed; // скорость снаряда
	[SerializeField] private float damage; // наносимый урон
	[SerializeField] private string[] tagList; // фильтр по тегам
	[SerializeField] private LayerMask layers; // или по слоям

	public void SetDirection(Vector3 direction)
	{
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.gravityScale = 0;
		body.velocity = direction.normalized * speed;
	}

	bool Check(GameObject obj)
	{
		if(((1 << obj.layer) & layers) != 0)
		{
			return true;
		}

		foreach(string t in tagList)
		{
			if(obj.tag == t) return true;
		}

		return false;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger && Check(coll.gameObject))
		{
			HPObject HP = coll.GetComponent<HPObject>();

			if(HP != null)
			{
				HP.Adjust(-damage);
			}
		}

		Destroy(gameObject);
	}
}
