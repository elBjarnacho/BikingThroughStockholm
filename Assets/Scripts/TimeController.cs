﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    [SerializeField] private float rewardInterval;
    [SerializeField] private float rewardDuration;
    [SerializeField] GameObject milestonePanel;
    [SerializeField] GameObject timePanel;
    [SerializeField] GameObject fireworkPrefab;

    private GameObject milestonePrompt;
    private GameObject complimentPrompt;
    private GameObject timePrompt;

    private float timeSoFar;
    private float lastMilestone;
    private string secondsSoFar;
    private string minutesSoFar;

    private Text timeText;
    private Text milestoneText;
    private Text complimentText;

    private static List<string> compliments = new List<string>
    {
        "Go you!!", 
        "Well done!!",
        "WOW!",
        "Biking saves money and burns calories",
        "Keep up the good work!!",
        "Good job :)",
        "Nicely done!"
    };

	void Start ()
    {
        timeSoFar = 0.0f;

        milestonePrompt = milestonePanel.transform.GetChild(0).gameObject;
        complimentPrompt = milestonePanel.transform.GetChild(1).gameObject;
        timePrompt = timePanel.transform.GetChild(0).gameObject;

        timeText = timePrompt.GetComponent<Text>();
        milestoneText = milestonePrompt.GetComponent<Text>();
        complimentText = complimentPrompt.GetComponent<Text>();
	}
	
	
	void Update ()
    {
        UpdateTimer();
        CheckMilestones();
    }

    private void UpdateTimer()
    {
        timeSoFar += Time.deltaTime;
        minutesSoFar = Mathf.Floor(timeSoFar / 60).ToString("00");
        secondsSoFar = (timeSoFar % 60).ToString("00");
        timeText.text = minutesSoFar + ":" + secondsSoFar;
    }

    private void CheckMilestones()
    {
        float progressSinceLastMilestone = timeSoFar - lastMilestone;
        if(progressSinceLastMilestone > rewardInterval)
        {
            Debug.Log("progress since last " + progressSinceLastMilestone);
            Debug.Log("time so far " + timeSoFar);
            Debug.Log("reward intervarl " + rewardInterval);
            StartCoroutine("DisplayReward");
            lastMilestone = timeSoFar;
        }
    }

    private IEnumerator DisplayReward()
    {
        float timeActive = 0.0f;                

        int complimentIndex = Random.Range(0, compliments.Count);
        milestoneText.text = minutesSoFar + ":" + secondsSoFar;
        complimentText.text = compliments[complimentIndex];

        launchFireworks();

        milestonePanel.SetActive(true);
        while (timeActive < rewardDuration)
        {
            timeActive += Time.deltaTime;
            yield return null;
        }

        milestonePanel.SetActive(false);
        yield break;
    }

    private void launchFireworks()
    {
        Vector3 panelPosition = milestonePanel.GetComponent<RectTransform>().transform.position;
        float offsetX = milestonePanel.GetComponent<RectTransform>().rect.width / 2;
        float offsetY = milestonePanel.GetComponent<RectTransform>().rect.height / 2;

        RectTransform rect = milestonePanel.GetComponent<RectTransform>();

        GameObject firework1 = Instantiate(fireworkPrefab, milestonePanel.transform, false);
        GameObject firework2 = Instantiate(fireworkPrefab, milestonePanel.transform, false);
        GameObject firework3 = Instantiate(fireworkPrefab, milestonePanel.transform, false);
        GameObject firework4 = Instantiate(fireworkPrefab, milestonePanel.transform, false);

        firework1.GetComponent<ParticleSystem>().startColor = new Vector4(1, 0, 0, 1);
        firework1.transform.localPosition += new Vector3(rect.offsetMin.x, rect.offsetMax.y / 2, 0);

        firework2.GetComponent<ParticleSystem>().startColor = new Vector4(0, 1, 0, 1);
        firework2.transform.localPosition -= new Vector3(rect.offsetMin.x, rect.offsetMax.y / 2, 0);

        firework3.GetComponent<ParticleSystem>().startColor = new Vector4(0, 0, 1, 1);
        firework3.transform.localPosition -= new Vector3(rect.offsetMax.x, rect.offsetMax.y / 2, 0);

        firework4.GetComponent<ParticleSystem>().startColor = new Vector4(0, 1, 1, 1);
        firework4.transform.localPosition += new Vector3(rect.offsetMax.x, rect.offsetMax.y / 2, 0);
    }
}
