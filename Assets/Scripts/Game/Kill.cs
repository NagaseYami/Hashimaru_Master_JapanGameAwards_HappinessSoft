using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {
	public GameObject ArmL,ArmR;
    public int RespawnTimeForL = 5, RespawnTimeForR = 5;

	private Collision Lattach, Rattach;
	private Vector3 ArmLVec,ArmRVec;
	private Vector3 ArmRtoL,ArmRtoOL,ArmRtoOR;
    static private int TimerForL = 0, TimerForR = 0;
	// Use this for initialization
	void Start () {
		Lattach = ArmL.GetComponent<Attach> ().attach;
		Rattach = ArmR.GetComponent<Attach> ().attach;

	}
	
	// Update is called once per frame
	void Update () {
		Lattach = ArmL.GetComponent<Attach> ().attach;
		Rattach = ArmR.GetComponent<Attach> ().attach;

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
        if(TimerForL >= 60 * RespawnTimeForL)
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

}
