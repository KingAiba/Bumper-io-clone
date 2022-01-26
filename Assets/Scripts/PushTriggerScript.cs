using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTriggerScript : MonoBehaviour
{
    public bool inPushRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        inPushRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //&& other.gameObject != gameObject
        //inPushRange = false;
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")))
        {
            inPushRange = true;
        }
        else
        {
            inPushRange = false;
        }
    }
}
