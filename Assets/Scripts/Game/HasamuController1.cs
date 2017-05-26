using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasamuController1 : MonoBehaviour
{
	public GameObject ArmL,ArmR;
	public float CloseSpeed,OpenSpeed;

	private bool CloseFlag,OpenFlag;
	private float AlreadyRotation;
	// Use this for initialization
	void Start()
	{
		CloseFlag = false;
		OpenFlag = false;
		AlreadyRotation = 0;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.GetButtonDown("Fire1") && !CloseFlag && !OpenFlag)
		{
			CloseFlag = true;
		}

		if (CloseFlag)
		{
			ArmR.transform.RotateAround(transform.position, Vector3.up, -CloseSpeed * Time.deltaTime);
			ArmL.transform.RotateAround(transform.position, Vector3.up, CloseSpeed * Time.deltaTime);
			AlreadyRotation += CloseSpeed * Time.deltaTime;
			ArmR.GetComponent<ArmHasamuFlag>().bHasamu = true;
			ArmL.GetComponent<ArmHasamuFlag>().bHasamu = true;
			if (AlreadyRotation >= 30)
			{
				ArmR.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 30, 0);
				ArmL.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 30, 0);
				CloseFlag = false;
				OpenFlag = true;
				AlreadyRotation = 0;
				ArmR.GetComponent<ArmHasamuFlag>().bHasamu = false;
				ArmL.GetComponent<ArmHasamuFlag>().bHasamu = false;
			}
		}

		if (OpenFlag)
		{
			ArmR.transform.RotateAround(transform.position, Vector3.up, OpenSpeed * Time.deltaTime);
			ArmL.transform.RotateAround(transform.position, Vector3.up, -OpenSpeed * Time.deltaTime);
			AlreadyRotation += OpenSpeed * Time.deltaTime;
			if (AlreadyRotation >= 30)
			{
				ArmR.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				ArmL.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				OpenFlag = false;
				AlreadyRotation = 0;
			}
		}
	}
}
//transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);