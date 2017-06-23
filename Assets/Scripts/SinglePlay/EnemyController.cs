using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	// ゲームオブジェクト
	public GameObject Body;
	public GameObject ArmL;
	public GameObject ArmR;
	private GameObject Character;

	private Rigidbody EnemyRB;

	//Attack
	public float CloseSpeed = 100;
	public float OpenSpeed = 50;
	private bool CloseFlag = false;
	private bool OpenFlag = false;
	private float AlreadyRotation;

	// 徘徊行動
	private float speed = 4.0f;
	private float rotationSmooth = 1.0f;
	private Vector3 targetPosition;

	// 追跡行動
	private float changeTargetSqrDistance = 3.0f;
	public GameObject player;
	private float ChaseDistance;


	//=============================================================================
	// 初期化
	//=============================================================================
	void Start()
	{
		if (player.transform.Find("Dog").gameObject.activeSelf != false)
		{
			Character = player.transform.Find("Dog").gameObject;
		}
		else if (player.transform.Find("Elephants").gameObject.activeSelf != false)
		{
			Character = player.transform.Find("Elephants").gameObject;
		}
		else if (player.transform.Find("Giraffe").gameObject.activeSelf != false)
		{
			Character = player.transform.Find("Giraffe").gameObject;
		}
		else if (player.transform.Find("Mouse").gameObject.activeSelf != false)
		{
			Character = player.transform.Find("Mouse").gameObject;
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

		// ボディのRigidbody情報を取得
		EnemyRB = transform.Find("Body").GetComponent<Rigidbody>();

		// 目的地を設定
		targetPosition = GetRandomPositionOnLevel();
	}

	//=============================================================================
	// 更新
	//=============================================================================
	void FixedUpdate()
	{
		// 追跡距離を測る
		ChaseDistance = Vector3.Distance(EnemyRB.transform.position, transform.position);


		//=============================================================================
		// エネミーの行動選択
		//=============================================================================

		Debug.Log(ChaseDistance);

		// 追跡行動
		if (ChaseDistance <= 15.0f)
		{
			Chase();
		}
		// 徘徊行動
		else
		{
			Loitering();
		}
	}

	//=============================================================================
	// 追跡行動
	//=============================================================================
	void Chase()
	{
		// プレイヤーの方向を向く
		Vector3 relativePos = EnemyRB.transform.position - transform.position;
		
		Quaternion targetRotation = Quaternion.LookRotation(relativePos - EnemyRB.transform.position);
		targetRotation = Quaternion.Slerp(EnemyRB.transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
		EnemyRB.MoveRotation(targetRotation);

		// 前方に進む
		EnemyRB.MovePosition(EnemyRB.transform.position + EnemyRB.transform.forward * speed * Time.deltaTime);
	}

	//=============================================================================
	// 一定の範囲内のランダムな座標を決定
	//=============================================================================
	Vector3 GetRandomPositionOnLevel()
	{
		float levelSize = 12.0f;
		return new Vector3(Random.Range(-levelSize, levelSize), transform.localPosition.y, Random.Range(-levelSize, levelSize));
	}


	//=============================================================================
	// 徘徊行動
	//=============================================================================
	void Loitering()
	{
		// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(EnemyRB.transform.position - targetPosition);

		if (sqrDistanceToTarget < changeTargetSqrDistance)
		{
			targetPosition = GetRandomPositionOnLevel();
		}

		Debug.Log(targetPosition);

		// 目標地点の方向を向く
		Quaternion targetRotation = Quaternion.LookRotation(targetPosition - EnemyRB.transform.position);
		targetRotation = Quaternion.Slerp(EnemyRB.transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
		EnemyRB.MoveRotation(targetRotation);

		// 前方に進む
		EnemyRB.MovePosition(EnemyRB.transform.position + EnemyRB.transform.forward * speed * Time.deltaTime);
	}


	//=============================================================================
	// はさむ攻撃
	//=============================================================================
	void Attack()
	{
		int Probability = 0;        // 確率

		if (!CloseFlag & !OpenFlag)
		{
			// ランダムで確率を設定
			Probability = (int)Random.Range(1000.0f, 0.0f);
		}

		if (Probability <= 50 & !CloseFlag && !OpenFlag)
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