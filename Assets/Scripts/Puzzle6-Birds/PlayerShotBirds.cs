using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerShotBirds : NetworkBehaviour
{
    public string sceneName;
    public SolutionBirds birdsScript;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Puzzle6-Birds") {
            GameObject birdScriptObj = GameObject.Find("SolutionChecker");
            birdsScript = birdScriptObj.GetComponent<SolutionBirds>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void CmdShotBirds()
    {
        birdsScript.IncreaseBirds(4);
    }
}
