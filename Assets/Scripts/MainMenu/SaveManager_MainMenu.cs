using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager_MainMenu : MonoBehaviour
{
    public GameObject canvas;

    [SerializeField] private int _saveSlot, _loadSlot;

    public void Awake()
    {
        //Create save folder if it does not exists
        SaveSystem_MainMenu.Init();
    }

    public void Save(int saveSlot)
    {
        SaveObject saveObject = new SaveObject { };

        //SAVE CARAC

        //Saving Datas into playerPrefs
        canvas.GetComponent<Terrain_MainMenu>().SavePlayerPrefs();
        canvas.GetComponent<Bunny_MainMenu>().SavePlayerPrefs();
        canvas.GetComponent<Fox_MainMenu>().SavePlayerPrefs();
        canvas.GetComponent<Start_MainMenu>().SavePlayerPrefs();

        //Storing playerPrefs datas into saveObject

        //Terrain
        saveObject.xSize = PlayerPrefs.GetInt("TerrainXSize");
        saveObject.zSize = PlayerPrefs.GetInt("TerrainZSize");
        saveObject.xOffset = PlayerPrefs.GetFloat("TerrainXOffset");
        saveObject.zOffset = PlayerPrefs.GetFloat("TerrainZOffset");
        saveObject.zoom = PlayerPrefs.GetFloat("TerrainZoom");
        saveObject.seed = PlayerPrefs.GetInt("TerrainSeed");

        //Bunny
        saveObject.bunny_reducRate =  PlayerPrefs.GetFloat("BunnyReducRate");
        saveObject.bunny_adultSpeed = PlayerPrefs.GetFloat("BunnySpeed");
        saveObject.bunny_adultViewDist =  PlayerPrefs.GetFloat("BunnyViewDist");
        saveObject.bunny_adultConsTime = PlayerPrefs.GetFloat("BunnyConsumTime");

        saveObject.bunny_adultHungerLim = PlayerPrefs.GetInt("BunnyHungerLim");
        saveObject.bunny_adultThirstLim = PlayerPrefs.GetInt("BunnyThirstLim");
        saveObject.bunny_hungerRepro = PlayerPrefs.GetInt("BunnyHungerRepro");
        saveObject.bunny_thirstRepro = PlayerPrefs.GetInt("BunnyThirstRepro");
        saveObject.bunny_adultFoodValue = PlayerPrefs.GetInt("BunnyFoodValue");
        saveObject.bunny_adultWaterValue = PlayerPrefs.GetInt("BunnyWaterValue");
        saveObject.bunny_gestTime = PlayerPrefs.GetInt("BunnyGestTime");
        saveObject.bunny_growthTime = PlayerPrefs.GetInt("BunnyGrowthTime");
        saveObject.bunny_litterMin = PlayerPrefs.GetInt("BunnyBabyLitterMin");
        saveObject.bunny_litterMax = PlayerPrefs.GetInt("BunnyBabyLitterMax");

        saveObject.bunny_babyHungerLim = PlayerPrefs.GetInt("BunnyBabyHungerLim");
        saveObject.bunny_babyThirstLim = PlayerPrefs.GetInt("BunnyBabyThirstLim");
        saveObject.bunny_babyFoodValue = PlayerPrefs.GetInt("BunnyBabyFoodValue");
        saveObject.bunny_babyWaterValue = PlayerPrefs.GetInt("BunnyBabyWaterValue");

        saveObject.bunny_babySpeed = PlayerPrefs.GetFloat("BunnyBabySpeed");
        saveObject.bunny_babyViewDist = PlayerPrefs.GetFloat("BunnyBabyViewDist");
        saveObject.bunny_babyConsTime = PlayerPrefs.GetFloat("BunnyBabyConsumTime");

        //Fox
        saveObject.fox_reducRate = PlayerPrefs.GetFloat("FoxReducRate");
        saveObject.fox_adultSpeed = PlayerPrefs.GetFloat("FoxSpeedA");
        saveObject.fox_adultViewDist = PlayerPrefs.GetFloat("FoxViewDistA");
        saveObject.fox_adultConsTime = PlayerPrefs.GetFloat("FoxConsumTimeA");
                   
        saveObject.fox_adultHungerLim = PlayerPrefs.GetInt("FoxHungerLimA");
        saveObject.fox_adultThirstLim = PlayerPrefs.GetInt("FoxThirstLimA");
        saveObject.fox_hungerRepro = PlayerPrefs.GetInt("FoxHungerRepro");
        saveObject.fox_thirstRepro = PlayerPrefs.GetInt("FoxThirstRepro");
        saveObject.fox_adultFoodValue = PlayerPrefs.GetInt("FoxFoodValueA");
        saveObject.fox_adultWaterValue = PlayerPrefs.GetInt("FoxWaterValueA");
        saveObject.fox_gestTime = PlayerPrefs.GetInt("FoxGestTime");
        saveObject.fox_growthTime = PlayerPrefs.GetInt("FoxGrowthTime");
        saveObject.fox_litterMin = PlayerPrefs.GetInt("FoxBabyLitterMin");
        saveObject.fox_litterMax = PlayerPrefs.GetInt("FoxBabyLitterMax");
                   
        saveObject.fox_babyHungerLim = PlayerPrefs.GetInt("FoxHungerLimB");
        saveObject.fox_babyThirstLim = PlayerPrefs.GetInt("FoxThirstLimB");
        saveObject.fox_babyFoodValue = PlayerPrefs.GetInt("FoxFoodValueB");
        saveObject.fox_babyWaterValue = PlayerPrefs.GetInt("FoxWaterValueB");
                   
        saveObject.fox_babySpeed = PlayerPrefs.GetFloat("FoxSpeedB");
        saveObject.fox_babyViewDist = PlayerPrefs.GetFloat("FoxViewDistB");
        saveObject.fox_babyConsTime = PlayerPrefs.GetFloat("FoxConsumTimeB");

        //Start section
        saveObject.bunnyStartNumber = PlayerPrefs.GetInt("StartBunnyNumber");
        saveObject.foxStartNumber = PlayerPrefs.GetInt("StartFoxNumber");
        saveObject.bunnyMaleColor = canvas.GetComponent<AnimalColor_MainMenu>().bunnyMaleColor;
        saveObject.bunnyFemaleColor = canvas.GetComponent<AnimalColor_MainMenu>().bunnyFemaleColor;
        saveObject.foxMaleColor = canvas.GetComponent<AnimalColor_MainMenu>().foxMaleColor;
        saveObject.foxFemaleColor = canvas.GetComponent<AnimalColor_MainMenu>().foxFemaleColor;

        //Converting saveObject into json format
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem_MainMenu.Save(json, saveSlot);
    }

    public void Load(int saveSlot)
    {
        string saveString = SaveSystem_MainMenu.Load(saveSlot);

        //Reassigning datas from json file to gameobjects
        if (saveString != null)
        {

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);


            //Loading datas from json file into playerPrefs
            Debug.Log("Loaded" + saveSlot);

            //Terrain
            PlayerPrefs.SetInt("TerrainXSize", saveObject.xSize);
            PlayerPrefs.SetInt("TerrainZSize", saveObject.zSize);
            PlayerPrefs.SetFloat("TerrainXOffset", saveObject.xOffset);
            PlayerPrefs.SetFloat("TerrainZOffset", saveObject.zOffset);
            PlayerPrefs.SetFloat("TerrainZoom", saveObject.zoom);
            PlayerPrefs.SetInt("TerrainSeed", saveObject.seed);

            //Bunny
            PlayerPrefs.SetFloat("BunnyReducRate", saveObject.bunny_reducRate);
            PlayerPrefs.SetFloat("BunnySpeed", saveObject.bunny_adultSpeed);
            PlayerPrefs.SetFloat("BunnyViewDist", saveObject.bunny_adultViewDist);
            PlayerPrefs.SetFloat("BunnyConsumTime", saveObject.bunny_adultConsTime);

            PlayerPrefs.SetInt("BunnyHungerLim", saveObject.bunny_adultHungerLim);
            PlayerPrefs.SetInt("BunnyThirstLim", saveObject.bunny_adultThirstLim);
            PlayerPrefs.SetInt("BunnyHungerRepro", saveObject.bunny_hungerRepro);
            PlayerPrefs.SetInt("BunnyThirstRepro", saveObject.bunny_thirstRepro);
            PlayerPrefs.SetInt("BunnyFoodValue", saveObject.bunny_adultFoodValue);
            PlayerPrefs.SetInt("BunnyWaterValue", saveObject.bunny_adultWaterValue);
            PlayerPrefs.SetInt("BunnyGestTime", saveObject.bunny_gestTime);
            PlayerPrefs.SetInt("BunnyGrowthTime", saveObject.bunny_growthTime);
            PlayerPrefs.SetInt("BunnyBabyLitterMin", saveObject.bunny_litterMin);
            PlayerPrefs.SetInt("BunnyBabyLitterMax", saveObject.bunny_litterMax);

            PlayerPrefs.SetInt("BunnyBabyHungerLim", saveObject.bunny_babyHungerLim);
            PlayerPrefs.SetInt("BunnyBabyThirstLim", saveObject.bunny_babyThirstLim);
            PlayerPrefs.SetInt("BunnyBabyFoodValue", saveObject.bunny_babyFoodValue);
            PlayerPrefs.SetInt("BunnyBabyWaterValue", saveObject.bunny_babyWaterValue);

            PlayerPrefs.SetFloat("BunnyBabySpeed", saveObject.bunny_babySpeed);
            PlayerPrefs.SetFloat("BunnyBabyViewDist", saveObject.bunny_babyViewDist);
            PlayerPrefs.SetFloat("BunnyBabyConsumTime", saveObject.bunny_babyConsTime);

            //Fox
            PlayerPrefs.SetFloat("FoxReducRate", saveObject.fox_reducRate);
            PlayerPrefs.SetFloat("FoxSpeedA", saveObject.fox_adultSpeed);
            PlayerPrefs.SetFloat("FoxViewDistA", saveObject.fox_adultViewDist);
            PlayerPrefs.SetFloat("FoxConsumTimeA", saveObject.fox_adultConsTime);

            PlayerPrefs.SetInt("FoxHungerLimA", saveObject.fox_adultHungerLim);
            PlayerPrefs.SetInt("FoxThirstLimA", saveObject.fox_adultThirstLim);
            PlayerPrefs.SetInt("FoxHungerRepro", saveObject.fox_hungerRepro);
            PlayerPrefs.SetInt("FoxThirstRepro", saveObject.fox_thirstRepro);
            PlayerPrefs.SetInt("FoxFoodValueA", saveObject.fox_adultFoodValue);
            PlayerPrefs.SetInt("FoxWaterValueA", saveObject.fox_adultWaterValue);
            PlayerPrefs.SetInt("FoxGestTime", saveObject.fox_gestTime);
            PlayerPrefs.SetInt("FoxGrowthTime", saveObject.fox_growthTime);
            PlayerPrefs.SetInt("FoxBabyLitterMin", saveObject.fox_litterMin);
            PlayerPrefs.SetInt("FoxBabyLitterMax", saveObject.fox_litterMax);

            PlayerPrefs.SetInt("FoxHungerLimB", saveObject.fox_babyHungerLim);
            PlayerPrefs.SetInt("FoxThirstLimB", saveObject.fox_babyThirstLim);
            PlayerPrefs.SetInt("FoxFoodValueB", saveObject.fox_babyFoodValue);
            PlayerPrefs.SetInt("FoxWaterValueB", saveObject.fox_babyWaterValue);

            PlayerPrefs.SetFloat("FoxSpeedB", saveObject.fox_babySpeed);
            PlayerPrefs.SetFloat("FoxViewDistB", saveObject.fox_babyViewDist);
            PlayerPrefs.SetFloat("FoxConsumTimeB", saveObject.fox_babyConsTime);

            //Start section
            PlayerPrefs.SetInt("StartBunnyNumber", saveObject.bunnyStartNumber);
            PlayerPrefs.SetInt("StartFoxNumber", saveObject.foxStartNumber);

            //Color variable
            canvas.GetComponent<AnimalColor_MainMenu>().bunnyMaleColor = saveObject.bunnyMaleColor;
            canvas.GetComponent<AnimalColor_MainMenu>().bunnyFemaleColor = saveObject.bunnyFemaleColor;
            canvas.GetComponent<AnimalColor_MainMenu>().foxMaleColor = saveObject.foxMaleColor;
            canvas.GetComponent<AnimalColor_MainMenu>().foxFemaleColor = saveObject.foxFemaleColor;

            //Color images (start menu ui)
            canvas.GetComponent<AnimalColor_MainMenu>().bunnyMale.GetComponent<RawImage>().color = saveObject.bunnyMaleColor;
            canvas.GetComponent<AnimalColor_MainMenu>().bunnyFemale.GetComponent<RawImage>().color = saveObject.bunnyFemaleColor;
            canvas.GetComponent<AnimalColor_MainMenu>().foxMale.GetComponent<RawImage>().color = saveObject.foxMaleColor;
            canvas.GetComponent<AnimalColor_MainMenu>().foxFemale.GetComponent<RawImage>().color = saveObject.foxFemaleColor;
            //Updating shaders
            canvas.GetComponent<AnimalColor_MainMenu>().UpdateMaterials();

            //Loading Datas from playerPrefs into input fields
            canvas.GetComponent<Terrain_MainMenu>().LoadPlayerPrefs();
            canvas.GetComponent<Bunny_MainMenu>().LoadPlayerPrefs();
            canvas.GetComponent<Fox_MainMenu>().LoadPlayerPrefs();
            canvas.GetComponent<Start_MainMenu>().LoadPlayerPrefs();
        }
        else
        {
            //No save
        }
    }

    public void setSaveSlot(int index) { _saveSlot = index; }
    public void setLoadSlot(int index) { _loadSlot = index; }
    public int GetLoadSlot() { return _loadSlot; }
    public int GetSaveSlot() { return _saveSlot; }

    public void ExecuteSaveOrLoad() //Used by buttons
    {
        if (_saveSlot != 0)
        {
            Save(_saveSlot);
        }
        else if (_loadSlot != 0)
        {
            Load(_loadSlot);
        }

        _saveSlot = 0;
        _loadSlot = 0;
    }

    private class SaveObject
    {
        //Terrain
        public int xSize, zSize;
        public float xOffset, zOffset;
        public float zoom;
        public int seed;

        //Bunny
        public float bunny_adultSpeed, bunny_adultViewDist, bunny_adultConsTime;
        public float bunny_babySpeed, bunny_babyViewDist, bunny_babyConsTime;
        public int bunny_growthTime, bunny_hungerRepro, bunny_thirstRepro, bunny_gestTime;
        public int bunny_litterMin, bunny_litterMax;
        public float bunny_reducRate;
        public int bunny_adultFoodValue, bunny_adultWaterValue, bunny_adultHungerLim, bunny_adultThirstLim;
        public int bunny_babyFoodValue, bunny_babyWaterValue, bunny_babyHungerLim, bunny_babyThirstLim;

        //Fox
        public float fox_adultSpeed, fox_adultViewDist, fox_adultConsTime;
        public float fox_babySpeed, fox_babyViewDist, fox_babyConsTime;
        public int fox_growthTime, fox_hungerRepro, fox_thirstRepro, fox_gestTime;
        public int fox_litterMin, fox_litterMax;
        public float fox_reducRate;
        public int fox_adultFoodValue, fox_adultWaterValue, fox_adultHungerLim, fox_adultThirstLim;
        public int fox_babyFoodValue, fox_babyWaterValue, fox_babyHungerLim, fox_babyThirstLim;

        //Start section
        public int bunnyStartNumber, foxStartNumber;
        public Color bunnyMaleColor, bunnyFemaleColor;
        public Color foxMaleColor, foxFemaleColor;
    }
}
