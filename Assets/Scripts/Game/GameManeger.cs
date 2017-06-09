using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
	GameObject Player1, Player2;
	GameObject Player1WinText, Player2WinText;
	GameObject Ball;
	public int WinBallNum = 2;
	public int BallRespawnTime = 10;
	int BallRespawnTimer = 0;
	public CursorControl.CHARATYPE CharaType1, CharaType2;  // キャラクタータイプ

	// ポーズ画面用
	private bool stopTime = false;
	public GameObject ui;
	private GameObject obj;
	private int GamePadNum = 0;
	private RectTransform arrow;
	private int nCnt = 18;
	public bool GameStopFlag = false;

	// サウンド
	private AudioSource sound01;        // 効果音 stage1_BGM
	private AudioSource sound02;        // 効果音 stage2_BGM
	private AudioSource sound03;        // 効果音 ポーズオン
	private AudioSource sound04;        // 効果音 ポーズ解除オン

	// Use this for initialization
	void Start()
	{
		Ball = GameObject.Find("Ball").gameObject;
        Ball.SetActive(false);
		if (Ball == null) { Debug.Log("Cant find Ball!"); }
		Player1 = GameObject.Find("Player1").gameObject;
		if (Player1 == null) { Debug.Log("Cant find Palyer1!"); }
		Player2 = GameObject.Find("Player2").gameObject;
		if (Player2 == null) { Debug.Log("Cant find Player2!"); }

		Player1WinText = Player1.transform.Find("Canvas").gameObject.transform.Find("WinText").gameObject;
		if (Player1WinText == null) { Debug.Log("Cant find Player1WinText!"); }
		Player2WinText = Player2.transform.Find("Canvas").gameObject.transform.Find("WinText").gameObject;
		if (Player2WinText == null) { Debug.Log("Cant find Player2WinText!"); }
        
        // キャラクターのタイプを取得
        CharaType1 = CursorControl.GetCharaType1();
		CharaType2 = CursorControl.GetCharaType2();

        switch (CharaType1)
		{
			case CursorControl.CHARATYPE.DOG:
				Player1.transform.Find("Dog").gameObject.SetActive(true);
				Player1.transform.Find("Giraffe").gameObject.SetActive(false);
				Player1.transform.Find("Mouse").gameObject.SetActive(false);
				Player1.transform.Find("Elephants").gameObject.SetActive(false);

				break;

			case CursorControl.CHARATYPE.GIRFFE:
				Player1.transform.Find("Dog").gameObject.SetActive(false);
				Player1.transform.Find("Giraffe").gameObject.SetActive(true);
				Player1.transform.Find("Mouse").gameObject.SetActive(false);
				Player1.transform.Find("Elephants").gameObject.SetActive(false);

				break;

			case CursorControl.CHARATYPE.MOUSE:
				Player1.transform.Find("Dog").gameObject.SetActive(false);
				Player1.transform.Find("Giraffe").gameObject.SetActive(false);
				Player1.transform.Find("Mouse").gameObject.SetActive(true);
				Player1.transform.Find("Elephants").gameObject.SetActive(false);

				break;

			case CursorControl.CHARATYPE.ELEPHANTS:
				Player1.transform.Find("Dog").gameObject.SetActive(false);
				Player1.transform.Find("Giraffe").gameObject.SetActive(false);
				Player1.transform.Find("Mouse").gameObject.SetActive(false);
				Player1.transform.Find("Elephants").gameObject.SetActive(true);

				break;
		}

		switch (CharaType2)
		{
			case CursorControl.CHARATYPE.DOG:
				Player2.transform.Find("Dog").gameObject.SetActive(true);
				Player2.transform.Find("Giraffe").gameObject.SetActive(false);
				Player2.transform.Find("Mouse").gameObject.SetActive(false);
				Player2.transform.Find("Elephants").gameObject.SetActive(false);

				break;

			case CursorControl.CHARATYPE.GIRFFE:
				Player2.transform.Find("Dog").gameObject.SetActive(false);
				Player2.transform.Find("Giraffe").gameObject.SetActive(true);
				Player2.transform.Find("Mouse").gameObject.SetActive(false);
				Player2.transform.Find("Elephants").gameObject.SetActive(false);

				break;

			case CursorControl.CHARATYPE.MOUSE:
				Player2.transform.Find("Dog").gameObject.SetActive(false);
				Player2.transform.Find("Giraffe").gameObject.SetActive(false);
				Player2.transform.Find("Mouse").gameObject.SetActive(true);
				Player2.transform.Find("Elephants").gameObject.SetActive(false);

				break;

			case CursorControl.CHARATYPE.ELEPHANTS:
				Player2.transform.Find("Dog").gameObject.SetActive(false);
				Player2.transform.Find("Giraffe").gameObject.SetActive(false);
				Player2.transform.Find("Mouse").gameObject.SetActive(false);
				Player2.transform.Find("Elephants").gameObject.SetActive(true);

				break;
		}

		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound03 = audioSources[2];
		sound04 = audioSources[3];
	}

	// Update is called once per frame
	void Update()
	{
		Pause();

		// キャラクタータイプ デバッグ用
		if (Input.GetKeyDown(KeyCode.O))
		{
			Debug.Log("P1:" + CharaType1 + " P2:" + CharaType2);
		}

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

        if (Player1.GetComponent<PlayerManager>().bDead ||
            Player2.GetComponent<PlayerManager>().BallCount >= WinBallNum
            )
        {
            Player2WinText.SetActive(true);
        }
		if (Player2.GetComponent<PlayerManager>().bDead ||
            Player1.GetComponent<PlayerManager>().BallCount >= WinBallNum)
		{
			Player1WinText.SetActive(true);
		}

	}

	void Pause()
	{

		if (Input.GetButtonDown("Start1") & GameStopFlag == false & GamePadNum != 2)
		{
			GamePadNum = 1;
			GameStop();
		}
		else if (Input.GetButtonDown("Start2") & GameStopFlag == false & GamePadNum != 1)
		{
			GamePadNum = 2;
			GameStop();
		}

		if (stopTime)
		{// stopTimeフラグがtrueだったら
			if (nCnt >= 18)
			{
				// 上方向
				if (Input.GetAxisRaw("Vertical" + GamePadNum) > 0.1f)
				{
					nCnt = 0;
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
				if (Input.GetAxisRaw("Vertical" + GamePadNum) < -0.1f)
				{
					nCnt = 0;
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
			if (Input.GetButtonDown("Fire" + GamePadNum))
			{
				if (arrow.localPosition.y >= 195.0f)
				{
					GameStop();
				}
				else if (arrow.localPosition.y <= -205.0f)
				{
					GameStop();
					FadeManager.Instance.LoadScene("TitleScene", 1.0f);
					GameStopFlag = true;
				}
				else
				{
					GameStop();
					FadeManager.Instance.LoadScene("SelectScene", 1.0f);
					GameStopFlag = true;
				}
			}
			if (nCnt <= 18)
			{
				nCnt++;
			}
		}

		if (!Ball.activeSelf)
		{
			BallRespawnTimer++;
		}
		else
		{
			BallRespawnTimer = 0;
		}

		if (BallRespawnTimer >= BallRespawnTime * 60)
		{
			Ball.SetActive(true);
		}
	}

	void GameStop()
	{
		if (!stopTime)
		{
			obj = Instantiate(ui);
			arrow = GameObject.Find("Arrow").GetComponent<RectTransform>();

			Time.timeScale = 0;

			// 効果音 再生
			sound03.PlayOneShot(sound03.clip);
		}
		else
		{
			GamePadNum = 0;
			Time.timeScale = 1;
			Destroy(obj);

			// 効果音 再生
			sound04.PlayOneShot(sound04.clip);
		}
		stopTime = !stopTime;
	}
}
