using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SolutionMusic : MonoBehaviour
{
    public List<string> CorrectSounds = new List<string>();
    public List<string> PlayedSounds = new List<string>();
    public GameObject[] birds;
    public AudioSource crowSound;
    public bool isSolved;
    public SoundFeedback feedbackScript;

    private void Start()
    {
        for(int i = 0; i < birds.Length; i++)
        {
            birds[i].GetComponent<Renderer>().enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(PlayedSounds.Count == 8 && isSolved == false)
        {
            if (SoundCheck())
            {
                Debug.Log("is true");
                isSolved = true;
                ShowBirds();     
            }
            else
            {
                Debug.Log("is false");
                PlayedSounds.Clear();

            }
        }if(PlayedSounds.Count == 0 && isSolved == false)
        {
            feedbackScript.GetComponent<SoundFeedback>().removeLight();
        }
    }

    void ShowBirds()
    {
        crowSound = GetComponent<AudioSource>();
        crowSound.Play();
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].GetComponent<Renderer>().enabled = true;
        }

    }

    bool SoundCheck()
    {
        Debug.Log("called");
        for(int i = 0; i < PlayedSounds.Count; i++)
        {
            if(PlayedSounds[i] != CorrectSounds[i])
            {
                Debug.Log("this :" + PlayedSounds[i] + "and this: " + CorrectSounds[i] + "is not the same");
                return false;
                
            }
        }
        return true;
    }
}

