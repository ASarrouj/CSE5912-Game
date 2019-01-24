using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool paused;
    public GameObject[] toStop;
    public GameObject pauseText;

    private void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                pauseText.SetActive(false);
                Resume();
                paused = false;
            }
            else
            {
                pauseText.SetActive(true);
                Halt();
                paused = true;
            }
        }
    }

    private void Halt()
    {
        foreach (GameObject moveable in toStop)
        {
            moveable.SendMessage("Halt", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Resume()
    {
        foreach (GameObject moveable in toStop)
        {
            moveable.SendMessage("Resume", SendMessageOptions.DontRequireReceiver);
        }
    }
}
