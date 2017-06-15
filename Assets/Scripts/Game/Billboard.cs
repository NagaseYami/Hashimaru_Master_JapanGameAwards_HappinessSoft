using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

	public Transform targetToFace;
	public bool isAutoFace = true;
	Quaternion adjustEuler = Quaternion.Euler (0, 0, 0);

	// Use this for initialization
	void Start()
	{
		if (targetToFace == null && isAutoFace)
		{
			var mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			targetToFace = mainCameraObject.transform;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (targetToFace != null)
		{
			transform.rotation = targetToFace.rotation;
			transform.rotation *= adjustEuler;
		}
	}
}