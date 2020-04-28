using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ResetPosition : MonoBehaviour
{
    public Transform currentPos;
    public Transform goToPos;
    public ParticleSystem particleReset;
    public AudioSource resetSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentPos = goToPos;
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.SetPositionAndRotation(currentPos.position, currentPos.rotation);
            other.gameObject.GetComponent<CharacterController>().enabled = enabled;
            particleReset.Play();
            resetSound.Play();
        }
    }
}
