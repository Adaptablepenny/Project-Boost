using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]  float rotationThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float LoadLevelDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip winJingle;
    [SerializeField] AudioClip deathAudio;

    [SerializeField] ParticleSystem deathExplosion;
    [SerializeField] ParticleSystem winParticles;
    [SerializeField] ParticleSystem engineThrust;

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
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

 
    void RespondToThrustInput()
    {
        float thrustSpeed = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))//Thrust
        {
            ApplyThrust(thrustSpeed);
        }
        else
        {
            engineThrust.Stop();
            audioSource.Stop();
        }
    }

    void ApplyThrust(float thrustSpeed)
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed);
        if (!audioSource.isPlaying)
        {
            
            audioSource.PlayOneShot(mainEngine);
        }
        engineThrust.Play();
    }

    void RespondToRotateInput()
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
                break;
            case "finish":
                StartWin();
                break;
            case "Bad":
                StartDeath();
                break;
            default:
                print("Nothing to see here.");
                break;
        }
    }

    private void StartDeath()
    {
        deathExplosion.Play();
        state = State.dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio);
        Invoke("LoadFirstLevel", LoadLevelDelay);
    }

    private void StartWin()
    {
        winParticles.Play();
        state = State.load;
        audioSource.PlayOneShot(winJingle);
        Invoke("LoadNextLevel", LoadLevelDelay); //parameterise time
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



