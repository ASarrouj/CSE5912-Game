using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.NetworkLobby
{
    public class LobbyMainMenu : MonoBehaviour
    {
        public LobbyManager lobbyManager;

        public void OnClickPlay()
        {
            lobbyManager.ChangeTo(lobbyManager.gamesPanel);
            lobbyManager.backDelegate = lobbyManager.BackToMainClbk;
        }

        public void OnClickOptions()
        {
            lobbyManager.ChangeTo(lobbyManager.optionsPanel);
            lobbyManager.backDelegate = lobbyManager.BackToMainClbk;
            lobbyManager.backButton.gameObject.GetComponentInChildren<Text>().text = "Back";
        }

        public void OnClickCredits()
        {
            lobbyManager.ChangeTo(lobbyManager.creditsPanel);
            lobbyManager.backDelegate = lobbyManager.BackToMainClbk;
            lobbyManager.backButton.gameObject.GetComponentInChildren<Text>().text = "Back";
        }
    }
}
