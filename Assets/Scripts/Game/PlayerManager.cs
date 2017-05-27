using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    //KM_Mathを導入
    GameObject KM_Math;
    //すべての子オブジェクトを参照
    public GameObject Body;
    public GameObject ArmL;
    public GameObject ArmR;

    //Battle
    public int RespawnTimeForL = 5, RespawnTimeForR = 5;
    Collision Lattach, Rattach;
    Vector3 ArmRtoL, ArmRtoOL, ArmRtoOR;
    static int TimerForL = 0, TimerForR = 0;

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
		Lattach = ArmL.GetComponent<Attach> ().attach;
		Rattach = ArmR.GetComponent<Attach> ().attach;
        _slider = HpBar.GetComponent<Slider>();
        KM_Math = GameObject.Find("KM_Math");

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
        Lattach = ArmL.GetComponent<Attach>().attach;
        Rattach = ArmR.GetComponent<Attach>().attach;

        if (ArmL.GetComponent<Renderer>().enabled != false && ArmR.GetComponent<Renderer>().enabled != false &&
            Lattach != null && Rattach != null &&
            ArmL.GetComponent<ArmHasamuFlag>().bHasamu && ArmR.GetComponent<ArmHasamuFlag>().bHasamu &&
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
                    Lattach.gameObject.GetComponent<Renderer>().enabled = false;
                    Lattach.gameObject.GetComponent<Collider>().enabled = false;
                }
            }

            else if (Lattach.gameObject.tag == "ArmR" && Rattach.gameObject.tag == "ArmL")
            {
                ArmRtoL = ArmL.GetComponent<GetArmHead>().ArmHead - ArmR.GetComponent<GetArmHead>().ArmHead;
                ArmRtoOL = Lattach.gameObject.GetComponent<GetArmHead>().ArmHead - ArmR.GetComponent<GetArmHead>().ArmHead;
                ArmRtoOR = Rattach.gameObject.GetComponent<GetArmHead>().ArmHead - ArmR.GetComponent<GetArmHead>().ArmHead;

                if (Vector3.Cross(ArmRtoL, ArmRtoOL).y <= 0 &&
                    Vector3.Cross(ArmRtoL, ArmRtoOR).y <= 0 &&
                    Vector3.Dot(ArmRtoL, ArmRtoOL) > 0 &&
                    Vector3.Dot(ArmRtoL, ArmRtoOR) > 0
                )
                {
                    Lattach.gameObject.GetComponent<Renderer>().enabled = false;
                    Lattach.gameObject.GetComponent<Collider>().enabled = false;
                    Rattach.gameObject.GetComponent<Renderer>().enabled = false;
                    Rattach.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }

        if (!ArmL.GetComponent<Renderer>().enabled)
        {
            TimerForL++;
        }
        if (TimerForL >= 60 * RespawnTimeForL)
        {
            TimerForL = 0;
            ArmL.GetComponent<Renderer>().enabled = true;
            ArmL.GetComponent<Collider>().enabled = true;
        }

        if (!ArmR.GetComponent<Renderer>().enabled)
        {
            TimerForR++;
        }
        if (TimerForR >= 60 * RespawnTimeForR)
        {
            TimerForR = 0;
            ArmR.GetComponent<Renderer>().enabled = true;
            ArmR.GetComponent<Collider>().enabled = true;
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

           if (KM_Math.GetComponent<KM_Math>().KM_ChangeFlagTimer(6))
           {
               Body.GetComponent<Renderer>().enabled = false;
           }
           else
           {
               Body.GetComponent<Renderer>().enabled = true;
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