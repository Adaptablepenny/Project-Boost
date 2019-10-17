using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]  float rotationThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rb;
    AudioSource audioSource;
    enum State { alive, dying, load };
    State state = State.alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == State.alive)
        {
            Thrust();
            Rotation();
        }
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

        if (state != State.alive){return;}

        switch (col.gameObject.tag)
        {
            case "Friendly":
                print("Safe");
                break;
            case "finish":
                state = State.load;
                print("hit finish");
                Invoke("LoadNextLevel", 1f); //parameterise time
                break;
            case "Bad":
                print("Dead");
                state = State.dying;
                Invoke("LoadFirstLevel", 2f);
                break;
            default:
                print("Nothing to see here.");
                break;
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}



