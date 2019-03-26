using UnityEngine;
using UnityEngine.Networking;
public class MoonNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        TerrainGenerator tg = GameObject.Find("Terrain").GetComponent<TerrainGenerator>();

        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        Vector3 pos = tg.getSpawnPoint();
        player.transform.position = pos;
    }
}