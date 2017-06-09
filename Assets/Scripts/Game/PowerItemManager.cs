using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemManager : MonoBehaviour {

    public int PowerUpTime = 5;
    public float PowerUpValue = 1.5f;
    public int RespawnTime = 5;
    public int RespawnTimer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!transform.Find("PowerItem").gameObject.activeSelf)
        {
            transform.Find("PowerItem").gameObject.GetComponent<ItemManager>().bStartSuicide = false;
            transform.Find("PowerItem").gameObject.GetComponent<ItemManager>().bStartRespawn = true;
            transform.Find("PowerItem").gameObject.GetComponent<ItemManager>().SuicideTimer = 0;
            transform.Find("PowerItem").gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            RespawnTimer++;
        }
        if (RespawnTimer >= RespawnTime * 60) 
        {
            transform.Find("PowerItem").gameObject.SetActive(true);
            transform.Find("PowerItem").gameObject.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), -0.83f, Random.Range(-15.0f, 15.0f));
            RespawnTimer = 0;
        }
	}
}
