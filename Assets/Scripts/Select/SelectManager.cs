using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
	GameObject Cursor1, Cursor2;
	public bool LoadFlag = false;
	private bool ReleaseFlag;        // ボタンを押していないフラグ

	// サウンド
	private AudioSource sound01;        // 効果音 カーソル
	private AudioSource sound02;        // 効果音 決定
	private AudioSource sound03;        // 効果音 キャンセル
	private AudioSource sound04;        // 効果音 オープン
	private AudioSource sound05;        // 効果音 クローズ

	public enum STAGETYPE
	{
		STAGE01 = 0,
		STAGE02
	}

	//// ステージセレクト画面用
	private bool stopTime = false;
	public GameObject ui;
	private GameObject obj;
	private int GamePadNum = 1;
	public bool GameStopFlag = false;
	private RectTransform stage01;
	private RectTransform stage02;
	private int Count;

	// ポーズ画面用
	public bool stopTimeSelect = false;
	public GameObject uiSelect;
	private GameObject objSelect;
	private int GamePadNumSelect = 1;
	private RectTransform arrow;
	private int nCntSelect = 18;
	public bool GameStopFlagSelect = false;
	public bool ButtonReleaseSelect = false;
	private bool SelectPuseFlag = false;
	private bool FadeFlag;
	private bool FadeFlagSelect;

	private bool stage01Flag = true;
	private bool stage02Flag = false;
	private float ScalingMove = 0.05f;       // 拡縮率
	private bool PauseFlag;
	private bool ScalingFlag;     // 拡縮フラグ
	static STAGETYPE StageType;

	// Use this for initialization
	void Start()
	{
		Cursor.visible = false;
		FadeFlagSelect = GameObject.Find("FadeManager").GetComponent<FadeManager>().isFading;

		// カーソルオブジェクト情報取得
		Cursor1 = GameObject.Find("Cursor1");
		Cursor2 = GameObject.Find("Cursor2");

		LoadFlag = false;
		ReleaseFlag = true;
		GameStopFlagSelect = false;
		SelectPuseFlag = false;
		ButtonReleaseSelect = false;
		FadeFlag = false;
		FadeFlagSelect = false;

	StageType = STAGETYPE.STAGE01;

		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[1];
		sound02 = audioSources[2];
		sound03 = audioSources[3];
		sound04 = audioSources[4];
		sound05 = audioSources[5];
	}

	private void Update()
	{
		// エスケープキーが入力されたらアプリを終了する
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}

		if (LoadFlag == false)
		{
			if (SelectPuseFlag == false & FadeFlagSelect == false & ButtonReleaseSelect == false)
			{
				Pause();
			}

			if (FadeFlagSelect == false)
			{
				PauseSelect();
			}

			// ボタンがはなされているか
			if (Input.GetButtonDown("Fire" + GamePadNum))
			{
				ButtonReleaseSelect = false;
			}
		}
	}

	// Update is called once per frame
	private void FixedUpdate ()
	{
		// カーソルコントロール情報取得
		CursorControl CursorControl1 = Cursor1.GetComponent<CursorControl>();
		CursorControl CursorControl2 = Cursor2.GetComponent<CursorControl>();

		// キャラクター決定フラグが両プレイヤーオン
		if (CursorControl1.CharSelectFlag == true & CursorControl2.CharSelectFlag == true)
		{
			if (ReleaseFlag == false)
			{
				if (PauseFlag == false)
				{
					GameStop();

					PauseFlag = true;
				}
			}
			else
			{
				ReleaseFlag = false;
			}
		}

#if DEBUG
		// シーンのリセット
		if (Input.GetKeyDown(KeyCode.P))
		{
			// シーンを読み込む
			SceneManager.LoadScene("SelectScene");
		}
#endif
	}

	void Pause()
	{
		if (stopTime)
		{// stopTimeフラグがtrueだったら

			if (stage01Flag == true)
			{
				if(stage01.localScale.x >= 5 & stage01.localScale.y >= 5)
				{
					ScalingFlag = true;
				}

				if (stage01.localScale.x <= 4 & stage01.localScale.y <= 4)
				{
					ScalingFlag = false;
				}

				if(ScalingFlag == true)
				{
					stage01.localScale = new Vector3(stage01.localScale.x - ScalingMove, stage01.localScale.y - ScalingMove, stage01.localScale.z);
				}

				if (ScalingFlag == false)
				{
					stage01.localScale = new Vector3(stage01.localScale.x + ScalingMove, stage01.localScale.y + ScalingMove, stage01.localScale.z);
				}

				stage02.localScale = new Vector3(4, 4, stage01.localScale.z);
			}

			if (stage02Flag == true)
			{

				if (stage02.localScale.x >= 5 & stage02.localScale.y >= 5)
				{
					ScalingFlag = true;
				}

				if (stage02.localScale.x <= 4 & stage02.localScale.y <= 4)
				{
					ScalingFlag = false;
				}

				if (ScalingFlag == true)
				{
					stage02.localScale = new Vector3(stage02.localScale.x - ScalingMove, stage02.localScale.y - ScalingMove, stage02.localScale.z);
				}

				if (ScalingFlag == false)
				{
					stage02.localScale = new Vector3(stage02.localScale.x + ScalingMove, stage02.localScale.y + ScalingMove, stage02.localScale.z);
				}

				stage01.localScale = new Vector3(4, 4, stage01.localScale.z);

			}

			if (Count >= 3)
			{
				// 左方向
				if ((Input.GetAxisRaw("Horizontal1") > 0.1f) | (Input.GetAxisRaw("Horizontal2") > 0.1f))
				{
					// フラグがオフの場合
					if (stage01Flag == false)
					{
						stage01Flag = true;
						stage02Flag = false;

						// 効果音 再生
						sound01.PlayOneShot(sound01.clip);
					}
					
					Count = 0;

					StageType = STAGETYPE.STAGE01;
				}

				// 右方向
				if ((Input.GetAxisRaw("Horizontal1") < -0.1f) | (Input.GetAxisRaw("Horizontal2") < -0.1f))
				{
					// フラグがオフの場合
					if (stage02Flag == false)
					{
						stage01Flag = false;
						stage02Flag = true;

						// 効果音 再生
						sound01.PlayOneShot(sound01.clip);
					}

					Count = 0;

					StageType = STAGETYPE.STAGE02;
				}

				// Aボタンが押されたら
				if (Input.GetButtonDown("Fire1") | Input.GetButtonDown("Fire2") & LoadFlag == false & FadeFlagSelect == false)
				{
					GameStop();
					FadeManager.Instance.LoadScene("GameScene", 1.0f);
					GameStopFlag = true;
					LoadFlag = true;

					// 効果音 再生
					sound02.PlayOneShot(sound02.clip);
				}

				// Bボタンが押されたら
				if (Input.GetButtonDown("Back1") | Input.GetButtonDown("Back2") & LoadFlag == false)
				{
					GameStop();

					// カーソルコントロール情報取得
					CursorControl CursorControl1 = Cursor1.GetComponent<CursorControl>();
					CursorControl CursorControl2 = Cursor2.GetComponent<CursorControl>();

					CursorControl1.CharSelectFlag = false;
					CursorControl2.CharSelectFlag = false;
					PauseFlag = false;

					// 効果音 再生
					sound03.PlayOneShot(sound03.clip);
				}
			}

			if(Count < 3)
			{
				Count++;
			}
		}
	}

	void GameStop()
	{
		if (!stopTime)
		{
			obj = Instantiate(ui);

			stage01 = obj.transform.Find("stage1").GetComponent<RectTransform>();
			stage02 = obj.transform.Find("stage2").GetComponent<RectTransform>();

			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
			Destroy(obj);
		}
		stopTime = !stopTime;
	}

	public static STAGETYPE GetStageType()
	{
		return StageType;
	}



	void PauseSelect()
	{
		if (!stopTimeSelect)
		{
			if (Input.GetButtonDown("Start1") & GameStopFlagSelect == false)
			{
				GamePadNumSelect = 1;
				GameStopSelect();
			}
			else if (Input.GetButtonDown("Start2") & GameStopFlagSelect == false)
			{
				GamePadNumSelect = 2;
				GameStopSelect();
			}
		}

		else if (stopTimeSelect)
		{// stopTimeフラグがtrueだったら

			if (Input.GetButtonDown("Start1") & GameStopFlagSelect == false & GamePadNumSelect == 1)
			{
				GamePadNumSelect = 1;
				GameStopSelect();
			}
			else if (Input.GetButtonDown("Start2") & GameStopFlagSelect == false & GamePadNumSelect == 2)
			{
				GamePadNumSelect = 2;
				GameStopSelect();
			}

			if (nCntSelect >= 18)
			{
				// 上方向
				if (Input.GetAxisRaw("Vertical" + GamePadNumSelect) > 0.1f)
				{
					// カーソル音
					sound01.PlayOneShot(sound01.clip);

					nCntSelect = 0;
					if (arrow.localPosition.y >= 195.0f)
					{
						arrow.localPosition = new Vector3(arrow.localPosition.x, -205.0f, arrow.localPosition.z);
					}

					else
					{
						arrow.localPosition = new Vector3(arrow.localPosition.x, arrow.localPosition.y + 200.0f, arrow.localPosition.z);
					}
				}

				// 下方向
				if (Input.GetAxisRaw("Vertical" + GamePadNumSelect) < -0.1f)
				{
					// カーソル音
					sound01.PlayOneShot(sound01.clip);

					nCntSelect = 0;
					if (arrow.localPosition.y <= -205.0f)
					{
						arrow.localPosition = new Vector3(arrow.localPosition.x, 195.0f, arrow.localPosition.z);
					}

					else
					{
						arrow.localPosition = new Vector3(arrow.localPosition.x, arrow.localPosition.y - 200.0f, arrow.localPosition.z);
					}
				}
			}

			// Aボタンが押されたら
			if (Input.GetButtonDown("Fire" + GamePadNumSelect) & LoadFlag == false )
			{
				if (arrow.localPosition.y >= 195.0f)
				{
					GameStopSelect();
				}
				else if (arrow.localPosition.y <= -205.0f)
				{
					GameStopSelect();
					FadeManager.Instance.LoadScene("TitleScene", 1.0f);
					GameStopFlagSelect = true;
					LoadFlag = true;
				}
				else
				{
					GameStopSelect();
					FadeManager.Instance.LoadScene("SelectScene", 1.0f);
					GameStopFlagSelect = true;
					LoadFlag = true;
				}

				ButtonReleaseSelect = true;
			}
			if (nCntSelect <= 18)
			{
				nCntSelect++;
			}
		}
	}

	void GameStopSelect()
	{
		if (!stopTimeSelect)
		{
			objSelect = Instantiate(uiSelect);
			arrow = GameObject.Find("Arrow").GetComponent<RectTransform>();

			Time.timeScale = 0;

			// 効果音 再生
			sound04.PlayOneShot(sound04.clip);

			SelectPuseFlag = true;
		}
		else
		{
			GamePadNumSelect = 1;
			Time.timeScale = 1;
			Destroy(objSelect);

			// 効果音 再生
			sound05.PlayOneShot(sound05.clip);

			SelectPuseFlag = false;
		}
		stopTimeSelect = !stopTimeSelect;
	}

}
