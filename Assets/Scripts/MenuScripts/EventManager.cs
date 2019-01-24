using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public AudioClip menuSound;
    public GameObject[] buttons;
    public Color selectColor = Color.red;
    public Color defaultColor = Color.white;

    private AudioSource source;
    private float hor;
    private float vert;
    private int currentButton;
    private bool ignore;
    private float scrollTimer;

    // Start is called before the first frame update
    void Start()
    {
        ignore = false;
        scrollTimer = 0f;
        currentButton = 0;
        SetButton(currentButton); 
        buttons[0].GetComponent<Renderer>().material.color = selectColor;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollTimer > 0) {
            scrollTimer -= Time.deltaTime;
        }

        if (!ignore && scrollTimer <= 0f) {
            hor = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
            HandleButtonSelection();

            if (Input.GetButtonDown("Submit")) {
                ignore = true;
                EventSystem.current.currentSelectedGameObject.SendMessage("TakeAction");
            }
        }
    }
    
    private void HandleButtonSelection() {
        if (hor == 0 && vert == 0) {
            for (int i = 0; i < buttons.Length; i++) {
                if (buttons[i].GetComponent<Renderer>().material.color == selectColor) {
                    EventSystem.current.SetSelectedGameObject(buttons[i]);
                    currentButton = i;
                    SetButton(currentButton);
                    break;
                }
            }
            //return;
        } else if (hor > 0 || vert > 0) {
            ScrollPrev();
        } else if (hor < 0 || vert < 0) {
            ScrollNext();
        }

        if (EventSystem.current.currentSelectedGameObject != null) {
            foreach (GameObject b in buttons) {
                clearButton(b);
            }
            highlightButton(EventSystem.current.currentSelectedGameObject);
        }
    }

    private void ScrollNext() {
        if (currentButton < buttons.Length - 1) {
            currentButton++;
            SetButton(currentButton);
            source.PlayOneShot(menuSound);
        }
        scrollTimer = 0.25f;
    }

    private void ScrollPrev() {
        if (currentButton > 0) {
            currentButton--;
            SetButton(currentButton);
            source.PlayOneShot(menuSound);
        }
        scrollTimer = 0.25f;
    }

    private void SetButton(int index) {
        EventSystem.current.SetSelectedGameObject(buttons[index]);
    }

    private void highlightButton(GameObject button) {
        button.GetComponent<Renderer>().material.color = selectColor;
    }

    private void clearButton(GameObject button) {
        button.GetComponent<Renderer>().material.color = defaultColor;
    }
}
