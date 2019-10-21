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
    ScoreBoard scoreText;
    enum State { alive, dying, load };
    State state = State.alive;
    bool collisionsDisabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scoreText = GetComponent<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == State.alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
            if (Debug.isDebugBuild)
            {
                RespondToDebug();//debug actions
            }
            RespondToQuit();
            
        }
    }

    void RespondToDebug()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled; //enables disbales collision
        }

    }

    void RespondToQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
            StopThrust();
        }
    }

    private void StopThrust()
    {
        engineThrust.Stop();
        audioSource.Stop();
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
        
        if (Input.GetKey(KeyCode.A))//Rotate Left
        {
            ApplyRotateThrust(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))// Rotate Right
        {
            ApplyRotateThrust(-rotationSpeed);
        }
        
    }

    private void ApplyRotateThrust(float rotationSpeed)
    {
        rb.freezeRotation = true;//take manual control of rotation
        transform.Rotate(Vector3.forward * rotationSpeed);
        rb.freezeRotation = false;// resume physics control
    }

    void OnCollisionEnter(Collision col)
    {

        if (state != State.alive || collisionsDisabled) {return;}

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
        Invoke("ReloadLevel", LoadLevelDelay);
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
        int currScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currScene + 1;

        if (nextScene > 4)
        {
            Application.Quit();
            print("Quitting");
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
        
        
        
    }

    void ReloadLevel()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene);
    }
}



