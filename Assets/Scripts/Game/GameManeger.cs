using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// エスケープキーが入力されたらアプリを終了する
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}

		// シーンのリセット
		if (Input.GetKeyDown(KeyCode.P))
		{
			// シーンを読み込む
			SceneManager.LoadScene("GameScene");
		}
	}
}
