using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour {
    
    public Rigidbody2D rigBody;   

    void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();
    }
    
    public void setSpeed(int value)
    {
        rigBody.velocity = new Vector2(value, 0);

    }

    public void setDestroy(float time)
    {
        Destroy(this.gameObject, time);

    }

    public void setShotConfig(int value, float time)
    {
        rigBody.velocity = new Vector2(value, 0);
        Destroy(this.gameObject, time);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Change for explosion if the collision is with a wall
        setDestroy(0);
    }
}
