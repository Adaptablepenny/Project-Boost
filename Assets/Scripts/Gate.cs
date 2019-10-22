using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Vector3  startingPosition;
    [SerializeField] float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
        
    }


    private void FixedUpdate()
    {
        Invoke("OpenGate", 1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenGate()
    {
        float xMove = Time.deltaTime * speed;
        transform.Translate(0, xMove, 0);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.localPosition.x, -41f, -34f);
        transform.position = clampedPosition;
    }
    
}
