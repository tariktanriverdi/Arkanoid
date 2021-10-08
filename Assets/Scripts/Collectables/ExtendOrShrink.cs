using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendOrShrink : Collectable
{
    public float newWidth=3f;
    public override void ApplyEffect()
    {
        if(Paddle.Instance !=null && Paddle.Instance.PaddleIsTransforming)
        {
            Paddle.Instance.StartWidthAnimation(newWidth);
        }
    }
}
