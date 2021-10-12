using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBall : Collectable
{
    public override void ApplyEffect()
    {
        foreach(Ball ball in BallsManager.Instance.Balls){
            ball.StartLightning();
        }
    }
}
