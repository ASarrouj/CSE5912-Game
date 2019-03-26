using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TerrainGenerator : MonoBehaviour
{

    private TerrainData terrainData;
    private Terrain terrain;
    public TerrainLayer[] terrainLayers;
    private float[,] heights;
    private int maxIndex;
    public float roughness, initialDisplacement;
    private float displacement;
    private Transform glassDome;
    public GameObject[] spawnPos;
    public GameObject spawnPlatform;
    private Mesh domeMesh;
    public GameObject[] buildingPrefabs;

    private float spawnRadius;

    private int playersSpawnedCount;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        maxIndex = terrainData.heightmapWidth - 1;
        displacement = initialDisplacement;

        terrainData.terrainLayers = terrainLayers;
        heights = terrainData.GetHeights(0, 0, maxIndex + 1, maxIndex + 1);

        heights[0, 0] = 0;
        heights[maxIndex, 0] = 0;
        heights[0, maxIndex] = 0;
        heights[maxIndex, maxIndex] = 0;

        DiamondSquareAlgo();

        terrainData.SetHeights(0, 0, heights);

        CreateGlassDome();

        spawnRadius = glassDome.localScale.x / 2 - 30;
        playersSpawnedCount = 0;

        int instancePerBuilding = 6;
        for (int i = 0; i < instancePerBuilding; i++)
        {
            for (int j = 0; j < buildingPrefabs.Length; j++)
            {
                Vector3 buildingPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * (spawnRadius - 40) + glassDome.transform.position;
                buildingPos.y = terrain.SampleHeight(buildingPos) + 1;
                Quaternion buildingRot = Quaternion.Euler(0, Random.Range(-180, 180), 0);
                Instantiate(buildingPrefabs[j], buildingPos, buildingRot, transform);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DiamondSquareAlgo()
    {
        for (int i = maxIndex; i > 1; i = i / 2)
        {
            int midpoint = i / 2;

            for (int y = midpoint; y < maxIndex; y += i)
            {
                for (int x = midpoint; x < maxIndex; x += i)
                {
                    heights[x, y] = ((heights[x - midpoint, y - midpoint] + heights[x - midpoint, y + midpoint] +
                        heights[x + midpoint, y - midpoint] + heights[x + midpoint, y + midpoint]) / 4) + Random.Range(-displacement, displacement);
                }
            }

            for (int y = 0; y < maxIndex + 1; y += midpoint)
            {
                int x;

                if (y % i == 0)
                    x = midpoint;
                else
                    x = 0;

                while (x < maxIndex + 1)
                {

                    float sum = 0;
                    int denominator = 0;

                    try { sum += heights[x + midpoint, y]; denominator++; } catch { }
                    try { sum += heights[x - midpoint, y]; denominator++; } catch { }
                    try { sum += heights[x, y + midpoint]; denominator++; } catch { }
                    try { sum += heights[x, y - midpoint]; denominator++; } catch { }

                    heights[x, y] = (sum / denominator) + Random.Range(-displacement, displacement) / 2;

                    x += i;
                }
            }
            displacement *= Mathf.Pow(2, -roughness);
        }
    }

    private void CreateGlassDome()
    {
        glassDome = transform.GetChild(0);
        glassDome.position = transform.position + new Vector3(terrainData.size.x, 0, terrainData.size.z) / 2;
        glassDome.localScale = new Vector3(terrainData.size.x, Mathf.Min(terrainData.size.x, terrainData.size.z), terrainData.size.z) / 2;

        domeMesh = glassDome.GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < domeMesh.normals.Length; i++)
        {
            domeMesh.normals[i] = -domeMesh.normals[i];
        }

        for (int m = 0; m < domeMesh.subMeshCount; m++)
        {
            int[] triangles = domeMesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            domeMesh.SetTriangles(triangles, m);
        }
        glassDome.gameObject.AddComponent<MeshCollider>();
    }

    public Vector3 getSpawnPoint()
    {
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        float deg = 0;
        int i = playersSpawnedCount;

        Vector3 platPos = new Vector3(Mathf.Cos(deg), 0, Mathf.Sin(deg)) * spawnRadius + glassDome.transform.position;
        platPos.y = terrain.SampleHeight(platPos) + 2;
        deg += 2f * Mathf.PI / spawnPos.Length;
        Instantiate(spawnPlatform, platPos, Quaternion.identity);
        spawnPoint = platPos + new Vector3(0, 5, 0);
        playersSpawnedCount++;

        return spawnPoint;
    }
}
