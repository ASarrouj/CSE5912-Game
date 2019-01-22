using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mouseover : MonoBehaviour
{
    public AudioClip menuSound;
    public Color selectColor = Color.red;
    public Color defaultColor = Color.white;

    private Renderer r;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        r = GetComponent<Renderer>();
        r.material.color = defaultColor;
    } 

    void OnMouseEnter() {
        r.material.color = selectColor;
        source.PlayOneShot(menuSound);

        foreach (Transform child in transform.parent) {
            if (child != transform) {
                child.GetComponent<Renderer>().material.color = defaultColor;
            }
        }
    }

    void OnMouseExit() {
        r.material.color = defaultColor;
    }
}
