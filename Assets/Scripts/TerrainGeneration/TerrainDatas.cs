using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainDatas
{
    public static int xSize;
    public static int zSize;

    public static float noiseScale;
    public static float mapScale;
    public static Vector2 offset;

    public static float heightMultiplier;
    public static int octaves;
    public static float persistance;
    public static float lacunarity;

    public static int seed;

    public static Gradient terrainGradient;

    public static float[,] heightMap;
    public static Vector3[] vertices;
    public static int[] triangles;
    public static Color[] colors;
    public static bool[] natureMap;

    public static float maxTerrainHeight;
    public static float minTerrainHeight;

    public static bool spawnTrees;

    public static void GetInVerticesFunction(int newxSize, int newzSize, float newHeightMultiplier, float newMapScale)
    {
        xSize = newxSize;
        zSize = newzSize;
        heightMultiplier = newHeightMultiplier;
        mapScale = newMapScale;
    }

    public static void GetInHeightMapFunction(float newNoiseScale, int newOctaves, float newPersistance, float newLacunarity, int newSeed, float[,] newHeightMap)
    {
        noiseScale = newNoiseScale;
        octaves = newOctaves;
        persistance = newPersistance;
        lacunarity = newLacunarity;
        seed = newSeed;
        heightMap = newHeightMap;
    }

    public static void GetInUpdateFunction(Vector3[] newVertices, int[] newTriangles, Color[] newColors)
    {
        vertices = newVertices;
        triangles = newTriangles;
        colors = newColors;
    }
    public static void GetMinAndMaxTerrainHeight(float newMinHeight, float newMaxHeight)
    {
        maxTerrainHeight = newMaxHeight;
        minTerrainHeight = newMinHeight;
    }

    public static void GetNatureMap(bool[] newNatureMap)
    {
        natureMap = newNatureMap;
    }
}
