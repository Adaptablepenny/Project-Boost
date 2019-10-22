using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);//Destroys the Enemy Death FX prefab after 5 seconds //todo allow customization
    }

    // Update is called once per frame
    void Update()
    {

    }
}
