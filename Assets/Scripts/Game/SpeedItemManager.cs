using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItemManager : MonoBehaviour {

    public int SpeedUpTime = 5;
    public float SpeedUpValue = 1.5f;
    public int RespawnTime = 5;
    public int RespawnTimer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (Time.timeScale == 1)
		{
			if (!transform.Find("SpeedItem").gameObject.activeSelf)
			{
				transform.Find("SpeedItem").gameObject.GetComponent<ItemManager>().bStartSuicide = false;
				transform.Find("SpeedItem").gameObject.GetComponent<ItemManager>().bStartRespawn = true;
				transform.Find("SpeedItem").gameObject.GetComponent<ItemManager>().SuicideTimer = 0;
				transform.Find("SpeedItem").gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
				RespawnTimer++;
			}
			if (RespawnTimer >= RespawnTime * 60)
			{
				transform.Find("SpeedItem").gameObject.SetActive(true);
				transform.Find("SpeedItem").gameObject.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), -0.201f, Random.Range(-15.0f, 15.0f));
				RespawnTimer = 0;
			}
		}
    }
}
