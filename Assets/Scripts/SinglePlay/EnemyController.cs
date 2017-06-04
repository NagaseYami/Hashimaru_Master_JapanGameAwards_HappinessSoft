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

	public Transform target; //プレイヤーの位置
	
	//Attack
	public float CloseSpeed = 100;
	public float OpenSpeed = 50;
	private bool CloseFlag = false;
	private bool OpenFlag = false;
	private float AlreadyRotation;

	// 徘徊行動
	private float speed = 5.0f;
	private float rotationSmooth = 1.0f;
	private Vector3 targetPosition;
	
	// 追跡行動
	private float changeTargetSqrDistance = 3.0f;
	private Transform player;
	private float ChaseDistance;


	//=============================================================================
	// 初期化
	//=============================================================================
	void Start()
	{
		// 始めにプレイヤーの位置を取得できるようにする
		player = GameObject.FindWithTag("Player").transform;

		// 目的地を設定
		targetPosition = GetRandomPositionOnLevel();
	}

	//=============================================================================
	// 更新
	//=============================================================================
	void Update()
	{
		// 追跡距離を測る
		ChaseDistance = Vector3.Distance(player.transform.position, transform.position);


		//=============================================================================
		// エネミーの行動選択
		//=============================================================================
		
		// 追跡行動
		if (ChaseDistance <= 25.0f)
		{
			Chase();
		}
		//// 徘徊行動
		//else
		//{
		//	Loitering();
		//}
	}

	//=============================================================================
	// 追跡行動
	//=============================================================================
	void Chase()
	{
		// プレイヤーの方向を向く
		Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

		// 前方に進む
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	//=============================================================================
	// 一定の範囲内のランダムな座標を決定
	//=============================================================================
	Vector3 GetRandomPositionOnLevel()
	{
		float levelSize = 13.0f;
		return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
	}


	//=============================================================================
	// 徘徊行動
	//=============================================================================
	void Loitering()
	{
		// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);

		if (sqrDistanceToTarget < changeTargetSqrDistance)
		{
			targetPosition = GetRandomPositionOnLevel();
		}

		// 目標地点の方向を向く
		Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

		// 前方に進む
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

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