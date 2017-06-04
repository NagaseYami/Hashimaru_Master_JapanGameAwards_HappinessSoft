using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayManager : MonoBehaviour
{
	public GameObject Player1;  //プレイヤー
	public CursorControl.CHARATYPE CharaType1;      // プレイヤーのタイプ

	public GameObject Enemy1, Enemy2, Enemy3 , Enemy4;

	private EnemyController EnemyCon;

	private int i;
	private int Cnt = 1;

	// Use this for initialization
	void Start()
	{
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
					Enemy1 = Instantiate(Enemy1, new Vector3(Random.Range(-20.0f, 20.0f), 20.0f, Random.Range(-20.0f, 20.0f)), transform.rotation);

					Cnt++;

					i = 0;

					break;

				case 2:
					Enemy2 = Instantiate(Enemy2, new Vector3(Random.Range(-20.0f, 20.0f), 20.0f, Random.Range(-20.0f, 20.0f)), transform.rotation);

					Cnt++;

					i = 0;

					break;

				case 3:
					Enemy2 = Instantiate(Enemy2, new Vector3(Random.Range(-20.0f, 20.0f), 20.0f, Random.Range(-20.0f, 20.0f)), transform.rotation);

					Cnt++;

					i = 0;

					break;
			}

			if (Enemy1 != null)
			{
				EnemyCon = Enemy1.GetComponentInChildren<EnemyController>();

				EnemyCon.target = Player1.transform;
			}

			if (Enemy2 != null)
			{
				EnemyCon = Enemy2.GetComponentInChildren<EnemyController>();

				EnemyCon.target = Player1.transform;
			}

			if (Enemy3 != null)
			{
				EnemyCon = Enemy2.GetComponentInChildren<EnemyController>();

				EnemyCon.target = Player1.transform;
			}
		}
		
		if(Cnt <= 3)
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
