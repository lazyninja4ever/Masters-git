using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoundFeedback : NetworkBehaviour
{
    public GameObject[] feedbackLightsL;
    public GameObject[] feedbackLightsR;
    public bool lightOne;
    public int nextLight = 0;


    private void Start()
    {
        for(int i = 0; i < feedbackLightsL.Length; i++)
        {
            feedbackLightsL[i].SetActive(false);
            feedbackLightsR[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(lightOne == true)
        {
            feedbackLightsL[nextLight].SetActive(true);
            feedbackLightsR[nextLight].SetActive(true);
            nextLight++;
            lightOne = false;

        }
    }

    public void removeLight()
    {
        for(int i = 0; i < feedbackLightsL.Length; i++)
        {
            nextLight = 0;
            feedbackLightsL[i].SetActive(false);
            feedbackLightsR[i].SetActive(false);
        }
    }
}
