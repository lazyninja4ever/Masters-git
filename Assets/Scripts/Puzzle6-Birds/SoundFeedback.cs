using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoundFeedback : NetworkBehaviour
{
    public GameObject[] feedbackLights;
    public bool lightOne;
    public int nextLight = 0;


    private void Start()
    {
        for(int i = 0; i < feedbackLights.Length; i++)
        {
            feedbackLights[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(lightOne == true)
        {
            feedbackLights[nextLight].SetActive(true);
            nextLight++;
            lightOne = false;

        }
    }

    public void removeLight()
    {
        for(int i = 0; i < feedbackLights.Length; i++)
        {
            nextLight = 0;
            feedbackLights[i].SetActive(false);
        }
    }
}
