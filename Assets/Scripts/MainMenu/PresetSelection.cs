using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class PresetSelection : MonoBehaviour
{
    public GameObject canvas;

    [SerializeField] private TextMeshProUGUI _title, _description;
    [SerializeField] private GameObject _titleBackground, _descriptionBackground, _startSimulationButton;

    // Used to load the specified preset save 
    private int _presetNumber;

    private string[] _descriptionsTabFr  = { 
        "Simulation stable\nEnviron 60 lapins et 4 renards au commencement.",
        "Extinction des lapins en 10 min\nEnviron 40 lapins et 3 renards au commencement.",
        "Simulation stable avec lapins uniquement\nEnviron 40 lapins au commencement.",
        "Extinction des lapins par famine\nEnviron 40 lapins et 3 renards au commencement.",
        "Evolution sinusoïdale du nombre de lapin\nEnviron 35 lapins au commencement." 
    };

    private string[] _descriptionsTabEn = {
        "Simulation with stable population\nAround 60 bunnies and 4 foxs at the start.",
        "Bunny extinction in 10 minutes\nAround 40 bunnies and 3 foxs at the start.",
        "Simulation with bunnies only\nAround 40 bunnies at the start.",
        "Bunny extinction from starvation\nAround 40 bunnies at the start.",
        "Sinusoid evolution of the bunnies\nAround 35 bunnies at the start."
    };


    public void Start()
    {
        _title.enabled = false;
        _description.enabled = false;
        _titleBackground.SetActive(false);
        _descriptionBackground.SetActive(false);
        _startSimulationButton.SetActive(false);
    }

    // "presetNumber" is from one to five
    public void SelectPreset(int presetNumber)
    {
        // Setting objects visible
        _title.enabled = true;
        _description.enabled = true;
        _titleBackground.SetActive(true);
        _descriptionBackground.SetActive(true);
        _startSimulationButton.SetActive(true);

        // Display preset description
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            _title.text = "Préreglages " + presetNumber.ToString();
            _description.text = _descriptionsTabFr[presetNumber - 1];
        }
        else
        {
            _title.text = "Preset " + presetNumber.ToString();
            _description.text = _descriptionsTabEn[presetNumber - 1];
        }
        
        // Setting preset save number
        _presetNumber = presetNumber;
    }

    public void StartSimulation()
    {
        string saveString = SaveSystem_MainMenu.LoadPreset(_presetNumber);

        //Reassigning datas from json file to gameobjects
        if (saveString != null)
        {
            LoadDatas(saveString);

            SceneManager.LoadScene("Simulation");
        }
    }

    private void LoadDatas(string saveString)
    {
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

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

        //Loading Datas from playerPrefs into input fields
        canvas.GetComponent<Terrain_MainMenu>().LoadPlayerPrefs();
        canvas.GetComponent<Bunny_MainMenu>().LoadPlayerPrefs();
        canvas.GetComponent<Fox_MainMenu>().LoadPlayerPrefs();
        canvas.GetComponent<Start_MainMenu>().LoadPlayerPrefs();
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
