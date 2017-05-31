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

	private RectTransform CursorObj;    // カーソル情報
	private AudioSource sound01;        // 効果音 カーソル
	private AudioSource sound02;        // 効果音 決定
	private AudioSource sound03;        // 効果音 キャンセル

	public bool CharSelectFlag;		// キャラクター選択フラグ
	
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

		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound03 = audioSources[2];
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
		// 決定フラグがオフだったら更新
		if (CharSelectFlag == false)
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

					// 効果音 カーソル音 再生
					sound01.PlayOneShot(sound01.clip);
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

					// 効果音 カーソル音 再生
					sound01.PlayOneShot(sound01.clip);
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

					// 効果音 カーソル音 再生
					sound01.PlayOneShot(sound01.clip);
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

					// 効果音 カーソル音 再生
					sound01.PlayOneShot(sound01.clip);
				}
			}

			// カウンタ加算
			if (JoystickCnt < JoyCntMax)
			{
				JoystickCnt++;
			}
		}

		// 決定フラグがオフだったら
		if (CharSelectFlag == false)
		{
			// Aボタンが押されたら
			if (Input.GetButtonDown("Fire" + GamePadNum))
			{
				// 効果音 決定 再生
				sound02.PlayOneShot(sound02.clip);

				// フラグオン
				CharSelectFlag = true;

				// カーソルのスケールを元のサイズに戻す
				CursorObj.localScale = new Vector3(0.6f, 0.6f, CursorObj.localScale.z);
			}
		}

		// 決定フラグがオンだったら
		if (CharSelectFlag == true)
		{
			// Bボタンが押されたら
			if (Input.GetButtonDown("Back" + GamePadNum))
			{
				// 効果音 決定 再生
				sound03.PlayOneShot(sound03.clip);

				// フラグオフ
				CharSelectFlag = false;
			}
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
			Debug.Log("P1:" + CharaType1 + " P2:" + CharaType2);
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
		// 決定フラグがオフだったら
		if(CharSelectFlag == false)
		{
			// フラグをオフに
			if (CursorObj.localScale.x <= 0.6 & CursorObj.localScale.y <= 0.6)
			{
				ScalingFlag = false;
			}

			// フラグをオンに
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
}
