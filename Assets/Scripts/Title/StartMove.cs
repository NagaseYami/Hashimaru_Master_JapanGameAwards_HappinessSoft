using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;

public class StartMove : MonoBehaviour
{
	private bool ScalingFlag;    // 拡縮フラグ
	private bool TitleFlag;     // 決定フラグ
	public float Scaling;       // 拡縮率

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		// 決定フラグがオフだったら
		if (TitleFlag == false)
		{
			// フラグをオフに
			if (transform.localScale.x <= 0.3 & transform.localScale.y <= 0.3)
			{
				ScalingFlag = false;
			}

			// フラグをオンに
			if (transform.localScale.x >= 0.35 & transform.localScale.y >= 0.35)
			{
				ScalingFlag = true;
			}

			// フラグがオフだったら
			if (ScalingFlag == false)
			{
				transform.localScale = new Vector3(transform.localScale.x + Scaling, transform.localScale.y + Scaling, transform.localScale.z);
			}

			// フラグがオンだったら
			if (ScalingFlag == true)
			{
				transform.localScale = new Vector3(transform.localScale.x - Scaling, transform.localScale.y - Scaling, transform.localScale.z);
			}
		}
	}
}
