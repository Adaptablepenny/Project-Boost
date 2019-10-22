using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{

    Rocket rocket;
    // Start is called before the first frame update
    void Start()
    {
        rocket = GetComponent<Rocket>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider gravity)
    {
        gravity.attachedRigidbody.useGravity = true;        
    }

    private void OnTriggerExit(Collider gravity)
    {
        gravity.attachedRigidbody.useGravity = false;
    }
}
