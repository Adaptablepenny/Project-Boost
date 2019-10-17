using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotation();

    }

 
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))//Thrust
        {
            rb.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.A))//Rotate Left
        {

            transform.Rotate(Vector3.forward);

        }
        else if (Input.GetKey(KeyCode.D))// Rotate Right
        {

            transform.Rotate(Vector3.back);
        }
    }
}



