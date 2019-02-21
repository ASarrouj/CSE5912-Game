using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip music;

    private AudioSource source;
    private GameObject musicPlayer;
    private string sceneName, audioName = "menu_music";

    private void Awake() {
        musicPlayer = GameObject.Find(audioName);
        if (musicPlayer == null) {
            musicPlayer = gameObject;
            musicPlayer.name = audioName;
            source = GetComponent<AudioSource>();
            source.PlayOneShot(music);
            DontDestroyOnLoad(musicPlayer);
        } else if (gameObject.name != audioName) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (!(sceneName == "Main Menu" || sceneName == "Option" || sceneName == "Credit")) {
            Destroy(gameObject);
        }
    }
}
