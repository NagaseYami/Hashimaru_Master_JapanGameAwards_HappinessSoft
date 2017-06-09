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
	}

	// Update is called once per frame
	void Update()
	{
        if (!Ball.activeSelf)
        {
            BallRespawnTimer++;
        }
        else
        {
            BallRespawnTimer = 0;
        }

        if (BallRespawnTimer >= BallRespawnTime*60)
        {
            Ball.SetActive(true);
        }

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

    void LateUpdate()
    {
        
    }
}
