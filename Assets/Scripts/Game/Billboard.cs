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
	}
}