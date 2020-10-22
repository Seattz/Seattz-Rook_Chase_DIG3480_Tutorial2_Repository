using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour 

{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

   // Use this for initialization
    void Start ()
    {
        initialPosition = transform.position;
        direction = -1;
        maxDist += transform.position.x;
        minDist -= transform.position.x;
    }
   
    // Update is called once per frame
    void Update ()
    {
        switch (direction)
        {
             case -1:
                // Moving Left
                if( transform.position.x > minDist)
                    {
                       GetComponent <Rigidbody2D>().velocity = new Vector2(-movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
                    }
                else
                    {
                       direction = 1;
                    }
                break;
             case 1:
                  //Moving Right
                if(transform.position.x < maxDist)
                    {
                        GetComponent <Rigidbody2D>().velocity = new Vector2(movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
                    }
                else
                    {
                        direction = -1;
                    }
            break;
        }
    }
}
