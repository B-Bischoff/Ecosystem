using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsManager : MonoBehaviour
{
    //Prefabs
    public GameObject bunnyMPrefab, bunnyFPrefab;
    public GameObject babyBunnyMPrefab, babyBunnyFPrefab;

    public GameObject foxMPrefab, foxFPrefab;
    public GameObject babyFoxMPrefab, babyFoxFPrebaf;

    //Managers
    public GameObject BunnyManager, BabyBunnyManager;
    public GameObject FoxManager, BabyFoxManager;

    //Lists
    public static List<GameObject> bunnyList = new List<GameObject>();
    public static List<GameObject> babyBunnyList = new List<GameObject>();
    public static List<GameObject> foxList = new List<GameObject>();
    public static List<GameObject> babyFoxList = new List<GameObject>();

    //Counter
    public static int bunnyDead, bunnyDehydrated, bunnyStarved, bunnyEaten;
    public static int foxDead, foxDehydrated, foxStarved;

    //Private
    private Vector3 _spawnPos = new Vector3();

    private static GameObject _mother;
    private static int _numberOfChild;

    private void Awake()
    {
        //Clear lists to remove previous datas
        bunnyList.Clear();
        babyBunnyList.Clear();
        foxList.Clear();
        babyFoxList.Clear();
    }

    public void Update()
    {
        if (_mother != null)
            BabyAnimalBorn(_mother);
    }

    public void SpawnEntity(string entity) //Method that randomly spawn male / female entity 
    {
        RandomSpawnInMap();
        if (Random.Range(0, 1f) > .5f) //Female
        {
            if (entity == "Bunny")
            {
                GameObject bunnyF = Instantiate(bunnyFPrefab, _spawnPos, Quaternion.identity);
                bunnyF.transform.parent = BunnyManager.transform;
                bunnyList.Add(bunnyF);
                bunnyF.GetComponent<Bunny>().setGender("Female");
            }
            else if (entity == "Fox")
            {
                GameObject foxF = Instantiate(foxFPrefab, _spawnPos, Quaternion.identity);
                foxF.transform.parent = FoxManager.transform;
                foxList.Add(foxF);
                foxF.GetComponent<Fox>().setGender("Female");
            }
        }
        else //Male
        {
            if (entity == "Bunny")
            {
                GameObject bunnyM = Instantiate(bunnyMPrefab, _spawnPos, Quaternion.identity);
                bunnyM.transform.parent = BunnyManager.transform;
                bunnyList.Add(bunnyM);
                bunnyM.GetComponent<Bunny>().setGender("Male");
            }
            else if (entity == "Fox")
            {
                GameObject foxM = Instantiate(foxMPrefab, _spawnPos, Quaternion.identity);
                foxM.transform.parent = FoxManager.transform;
                foxList.Add(foxM);
                foxM.GetComponent<Fox>().setGender("Male");
            }
        }
    }

    public void SpawnEntity(string entity, string gender) //Method that spawn chosen gender entity
    {
        RandomSpawnInMap();
        if (gender == "Female") //Female
        {
            if (entity == "Bunny")
            {
                GameObject bunnyF = Instantiate(bunnyFPrefab, _spawnPos, Quaternion.identity);
                bunnyF.transform.parent = BunnyManager.transform;
                bunnyList.Add(bunnyF);
                bunnyF.GetComponent<Bunny>().setGender("Female");
            }
            else if (entity == "Fox")
            {
                GameObject foxF = Instantiate(foxFPrefab, _spawnPos, Quaternion.identity);
                foxF.transform.parent = FoxManager.transform;
                foxList.Add(foxF);
                foxF.GetComponent<Fox>().setGender("Female");
            }
        }
        else //Male
        {
            if (entity == "Bunny")
            {
                GameObject bunnyM = Instantiate(bunnyMPrefab, _spawnPos, Quaternion.identity);
                bunnyM.transform.parent = BunnyManager.transform;
                bunnyList.Add(bunnyM);
                bunnyM.GetComponent<Bunny>().setGender("Male");
            }
            else if (entity == "Fox")
            {
                GameObject foxM = Instantiate(foxMPrefab, _spawnPos, Quaternion.identity);
                foxM.transform.parent = FoxManager.transform;
                foxList.Add(foxM);
                foxM.GetComponent<Fox>().setGender("Male");
            }
        }
    }

    public void RandomSpawnInMap()
    {
        bool validSpawn = false;

        int x = 0;
        int z = 0;

        while (!validSpawn)
        {
            x = Random.Range(0, TerrainDatas.xSize);
            z = Random.Range(0, TerrainDatas.zSize);

            if (TerrainDatas.heightMap[z, x] > .25f) //Above Water
                validSpawn = true;
        }

        float xPos = x / TerrainDatas.mapScale;
        float zPos = z / TerrainDatas.mapScale;
        float yPos = TerrainDatas.vertices[z * (TerrainDatas.xSize + 1) + x].y + 1f;

        _spawnPos = new Vector3(xPos, yPos, zPos);
        //Debug.Log("Bunny SpawnPos : " + _spawnPos);
    }

    public void BabyAnimalBorn(GameObject mother)
    {
        _mother = null;
        for (int i = 0; i < _numberOfChild; i++)
        {
            if (mother.GetComponent<Bunny>() != null) //bunny
            {         
                if (Random.Range(0, 1f) > .5f) //Female
                {
                    GameObject babyBunnyF = Instantiate(babyBunnyFPrefab, mother.transform.position, Quaternion.identity);
                    babyBunnyF.transform.parent = BabyBunnyManager.transform;
                    babyBunnyList.Add(babyBunnyF);
                    babyBunnyF.GetComponent<Bunny>().setBaby(true);
                    babyBunnyF.GetComponent<Bunny>().SetParentsCarac(mother.GetComponent<Bunny>().GetParentsCarac());
                    babyBunnyF.GetComponent<Bunny>().setGender("Female");
                }
                else //Male
                {
                    GameObject babyBunnyM = Instantiate(babyBunnyMPrefab, mother.transform.position, Quaternion.identity);
                    babyBunnyM.transform.parent = BabyBunnyManager.transform;
                    babyBunnyList.Add(babyBunnyM);
                    babyBunnyM.GetComponent<Bunny>().setBaby(true);
                    babyBunnyM.GetComponent<Bunny>().SetParentsCarac(mother.GetComponent<Bunny>().GetParentsCarac());
                    babyBunnyM.GetComponent<Bunny>().setGender("Male");
                }
            }
            else //Fox
            {
                if (Random.Range(0, 1f) > .5f) //Female
                {
                    GameObject babyFoxF = Instantiate(babyFoxFPrebaf, mother.transform.position, Quaternion.identity);
                    babyFoxF.transform.parent = BabyFoxManager.transform;
                    babyFoxList.Add(babyFoxF);
                    babyFoxF.GetComponent<Fox>().setBaby(true);
                    babyFoxF.GetComponent<Fox>().SetParentsCarac(mother.GetComponent<Fox>().GetParentsCarac());
                    babyFoxF.GetComponent<Fox>().setGender("Female");
                }
                else //Male
                {
                    GameObject babyFoxM = Instantiate(babyFoxMPrefab, mother.transform.position, Quaternion.identity);
                    babyFoxM.transform.parent = BabyFoxManager.transform;
                    babyFoxList.Add(babyFoxM);
                    babyFoxM.GetComponent<Fox>().setBaby(true);
                    babyFoxM.GetComponent<Fox>().SetParentsCarac(mother.GetComponent<Fox>().GetParentsCarac());
                    babyFoxM.GetComponent<Fox>().setGender("Male");
                }
            }
        }
    }

    public static void GiveBirth(GameObject mother, int nbChild)
    {
        _mother = mother;
        _numberOfChild = nbChild;
    }

    private Vector3 SpawnNearMother(GameObject mother)
    {
        bool validSpawn = false;

        float x = 0;
        float z = 0;

        int nbTry = 0;

        Vector3 motherPos = mother.transform.position;
        while (!validSpawn && nbTry <= 200)
        {
            if (mother.GetComponent<Animals>() != null)
            {
                x = Random.Range(motherPos.x - .15f, motherPos.x + .15f) * (int)TerrainDatas.mapScale;
                z = Random.Range(motherPos.z - .15f, motherPos.z + .15f) * (int)TerrainDatas.mapScale;

                //Checking position : Map borders & Water
                if (x < TerrainDatas.xSize && x > 0 && z < TerrainDatas.zSize && z > 0 && TerrainDatas.heightMap[(int)z, (int)x] > .25f)
                    validSpawn = true;
                else
                    nbTry++;
            }
            else return Vector3.zero;
        }

        if (nbTry >= 200)
            Debug.Log("Many tries : " + nbTry);

        float xPos = x / TerrainDatas.mapScale;
        float zPos = z / TerrainDatas.mapScale;
        float yPos = TerrainDatas.vertices[(int)z * (TerrainDatas.xSize + 1) + (int)x].y + 1f;

        Vector3 spawnPos = new Vector3(xPos, yPos, zPos);
        Debug.Log("SpawnPos : " + spawnPos);
        return spawnPos;
    }
}
