using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	//すべての子オブジェクトを参照
	public GameObject Body;
	public GameObject ArmL;
	public GameObject ArmR;

	//Battle
	GameObject Lattach, Rattach;
	Vector3 ArmRtoL, ArmRtoOL, ArmRtoOR;

	//HP
	public float Hp;
	public float Damage;
	public bool bDead;

	public bool Invincible = false;
	public int InvincibleTimerMax = 180;
	public int InvincibleTimer = 0;

	// Use this for initialization
	void Start()
	{
		Lattach = ArmL.GetComponent<ChopsticksManager>().attach;
		Rattach = ArmR.GetComponent<ChopsticksManager>().attach;
		
		InvincibleTimer = InvincibleTimerMax;
	}

	// Update is called once per frame
	void Update()
	{
		if (!bDead)
		{
			Battle();
			HpUpdate();
		}
		else
		{
			Dead();
		}
	}

	void Battle()
	{
		Lattach = ArmL.GetComponent<ChopsticksManager>().attach;
		Rattach = ArmR.GetComponent<ChopsticksManager>().attach;

		if (ArmL.GetComponent<Renderer>().enabled != false && ArmR.GetComponent<Renderer>().enabled != false &&
			Lattach != null && Rattach != null &&
			ArmL.GetComponent<ChopsticksManager>().bHasamu && ArmR.GetComponent<ChopsticksManager>().bHasamu &&
			(Lattach.tag == "ArmR" || Lattach.tag == "ArmL" || Lattach.tag == "Body") &&
			(Rattach.tag == "ArmR" || Rattach.tag == "ArmL" || Rattach.tag == "Body")
			)
		{
			if (Lattach == Rattach)
			{
				if (Lattach.tag == "Body")
				{
					Lattach.GetComponent<BodyManager>().GetDamage = true;
				}
				else
				{
					if (Lattach.GetComponent<ChopsticksManager>().m_bDead == false)
					{
						Lattach.GetComponent<ChopsticksManager>().m_bDead = true;
					}
				}

			}
			else if (Lattach.tag == "ArmR" && Rattach.tag == "ArmL")
			{
				ArmRtoL = ArmL.GetComponent<ChopsticksManager>().ArmHead - ArmR.GetComponent<ChopsticksManager>().ArmHead;
				ArmRtoOL = Lattach.GetComponent<ChopsticksManager>().ArmHead - ArmR.GetComponent<ChopsticksManager>().ArmHead;
				ArmRtoOR = Rattach.GetComponent<ChopsticksManager>().ArmHead - ArmR.GetComponent<ChopsticksManager>().ArmHead;

				if (Vector3.Cross(ArmRtoL, ArmRtoOL).y <= 0 &&
					Vector3.Cross(ArmRtoL, ArmRtoOR).y <= 0 &&
					Vector3.Dot(ArmRtoL, ArmRtoOL) > 0 &&
					Vector3.Dot(ArmRtoL, ArmRtoOR) > 0
				)
				{
					if (Lattach.GetComponent<ChopsticksManager>().m_bDead == false)
					{
						Lattach.GetComponent<ChopsticksManager>().m_bDead = true;
					}
					if (Rattach.GetComponent<ChopsticksManager>().m_bDead == false)
					{
						Rattach.GetComponent<ChopsticksManager>().m_bDead = true;
					}
				}
			}
		}
	}


	void HpUpdate()
	{
		if (Body.GetComponent<BodyManager>().GetDamage && !Invincible)
		{
			Hp -= Damage;
			Invincible = true;
		}

		if (Invincible)
		{

			if (KM_Math.KM_ChangeFlagTimer(6))
			{
				Body.GetComponent<Renderer>().enabled = false;
				ArmL.GetComponent<Renderer>().enabled = false;
				ArmR.GetComponent<Renderer>().enabled = false;
			}
			else
			{
				Body.GetComponent<Renderer>().enabled = true;
				ArmL.GetComponent<Renderer>().enabled = true;
				ArmR.GetComponent<Renderer>().enabled = true;
			}
			InvincibleTimer--;
			if (InvincibleTimer <= 0)
			{
				InvincibleTimer = 0;
				Invincible = false;
				Body.GetComponent<BodyManager>().GetDamage = false;
				InvincibleTimer = InvincibleTimerMax;
			}
		}
		else
		{
			Body.GetComponent<Renderer>().enabled = true;
			ArmL.GetComponent<Renderer>().enabled = true;
			ArmR.GetComponent<Renderer>().enabled = true;
		}

		if (Hp > 100)
		{
			// 最大を超えたら1000に戻す
			Hp = 100;
		}

		if (Hp <= 0)
		{
			// 最小を超えたら0に戻す
			Hp = 0;
			bDead = true;
		}
	}

	void Dead()
	{
		Body.SetActive(false);
	}
}
