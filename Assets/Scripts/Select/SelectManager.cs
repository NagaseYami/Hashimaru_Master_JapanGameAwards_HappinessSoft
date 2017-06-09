using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
	GameObject Cursor1, Cursor2;
	private bool LoadFlag;
	private bool ReleaseFlag;        // ボタンを押していないフラグ

	// ポーズ画面用
	private bool stopTime = false;
	public GameObject ui;
	private GameObject obj;
	private int GamePadNum = 0;
	private RectTransform arrow;
	private int nCnt = 18;
	public bool GameStopFlag = false;

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

	void Pause()
	{
		GamePadNum = 1;
		GameStop();

		if (stopTime)
		{// stopTimeフラグがtrueだったら

			// Aボタンが押されたら
			if (Input.GetButtonDown("Fire" + GamePadNum))
			{

			}
		}
	}

	void GameStop()
	{
		if (!stopTime)
		{
			obj = Instantiate(ui);
			arrow = GameObject.Find("Arrow").GetComponent<RectTransform>();

			Time.timeScale = 0;

		}
		else
		{
			GamePadNum = 0;
			Time.timeScale = 1;
			Destroy(obj);
		}
		stopTime = !stopTime;
	}
}
