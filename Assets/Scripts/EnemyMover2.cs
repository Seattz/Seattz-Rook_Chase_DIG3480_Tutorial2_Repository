﻿ using UnityEngine;
 using System.Collections;
 
 public class EnemyMover2 : MonoBehaviour  {
 
     private bool dirRight = true;
     public float speed = 1.0f;
 
     void Update () {
         if (dirRight)
             transform.Translate (Vector2.right * speed * Time.deltaTime);
         else
             transform.Translate (-Vector2.right * speed * Time.deltaTime);
         
         if(transform.position.x >= 50.0f) {
             dirRight = false;
         }
         
         if(transform.position.x <= 35.2f) {
             dirRight = true;
         }
     }
 }

