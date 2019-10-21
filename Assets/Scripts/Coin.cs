using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int score = 1;

    [SerializeField] float speed = 10f;

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
        scoreBoard.AddScore(score);
        Destroy(gameObject);
    }

    void CoinRotation()
    {
        transform.Rotate(0f, speed, 0f);
    }
}
