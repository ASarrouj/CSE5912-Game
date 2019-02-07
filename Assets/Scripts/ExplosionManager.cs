using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip clip;
    public AudioSource source;
    void Start()
    {
                source=GetComponent<AudioSource>();
    }
    public void playSound()
    {
            source.PlayOneShot(clip,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
