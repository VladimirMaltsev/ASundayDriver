/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

using UnityEngine;
using System.Collections;

public class HPObject : MonoBehaviour {

	[SerializeField] private float _HP = 100;
	[SerializeField] private bool autoDestroy;

	public float currentHP
	{
		get{ return _HP; }
	}

	public void Adjust(float value)
	{
		_HP += value;

		if(autoDestroy && _HP <= 0)
		{
			Destroy(gameObject);
		}
	}
}
