using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public GameObject Player1, Player2;
    public GameObject Player1WinText, Player2WinText;
	public CursorControl.CHARATYPE CharaType1, CharaType2;	// キャラクタータイプ

	// Use this for initialization
	void Start()
	{
		// キャラクターのタイプを取得
		CharaType1 = CursorControl.GetCharaType1();
		CharaType2 = CursorControl.GetCharaType2();
	}

	// Update is called once per frame
	void Update()
	{
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

        if (Player1.GetComponent<PlayerManager>().bDead)
        {
            Player2WinText.SetActive(true);
        }
		if (Player2.GetComponent<PlayerManager>().bDead)
		{
			Player1WinText.SetActive(true);
		}

	}

    void LateUpdate()
    {
        
    }
}
