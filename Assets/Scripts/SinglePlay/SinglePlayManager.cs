using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayManager : MonoBehaviour
{
	public GameObject Player1;  //プレイヤー
	public CursorControl.CHARATYPE CharaType1;      // プレイヤーのタイプ

	public GameObject Enemy1, Enemy2, Enemy3, Enemy4;
	public GameObject CloneEnemy1, CloneEnemy2, CloneEnemy3, CloneEnemy4;

	private int i;
	private int Cnt = 1;

	// Use this for initialization
	void Start()
	{
		CloneEnemy1 = Enemy1;
		CloneEnemy2 = Enemy2;
		CloneEnemy3 = Enemy3;
		CloneEnemy4 = Enemy4;

		// キャラクターのタイプを取得
		CharaType1 = CursorControl.GetCharaType1();

		// キャラクタータイプ デバッグ用
		if (Input.GetKeyDown(KeyCode.O))
		{
			Debug.Log("P1:" + CharaType1);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (i >= 100)
		{
			switch (Cnt)
			{
				case 1:
					CloneEnemy1 = Instantiate(Enemy1, new Vector3(Random.Range(-20.0f, 20.0f), -1.15f, Random.Range(-20.0f, 20.0f)), transform.rotation);

					Cnt++;

					i = 0;

					break;

				case 2:
					CloneEnemy2 = Instantiate(Enemy2, new Vector3(Random.Range(-20.0f, 20.0f), -1.15f, Random.Range(-20.0f, 20.0f)), transform.rotation);

					Cnt++;

					i = 0;

					break;

				case 3:
					CloneEnemy4 = Instantiate(Enemy3, new Vector3(Random.Range(-20.0f, 20.0f), -1.15f, Random.Range(-20.0f, 20.0f)), transform.rotation);

					Cnt++;

					i = 0;

					break;
			}
		}

		if (Cnt <= 3)
		{
			i++;
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
			SceneManager.LoadScene("TestScene");
		}
	}
}
