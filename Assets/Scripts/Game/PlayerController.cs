using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject Character, Body, ArmL, ArmR;
    
	public int GamePadNum;

    //Controller
    float MoveSpeed,RotateSpeed;
    private Rigidbody rb;

    //Attack    
    float CloseSpeed, OpenSpeed;

    private bool CloseFlag = false;
    private bool OpenFlag = false;
    private float AlreadyRotation;

    // Use this for initialization
    void Start()
	{
        if (transform.Find("Dog").gameObject.activeSelf != false)
        {
            Character = transform.Find("Dog").gameObject;
        }
        else if (transform.Find("Elephants").gameObject.activeSelf != false)
        {
            Character = transform.Find("Elephants").gameObject;
        }
        else if (transform.Find("Giraffe").gameObject.activeSelf != false)
        {
            Character = transform.Find("Giraffe").gameObject;
        }
        else if (transform.Find("Mouse").gameObject.activeSelf != false)
        {
            Character = transform.Find("Mouse").gameObject;
        }
        else
        {
            Debug.Log("Cant find Character!");
        }

        Body = Character.transform.Find("Body").gameObject;
        if (Body == null)
        {
            Debug.Log("Cant find Body!");
        }
        ArmL = Character.transform.Find("ArmL").gameObject;
        if (ArmL == null)
        {
            Debug.Log("Cant find ArmL!");
        }
        ArmR = Character.transform.Find("ArmR").gameObject;
        if (ArmR == null)
        {
            Debug.Log("Cant find ArmR!");
        }

        MoveSpeed = Character.GetComponent<CharacterManager>().MoveSpeed;
        RotateSpeed = Character.GetComponent<CharacterManager>().RotateSpeed;
        CloseSpeed = Character.GetComponent<CharacterManager>().CloseSpeed;
        OpenSpeed = Character.GetComponent<CharacterManager>().OpenSpeed;

        rb = Body.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
        if (!gameObject.GetComponent<PlayerManager>().bDead)
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