using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    //public public
    public GameObject ArmL, ArmR;

    //Controller
    public float RotateSpeed;
    public float MoveSpeed;
    public GameObject Body;
    private Rigidbody rb;

    //Attack    
    public float CloseSpeed, OpenSpeed;
    private bool CloseFlag, OpenFlag;
    private float AlreadyRotation;

    //Battle
    public int RespawnTimeForL = 5, RespawnTimeForR = 5;
    Collision Lattach, Rattach;
    Vector3 ArmLVec, ArmRVec;
    Vector3 ArmRtoL, ArmRtoOL, ArmRtoOR;
    static int TimerForL = 0, TimerForR = 0;

    //HP
    public GameObject HpBar;
    public float Hp;    
    Slider _slider;   

    // Use this for initialization
    void Start () {
		Lattach = ArmL.GetComponent<Attach> ().attach;
		Rattach = ArmR.GetComponent<Attach> ().attach;
        rb = Body.GetComponent<Rigidbody>();
        _slider = HpBar.GetComponent<Slider>();

        CloseFlag = false;
        OpenFlag = false;
        AlreadyRotation = 0;
    }

    void FixedUpdate()
    {
        Controller();
        Attack();
    }

    // Update is called once per frame
    void Update () {        
        Battle();
        HpUpdate();
    }

    void Battle()
    {
        Lattach = ArmL.GetComponent<Attach>().attach;
        Rattach = ArmR.GetComponent<Attach>().attach;

        if (ArmL.GetComponent<Renderer>().enabled != false && ArmR.GetComponent<Renderer>().enabled != false &&
            Lattach != null && Rattach != null &&
            ArmL.GetComponent<ArmHasamuFlag>().bHasamu && ArmR.GetComponent<ArmHasamuFlag>().bHasamu &&
            (Lattach.gameObject.tag == "ArmR" || Lattach.gameObject.tag == "ArmL") &&
            (Rattach.gameObject.tag == "ArmR" || Rattach.gameObject.tag == "ArmL")
            )
        {
            if (Lattach.gameObject == Rattach.gameObject)
            {
                Debug.Log("LeftArm and RightArm are attaching same obj!!");
                Lattach.gameObject.GetComponent<Renderer>().enabled = false;
                Lattach.gameObject.GetComponent<Collider>().enabled = false;
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
        if (Hp > _slider.maxValue)
        {
            // 最大を超えたら0に戻す
            Hp = _slider.maxValue;
        }

        if (Hp < _slider.minValue)
        {
            // 最大を超えたら0に戻す
            Hp = _slider.minValue;
        }

        // HPゲージに値を設定
        _slider.value = Hp + 0.01f;
    }

    void Controller()
    {
        // 上方向
        if (Input.GetAxisRaw("Vertical1") > 0)
        {
            rb.AddForce(Body.transform.forward * MoveSpeed);
        }

        // 下方向
        if (Input.GetAxisRaw("Vertical1") < 0)
        {
            rb.AddForce(-Body.transform.forward * MoveSpeed);
        }

        Vector3 TurnLeft = new Vector3(0.0f, -RotateSpeed, 0.0f);
        Vector3 TurnRight = new Vector3(0.0f, RotateSpeed, 0.0f);

        Quaternion deltaRotation;

        // 左方向
        if (Input.GetAxisRaw("Horizontal1") > 0.01)
        {
            deltaRotation = Quaternion.Euler(TurnLeft * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            ArmR.transform.RotateAround(Body.transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
            ArmL.transform.RotateAround(Body.transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
        }

        // 右方向
        if (Input.GetAxisRaw("Horizontal1") < -0.01)
        {
            deltaRotation = Quaternion.Euler(TurnRight * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            ArmR.transform.RotateAround(Body.transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
            ArmL.transform.RotateAround(Body.transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !CloseFlag && !OpenFlag)
        {
            CloseFlag = true;
        }

        if (CloseFlag)
        {
            ArmR.transform.RotateAround(Body.transform.position, Vector3.up, -CloseSpeed * Time.deltaTime);
            ArmL.transform.RotateAround(Body.transform.position, Vector3.up, CloseSpeed * Time.deltaTime);
            AlreadyRotation += CloseSpeed * Time.deltaTime;
            ArmR.GetComponent<ArmHasamuFlag>().bHasamu = true;
            ArmL.GetComponent<ArmHasamuFlag>().bHasamu = true;
            if (AlreadyRotation >= 30)
            {
                ArmR.transform.eulerAngles = new Vector3(0, Body.transform.eulerAngles.y - 30, 0);
                ArmL.transform.eulerAngles = new Vector3(0, Body.transform.eulerAngles.y + 30, 0);
                CloseFlag = false;
                OpenFlag = true;
                AlreadyRotation = 0;
                ArmR.GetComponent<ArmHasamuFlag>().bHasamu = false;
                ArmL.GetComponent<ArmHasamuFlag>().bHasamu = false;
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