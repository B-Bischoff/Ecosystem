using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshCreator : MonoBehaviour
{
    [Header("Terrain size :")]

    [Range(1, 254)] public int xSize;
    [Range(1, 254)] public int zSize;

    [Header("Scale :")]
    [Range(40f, 100)] public float noiseScale;
    public float mapScale = 1; //Personnal choice. It make the map more detailed
    public Vector2 offset;

    [Header("Relief :")]
    [Range(0.1f, 10)] public float heightMultiplier;
    [Range(1, 5)] public int octaves;
    [Range(0, 1)] public float persistance;
    [Range(0, 10)] public float lacunarity;

    [Header("Terrain Seed :")]
    public int seed;

    [Header("Terrain Colors :")]
    public Gradient terrainGradient;

    [HideInInspector] public bool terrainReady; //Used to spawn trees & bake navMesh once the mesh is created

    private Mesh _mesh;
    private MeshCollider _meshCollider;

    protected float[,] _heightMap;
    protected Vector3[] _vertices;
    private int[] _triangles;
    private Color[] _colors;
    private bool[] _natureMap;
  

    public void Start()
    {
        terrainReady = false;
        Init();
    }

    public void Update()
    {
        //CreateMesh(); //In Update() -> terrain will be re-generated Every frame - LAG
    }

    public void Init()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Procedural Terrain";
        _meshCollider = gameObject.GetComponent<MeshCollider>();
        CreateMesh(); //In Init() -> terrain will be generated once
    }

    public void CreateMesh()
    {
        _heightMap = MeshGenerator.GenerateHeightMap(xSize, zSize, noiseScale, octaves, persistance, lacunarity, seed, offset);
        _vertices = MeshGenerator.GenerateVerticesArray(xSize, zSize, _heightMap, heightMultiplier, mapScale);
        _triangles = MeshGenerator.GenerateTriangleArray(xSize, zSize);
        _colors = MeshGenerator.GenerateColorMap(xSize, zSize, _vertices, terrainGradient);
        _natureMap = MeshGenerator.GenerateNatureMap(xSize, zSize);
        _mesh = MeshGenerator.UpdateMesh(_mesh, _meshCollider, _vertices, _triangles, _colors);

        terrainReady = true;
        TerrainDatas.spawnTrees = true;
    }

}
