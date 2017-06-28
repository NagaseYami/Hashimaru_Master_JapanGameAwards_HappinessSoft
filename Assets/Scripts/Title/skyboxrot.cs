using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxrot : MonoBehaviour
{

	public GameObject skyboxCamera;//スカイボックスカメラ
	public float skyboxChangeAngle;//１フレームで回転させたい値
	public Vector3 skyboxAxis;//回転軸
	float skyboxAngle;//変更後の角度（アングル）

	//スカイボックスを回転させる
	void Update()
	{
		skyboxAngle += skyboxChangeAngle;
		skyboxCamera.transform.rotation = Quaternion.AngleAxis(skyboxAngle, skyboxAxis);
	}
}
