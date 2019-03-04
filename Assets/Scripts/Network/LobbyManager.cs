using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyManager : NetworkLobbyManager
{

    private Text lobbyTitle;
    private InputField matchNameField;
    private InputField ipAddress;

    private void Start()
    {
        lobbyTitle = GameObject.Find("LobbyTitle").GetComponent<Text>();
        matchNameField = GameObject.Find("MatchName").GetComponent<InputField>();
        ipAddress = GameObject.Find("IPAddress").GetComponent<InputField>();
    }

    public void CreateMatch()
    {
        if (matchNameField.text != "")
        {
            singleton.matchName = matchNameField.text;
            lobbyTitle.text = singleton.matchName;
        }
        singleton.StartHost();
    }

    public void JoinMatch()
    {
        singleton.networkAddress = ipAddress.text;
        singleton.StartClient();
    }
}

