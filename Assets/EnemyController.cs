using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public Transform target; //プレイヤーの位置
	static Vector3 pos;
	NavMeshAgent agent;

	float agentToPatroldistance;
	float agentToTargetdistance;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}


	void Start()
	{
		DoPatrol();
	}


	void Update()
	{
		//Agentと目的地の距離
		agentToPatroldistance = Vector3.Distance(this.agent.transform.position, pos);

		//Agentとプレイヤーの距離
		agentToTargetdistance = Vector3.Distance(this.agent.transform.position, target.transform.position);

		//プレイヤーとAgentの距離が近くになると追跡開始
		if (agentToTargetdistance <= 20.0f)
		{
			DoTracking();

		}//プレイヤーと目的地の距離が近くになると次の目的地をランダム指定
		else if (agentToPatroldistance < 30.0f)
		{
			DoPatrol();
		}

	}

	//エージェントが向かう先をランダムに指定するメソッド
	public void DoPatrol()
	{
		var x = Random.Range(-50.0f, 50.0f);
		var z = Random.Range(-50.0f, 50.0f);
		pos = new Vector3(x, 0, z);
		agent.SetDestination(pos);
	}

	//targetに指定したplayerを追いかけるメソッド
	public void DoTracking()
	{
		pos = target.position;
		agent.SetDestination(pos);
	}
}