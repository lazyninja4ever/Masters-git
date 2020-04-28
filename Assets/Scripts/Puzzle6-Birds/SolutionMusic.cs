using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SolutionMusic : NetworkBehaviour
{
    public List<string> CorrectSounds = new List<string>();
    public List<string> PlayedSounds = new List<string>();
    public List<int> CorrectRotation = new List<int>();
    public List<int> currentRotation = new List<int>();
    public GameObject[] bricks;
    public GameObject[] birds;
    public AudioSource crowSound;
    public bool isSolved;
    public SoundFeedback feedbackScript;
    public AudioSource rotationSolvedSound;

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
                isSolved = true;
                ShowBirds();     
            }
            else
            {
                PlayedSounds.Clear();

            }
        }if(PlayedSounds.Count == 0 && isSolved == false)
        {
            feedbackScript.GetComponent<SoundFeedback>().removeLight();
        }
    }


    void ShowBirds()
    {
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
                return false;
            }
        }
        return true;
    }

    public void CallDeactivate()
    {
        if (!isServer) return;
        RpcDeactivate();
    }

    public bool RotationCheck()
    {
        for (int i = 0; i < currentRotation.Count; i++)
        {
            if (currentRotation[i] != CorrectRotation[i])
            {
                Debug.Log("is false");
                return false;
            }
        }
        return true;
    }

    [ClientRpc]
    public void RpcCheckboard()
    {
        for(int i = 0; i< bricks.Length; i++)
        {
            currentRotation.Add(bricks[i].GetComponent<RotateBricks>().currentAngle);
        }

        if (RotationCheck())
        {
            CallDeactivate();
        }
        else
        {
            currentRotation.Clear();
        }
    }

    [ClientRpc]
    public void RpcDeactivate()
    {
        for(int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<RotateBricks>().InteractMsg = "";
            bricks[i].GetComponent<RotateBricks>().isRotating = true;
        }
        rotationSolvedSound.Play();
    }
}

