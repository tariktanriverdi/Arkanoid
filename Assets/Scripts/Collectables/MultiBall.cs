using System.Linq;
using UnityEngine;

public class MultiBall:Collectable
{
  public override void ApplyEffect(){
      foreach(Ball ball in BallsManager.Instance.Balls.ToList()){
         
       BallsManager.Instance.SpawnBalls(ball.gameObject.transform.position,2);
      }
  }
}