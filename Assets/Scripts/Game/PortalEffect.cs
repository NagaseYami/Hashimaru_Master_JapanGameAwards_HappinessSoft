using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEffect : MonoBehaviour {
    //public GameObject PartcleSystem;
    int Timer = 0;
	// Use this for initialization
	void Start () {
        gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Timer <= 5*60)
        {
            gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            gameObject.transform.localScale *= 0.95f;
        }

        if (gameObject.transform.localScale.x <= 0.0005f)
        {
            Destroy(gameObject);
        }

        Timer++;
    }
}
