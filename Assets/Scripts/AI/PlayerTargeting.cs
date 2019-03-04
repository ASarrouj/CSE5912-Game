using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    private GameObject[] players;
    private int updateCount = 0;

    public GameObject Target { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GetPlayers();
        Target = FindClosestPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        updateCount++;
        if (updateCount > 9) {
            GetPlayers();
            Target = FindClosestPlayer();
            updateCount = 0;
        }
    }

    public float TargetAngle() {
        Vector3 tarPos = transform.InverseTransformPoint(Target.transform.position);
        return Mathf.Atan2(tarPos.x, tarPos.z) * (180 / Mathf.PI);
    }

    private void GetPlayers() {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private GameObject FindClosestPlayer() {
        GameObject closestPlayer = null;
        float dist, minDist = Mathf.Infinity;
        foreach (GameObject p in players) {
            dist = Vector3.Magnitude(p.transform.position - transform.position);
            if (dist < minDist) {
                minDist = dist;
                closestPlayer = p;
            }
        }
        return closestPlayer;
    }
}
