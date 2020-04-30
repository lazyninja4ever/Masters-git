using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ResetPosition : NetworkBehaviour
{
    public Transform currentPos;
    public Transform goToPos;
    public ParticleSystem particleReset;
    public AudioSource resetSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isServer) return;
            currentPos = goToPos;
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.SetPositionAndRotation(currentPos.position, currentPos.rotation);
            other.gameObject.GetComponent<CharacterController>().enabled = enabled;
            resetSound.Play();
            RpcPlayParticle();
        }
    }

    [ClientRpc]
    public void RpcPlayParticle()
    {
        particleReset.Play();      
    }
}
