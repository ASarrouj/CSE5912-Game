using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadTargetScreenButton : MonoBehaviour
{

    public Image fadeOverlay;
    public float fadeDuration = 1f;
    public GameObject ipInput;

    public void LoadSceneNum(int num)
    {
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load scene " + num + "since SceneManager only has" + SceneManager.sceneCountInBuildSettings + "scenes in BuildSettings");
            return;
        }
        GameStateManager.LoadState(num);
    }

    public void QuickLoadScene(int num)
    {
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load scene " + num + "since SceneManager only has" + SceneManager.sceneCountInBuildSettings + "scenes in BuildSettings");
            return;
        }
        GameStateManager.QuickLoadState(num);
    }

    public void LoadSceneName(string name) {
        SceneManager.LoadScene(name);
    }

    public void ExitToMenu() {
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopMatchMaker();
        
        GameObject lobbyManager = GameObject.Find("LobbyManager");
        Prototype.NetworkLobby.LobbyManager l = lobbyManager.GetComponent<Prototype.NetworkLobby.LobbyManager>();
        l.ChangeTo(l.mainMenuPanel);
    }

    public void Quit()
    {

        Application.Quit();

    }
}

