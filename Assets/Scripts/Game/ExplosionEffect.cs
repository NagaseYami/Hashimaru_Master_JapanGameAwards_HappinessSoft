using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {
    int Timer = 0;
	// Use this for initialization
	void Start () {
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Timer>=60)
        {
            Timer = 0;
            Destroy(gameObject);
        }
        Timer++;
	}
}
