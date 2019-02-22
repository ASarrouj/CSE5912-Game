﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadTargetScreenButton : MonoBehaviour
{

    public Image fadeOverlay;
    public float fadeDuration = 1f;
    public NetworkManager netManager;
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

    public void StartHost()
    {
        netManager.StartHost();
    }

    public void JoinGame()
    {
        //gameObject.SetActive(false);
        //ipInput.SetActive(true);
        //Debug.Log("joingame called, connecting to palware.space:7777");
        //netManager.networkAddress = "palware.space";
        //netManager.networkPort = 7777;
        netManager.StartClient();
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

    public void Quit()
    {

        Application.Quit();

    }
}

