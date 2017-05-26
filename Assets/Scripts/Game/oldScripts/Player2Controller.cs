using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
	public float RotateSpeed;
	public float MoveSpeed;
	public GameObject ArmL, ArmR;

	private Rigidbody rb;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		// 上方向
		if (Input.GetAxisRaw("Vertical2") > 0)
		{
			rb.AddForce(transform.forward * MoveSpeed);
		}

		// 下方向
		if (Input.GetAxisRaw("Vertical2") < 0)
		{
			rb.AddForce(-transform.forward * MoveSpeed);
		}

		Vector3 TurnLeft = new Vector3(0.0f, -RotateSpeed, 0.0f);
		Vector3 TurnRight = new Vector3(0.0f, RotateSpeed, 0.0f);

		Quaternion deltaRotation;

		// 左方向
		if (Input.GetAxisRaw("Horizontal2") > 0.01)
		{
			deltaRotation = Quaternion.Euler(TurnLeft * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			ArmR.transform.RotateAround(transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
			ArmL.transform.RotateAround(transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
		}

		// 右方向
		if (Input.GetAxisRaw("Horizontal2") < -0.01)
		{
			deltaRotation = Quaternion.Euler(TurnRight * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			ArmR.transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
			ArmL.transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
		}
	}
}