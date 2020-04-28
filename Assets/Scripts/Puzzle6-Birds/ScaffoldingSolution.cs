using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScaffoldingSolution : NetworkBehaviour
{
    public List<int> CorrectRotation = new List<int>();
    public List<int> currentRotation = new List<int>();
    public GameObject[] bricks;
    public AudioSource fenceSound;
    public Animator animFence;
    public ParticleSystem scaffParticle;
    public HideObjects hideScaffolding;
    public HideObjects hideTiles;
    public AudioSource particleSound;

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
    public void RpcCheckboardScaf()
    {
        Debug.Log("got here");
        for (int i = 0; i < bricks.Length; i++)
        {
            currentRotation.Add(bricks[i].GetComponent<ScaffoldingRotate>().currentAngle);
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
        scaffParticle.Play();
        particleSound.Play();
        hideScaffolding.moveToPosition();
        hideTiles.moveToPosition();
        fenceSound.Play();
        animFence.Play("fenceDown");

    }
}
