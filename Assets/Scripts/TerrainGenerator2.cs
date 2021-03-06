﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TerrainGenerator2 : MonoBehaviour
{

    private TerrainData terrainData;
    private Terrain terrain;
    public TerrainLayer[] terrainLayers;
    private float[,] heights;
    private int maxIndex, i;
    public float roughness, initialDisplacement;
    private float displacement;
    private Transform glassDome;
    private Mesh domeMesh;

    private float spawnRadius;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        maxIndex = terrainData.heightmapWidth - 1;
        displacement = initialDisplacement;

        terrainData.terrainLayers = terrainLayers;
        heights = terrainData.GetHeights(0, 0, maxIndex + 1, maxIndex + 1);

        for (int i = 0; i < maxIndex + 1; i++)
            for (int j = 0; j < maxIndex + 1; j++)
            {
                heights[i, j] = 0;
            }

        terrainData.SetHeights(0, 0, heights);

        heights[0, 0] = 0;
        heights[maxIndex, 0] = 0;
        heights[0, maxIndex] = 0;
        heights[maxIndex, maxIndex] = 0;

        InvokeRepeating("DiamondSquareAlgo", 5, 1);
        i = maxIndex;


        /*CreateGlassDome();

        spawnRadius = glassDome.localScale.x / 2 - 30;

        float deg = 0;
        for (int i = 0; i < spawnPos.Length; i++)
        {
            Vector3 platPos = new Vector3(Mathf.Cos(deg), 0, Mathf.Sin(deg)) * spawnRadius + glassDome.transform.position;
            platPos.y = terrain.SampleHeight(platPos) + 2;
            deg += 2f * Mathf.PI / spawnPos.Length;
            Instantiate(spawnPlatform, platPos, Quaternion.identity);
            spawnPos[i].transform.position = platPos + new Vector3(0, 5, 0);
            spawnPos[i].AddComponent<NetworkStartPosition>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DiamondSquareAlgo()
    {
        if (i > 1)
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
            i = i / 2;
        }
        terrainData.SetHeights(0, 0, heights);
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
}
