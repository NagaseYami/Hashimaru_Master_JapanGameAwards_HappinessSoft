using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour {
	public Collision attach;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		attach = null;
	}

	void OnCollisionStay(Collision collision) {
		attach = collision;
		//Debug.Log (attach.gameObject.name);
	}
}
