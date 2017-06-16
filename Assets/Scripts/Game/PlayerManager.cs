using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    GameObject Body, ArmL, ArmR;
    public GameObject Character; 
    public GameObject Effect;
    public GameObject DeadArmD,DeadArmG,DeadArmE,DeadArmM;

    //Battle
    GameObject Lattach, Rattach;
    Vector3 ArmRtoL, ArmRtoOL, ArmRtoOR;

    //状態
    public bool bDead = false;
    public bool bSpeedUp = false;
    public bool bPowerUp = false;
    public bool Invincible = false;

    //ステータス
    float Health, Damage,BeDamage=0.0f;
    float DamageUp = 1.0f;
    public int InvincibleTimerMax = 180;
    int InvincibleTimer = 0;
    int SpeedUpTimer = 0;
    int PowerUpTimer = 0;
    Slider HealthBarSlider;

    //Ball
    public int BallCount = 0;

	// サウンド
	private AudioSource power_UP;        // 効果音パワーアップ

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
        HealthBarSlider = gameObject.transform.Find("Canvas").gameObject.transform.Find("Slider").GetComponent<Slider>();
        InvincibleTimer = InvincibleTimerMax;

		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		power_UP = audioSources[4];
	}

    // Update is called once per frame
    void Update () {
        if (!bDead)
        {
			if (Time.timeScale == 1)
			{
				Battle();
				HpUpdate();
				BuffChecker();
			}
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
            (Lattach.tag == "ArmR" || Lattach.tag == "ArmL" || Lattach.tag == "Body" || Lattach.tag == "Ball" || Lattach.tag == "SpeedItem" || Lattach.tag == "PowerItem") &&
            (Rattach.tag == "ArmR" || Rattach.tag == "ArmL" || Rattach.tag == "Body" || Rattach.tag == "Ball" || Rattach.tag == "SpeedItem" || Rattach.tag == "PowerItem") 
            )
        {
            if (Lattach == Rattach)
            {
                if (Lattach.tag == "Body")
                {
                    Lattach.GetComponent<BodyManager>().GetDamage = true;
                    Debug.Log(Lattach.transform.root.gameObject.name);
                    Lattach.transform.root.gameObject.GetComponent<PlayerManager>().TakeDamage(Damage*DamageUp);
                }
                else if (Lattach.tag == "Ball")
                {
                    BallCount++;
                    Lattach.SetActive(false);
                }
                else if (Lattach.tag == "SpeedItem")
                {
                   // GameObject i = Instantiate(Effect);
                   // i.transform.position = Lattach.transform.position;
                    Lattach.SetActive(false);
                    bSpeedUp = true;
					power_UP.PlayOneShot(power_UP.clip);
                }
                else if (Lattach.tag == "PowerItem")
                {
                   // GameObject i = Instantiate(Effect);
                   // i.transform.position = Lattach.transform.position;
                    Lattach.SetActive(false);
                    bPowerUp = true;
					power_UP.PlayOneShot(power_UP.clip);
				}
				else
                {
                    if (Lattach.GetComponent<ChopsticksManager>().m_bDead == false)
                    {
                        GameObject i;
                        if (Lattach.transform.root.gameObject.transform.Find("Dog").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmD);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Lattach.transform.root.gameObject.transform.Find("Giraffe").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmG);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Lattach.transform.root.gameObject.transform.Find("Elephants").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmE);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Lattach.transform.root.gameObject.transform.Find("Mouse").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmM);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }                        
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
                        GameObject i;
                        if (Lattach.transform.root.gameObject.transform.Find("Dog").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmD);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Lattach.transform.root.gameObject.transform.Find("Giraffe").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmG);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Lattach.transform.root.gameObject.transform.Find("Elephants").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmE);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Lattach.transform.root.gameObject.transform.Find("Mouse").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmM);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        Lattach.GetComponent<ChopsticksManager>().m_bDead = true;
                    }
                    if (Rattach.GetComponent<ChopsticksManager>().m_bDead == false)
                    {
                        GameObject i;
                        if (Rattach.transform.root.gameObject.transform.Find("Dog").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmD);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Rattach.transform.root.gameObject.transform.Find("Giraffe").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmG);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Rattach.transform.root.gameObject.transform.Find("Elephants").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmE);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        else if (Rattach.transform.root.gameObject.transform.Find("Mouse").gameObject.activeSelf != false)
                        {
                            i = Instantiate(DeadArmM);
                            i.transform.position = Lattach.transform.position;
                            i.transform.rotation = Lattach.transform.rotation;
                        }
                        Rattach.GetComponent<ChopsticksManager>().m_bDead = true;
                    }                    
                }
            }
        }
    }

    void HpUpdate()
    {
        if (Health > HealthBarSlider.maxValue)
        {
            // 最大を超えたら0に戻す
            Health = HealthBarSlider.maxValue;
        }

        if (Health <= HealthBarSlider.minValue)
        {
            // 最大を超えたら0に戻す
            Health = HealthBarSlider.minValue;
            bDead = true;
        }

        if (Body.GetComponent<BodyManager>().GetDamage && !Invincible)
        {
            Health -= BeDamage;
            Invincible = true;
        }
        // HPゲージに値を設定
        HealthBarSlider.value = Health + 0.01f;
    }

    void BuffChecker()
    {
        if (bSpeedUp)
        {
            SpeedUpTimer++;
            if (SpeedUpTimer > GameObject.Find("SpeedItemManager").gameObject.GetComponent<SpeedItemManager>().SpeedUpTime*60)
            {
                bSpeedUp = false;
                SpeedUpTimer = 0;
            }
        }
        if (bPowerUp)
        {
            DamageUp = GameObject.Find("PowerItemManager").gameObject.GetComponent<PowerItemManager>().PowerUpValue;
            PowerUpTimer++;
            if (PowerUpTimer > GameObject.Find("PowerItemManager").gameObject.GetComponent<PowerItemManager>().PowerUpTime*60)
            {
                bPowerUp = false;
                PowerUpTimer = 0;
            }
        }
        else
        {
            DamageUp = 1.0f;
        }
        if (Invincible && !bDead)
        {

            if (KM_Math.KM_ChangeFlagTimer(6))
            {
                Body.GetComponent<Renderer>().enabled = false;
                /*
                if (ArmL.GetComponent<ChopsticksManager>().m_bDead == false)
                {
                    ArmL.GetComponent<Renderer>().enabled = false;
                }
                if (ArmR.GetComponent<ChopsticksManager>().m_bDead == false)
                {
                    ArmR.GetComponent<Renderer>().enabled = false;
                }
                */
            }
            else
            {
                Body.GetComponent<Renderer>().enabled = true;
                /*
                if (ArmL.GetComponent<ChopsticksManager>().m_bDead == false)
                {
                    ArmL.GetComponent<Renderer>().enabled = true;
                }
                if (ArmR.GetComponent<ChopsticksManager>().m_bDead == false)
                {
                    ArmR.GetComponent<Renderer>().enabled = true;
                }
                */
            }
            InvincibleTimer--;
            if (InvincibleTimer <= 0)
            {
                InvincibleTimer = 0;
                Invincible = false;
                Body.GetComponent<BodyManager>().GetDamage = false;
                InvincibleTimer = InvincibleTimerMax;
                Body.GetComponent<Renderer>().enabled = true;
                if (ArmL.GetComponent<ChopsticksManager>().m_bDead == false)
                {
                    ArmL.GetComponent<Renderer>().enabled = true;
                }
                if (ArmR.GetComponent<ChopsticksManager>().m_bDead == false)
                {
                    ArmR.GetComponent<Renderer>().enabled = true;
                }
            }
        }
    }

    void TakeDamage(float l_Damage)
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
