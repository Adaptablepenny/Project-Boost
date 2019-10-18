using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f,0,0);
    [SerializeField] float period = 5f;

    // todo remove from inspector later;
    float movementFactor; //0 for not moved, 1 for fully moved
    float rawSinWave;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillation();
    }

    private void Oscillation()
    {
        if (period <= 0){ return; }
        
        float cycles = Time.time / period; // grows continually from 0
        const float tau = Mathf.PI * 2; // about 6.28
        rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to + 1
        movementFactor = rawSinWave / 2f + 0.5f; // makes the sin start at 0 instead of -1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
