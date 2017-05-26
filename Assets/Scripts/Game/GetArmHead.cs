using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArmHead : MonoBehaviour {

	public GameObject Body;
	public Vector3 ArmHead;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ArmHead = transform.position - Body.transform.position;
		ArmHead.Normalize ();
		ArmHead = ArmHead * 3.5f + Body.transform.position;
	}
}
