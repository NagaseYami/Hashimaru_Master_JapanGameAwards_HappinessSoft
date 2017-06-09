using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopsticksManager : MonoBehaviour {

    public GameObject attach;
    public bool m_bDead = false;
    public int RespawnTime = 5;
    int m_respawntimer = 0;
    Renderer m_renderer;
    Collider m_collider;
    public bool bHasamu = false;
    public GameObject Body;
    public Vector3 ArmHead;
    // Use this for initialization
    void Start () {
        m_renderer = GetComponent<Renderer>();
        m_collider = GetComponent<Collider>();
	}

    void FixedUpdate()
    {
        attach = null;
    }
    // Update is called once per frame
    void Update () {

        ArmHead = transform.position - Body.transform.position;
        ArmHead.Normalize();
        ArmHead = ArmHead * 3.5f + Body.transform.position;

        if (m_bDead)
        {
            m_collider.enabled = false;
            m_renderer.enabled = false;
            m_respawntimer++;            
        }
        else
        {            
            m_collider.enabled = true;
            m_renderer.enabled = true;
        }

        if (m_respawntimer >= 60 * RespawnTime)
        {
            m_respawntimer = 0;
            m_bDead = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        attach = collision.gameObject;
    }

    void OnTriggerStay(Collider collider)
    {
        attach = collider.gameObject;
    }
}
