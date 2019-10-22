using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int score = 1;
    int coinTouch = 0;//How many trigger events are occuring

    
    [SerializeField] float speed = 10f;

    [SerializeField] Transform parent;
    [SerializeField] GameObject coinSound;
    ScoreBoard scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        CoinRotation();
    }

    private void OnTriggerEnter(Collider coin)
    {
        
        PickupCoin();
    }

    private void PickupCoin()
    {
        coinTouch += 1;
        if (coinTouch == 1)
        {
            GameObject fx = Instantiate(coinSound, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            scoreBoard.AddScore(score);
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }//Limits the score by detecting only one trigger

    void CoinRotation()
    {
        transform.Rotate(0f, speed, 0f);
    }
}
