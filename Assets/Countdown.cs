using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
	Text text;
	int text_num = 3;
	float count = 0;
	float margin = 0;
	bool marginFlag;
	bool countDown;

	// Use this for initialization
	void Start()
	{
		Time.timeScale = 0;

		text = GameObject.Find(text_num + "_text").GetComponent<Text>();
		marginFlag = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (countDown == false)
		{
			margin++;

			if (text_num <= 0)
			{
				text = GameObject.Find("Start_text").GetComponent<Text>();

				text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.04f);

				if (marginFlag == false)
				{
					text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
					marginFlag = true;
				}
				else if (text.color.a <= 0 & marginFlag == true)
				{
					countDown = true;

					Time.timeScale = 1;
				}

				
			}
			else if (text_num > 0 & margin >= 40)
			{
				count++;

				if (marginFlag == false)
				{
					text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
					marginFlag = true;
				}

				if (count >= 40 & marginFlag == true)
				{
					text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.05f);
				}

				if (text.color.a <= 0 & marginFlag == true)
				{
					text_num--;

					if (text_num <= 0)
					{
						marginFlag = false;

					}

					if (text_num > 0)
					{
						text = GameObject.Find(text_num + "_text").GetComponent<Text>();

						text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

						text.gameObject.SetActive(true);

						count = 0;
					}
				}

				margin = 40;
			}
		}
	}
}