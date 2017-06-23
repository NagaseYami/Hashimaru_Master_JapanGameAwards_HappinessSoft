using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadChopstick : MonoBehaviour {
    int Timer = 0;
    public GameObject Pivot;
    Vector3 Dir;
	// Use this for initialization
	void Start () {
        Dir = new Vector3(Random.Range(-1000.0f, 0.0f), 3000.0f, Random.Range(-1000.0f, 1000.0f));
        gameObject.GetComponent<Rigidbody>().AddForce(Dir);
    }
	
	// Update is called once per frame
	void Update () {       
        if (Timer<=120)
        {
            
        }
        else
        {

        }
        
        if (Timer >= 300)
        {
            Timer = 0;
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.RotateAround(Pivot.transform.position, Vector3.right, 1600 * Time.deltaTime);
            Timer++;
        }
	}
}
