using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public Text playerText;
    public string introText;
  //  public Animation anim;
     // Start is called before the first frame update
    private void Awake()
    {
        // anim = gameObject.GetComponent<Animation>();
        // anim.Play("soulsAnim");
        playerText.enabled = true;
        playerText.text = introText;
      //  playerText.enabled = true;
        Debug.Log("halo");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
