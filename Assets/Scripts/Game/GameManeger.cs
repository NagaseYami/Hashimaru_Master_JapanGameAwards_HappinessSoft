using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public GameObject Player1, Player2;
    public GameObject Player1WinText, Player2WinText;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
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
