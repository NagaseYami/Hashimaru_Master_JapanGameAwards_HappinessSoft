using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public int LiveTime = 5;
    public int SuicideTimer = 0;
    public bool bStartRespawn = true;
    public bool bStartSuicide = false;
    Vector3 ScaleCache;
	// Use this for initialization
	void Start () {
        ScaleCache = transform.localScale;
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (bStartRespawn)
        {
            transform.localScale += new Vector3(3.0f,3.0f,3.0f);
            if (transform.localScale.x > ScaleCache.x)
            {
                transform.localScale = ScaleCache;
                bStartRespawn = false;
            }
        }
        if (SuicideTimer >= LiveTime * 60) 
        {
            SuicideTimer = 0;
            bStartSuicide = true;
        }
        else
        {
            SuicideTimer++;
        }

        if (bStartSuicide)
        {
            transform.localScale *= 0.9f;
        }
        if (transform.localScale.x < 0.1)
        {
            bStartSuicide = false;
            bStartRespawn = true;
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            gameObject.SetActive(false);
        }
	}
}
