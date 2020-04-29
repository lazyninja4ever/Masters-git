using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ResetPositionP11 : NetworkBehaviour
{
    public GameObject[] players;
    public Transform playerPosL;
    public Transform playerPosR;
    public Handles handlesScript;
    public ParticleSystem particleLeft;
    public ParticleSystem particleRight;
    public AudioSource resetSound;
    

    public void ResetPosition()
    {
        if (!isServer) return;
        RpcResetPlayerPosition();
    }


    [ClientRpc]
    void RpcResetPlayerPosition()
    {
        Debug.Log("ResetPosition");
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
        {
            Transform newPosition;
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                if (isServer)
                {
                    if(handlesScript.serverIsLeft == true)
                    {
                        newPosition = playerPosL;
                    }
                    else
                    {
                        newPosition = playerPosR;
                    }
                    player.GetComponent<CharacterController>().enabled = false;
                    player.transform.SetPositionAndRotation(newPosition.position, newPosition.rotation);
                    player.GetComponent<CharacterController>().enabled = enabled;
                    particleLeft.Play();
                    particleRight.Play();
                    resetSound.Play();
                }
                else if (!isServer)
                {
                    if(handlesScript.serverIsLeft == true)
                    {
                        newPosition = playerPosR;
                    }
                    else
                    {
                        newPosition = playerPosL;
                    }
                    player.GetComponent<CharacterController>().enabled = false;
                    player.transform.SetPositionAndRotation(newPosition.position, newPosition.rotation);
                    player.GetComponent<CharacterController>().enabled = enabled;
                    particleLeft.Play();
                    particleRight.Play();
                    resetSound.Play();
                }

            }
        }
    }
}
