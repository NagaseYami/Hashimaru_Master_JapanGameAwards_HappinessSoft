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
	public bool countDown = true;
	public bool PlayGameBGM;

	// サウンド
	private AudioSource sound01;        // 効果音 スタート1
	private AudioSource sound02;        // 効果音 スタート2

	// Use this for initialization
	void Start()
	{
		text = GameObject.Find(text_num + "_text").GetComponent<Text>();
		marginFlag = false;

		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
	}

	// Update is called once per frame
	void Update()
	{
		bool stopFlag = GameObject.Find("GameManager").GetComponent<GameManager>().stopTime;

		bool FadeFlag = GameObject.Find("FadeManager").GetComponent<FadeManager>().isFading;

		if(FadeFlag == false)
		{
			countDown = false;
			Time.timeScale = 0;
		}

		if (stopFlag == false)
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

						sound02.PlayOneShot(sound02.clip);

						PlayGameBGM = true;
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

						sound01.PlayOneShot(sound01.clip);
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

							sound01.PlayOneShot(sound01.clip);

							count = 0;
						}
					}

					margin = 40;
				}
			}
		}
	}

	public void SetPlayFlag(bool Flag)
	{
		PlayGameBGM = Flag;
	}
}