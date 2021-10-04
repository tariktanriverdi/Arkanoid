using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag=="Ball"){
        Ball ball=collider.GetComponent<Ball>();
        BallsManager.Instance.Balls.Remove(ball);
        ball.Die();
        }


    }
}
