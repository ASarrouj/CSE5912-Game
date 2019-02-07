 using UnityEngine;
 using System.Collections;
 
 public class ParticleDestroyer : MonoBehaviour 
 {
     public ParticleSystem ps;
    float timer=0.0f;
 
     public void Start() 
     {
         ps = GetComponent<ParticleSystem>();
     }
 
     public void Update() 
     {
        timer+=Time.deltaTime;
             if(timer>ps.main.duration)
             {
                 Destroy(gameObject);
             }
     }
 }