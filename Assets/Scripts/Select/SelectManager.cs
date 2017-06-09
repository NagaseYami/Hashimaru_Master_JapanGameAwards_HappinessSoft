using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
	GameObject Cursor1, Cursor2;
	private bool LoadFlag;
	private bool ReleaseFlag;        // ボタンを押していないフラグ

	// Use this for initialization
	void Start()
	{
		// カーソルオブジェクト情報取得
		Cursor1 = GameObject.Find("Cursor1");
		Cursor2 = GameObject.Find("Cursor2");

		LoadFlag = false;
		ReleaseFlag = true;
	}

	// Update is called once per frame
	void Update()
	{
		// カーソルコントロール情報取得
		CursorControl CursorControl1 = Cursor1.GetComponent<CursorControl>();
		CursorControl CursorControl2 = Cursor2.GetComponent<CursorControl>();

		// キャラクター決定フラグが両プレイヤーオンだったらシーン遷移
		if (CursorControl1.CharSelectFlag == true & CursorControl2.CharSelectFlag == true & LoadFlag == false)
		{
			if (Input.GetButtonDown("Fire1") & LoadFlag == false & ReleaseFlag == false)
			{
				FadeManager.Instance.LoadScene("GameScene", 1.0f);

				LoadFlag = true;
			}
			else
			{
				ReleaseFlag = false;
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
