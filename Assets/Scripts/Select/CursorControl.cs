using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorControl : MonoBehaviour
{
	public int GamePadNum;		// ゲームパッドの割り当て数
	private int JoystickCnt;	// ジョイスティックカウンタ
	public int JoyCntMax;       // ジョイススティックカウンタ最大量

	public float UpMax;
	public float DownMax;
	public float LeftAndRightMax;

	public int CursorMove;      // カーソルの移動量

	public float Scaling;       // 拡縮率
	private bool ScalingFlag;    // 拡縮フラグ

	static public CHARATYPE CharaType1 = CHARATYPE.DOG;  // プレイヤーのタイプ1
	static public CHARATYPE CharaType2 = CHARATYPE.GIRFFE;  // プレイヤーのタイプ2

	private RectTransform CursorObj;	// カーソル情報

	// キャラクタータイプ
	public enum CHARATYPE
	{
		DOG = 0,	// イヌ
		GIRFFE,		// キリン
		MOUSE,		// ネズミ
		ELEPHANTS	// ゾウ
	}

	//// ステージタイプ
	//public enum STAGETYPE
	//{
	//	EASY_DOG = 0,   // イヌ		(簡単)
	//	EASY_GIRFFE,    // キリン	(簡単)
	//	EASY_MOUSE,     // ネズミ	(簡単)
	//	EASY_ELEPHANTS, // ゾウ		(簡単)
	//	HARD_DOG,       // イヌ		(難しい)
	//	HARD_GIRFFE,    // キリン	(難しい)
	//	HARD_MOUSE,     // ネズミ	(難しい)
	//	HARD_ELEPHANTS  // ゾウ		(難しい)
	//}

	// Use this for initialization
	void Start()
	{
		CursorObj = GameObject.Find("Cursor" + GamePadNum).GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		CharacterSelect();
		SetCharaType();
		CursorScaling();
	}

	//=============================================================================
	// キャラクターセレクト関数
	//=============================================================================
	private void CharacterSelect()
	{
		// キャラセレクトカーソル移動処理
		if (JoystickCnt >= JoyCntMax)
		{
			// 上方向
			if (Input.GetAxisRaw("Vertical" + GamePadNum) > 0.1f)
			{
				if (CursorObj.localPosition.y >= UpMax)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y - CursorMove, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y + CursorMove, CursorObj.localPosition.z);
				}

				JoystickCnt = 0;    // カウンタ初期化
			}

			// 下方向
			if (Input.GetAxisRaw("Vertical" + GamePadNum) < -0.1f)
			{
				if (CursorObj.localPosition.y <= DownMax)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y + CursorMove, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x, CursorObj.localPosition.y - CursorMove, CursorObj.localPosition.z);
				}

				JoystickCnt = 0;    // カウンタ初期化
			}

			// 左方向
			if (Input.GetAxisRaw("Horizontal" + GamePadNum) > 0.1f)
			{
				if (CursorObj.localPosition.x <= -LeftAndRightMax)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x + CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x - CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}

				JoystickCnt = 0;    // カウンタ初期化
			}

			// 右方向
			if (Input.GetAxisRaw("Horizontal" + GamePadNum) < -0.1f)
			{
				if (CursorObj.localPosition.x >= LeftAndRightMax)
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x - CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}
				else
				{
					CursorObj.localPosition = new Vector3(CursorObj.localPosition.x + CursorMove, CursorObj.localPosition.y, CursorObj.localPosition.z);
				}

				JoystickCnt = 0;    // カウンタ初期化
			}
		}

		// カウンタ加算
		if (JoystickCnt < JoyCntMax)
		{
			JoystickCnt++;
		}
	}

	//=============================================================================
	// キャラクタータイプ設定関数
	//=============================================================================
	private void SetCharaType()
	{
		// キャラクタータイプを設定
		switch (GamePadNum)
		{
			case 1:	// プレイヤー1
				if (CursorObj.localPosition.x <= -LeftAndRightMax & CursorObj.localPosition.y >= UpMax)
				{
					CharaType1 = CHARATYPE.DOG;
				}

				if (CursorObj.localPosition.x >= LeftAndRightMax & CursorObj.localPosition.y >= UpMax)
				{
					CharaType1 = CHARATYPE.GIRFFE;
				}

				if (CursorObj.localPosition.x <= -LeftAndRightMax & CursorObj.localPosition.y <= DownMax)
				{
					CharaType1 = CHARATYPE.MOUSE;
				}

				if (CursorObj.localPosition.x >= LeftAndRightMax & CursorObj.localPosition.y <= DownMax)
				{
					CharaType1 = CHARATYPE.ELEPHANTS;
				}
				break;

			case 2:// プレイヤー2
				if (CursorObj.localPosition.x <= -LeftAndRightMax & CursorObj.localPosition.y >= UpMax)
				{
					CharaType2 = CHARATYPE.DOG;
				}

				if (CursorObj.localPosition.x >= LeftAndRightMax & CursorObj.localPosition.y >= UpMax)
				{
					CharaType2 = CHARATYPE.GIRFFE;
				}

				if (CursorObj.localPosition.x <= -LeftAndRightMax & CursorObj.localPosition.y <= DownMax)
				{
					CharaType2 = CHARATYPE.MOUSE;
				}

				if (CursorObj.localPosition.x >= LeftAndRightMax & CursorObj.localPosition.y <= DownMax)
				{
					CharaType2 = CHARATYPE.ELEPHANTS;
				}
				break;
		}

		// キャラクタータイプ デバッグ用
		if (Input.GetKeyDown(KeyCode.O))
		{
			Debug.Log("P1:" + CharaType1 + "P2:" + CharaType2);
		}
	}

	//=============================================================================
	// キャラクタータイプ1取得関数
	//=============================================================================
	public static CHARATYPE GetCharaType1()
	{
		return CharaType1;
	}

	//=============================================================================
	// キャラクタータイプ2取得関数
	//=============================================================================
	public static CHARATYPE GetCharaType2()
	{
		return CharaType2;
	}


	//=============================================================================
	// カーソル拡大縮小関数
	//=============================================================================
	public void CursorScaling()
	{
		
		if(CursorObj.localScale.x <= 0.6 & CursorObj.localScale.y <= 0.6)
		{
			ScalingFlag = false;
		}

		if (CursorObj.localScale.x >= 0.68 & CursorObj.localScale.y >= 0.68)
		{
			ScalingFlag = true;
		}

		// フラグがオフだったら
		if (ScalingFlag == false)
		{
			CursorObj.localScale = new Vector3(CursorObj.localScale.x + Scaling, CursorObj.localScale.y + Scaling, CursorObj.localScale.z);
		}

		// フラグがオンだったら
		if (ScalingFlag == true)
		{
			CursorObj.localScale = new Vector3(CursorObj.localScale.x - Scaling, CursorObj.localScale.y - Scaling, CursorObj.localScale.z);
		}
	}
}
