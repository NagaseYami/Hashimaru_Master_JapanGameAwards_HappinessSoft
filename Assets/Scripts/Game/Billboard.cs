using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

	public Transform targetToFace;
	public bool isAutoFace = true;
	Quaternion adjustEuler = Quaternion.Euler (0, 0, 0);
	private Transform BodyPos;
	private Transform TextPos;
	private GameObject Character;
	public GameObject Player;
	private TextMesh Ptext;
	private TextMesh Stext;
	private bool UP_P;
	private bool UP_S;
	private bool PFlag;
	private bool SFlag;

	// Use this for initialization
	void Start()
	{
		if (Player.transform.Find("Dog").gameObject.activeSelf != false)
		{
			Character = Player.transform.Find("Dog").gameObject;
		}
		else if (Player.transform.Find("Elephants").gameObject.activeSelf != false)
		{
			Character = Player.transform.Find("Elephants").gameObject;
		}
		else if (Player.transform.Find("Giraffe").gameObject.activeSelf != false)
		{
			Character = Player.transform.Find("Giraffe").gameObject;
		}
		else if (Player.transform.Find("Mouse").gameObject.activeSelf != false)
		{
			Character = Player.transform.Find("Mouse").gameObject;
		}
		else
		{
			Debug.Log("Cant find Character!");
		}

		BodyPos = Character.transform.Find("Body").GetComponent<Transform>();
		TextPos = GetComponent<Transform>();

		if (targetToFace == null && isAutoFace)
		{
			var mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			targetToFace = mainCameraObject.transform;
		}

		Ptext = Player.transform.Find("P_text").transform.Find("PowerText").GetComponent<TextMesh>();
		Stext = Player.transform.Find("P_text").transform.Find("SpeedText").GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update()
	{
		if (targetToFace != null)
		{
			transform.rotation = targetToFace.rotation;
			transform.rotation *= adjustEuler;
		}

		TextPos.transform.localPosition = BodyPos.transform.localPosition * 50;
		
		// パワーアップ
		if (Player.GetComponent<PlayerManager>().bPowerUp == true & PFlag == false)
		{
			// パワーテキスト
			Ptext.color = new Color(Ptext.color.r, Ptext.color.g, Ptext.color.b, 1);

			PFlag = true;

			// スピードアップしているかどうか
			if (Player.GetComponent<PlayerManager>().bSpeedUp == true & UP_P == false & UP_S == false)
			{
				// スピードテキスト
				Stext.transform.localPosition = new Vector3(Stext.transform.localPosition.x, 0.15f, Stext.transform.localPosition.z);

				// スピードフラグ
				UP_S = true;
			}
		}
		else if (Player.GetComponent<PlayerManager>().bPowerUp == false)
		{
			PFlag = false;
		}

		// スピードアップ
		if (Player.GetComponent<PlayerManager>().bSpeedUp == true & SFlag == false)
		{
			// スピードテキスト
			Stext.color = new Color(Stext.color.r, Stext.color.g, Stext.color.b, 1);

			SFlag = true;

			// パワーアップしているかどうか
			if (Player.GetComponent<PlayerManager>().bPowerUp == true & UP_P == false & UP_S == false)
			{
				// パワーテキスト
				Ptext.transform.localPosition = new Vector3(Ptext.transform.localPosition.x, 0.15f, Ptext.transform.localPosition.z);

				// パワーフラグ
				UP_P = true;
			}
		}
		else if (Player.GetComponent<PlayerManager>().bSpeedUp == false)
		{
			SFlag = false;
		}

		// パワーテキスト
		if (Ptext.color.a > 0)
		{
			Ptext.color = new Color(Ptext.color.r, Ptext.color.g, Ptext.color.b, Ptext.color.a - 0.003f);
			
		}
		else if (Ptext.color.a <= 0)
		{
			UP_P = false;
			Ptext.transform.localPosition = new Vector3(Ptext.transform.localPosition.x, 0.13f, Ptext.transform.localPosition.z);
		}


		// スピードテキスト
		if (Stext.color.a > 0)
		{
			Stext.color = new Color(Stext.color.r, Stext.color.g, Stext.color.b, Stext.color.a - 0.003f);
		}
		else if (Stext.color.a <= 0)
		{
			UP_S = false;
			Stext.transform.localPosition = new Vector3(Stext.transform.localPosition.x, 0.13f, Stext.transform.localPosition.z);
		}
	}
}