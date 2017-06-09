using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    GameObject Character, Body, ArmL, ArmR;

    //Battle
    Collision Lattach, Rattach;
    Vector3 ArmRtoL, ArmRtoOL, ArmRtoOR;

    //状態
    public bool bDead;
<<<<<<< HEAD

    //無敵
=======
>>>>>>> 41b42989693b70a085d3755601732a60643dcf5f
    public bool Invincible = false;

    //ステータス
    float Health, Damage,BeDamage=0.0f;
    public int InvincibleTimerMax = 180;
    int InvincibleTimer = 0;
    Slider HealthBarSlider;

    //Ball
    public int BallCount = 0;


    //Ball
    public int BallCount = 0;


    // Use this for initialization
    void Start () {

        if(transform.Find("Dog").gameObject.activeSelf != false)
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

        Health = Character.GetComponent<CharacterManager>().Health;
        Damage = Character.GetComponent<CharacterManager>().Damage;

        Lattach = ArmL.GetComponent<ChopsticksManager> ().attach;
		Rattach = ArmR.GetComponent<ChopsticksManager> ().attach;
        HealthBarSlider = GameObject.Find("Canvas").gameObject.transform.Find("Slider").GetComponent<Slider>();
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
            (Lattach.gameObject.tag == "ArmR" || Lattach.gameObject.tag == "ArmL" || Lattach.gameObject.tag == "Body" || Lattach.gameObject.tag == "Ball") &&
            (Rattach.gameObject.tag == "ArmR" || Rattach.gameObject.tag == "ArmL" || Rattach.gameObject.tag == "Body" || Rattach.gameObject.tag == "Ball") 
            )
        {
            if (Lattach.gameObject == Rattach.gameObject)
            {
                if (Lattach.gameObject.tag == "Body")
                {
                    Lattach.gameObject.GetComponent<BodyManager>().GetDamage = true;
                    Lattach.gameObject.transform.root.gameObject.transform.root.gameObject.GetComponent<PlayerManager>().TakeDamage(Damage);
                }
                else if (Lattach.gameObject.tag == "Ball")
                {
                    BallCount++;
                    Lattach.gameObject.SetActive(false);
                }
                else if (Lattach.gameObject.tag == "Ball")
                {
                    BallCount++;
                    Lattach.gameObject.SetActive(false);
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
<<<<<<< HEAD
        if (Hp > _slider.maxValue)
        {
            // 最大を超えたら0に戻す
            Hp = _slider.maxValue;
        }

        if (Hp <= _slider.minValue)
        {
            // 最大を超えたら0に戻す
            Hp = _slider.minValue;
=======
        if (Health > HealthBarSlider.maxValue)
        {
            // 最大を超えたら0に戻す
            Health = HealthBarSlider.maxValue;
        }

        if (Health <= HealthBarSlider.minValue)
        {
            // 最大を超えたら0に戻す
            Health = HealthBarSlider.minValue;
>>>>>>> 41b42989693b70a085d3755601732a60643dcf5f
            bDead = true;
        }

        if (Body.GetComponent<BodyManager>().GetDamage && !Invincible)
        {
            Health -= BeDamage;
            Invincible = true;
        }

        if (Invincible && !bDead)
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
                Body.GetComponent<Renderer>().enabled = true;
                ArmL.GetComponent<Renderer>().enabled = true;
                ArmR.GetComponent<Renderer>().enabled = true;
            }
        }     

        // HPゲージに値を設定
        HealthBarSlider.value = Health + 0.01f;
    }

    public void TakeDamage(float l_Damage)
    {
        BeDamage = l_Damage;
    }

    void Dead()
    {
        Body.SetActive(false);
        ArmL.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        ArmR.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
