using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_UI : MonoBehaviour
{
	private int BallCount1;
	private int BallCount2;
	private bool P1_1, P1_2, P1_3, P2_1, P2_2, P2_3;

	// サウンド
	private AudioSource sound01;        // 効果音 1

	// Use this for initialization
	void Start()
	{
		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
	}

	// Update is called once per frame
	void Update()
	{
		Image BallUI1;
		Image BallUI2;

		BallCount1 = GameObject.Find("Player1").GetComponent<PlayerManager>().BallCount;
		BallCount2 = GameObject.Find("Player2").GetComponent<PlayerManager>().BallCount;

		switch (BallCount1)
		{
			case 1:

				BallUI1 = GameObject.Find("P1_Ball_UI1").GetComponent<Image>();

				BallUI1.color = new Color(255, 255, 255, 255);


				if (P1_1 == false)
				{
					sound01.PlayOneShot(sound01.clip);
					P1_1 = true;
				}

				break;

				
			case 2:
				BallUI1 = GameObject.Find("P1_Ball_UI2").GetComponent<Image>();

				BallUI1.color = new Color(255, 255, 255, 255);

				if (P1_2 == false)
				{
					sound01.PlayOneShot(sound01.clip);
					P1_2 = true;
				}

				break;

			case 3:
				BallUI1 = GameObject.Find("P1_Ball_UI3").GetComponent<Image>();

				BallUI1.color = new Color(255, 255, 255, 255);

				if (P1_3 == false)
				{
					sound01.PlayOneShot(sound01.clip);
					P1_3 = true;
				}

				break;
		}

		switch (BallCount2)
		{
			case 1:

				BallUI2 = GameObject.Find("P2_Ball_UI1").GetComponent<Image>();

				BallUI2.color = new Color(255, 255, 255, 255);

				if (P2_1 == false)
				{
					sound01.PlayOneShot(sound01.clip);
					P2_1 = true;
				}

				break;


			case 2:
				BallUI2 = GameObject.Find("P2_Ball_UI2").GetComponent<Image>();

				BallUI2.color = new Color(255, 255, 255, 255);

				if (P2_2 == false)
				{
					sound01.PlayOneShot(sound01.clip);
					P2_2 = true;
				}

				break;

			case 3:
				BallUI2 = GameObject.Find("P2_Ball_UI3").GetComponent<Image>();

				BallUI2.color = new Color(255, 255, 255, 255);

				if (P2_3 == false)
				{
					sound01.PlayOneShot(sound01.clip);
					P2_3 = true;
				}

				break;
		}
	}
}
