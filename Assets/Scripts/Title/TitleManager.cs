using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	private bool LoadFlag;


	// Use this for initialization
	void Start()
	{
		LoadFlag = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Fire1") & LoadFlag == false)
		{
			FadeManager.Instance.LoadScene("SelectScene", 1.0f);

			LoadFlag = true;
		}

		// エスケープキーが入力されたらアプリを終了する
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}
}
