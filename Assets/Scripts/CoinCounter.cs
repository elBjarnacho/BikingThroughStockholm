﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {
    public PickupGatherer gatherer;
    public Text counterText;

    int count;
    int goal;

	void Start () {
        gatherer.OnPickup += OnPickup;
        count = 0;
        goal = GameObject.FindGameObjectsWithTag("Pickup").Length;
	}

    void OnPickup(GameObject pickup)
    {
        count++;
        counterText.text = count + " / " + goal;
    }
	
	void Update () {
		
	}
}