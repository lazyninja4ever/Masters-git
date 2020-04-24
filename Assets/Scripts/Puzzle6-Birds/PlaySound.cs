using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlaySound : NetworkInteraction
{
    public AudioSource playSound;
    public string soundNumber;
    public SolutionMusic musicSolutionScript;
    public SoundFeedback feedbackScript;

    public override void InterActionFuntion(GameObject player)
    {
        base.InterActionFuntion(player);
        RpcInteract();
    }

    [ClientRpc]
    void RpcInteract()
    {
        musicSolutionScript.GetComponent<SolutionMusic>().PlayedSounds.Add(soundNumber);
        feedbackScript.GetComponent<SoundFeedback>().lightOne = true;
        playSound = GetComponent<AudioSource>();
        playSound.Play();
        Debug.Log("played sound");

    }

}
