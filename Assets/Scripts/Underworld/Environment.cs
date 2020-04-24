using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
   
    public Animation anim;
     // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.Play("soulsAnim");
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
