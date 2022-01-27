using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainMenuInputs : MonoBehaviour
{
    public GameObject meshGenerator, natureManager, navMeshManager, animalManager;
 
    [Header("terrain")]
    public float terrainXOffsetDefault;
    public float terrainZOffsetDefault, terrainZoomDefault;
    public int terrainZSizeDefault, terrainXSizeDefault, terrainSeedDefault;

    [Header("Bunny")]
    public int bunnyStartNumberDefault;

    [Header("Fox")]
    public int foxStartNumberDefault;

    public void Awake()
    {
        meshGenerator.GetComponent<MeshCreator>().offset.x = PlayerPrefs.GetFloat("TerrainXOffset", terrainXOffsetDefault);
        meshGenerator.GetComponent<MeshCreator>().offset.y = PlayerPrefs.GetFloat("TerrainZOffset", terrainZOffsetDefault);
        meshGenerator.GetComponent<MeshCreator>().noiseScale = PlayerPrefs.GetFloat("TerrainZoom", terrainZoomDefault);
        meshGenerator.GetComponent<MeshCreator>().zSize = PlayerPrefs.GetInt("TerrainZSize", terrainZSizeDefault);
        meshGenerator.GetComponent<MeshCreator>().xSize = PlayerPrefs.GetInt("TerrainXSize", terrainXSizeDefault);
        meshGenerator.GetComponent<MeshCreator>().seed = PlayerPrefs.GetInt("TerrainSeed", terrainSeedDefault);
    }

    public void Update()
    {
        if(meshGenerator.GetComponent<MeshCreator>().terrainReady) //Terrain created
        {
            meshGenerator.GetComponent<MeshCreator>().terrainReady = false;

            natureManager.GetComponent<NatureManager>().SpawnNature(); //Spawning trees & carrots
            navMeshManager.GetComponent<NavMeshManager>().BakeNavMesh(); //Generating navMesh


            // -> Pass animals stats
            SpawnAnimals(); //Spawning Bunny & Foxs according to user inputs (in main menu)
            enabled = false;
        }
    }

    private void SpawnAnimals()
    {
        //Bunny
        for (int i = 0; i < PlayerPrefs.GetInt("StartBunnyNumber", bunnyStartNumberDefault); i++)
        {
            if(i % 2 != 1) //One on two bunny will be female
                animalManager.GetComponent<AnimalsManager>().SpawnEntity("Bunny", "Female");
            else
                animalManager.GetComponent<AnimalsManager>().SpawnEntity("Bunny", "Male");
        }

        //Fox
        for (int i = 0; i < PlayerPrefs.GetInt("StartFoxNumber", foxStartNumberDefault); i++)
        {
            if (i % 2 != 1) //One on two fox will be female
                animalManager.GetComponent<AnimalsManager>().SpawnEntity("Fox", "Female");
            else
                animalManager.GetComponent<AnimalsManager>().SpawnEntity("Fox", "Male");
        }
    }
}
