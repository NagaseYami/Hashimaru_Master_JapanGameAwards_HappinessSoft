using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public float Speed = 10.0f;
    public Vector3 Dir;

	// Use this for initialization
	void Start () {
        Dir = new Vector3(Random.Range(-100.0f,100.0f), 0.0f, Random.Range(-100.0f, 100.0f));
	}

    private void FixedUpdate()
    {
        transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void LateUpdate()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Field")
        {
            Dir = new Vector3(Random.Range(-100.0f, 100.0f), 0.0f, Random.Range(-100.0f, 100.0f));
        }       
    }
}
