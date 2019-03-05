using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    private TerrainData terrainData;
    private Terrain terrain;
    public TerrainLayer[] terrainLayers;
    private float[,] heights;
    private int maxIndex;
    public float roughness, initialDisplacement;
    private float displacement;

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

    private void DiamondStep()
    {

    }

    private void SquareStep()
    {
        float sum = 0;
        int denominator = 0;

        /*if (offsetMidpoint.x - midpoint.x >= 0)
        {
            sum += heights[offsetMidpoint.x - midpoint.x, offsetMidpoint.y];
            denominator++;
        }
        if (offsetMidpoint.x + midpoint.x <= maxIndex)
        {
            sum += heights[offsetMidpoint.x + midpoint.x, offsetMidpoint.y];
            denominator++;
        }
        if (offsetMidpoint.y - midpoint.y >= 0)
        {
            sum += heights[offsetMidpoint.x, offsetMidpoint.y - midpoint.y];
            denominator++;
        }
        if (offsetMidpoint.y + midpoint.y <= maxIndex)
        {
            sum += heights[offsetMidpoint.x, offsetMidpoint.y + midpoint.y];
            denominator++;
        }

        heights[offsetMidpoint.x, offsetMidpoint.y] = (sum / denominator) + Random.Range(-displacement, displacement);
    }

    try { sum += heights[x + midpoint, y]; denominator++; } catch { }
                    try { sum += heights[x - midpoint, y]; denominator++; } catch { }
                    try { sum += heights[x, y + midpoint]; denominator++; } catch { }
                    try { sum += heights[x, y - midpoint]; denominator++; } catch { }*/
    }
}
