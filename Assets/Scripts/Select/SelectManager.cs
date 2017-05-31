using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
	GameObject Cursor1, Cursor2;

	// Use this for initialization
	void Start()
	{
		// カーソルオブジェクト情報取得
		Cursor1 = GameObject.Find("Cursor1");
		Cursor2 = GameObject.Find("Cursor2");
	}

	// Update is called once per frame
	void Update()
	{
		// カーソルコントロール情報取得
		CursorControl CursorControl1 = Cursor1.GetComponent<CursorControl>();
		CursorControl CursorControl2 = Cursor2.GetComponent<CursorControl>();

		// キャラクター決定フラグが両プレイヤーオンだったらシーン遷移
		if (CursorControl1.CharSelectFlag == true & CursorControl2.CharSelectFlag == true)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				FadeManager.Instance.LoadScene("GameScene", 1.0f);
			}
		}	

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
