using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NatureManager : MonoBehaviour
{
    public GameObject treePrefab, carrotPrefab, waterSpotPrefab, deepWaterSpotPrefab;
    public GameObject treesParent, carrotsParent, waterSpotParent, deepWaterSpotParent;
    public GameObject waterPlane;

    private List<GameObject> treesList = new List<GameObject>();
    public static List<GameObject> carrotsList = new List<GameObject>();
    public static List<GameObject> waterSpotList = new List<GameObject>();
    public static List<GameObject> deepWaterSpotList = new List<GameObject>();

    public static bool spawnCarrot_s;

    private GameObject _waterPlane;

    private float _waterLevel = .15f;
    private float _sandLevel = .30f;
    private float _forestLevel = .50f;
    private float _deepForestLevel = .85f;

    private bool _spawnWaterPlane = false;

    public void Awake()
    {
        carrotsList.Clear();
        waterSpotList.Clear();
        deepWaterSpotList.Clear();
    }

    public void Update()
    {
        if (spawnCarrot_s)
            NewCarrotWave();
    }

    public void SpawnNature()
    {
        Debug.Log("Spawning trees ... ");
        TerrainDatas.spawnTrees = false;

        _spawnWaterPlane = false;

        int xSize = TerrainDatas.xSize;
        int zSize = TerrainDatas.zSize;

        for (int z = 0; z <= zSize; z += 2)
        {
            for (int x = 0; x <= xSize; x += 2)
            {
                if ((TerrainDatas.heightMap[z, x] > _deepForestLevel)) //DeepForest
                {
                    if (Random.Range(0f, 1f) > .92f)
                    {
                        SpawnTree(z * (xSize + 1) + x, TerrainDatas.heightMap[z, x], "Deep Forest");
                    }
                    else if (Random.Range(0f, 1f) > .70f)
                    {
                        SpawnCarrot(z * (xSize + 1) + x, z, x);
                    }
                }
                else if ((TerrainDatas.heightMap[z, x] > _forestLevel)) //Forest
                {
                    if (Random.Range(0f, 1f) > .96f)
                    {
                        SpawnTree(z * (xSize + 1) + x, TerrainDatas.heightMap[z, x], "Forest");
                    }
                    else if (Random.Range(0f, 1f) > .97f)
                    {
                        SpawnCarrot(z * (xSize + 1) + x, z, x);
                    }
                }
                else if ((TerrainDatas.heightMap[z, x] > _sandLevel)) //Sand
                {
                    if (Random.Range(0f, 1f) > .995f)
                    {
                        SpawnTree(z * (xSize + 1) + x, TerrainDatas.heightMap[z, x], "Sand");
                    }
                    else if (Random.Range(0f, 1f) > .99f)
                    {
                        SpawnCarrot(z * (xSize + 1) + x, z, x);
                    }
                }
                else if (TerrainDatas.heightMap[z, x] > .22f && TerrainDatas.heightMap[z, x] <= .25f) //Water
                {
                    SpawnWaterSpot(z * (xSize + 1) + x);

                    if (TerrainDatas.heightMap[z, x] > .239f && TerrainDatas.heightMap[z, x] <= .24f && !_spawnWaterPlane)
                        SpawnWaterPlane(z * (xSize + 1) + x);

                }
                else
                {
                    if (TerrainDatas.heightMap[z, x] < .22f)
                        SpawnDeepWaterSpot(z * (xSize + 1) + x);
                }
            }
        }
        Debug.Log("Number of trees : " + treesList.Count);
        Debug.Log("Number of carrots : " + carrotsList.Count);
        Debug.Log("Trees spawned !");
    }

    public void NewCarrotWave()
    {
        spawnCarrot_s = false;

        int xSize = TerrainDatas.xSize;
        int zSize = TerrainDatas.zSize;

        for (int z = 0; z <= zSize; z += 2)
        {
            for (int x = 0; x <= xSize; x += 2)
            {
                if(!TerrainDatas.natureMap[z * (xSize + 1) + x])
                {
                    if ((TerrainDatas.heightMap[z, x] > _deepForestLevel))
                    { //DeepForest
                        if (Random.Range(0f, 1f) > .70f)
                            SpawnCarrot(z * (xSize + 1) + x, z, x);
                    }
                    else if ((TerrainDatas.heightMap[z, x] > _forestLevel))
                    { //Forest
                        if (Random.Range(0f, 1f) > .97f)
                            SpawnCarrot(z * (xSize + 1) + x, z, x);
                    }
                    else if ((TerrainDatas.heightMap[z, x] > _sandLevel))
                    { //Sand
                        if (Random.Range(0f, 1f) > .99f)
                            SpawnCarrot(z * (xSize + 1) + x, z, x);
                    }
                }
            }
        }
    }

    private void SpawnTree(int index, float height, string layer)
    {
        GameObject tree = Instantiate(treePrefab, TerrainDatas.vertices[index], Quaternion.Euler(0, Random.Range(0, 359), 0));
        tree.transform.parent = treesParent.transform;

        TerrainDatas.natureMap[index] = true;

        treesList.Add(tree);

        StoreTreeData(tree, height, layer);
    }

    private void SpawnCarrot(int index, int z, int x)
    {
        if (z >= 1 && x >= 1 && z < TerrainDatas.zSize && x < TerrainDatas.xSize) //check spawn on map edge 
        {
            GameObject carrot = Instantiate(carrotPrefab, TerrainDatas.vertices[index], Quaternion.Euler(0, Random.Range(0, 359), 0));
            carrot.transform.parent = carrotsParent.transform;

            carrot.GetComponent<Carrot>().SetIndex(index);
            
            TerrainDatas.natureMap[index] = true; //Nature element is now present on that vertice

            carrotsList.Add(carrot);
        }
    }

    private void SpawnWaterSpot(int index)
    {
        GameObject waterSpot = Instantiate(waterSpotPrefab, TerrainDatas.vertices[index], Quaternion.Euler(0, 0, 0));
        waterSpot.transform.parent = waterSpotParent.transform;

        waterSpotList.Add(waterSpot);
    }

    private void SpawnDeepWaterSpot(int index)
    {
        GameObject deepWaterSpot = Instantiate(deepWaterSpotPrefab, TerrainDatas.vertices[index], Quaternion.Euler(0, 0, 0));
        deepWaterSpot.transform.parent = deepWaterSpotParent.transform;


        deepWaterSpotList.Add(deepWaterSpot);
    }

    private void StoreTreeData(GameObject tree, float height, string layer)
    {
        tree.GetComponent<Tree_>().height = height;
        tree.GetComponent<Tree_>().layerType = layer;
    }

    private void SpawnWaterPlane(int index)
    {
        _spawnWaterPlane = true;

        float xPos = TerrainDatas.xSize / 2 / TerrainDatas.mapScale;
        float zPos = TerrainDatas.zSize / 2 / TerrainDatas.mapScale;

        _waterPlane = Instantiate(waterPlane, new Vector3(xPos, TerrainDatas.vertices[index].y, zPos), Quaternion.identity);
        _waterPlane.transform.parent = gameObject.transform;
        _waterPlane.transform.localScale = new Vector3(TerrainDatas.xSize / TerrainDatas.mapScale, 1, TerrainDatas.zSize / TerrainDatas.mapScale) * .1f;
        //.1f because in unity, plane scale is multiplied by 10
    }

    void DestroyNatureElements()
    {
        foreach (GameObject gameObject in treesList)
            Destroy(gameObject);
        foreach (GameObject gameObject in carrotsList)
            Destroy(gameObject);
        foreach (GameObject gameObject in waterSpotList)
            Destroy(gameObject);
        foreach (GameObject gameObject in deepWaterSpotList)
            Destroy(gameObject);

        Destroy(_waterPlane);

        treesList.Clear();
        carrotsList.Clear();
        waterSpotList.Clear();
        deepWaterSpotList.Clear();
    }
}