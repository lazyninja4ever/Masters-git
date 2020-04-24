using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionArrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Bird"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            transform.parent = collision.transform;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform;
        }
    }
}
