using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class RoundsManager : NetworkBehaviour
    {
        public int numRounds;
        public int roundLengthSeconds = 180;
        public LobbyManager lobbyManager;

        private int roundCount;
        private int totalPlayersEver = 0;
        private List<PlayerStatus> playerStatuses;
        private int numPlayers;
        public float roundStartTime;
        private bool newRoundStarting = false;

        private void Awake()
        {
            lobbyManager = LobbyManager.s_Singleton;
        }

        private void Start()
        {
            name = "RoundsManager";
            DontDestroyOnLoad(gameObject);
            numPlayers = lobbyManager.numPlayers;
            totalPlayersEver = lobbyManager.numPlayers;
            roundCount = 0;
            playerStatuses = lobbyManager.Statuses;
            roundStartTime = Time.time;
        }

        private void Update()
        {
            if (Time.time > roundStartTime + roundLengthSeconds && !newRoundStarting)
            {
                roundCount++;
                if (roundCount >= numRounds)
                {
                    foreach (PlayerStatus s in playerStatuses)
                    {
                        s.RpcSetVictoryText("Tie");
                        s.RpcSetTextActive(true);
                    }
                    StartCoroutine(EndMatch());
                }
                else
                {
                    foreach (PlayerStatus s in playerStatuses)
                    {
                        s.RpcSetVictoryText("Round Over");
                        s.RpcSetTextActive(true);
                    }
                    StartCoroutine(NewRound());
                }
            }
        }

        public void CheckRoundOver()
        {
            if (totalPlayersEver < 2)
            {
                return;
            }

            int destroyedPlayerCount = 0;
            foreach (PlayerStatus s in playerStatuses)
            {
                if (s.Destroyed == true)
                {
                    destroyedPlayerCount++;
                }
            }


            if (destroyedPlayerCount >= numPlayers - 1)
            {
                roundCount++;
                if (roundCount >= numRounds)
                {
                    foreach (PlayerStatus s in playerStatuses)
                    {
                        if (s.Destroyed == false)
                        {
                            s.RpcSetVictoryText("Victory");
                        }
                        else
                        {
                            s.RpcSetVictoryText("Defeat");
                        }
                        s.RpcSetTextActive(true);
                        StartCoroutine(EndMatch());
                    }
                }
                else
                {
                    foreach (PlayerStatus s in playerStatuses)
                    {
                        if (s.Destroyed == false)
                        {
                            s.RpcSetVictoryText("Round Win");
                        }
                        else
                        {
                            s.RpcSetVictoryText("Round Loss");
                        }
                        s.RpcSetTextActive(true);
                    }
                    StartCoroutine(NewRound());
                }
            }
        }

        private IEnumerator NewRound()
        {
            newRoundStarting = true;
            Cursor.lockState = CursorLockMode.None;
            yield return new WaitForSecondsRealtime(3);
            foreach (PlayerStatus s in playerStatuses)
            {
                s.RpcSetTextActive(false);
            }
            Debug.Log("coroutine");
            List<GameObject> newPlayers = new List<GameObject>();
            for (int i = 0; i < playerStatuses.Count; i++)
            {
                GameObject obj = Instantiate(lobbyManager.gamePlayerPrefab.gameObject) as GameObject;
                newPlayers.Add(obj);

                // get start position from base class
                Transform startPos = lobbyManager.GetStartPosition();
                if (startPos != null)
                {
                    obj.transform.position = startPos.transform.position;
                    obj.transform.rotation = startPos.rotation;
                }
                else
                {
                    obj.transform.position = startPos.transform.position;
                    obj.transform.rotation = startPos.rotation;
                }
                NetworkServer.ReplacePlayerForConnection(playerStatuses[i].connectionToClient, obj, playerStatuses[i].playerControllerId);
                Destroy(playerStatuses[i].gameObject);
            }
            playerStatuses.Clear();

            roundStartTime = Time.time;
            foreach (GameObject o in newPlayers)
            {
                playerStatuses.Add(o.GetComponent<PlayerStatus>());
                Timer t = o.GetComponent<Timer>();
                t.startTime = roundStartTime;
                t.roundLength = roundLengthSeconds;
            }

            newRoundStarting = false;
        }

        private IEnumerator EndMatch()
        {
            yield return new WaitForSecondsRealtime(3);
            lobbyManager.StopClient();
            lobbyManager.StopHost();
            lobbyManager.StopMatchMaker();
            lobbyManager.ChangeTo(lobbyManager.gamesPanel);
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}