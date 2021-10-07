using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D collision) {
    
    if(collision.tag=="Paddle"){
        this.ApplyEffect();
    }
    if(collision.tag=="Paddle"|| collision.tag=="DeathWall"){
       Destroy(this.gameObject); 
    }
    
}

    public abstract void ApplyEffect();
}
