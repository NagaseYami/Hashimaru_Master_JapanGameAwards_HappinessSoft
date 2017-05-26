using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorControl : MonoBehaviour
{
	private const int CursorMove = 200; // カーソルの移動量

	private RectTransform CursorObj;	// カーソル情報

	private int JoystickCnt;			// ジョイスティックカウンタ
	private PLAYERTYPE PlayerType;		// プレイヤーのタイプ

	// プレイヤータイプ
	private enum PLAYERTYPE
	{
		DOG = 0,	// イヌ
		GIRFFE,		// キリン
		MOUSE,		// ネズミ
		ELEPHANTS	// ゾウ
	}

	// ステージタイプ
	private enum STAGETYPE
	{
		EASY_DOG = 0,   // イヌ		(簡単)
		EASY_GIRFFE,    // キリン	(簡単)
		EASY_MOUSE,     // ネズミ	(簡単)
		EASY_ELEPHANTS, // ゾウ		(簡単)
		HARD_DOG,       // イヌ		(難しい)
		HARD_GIRFFE,    // キリン	(難しい)
		HARD_MOUSE,     // ネズミ	(難しい)
		HARD_ELEPHANTS  // ゾウ		(難しい)
	}

	// Use this for initialization
	void Start()
	{
		CursorObj = GameObject.Find("Cursor").GetComponent<RectTransform>();

		JoystickCnt	=	0;					// ジョイスティックカウンタ
		PlayerType	=	PLAYERTYPE.DOG;		// プレイヤータイプ
	}

	// Update is called once per frame
	void Update()
	{
		CharacterSelect();
	}


	//=============================================================================
	// キャラクターセレクト関数
	//=============================================================================
	private void CharacterSelect()
	{
		// キャラセレクトカーソル移動処理
		if (JoystickCnt >= 20)
		{
			// 上方向
			if (Input.GetAxisRaw("Vertical1") > 0)
			{
				if (CursorObj.localPosition.y == 100)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y - CursorMove, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y + CursorMove, CursorObj.localPosition.z);
				}
			}

			// 下方向
			else if (Input.GetAxisRaw("Vertical1") < 0)
			{
				if (CursorObj.localPosition.y == -100)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y + CursorMove, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y - CursorMove, CursorObj.localPosition.z);
				}
			}

			// 左方向
			else if (Input.GetAxisRaw("Horizontal1") > 0)
			{
				if (CursorObj.localPosition.x == -100)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x + CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x - CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}
			}

			// 右方向
			else if (Input.GetAxisRaw("Horizontal1") < 0)
			{
				if (CursorObj.localPosition.x == 100)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x - CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x + CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}
			}

			JoystickCnt = 0;    // カウンタ初期化

			// キャラクタータイプを設定
			if (CursorObj.localPosition.x == -100 & CursorObj.localPosition.y == 100)
			{
				PlayerType = PLAYERTYPE.DOG;
			}

			else if (CursorObj.localPosition.x == 100 & CursorObj.localPosition.y == 100)
			{
				PlayerType = PLAYERTYPE.GIRFFE;
			}

			else if (CursorObj.localPosition.x == -100 & CursorObj.localPosition.y == -100)
			{
				PlayerType = PLAYERTYPE.MOUSE;
			}

			else if (CursorObj.localPosition.x == 100 & CursorObj.localPosition.y == -100)
			{
				PlayerType = PLAYERTYPE.ELEPHANTS;
			}
		}

		JoystickCnt++;  // カウンタ加算

		// キャラクタータイプ デバッグ用
		if(Input.GetKeyDown(KeyCode.O))
		{
			Debug.Log(PlayerType);
		}
	}
}
