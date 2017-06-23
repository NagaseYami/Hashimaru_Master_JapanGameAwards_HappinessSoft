using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
	private Image Logo;
	private bool FadeFlag;
	private int Count;
	private GameObject Dog;
	private GameObject Giraffe;
	private GameObject Ball;
	private GameObject Body0, Body1;
	private GameObject ArmL0, ArmR0;
	private GameObject ArmL1, ArmR1;
	private bool CloseFlag0;
	private bool OpenFlag0;
	private bool CloseFlag1;
	private bool OpenFlag1;

	private float ArmRote;
	private bool HasamuFlag;
	private bool FadeStart;
	private bool FadeStop;
	private bool LoadFlag;

	// Use this for initialization
	void Start()
	{
		Cursor.visible = false;

		Dog = GameObject.Find("Dog");
		Body0 = Dog.transform.Find("Body").gameObject;
		ArmL0 = Dog.transform.Find("ArmL").gameObject;
		ArmR0 = Dog.transform.Find("ArmR").gameObject;

		Giraffe = GameObject.Find("Giraffe");
		Body1 = Giraffe.transform.Find("Body").gameObject;
		ArmL1 = Giraffe.transform.Find("ArmL").gameObject;
		ArmR1 = Giraffe.transform.Find("ArmR").gameObject;

		Ball = GameObject.Find("Ball").gameObject;

		Logo = GameObject.Find("TeamLogo").GetComponent<Image>();
		Logo.color = new Color(Logo.color.r, Logo.color.g, Logo.color.b, 0);

		ArmRote = 1.5f;
	}

	// Update is called once per frame
	void Update()
	{
		// エスケープキーが入力されたらアプリを終了する
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}

		if (FadeStart == true)
		{
			// タイトルロゴをフェードイン
			if (FadeFlag == false)
			{
				Logo.color = new Color(Logo.color.r, Logo.color.g, Logo.color.b, Logo.color.a + 0.02f);
			}
			// タイトルロゴをフェードアウト
			else if (FadeFlag == true)
			{
				Logo.color = new Color(Logo.color.r, Logo.color.g, Logo.color.b, Logo.color.a - 0.02f);

				if(Logo.color.a <= 0)
				{
					FadeStop = true;
				}
			}

			// 一定時間たったらフェードイン→フェードアウトへ
			if (Count >= 100 & FadeFlag == false)
			{
				FadeFlag = true;
				Count = 0;
			}
			else if (Count < 100 & FadeFlag == false)
			{
				Count++;
			}
		}

		// ボールを動かす
		Ball.transform.localPosition = new Vector3(Ball.transform.localPosition.x - 0.21f, Ball.transform.localPosition.y, Ball.transform.localPosition.z);


		// イヌ動かす
		Dog.transform.localPosition = new Vector3(Dog.transform.localPosition.x - 0.22f, Dog.transform.localPosition.y, Dog.transform.localPosition.z);
		
		if(Dog.transform.localPosition.x <= -15)
		{
			FadeStart = true;
		}

		// はさみ始める
		if (CloseFlag0 == false & HasamuFlag == false)
		{
			CloseFlag0 = true;
		}

		// はさむ
		if (CloseFlag0)
		{
			ArmL0.transform.Rotate(0, ArmRote, 0);
			ArmR0.transform.Rotate(0, -ArmRote, 0);

			if (ArmL0.transform.localEulerAngles.y >= 30)
			{
				OpenFlag0 = true;
				CloseFlag0 = false;
			}
		}
		// ひらく
		else if (OpenFlag0)
		{
			ArmL0.transform.Rotate(0, -ArmRote, 0);
			ArmR0.transform.Rotate(0, ArmRote, 0);

			if (ArmL0.transform.localEulerAngles.y >= 355)
			{
				OpenFlag0 = false;
				CloseFlag0 = true;
			}
		}

		// キリン動かす
		Giraffe.transform.localPosition = new Vector3(Giraffe.transform.localPosition.x - 0.17f, Giraffe.transform.localPosition.y, Giraffe.transform.localPosition.z);

		// はさみ始める
		if (CloseFlag1 == false & HasamuFlag == false)
		{
			CloseFlag1 = true;
			HasamuFlag = true;
		}

		// はさむ
		if (CloseFlag1)
		{
			ArmL1.transform.Rotate(0, ArmRote, 0);
			ArmR1.transform.Rotate(0, -ArmRote, 0);

			if (ArmL1.transform.localEulerAngles.y >= 30)
			{
				OpenFlag1 = true;
				CloseFlag1 = false;
			}
		}
		// ひらく
		else if (OpenFlag1)
		{
			ArmL1.transform.Rotate(0, -ArmRote, 0);
			ArmR1.transform.Rotate(0, ArmRote, 0);

			if (ArmL1.transform.localEulerAngles.y >= 355)
			{
				OpenFlag1 = false;
				CloseFlag1 = true;
			}
		}

		// シーン遷移
		if(FadeStop == true & LoadFlag == false)
		{
			FadeManager.Instance.LoadScene("TitleScene", 0.1f);
			LoadFlag = true;
		}
	}
}
