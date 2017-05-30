using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetButtonDown("Fire1"))
		//{
		//	SceneManager.LoadScene("GameScene");
		//}

		if (Input.GetKeyDown(KeyCode.P))
		{
			SceneManager.LoadScene("SelectScene");
		}

		// エスケープキーが入力されたらアプリを終了する
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}
}
