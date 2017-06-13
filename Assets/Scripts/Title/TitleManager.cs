using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	private bool LoadFlag;

	private AudioSource sound01;        // 効果音 決定

	// Use this for initialization
	void Start()
	{
		LoadFlag = false;

		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[1];
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Fire1") & LoadFlag == false)
		{
			FadeManager.Instance.LoadScene("SelectScene", 1.0f);

			// 決定音再生
			sound01.PlayOneShot(sound01.clip);

			LoadFlag = true;
		}

		// エスケープキーが入力されたらアプリを終了する
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}
}
