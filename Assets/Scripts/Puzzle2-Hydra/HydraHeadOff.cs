using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraHeadOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Crumble", 7.0f);
    }

    
    private void Crumble() {
        Destroy(this.gameObject);
    }
}
