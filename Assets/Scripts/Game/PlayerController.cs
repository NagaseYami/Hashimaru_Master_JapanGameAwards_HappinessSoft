using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerManager;
    public GameObject Body;
    public GameObject ArmL;
    public GameObject ArmR;

	public int GamePadNum;

    //Controller
    public float RotateSpeed;
    public float MoveSpeed;
    private Rigidbody rb;

    //Attack    
    public float CloseSpeed = 100;
    public float OpenSpeed = 50;
    private bool CloseFlag = false;
    private bool OpenFlag = false;
    private float AlreadyRotation;

    // Use this for initialization
    void Start()
	{
		rb = Body.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
        if (!PlayerManager.GetComponent<PlayerManager>().bDead)
        {
            Controller();
            Attack();
        }        
    }

    void Controller()
    {
		// 上方向
		if (Input.GetAxisRaw("Vertical" + GamePadNum) > 0)
		{
			rb.AddForce(Body.transform.forward * MoveSpeed);
		}

		// 下方向
		if (Input.GetAxisRaw("Vertical" + GamePadNum) < 0)
		{
			rb.AddForce(-Body.transform.forward * MoveSpeed);
		}

		Vector3 TurnLeft = new Vector3(0.0f, -RotateSpeed, 0.0f);
		Vector3 TurnRight = new Vector3(0.0f, RotateSpeed, 0.0f);

		Quaternion deltaRotation;

		// 左方向
		if (Input.GetAxisRaw("Horizontal" + GamePadNum) > 0.01)
		{
			deltaRotation = Quaternion.Euler(TurnLeft * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			ArmR.transform.RotateAround(Body.transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
			ArmL.transform.RotateAround(Body.transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
		}

		// 右方向
		if (Input.GetAxisRaw("Horizontal" + GamePadNum) < -0.01)
		{
			deltaRotation = Quaternion.Euler(TurnRight * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			ArmR.transform.RotateAround(Body.transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
			ArmL.transform.RotateAround(Body.transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
		}
    }
    void Attack()
    {
        if (Input.GetButtonDown("Fire" + GamePadNum) && !CloseFlag && !OpenFlag)
        {
            CloseFlag = true;
        }

        if (CloseFlag)
        {
            ArmR.transform.RotateAround(Body.transform.position, Vector3.up, -CloseSpeed * Time.deltaTime);
            ArmL.transform.RotateAround(Body.transform.position, Vector3.up, CloseSpeed * Time.deltaTime);
            AlreadyRotation += CloseSpeed * Time.deltaTime;
            ArmR.GetComponent<ChopsticksManager>().bHasamu = true;
            ArmL.GetComponent<ChopsticksManager>().bHasamu = true;
            if (AlreadyRotation >= 30)
            {
                ArmR.transform.eulerAngles = new Vector3(0, Body.transform.eulerAngles.y - 30, 0);
                ArmL.transform.eulerAngles = new Vector3(0, Body.transform.eulerAngles.y + 30, 0);
                CloseFlag = false;
                OpenFlag = true;
                AlreadyRotation = 0;
                ArmR.GetComponent<ChopsticksManager>().bHasamu = false;
                ArmL.GetComponent<ChopsticksManager>().bHasamu = false;
            }
        }

        if (OpenFlag)
        {
            ArmR.transform.RotateAround(Body.transform.position, Vector3.up, OpenSpeed * Time.deltaTime);
            ArmL.transform.RotateAround(Body.transform.position, Vector3.up, -OpenSpeed * Time.deltaTime);
            AlreadyRotation += OpenSpeed * Time.deltaTime;
            if (AlreadyRotation >= 30)
            {
                ArmR.transform.eulerAngles = new Vector3(0, Body.transform.eulerAngles.y, 0);
                ArmL.transform.eulerAngles = new Vector3(0, Body.transform.eulerAngles.y, 0);
                OpenFlag = false;
                AlreadyRotation = 0;
            }
        }
    }
}