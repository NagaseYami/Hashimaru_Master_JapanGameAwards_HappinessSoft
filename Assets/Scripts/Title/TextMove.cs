using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;

public class TextMove : MonoBehaviour
{
	private const float a_move = 0.01f;

	private Text text;
	private float a_color;
	private bool a_flag;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<Text>();

		a_color = 0;
		a_flag = false;
	}

	// Update is called once per frame
	void Update()
	{
		// アルファ値を徐々に減算
		if (a_flag == false)
		{
			a_color -= a_move;

			if (a_color <= 0)
			{
				a_flag = true;	// フラグオン
			}
		}

		// アルファ値を徐々に加算
		else if(a_flag == true)
		{
			a_color += a_move;

			if(a_color >= 1)
			{
				a_flag = false; // フラグオフ
			}
		}

		text.color = new Color(text.color.r, text.color.g, text.color.b, a_color);
	}
}
