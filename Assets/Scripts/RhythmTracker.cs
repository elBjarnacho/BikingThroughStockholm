﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RhythmTracker : MonoBehaviour
{

    public Image ProgressBar;
    private float progress;
    private float loseTime = 5.0f;
    private float timeLeft;

    private bool simulationStarted = false;

	// Use this for initialization
	void Start ()
    {
        timeLeft = loseTime;
        progress = 0.8f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //progress = Mathf.Max(progress - 0.1f * Time.deltaTime, 0f);
        if (Input.GetKeyDown("space"))
        {
            if (!simulationStarted)
                StartSimulation();

            progress = Mathf.Min(progress + 0.1f, 1.0f);
        }

        if (simulationStarted)
        {
            ProgressBar.fillAmount = progress;
            if (progress < 0.7f) {
                timeLeft -= Time.deltaTime;
                ProgressBar.color = Color.red;

            } else {
                timeLeft = loseTime;
                ProgressBar.color = Color.green;
            }

            if (timeLeft <= 0) {
                GameOver();
            }
        }
    }

    private void GameOver() {
        EndgameController.Instance.GameOver();
    }

    public void UpdateRhythm()
    {
        if (!simulationStarted)
            StartSimulation();

        progress = Mathf.Min(progress + 0.1f, 1.0f);
        ProgressBar.fillAmount = progress;
    }

    public void UpdateRhythm(float speed)
    {
        if (!simulationStarted && speed > 0)
        {
            Debug.Log("Starting simulation. Speed: " + speed);
            StartSimulation();
        }            

        //progress = Mathf.Min(progress + 0.1f, 1.0f);
        progress = Mathf.Min(speed, 1.0f);
        ProgressBar.fillAmount = progress;

    }

    private void StartSimulation()
    {        
        GameObject.FindWithTag("BikeFootage").GetComponent<VideoPlayer>().Play();
        GameObject.FindWithTag("PlayerContainer").GetComponent<PathFollower>().enabled = true;
        simulationStarted = true;
    }
}
