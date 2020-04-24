using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HandleCollision : NetworkBehaviour
{
    public GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;

        }
    }
}
