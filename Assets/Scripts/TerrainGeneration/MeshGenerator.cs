using System.Collections;
using System.Security;
using UnityEngine;
using UnityEngine.Networking.Match;

public class MeshGenerator
{
    public static Vector3[] GenerateVerticesArray(int xSize, int zSize, float[,] heightMap, float heightMultiplier, float mapScale)
    {
        Vector3[] vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int z = 0; z <= zSize; z++)
            for (int x = 0; x <= xSize; x++)
            {
                if(heightMap != null)
                    vertices[z * (xSize + 1) + x] = new Vector3(x / mapScale, heightMap[z, x] * heightMultiplier, z / mapScale);
                else //Security
                    vertices[z * (xSize + 1) + x] = new Vector3(x / mapScale, 0, z / mapScale);
            }

        TerrainDatas.GetInVerticesFunction(xSize, zSize, heightMultiplier, mapScale);
        return vertices;
    }

    public static int[] GenerateTriangleArray(int xSize, int zSize) 
    {
        int[] triangles = new int[xSize * zSize * 6];
   
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert;
                triangles[tris + 1] = triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 2] = triangles[tris + 3] = vert + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        return triangles;
    }

    public static float[,] GenerateHeightMap(int xSize, int zSize, float noiseScale, int octaves, float persistance, float lacunarity, int seed, Vector2 offset)
    {
        float[,] heightMap = new float[zSize + 1, xSize + 1];

        System.Random random = new System.Random(seed); //random generator
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = random.Next(-10000, 10000) + offset.x; //random X position
            float offsetY = random.Next(-10000, 10000) + offset.y; //random Y position
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        
        for(int y = 0; y <= zSize; y++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                float halfX = xSize / 2; //Noise will be scaled from the center and not from the top right corner
                float halfY = zSize / 2;

                for(int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfX) / noiseScale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfY) / noiseScale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; // '2 -1' makes result more interesting (more contrast)
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;


                heightMap[y, x] = noiseHeight;
            }
        }
        
        for (int y = 0; y <= zSize; y++)
            for (int x = 0; x <= xSize; x++)
                heightMap[y, x] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heightMap[y, x]); //clamping values between 0 - 1


        TerrainDatas.GetMinAndMaxTerrainHeight(minNoiseHeight, maxNoiseHeight); //Passing min & max terrain height
        TerrainDatas.GetInHeightMapFunction(noiseScale, octaves, persistance, lacunarity, seed, heightMap);
        return heightMap;
    }

    public static Color[] GenerateColorMap(int xSize, int zSize, Vector3[] vertices, Gradient terrainGradient)
    {
        Color[] colors = new Color[(xSize + 1) * (zSize + 1)];

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y <= zSize; y++) //Getting min & max height values
            for(int x = 0; x <= xSize; x++)
            {
                if (vertices[y * (xSize + 1) + x].y > maxNoiseHeight)
                    maxNoiseHeight = vertices[y * (xSize + 1) + x].y;
                else if (vertices[y * (xSize + 1) + x].y < minNoiseHeight)
                    minNoiseHeight = vertices[y * (xSize + 1) + x].y;
            }

        for(int y = 0; y <= zSize; y++)
            for(int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, vertices[y * (xSize + 1) + x].y);
                colors[y * (xSize + 1) + x] = terrainGradient.Evaluate(height);
            }

        return colors;
    }

    public static bool[] GenerateNatureMap(int xSize, int zSize)
    {
        bool[] natureMap = new bool[(xSize + 1) * (zSize + 1)];
        TerrainDatas.GetNatureMap(natureMap);

        return natureMap;
    }

    public static Mesh UpdateMesh(Mesh mesh, MeshCollider meshCollider, Vector3[] vertices, int[] triangles, Color[] colors)
    {
        mesh.Clear();
        //Mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); //Lighting recalculation
        //Collider
        mesh.RecalculateBounds(); //Physics recalculation
        meshCollider.sharedMesh = mesh;
        //Color
        mesh.colors = colors;


        TerrainDatas.GetInUpdateFunction(vertices, triangles, colors);
        return mesh;
    }
}
