using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    //すべての子オブジェクトを参照
    public GameObject Body;
    public GameObject ArmL;
    public GameObject ArmR;

    //Battle
    Collision Lattach, Rattach;
    Vector3 ArmRtoL, ArmRtoOL, ArmRtoOR;

    //HP
    public GameObject HpBar;
    public float Hp;
    public float Damage = 20;
    Slider _slider;
    public bool bDead;

    public bool Invincible = false;
    public int InvincibleTimerMax = 180;
    public int InvincibleTimer = 0;

    // Use this for initialization
    void Start () {
		Lattach = ArmL.GetComponent<ChopsticksManager> ().attach;
		Rattach = ArmR.GetComponent<ChopsticksManager> ().attach;
        _slider = HpBar.GetComponent<Slider>();
        InvincibleTimer = InvincibleTimerMax;
    }

    // Update is called once per frame
    void Update () {
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
            (Lattach.gameObject.tag == "ArmR" || Lattach.gameObject.tag == "ArmL" || Lattach.gameObject.tag == "Body") &&
            (Rattach.gameObject.tag == "ArmR" || Rattach.gameObject.tag == "ArmL" || Rattach.gameObject.tag == "Body") 
            )
        {
            if (Lattach.gameObject == Rattach.gameObject)
            {
                if (Lattach.gameObject.tag == "Body")
                {
                    Lattach.gameObject.GetComponent<BodyManager>().GetDamage = true;
                }
                else
                {
                    if (Lattach.gameObject.GetComponent<ChopsticksManager>().m_bDead == false)
                    {
                        Lattach.gameObject.GetComponent<ChopsticksManager>().m_bDead = true;
                    }
                }

            }
            else if (Lattach.gameObject.tag == "ArmR" && Rattach.gameObject.tag == "ArmL")
            {
                ArmRtoL = ArmL.GetComponent<ChopsticksManager>().ArmHead - ArmR.GetComponent<ChopsticksManager>().ArmHead;
                ArmRtoOL = Lattach.gameObject.GetComponent<ChopsticksManager>().ArmHead - ArmR.GetComponent<ChopsticksManager>().ArmHead;
                ArmRtoOR = Rattach.gameObject.GetComponent<ChopsticksManager>().ArmHead - ArmR.GetComponent<ChopsticksManager>().ArmHead;

                if (Vector3.Cross(ArmRtoL, ArmRtoOL).y <= 0 &&
                    Vector3.Cross(ArmRtoL, ArmRtoOR).y <= 0 &&
                    Vector3.Dot(ArmRtoL, ArmRtoOL) > 0 &&
                    Vector3.Dot(ArmRtoL, ArmRtoOR) > 0
                )
                {
                    if (Lattach.gameObject.GetComponent<ChopsticksManager>().m_bDead == false)
                    {
                        Lattach.gameObject.GetComponent<ChopsticksManager>().m_bDead = true;
                    }
                    if (Rattach.gameObject.GetComponent<ChopsticksManager>().m_bDead == false)
                    {
                        Rattach.gameObject.GetComponent<ChopsticksManager>().m_bDead = true;
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
            if (InvincibleTimer<=0)
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

        if (Hp > _slider.maxValue)
        {
            // 最大を超えたら0に戻す
            Hp = _slider.maxValue;
        }

        if (Hp <= _slider.minValue)
        {
            // 最大を超えたら0に戻す
            Hp = _slider.minValue;
            bDead = true;
        }

        // HPゲージに値を設定
        _slider.value = Hp + 0.01f;
    }

    void Dead()
    {
        Body.SetActive(false);
    }
}
