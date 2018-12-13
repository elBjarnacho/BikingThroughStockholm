﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGatherer : MonoBehaviour {
    public delegate void OnPickupDelegate(GameObject pickup);
    public event OnPickupDelegate OnPickup;

    public float distance;
    public float delay;
    public AudioSource coinSound;

    GameObject last;
    float timeCounter;

	void Start () {

	}
	
	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit.CompareTag("Pickup"))
            {
                if (objectHit == last)
                {
                    timeCounter += Time.deltaTime;
                    //last.GetComponent<MeshRenderer>().material.color = new Color(251, 129, 8);
                    last.GetComponent<MeshRenderer>().material.SetInt("_IsPickingUp", 1);
                    if (timeCounter >= delay)
                    {
                        coinSound.Play();
                        if (OnPickup != null)
                            OnPickup(objectHit);
                        Destroy(objectHit);
                    }
                } else
                {
                    last = objectHit;
                    timeCounter = 0;
                }
            
            }
        } else
        {
            if (last != null)
            last.GetComponent<MeshRenderer>().material.SetInt("_IsPickingUp", 0);
            //last.GetComponent<MeshRenderer>().material.color = new Color(231, 189, 8);
            last = null;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        Gizmos.DrawSphere(transform.position + transform.forward * distance, 0.2f);
    }
}
