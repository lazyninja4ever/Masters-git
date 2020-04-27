using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionArrow : MonoBehaviour
{
    public AudioSource arrowHit;
    public AudioSource arrowHitBird;
    public bool hasHitBird;
    public bool hasHit;
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Bird"))
        {
            if(hasHitBird == false)
            {
                arrowHit.Play();
                arrowHitBird.Play();
                hasHitBird = true;
            }
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            transform.parent = collision.transform;
        }
        else
        {
            if(hasHit == false)
            {
                arrowHit.Play();
                hasHit = true;
            }
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform;
        }
    }
}
