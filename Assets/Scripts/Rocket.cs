using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]  float rotationThrust = 100f;
    [SerializeField] float mainThrust = 100f;
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
        float thrustSpeed = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))//Thrust
        {
            rb.AddRelativeForce(Vector3.up * thrustSpeed);
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
        
        float rotationSpeed = rotationThrust * Time.deltaTime;
        rb.freezeRotation = true;//take manual control of rotation
        if (Input.GetKey(KeyCode.A))//Rotate Left
        {
            
            transform.Rotate(Vector3.forward * rotationSpeed);

        }
        else if (Input.GetKey(KeyCode.D))// Rotate Right
        {

            transform.Rotate(Vector3.back * rotationSpeed);
        }
        rb.freezeRotation = false;// resume physics control
    }

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Friendly":
                print("Safe");
                break;
            case "Bad":
                print("Dead");
                break;
            default:
                print("Nothing to see here.");
                break;
        }
    }
}



