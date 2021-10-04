using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton
    private static BallsManager _instance;
    public static BallsManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField]
    private Ball ballPrefab;
    private Ball initialBall;
    private Rigidbody2D initialBallRb;
    public List<Ball> Balls { get; set; }
    public float initialBallSpeed = 250;
    private void Start()
    {
        InitBall();
    }
    private void Update()
    {
        SetBallPosition();

    }
    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();
        this.Balls = new List<Ball> { initialBall };
    }

    public void ResetBalls()
    {
       foreach(var ball in Balls.ToList()){
           Destroy(ball.gameObject);
       }
       InitBall();
    }

    private void SetBallPosition()
    {
        if (!GameManager.Instance.IsGameStarted)
        {   Debug.Log(GameManager.Instance.IsGameStarted);
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.26f, 0);
            initialBall.transform.position = ballPosition;
            if (Input.GetMouseButtonDown(0))
            {
                initialBallRb.isKinematic = false;
                initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }

        }
    }

}
