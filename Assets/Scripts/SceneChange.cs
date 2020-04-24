using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SceneChange : NetworkBehaviour
{
    public string newScene;
    public NetworkManager networkManagerScript;
    public int playerCount;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            if (playerCount > 1) {
                if (!isServer) return;
                networkManagerScript.ServerChangeScene(newScene);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
        }
    }

}
