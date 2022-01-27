using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Bunny_MainMenu : MonoBehaviour
{
    public Toggle adultStatToggle, babyStatToggle, reproToggle, foodToggle, mutationToggle;

    [Header("AdultCarac")]
    public TMP_InputField speedIn;
    public TMP_InputField viewDistIn, consumTimeIn;

    [Header("BabyCarac")]
    public TMP_InputField babySpeedIn;
    public TMP_InputField growthTimeIn, babyViewDistIn, babyConsTimeIn;

    [Header("Repro")]
    public TMP_InputField hungerReproIn;
    public TMP_InputField thirstReproIn, gestTimeIn, babyLitterMinIn, babyLitterMaxIn;

    [Header("Food&Thirst")] 
    public TMP_InputField reducRateIn;
    public TMP_InputField hungerLimIn, thirstLimIn, babyHungerLimIn, babyThirstLimIn, foodValueIn, waterValueIn, babyFoodValueIn, babyWaterValueIn;

    //Header mutation ...

    [Header("BunnyPreview")] public GameObject bunnySphereView; //Visualize bunny view distance

    //Values will be stored in these variables 
    [HideInInspector] float reducRate, speed, viewDist, consumTime;
    [HideInInspector] int hungerLim, thirstLim, hungerRepro, thirstRepro, foodValue, waterValue;
    [HideInInspector] int gestTime, growthTime, babyLitterMin, babyLitterMax;
    [HideInInspector] int babyHungerLim, babyThirstLim, babyFoodValue, babyWaterValue;
    [HideInInspector] float babySpeed, babyViewDist, babyConsumTime;

    private bool _adultCaracShown, _babyCaracShown, _reproShown, _foodShown, _mutationShown;

    public void Start()
    {
        adultStatToggle.isOn = false;
        babyStatToggle.isOn = false;
        reproToggle.isOn = false;
        foodToggle.isOn = false;

        _adultCaracShown = true;
        _babyCaracShown = true;
        _reproShown = true;
        _foodShown = true;
    }

    public void Update()
    {
        if(adultStatToggle.isOn && !_adultCaracShown)//Enable adult input fields
        {
            _adultCaracShown = true;

            speedIn.interactable = true;
            viewDistIn.interactable = true;
            consumTimeIn.interactable = true;
        }
        else if(!adultStatToggle.isOn && _adultCaracShown)//Disable adult input fields
        {
            _adultCaracShown = false;

            speedIn.interactable = false;
            viewDistIn.interactable = false;
            consumTimeIn.interactable = false;

            SetDefaultAdultCarac();
        }

        if (babyStatToggle.isOn && !_babyCaracShown)//Enable baby input fields
        {
            _babyCaracShown = true;

            babySpeedIn.interactable = true;
            babyViewDistIn.interactable = true;
            growthTimeIn.interactable = true;
            babyConsTimeIn.interactable = true;
        }
        else if (!babyStatToggle.isOn && _babyCaracShown)//Disable baby input fields
        {
            _babyCaracShown = false;

            babySpeedIn.interactable = false;
            babyViewDistIn.interactable = false;
            growthTimeIn.interactable = false;
            babyConsTimeIn.interactable = false;

            SetDefaultBabyCarac();
        }

        if(reproToggle.isOn && !_reproShown)
        {
            _reproShown = true;

            hungerReproIn.interactable = true;
            thirstReproIn.interactable = true;
            gestTimeIn.interactable = true;
            babyLitterMinIn.interactable = true;
            babyLitterMaxIn.interactable = true;
        }
        else if(!reproToggle.isOn && _reproShown)
        {
            _reproShown = false;

            hungerReproIn.interactable = false;
            thirstReproIn.interactable = false;
            gestTimeIn.interactable = false;
            babyLitterMinIn.interactable = false;
            babyLitterMaxIn.interactable = false;

            SetDefaultRepro();
        }

        if(foodToggle.isOn && !_foodShown)
        {
            _foodShown = true;

            reducRateIn.interactable = true;
            foodValueIn.interactable = true;
            babyFoodValueIn.interactable = true;
            waterValueIn.interactable = true;
            babyWaterValueIn.interactable = true;
            hungerLimIn.interactable = true;
            babyHungerLimIn.interactable = true;
            thirstLimIn.interactable = true;
            babyThirstLimIn.interactable = true;
        }
        else if(!foodToggle.isOn && _foodShown)
        {
            _foodShown = false;

            reducRateIn.interactable = false;
            foodValueIn.interactable = false;
            babyFoodValueIn.interactable = false;
            waterValueIn.interactable = false;
            babyWaterValueIn.interactable = false;
            hungerLimIn.interactable = false;
            babyHungerLimIn.interactable = false;
            thirstLimIn.interactable = false;
            babyThirstLimIn.interactable = false;

            SetDefaultFood();
        }
    }

    private void SetDefaultAdultCarac()
    {
        speedIn.text = PlayerPrefs.GetFloat("BunnySpeed",1).ToString();
        speed = PlayerPrefs.GetFloat("BunnySpeed",1.4f);
        viewDistIn.text = PlayerPrefs.GetFloat("BunnyViewDist",15).ToString();
        viewDist = PlayerPrefs.GetFloat("BunnyViewDist",15);
        consumTimeIn.text = PlayerPrefs.GetFloat("BunnyConsumTime",15).ToString();
        consumTime = PlayerPrefs.GetFloat("BunnyConsumTime",1.5f);
    }

    private void SetDefaultBabyCarac()
    {
        babySpeedIn.text = PlayerPrefs.GetFloat("BunnyBabySpeed",0.9f).ToString();
        babySpeed = PlayerPrefs.GetFloat("BunnyBabySpeed",0.9f);
        babyViewDistIn.text = PlayerPrefs.GetFloat("BunnyBabyViewDist",10).ToString();
        babyViewDist = PlayerPrefs.GetFloat("BunnyBabyViewDist", 10);
        babyConsTimeIn.text = PlayerPrefs.GetFloat("BunnyBabyConsumTime",3).ToString();
        babyConsumTime = PlayerPrefs.GetFloat("BunnyBabyConsumTime",3);
        growthTimeIn.text = PlayerPrefs.GetInt("BunnyGrowthTime", 30).ToString();
        growthTime = PlayerPrefs.GetInt("BunnyGrowthTime",30);
    }

    private void SetDefaultRepro()
    {
        hungerReproIn.text = PlayerPrefs.GetInt("BunnyHungerRepro",70).ToString();
        hungerRepro = PlayerPrefs.GetInt("BunnyHungerRepro",70);
        thirstReproIn.text = PlayerPrefs.GetInt("BunnyThirstRepro",70).ToString();
        thirstRepro = PlayerPrefs.GetInt("BunnyThirstRepro",70);
        gestTimeIn.text = PlayerPrefs.GetInt("BunnyGestTime",60).ToString();
        gestTime = PlayerPrefs.GetInt("BunnyGestTime",60);
        babyLitterMinIn.text = PlayerPrefs.GetInt("BunnyBabyLitterMin",2).ToString();
        babyLitterMin = PlayerPrefs.GetInt("BunnyBabyLitterMin",2);
        babyLitterMaxIn.text = PlayerPrefs.GetInt("BunnyBabyLitterMax",4).ToString();
        babyLitterMax = PlayerPrefs.GetInt("BunnyBabyLitterMax",4);
    }

    private void SetDefaultFood()
    {
        reducRateIn.text = PlayerPrefs.GetFloat("BunnyReducRate",1.5f).ToString();
        reducRate = PlayerPrefs.GetFloat("BunnyReducRate", 1.5f);
        foodValueIn.text = PlayerPrefs.GetInt("BunnyFoodValue",25).ToString();
        foodValue = PlayerPrefs.GetInt("BunnyFoodValue",25);
        babyFoodValueIn.text = PlayerPrefs.GetInt("BunnyBabyFoodValue",35).ToString();
        babyFoodValue = PlayerPrefs.GetInt("BunnyBabyFoodValue",35);
        waterValueIn.text = PlayerPrefs.GetInt("BunnyWaterValue",25).ToString();
        waterValue = PlayerPrefs.GetInt("BunnyWaterValue",25);
        babyWaterValueIn.text = PlayerPrefs.GetInt("BabyBunnyWaterValue",35).ToString();
        babyWaterValue = PlayerPrefs.GetInt("BabyBunnyWaterValue",35);
        hungerLimIn.text = PlayerPrefs.GetInt("BunnyHungerLim", 60).ToString() ;
        hungerLim = PlayerPrefs.GetInt("BunnyHungerLim",60);
        babyHungerLimIn.text = PlayerPrefs.GetInt("BunnyBabyHungerLim",50).ToString();
        babyHungerLim = PlayerPrefs.GetInt("BunnyBabyHungerLim",50);
        thirstLimIn.text = PlayerPrefs.GetInt("BunnyThirstLim",60).ToString();
        thirstLim = PlayerPrefs.GetInt("BunnyThirstLim",60);
        babyThirstLimIn.text = PlayerPrefs.GetInt("BunnyBabyThirstLim",50).ToString();
        babyThirstLim = PlayerPrefs.GetInt("BunnyBabyThirstLim",50);
    }

    private void UpdateBunnyPreviewViewDist() //Change the preview (sphere gameObject) of the bunny view distance
    {
        //NOTE : Sphere radius is x2 x10 number because of the bunny scale
        //Ex : for 10 a sphere radius would be 200

        float size = viewDist * 2 * 10;
        bunnySphereView.transform.localScale = new Vector3(size, size, size);
    }

    private float CheckInput(float input)
    {
        if (input < 0)
            return -input;
        else
            return input;
    }

    private int CheckInput(int input)
    {
        if (input < 0)
            return -input;
        else
            return input;
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat("BunnyReducRate", reducRate);
        PlayerPrefs.SetFloat("BunnySpeed", speed);
        PlayerPrefs.SetFloat("BunnyViewDist", viewDist);
        PlayerPrefs.SetFloat("BunnyConsumTime", consumTime);

        PlayerPrefs.SetInt("BunnyHungerLim", hungerLim);
        PlayerPrefs.SetInt("BunnyThirstLim", thirstLim);
        PlayerPrefs.SetInt("BunnyHungerRepro", hungerRepro);
        PlayerPrefs.SetInt("BunnyThirstRepro", thirstRepro);
        PlayerPrefs.SetInt("BunnyFoodValue", foodValue);
        PlayerPrefs.SetInt("BunnyWaterValue", waterValue);
        PlayerPrefs.SetInt("BunnyGestTime", gestTime);
        PlayerPrefs.SetInt("BunnyGrowthTime", growthTime);
        PlayerPrefs.SetInt("BunnyBabyLitterMin", babyLitterMin);
        PlayerPrefs.SetInt("BunnyBabyLitterMax", babyLitterMax);

        PlayerPrefs.SetInt("BunnyBabyHungerLim", babyHungerLim);
        PlayerPrefs.SetInt("BunnyBabyThirstLim", babyThirstLim);
        PlayerPrefs.SetInt("BunnyBabyFoodValue", babyFoodValue);
        PlayerPrefs.SetInt("BunnyBabyWaterValue", babyWaterValue);

        PlayerPrefs.SetFloat("BunnyBabySpeed", babySpeed);
        PlayerPrefs.SetFloat("BunnyBabyViewDist", babyViewDist);
        PlayerPrefs.SetFloat("BunnyBabyConsumTime", babyConsumTime);
    }

    public void LoadPlayerPrefs()
    {
        reducRate = PlayerPrefs.GetFloat("BunnyReducRate");
        reducRateIn.text = reducRate.ToString();

        speed = PlayerPrefs.GetFloat("BunnySpeed");
        speedIn.text = speed.ToString();

        viewDist = PlayerPrefs.GetFloat("BunnyViewDist");
        viewDistIn.text = viewDist.ToString();

        consumTime = PlayerPrefs.GetFloat("BunnyConsumTime");
        consumTimeIn.text = consumTime.ToString();

        hungerLim = PlayerPrefs.GetInt("BunnyHungerLim");
        hungerLimIn.text = hungerLim.ToString();

        thirstLim = PlayerPrefs.GetInt("BunnyThirstLim");
        thirstLimIn.text = thirstLim.ToString();

        hungerRepro = PlayerPrefs.GetInt("BunnyHungerRepro");
        hungerReproIn.text = hungerRepro.ToString();

        thirstRepro = PlayerPrefs.GetInt("BunnyThirstRepro");
        thirstReproIn.text = thirstRepro.ToString();

        foodValue = PlayerPrefs.GetInt("BunnyFoodValue");
        foodValueIn.text = foodValue.ToString();

        waterValue = PlayerPrefs.GetInt("BunnyWaterValue");
        waterValueIn.text = waterValue.ToString();

        gestTime = PlayerPrefs.GetInt("BunnyGestTime");
        gestTimeIn.text = gestTime.ToString();

        growthTime = PlayerPrefs.GetInt("BunnyGrowthTime");
        growthTimeIn.text = growthTime.ToString();

        babyLitterMin = PlayerPrefs.GetInt("BunnyBabyLitterMin");
        babyLitterMinIn.text = babyLitterMin.ToString();

        babyLitterMax = PlayerPrefs.GetInt("BunnyBabyLitterMax");
        babyLitterMaxIn.text = babyLitterMax.ToString();

        babyHungerLim = PlayerPrefs.GetInt("BunnyBabyHungerLim");
        babyHungerLimIn.text = babyHungerLim.ToString();

        babyThirstLim = PlayerPrefs.GetInt("BunnyBabyThirstLim");
        babyThirstLimIn.text = babyThirstLim.ToString();

        babyFoodValue = PlayerPrefs.GetInt("BunnyBabyFoodValue");
        babyFoodValueIn.text = babyFoodValue.ToString();

        babyWaterValue = PlayerPrefs.GetInt("BunnyBabyWaterValue");
        babyWaterValueIn.text = babyWaterValue.ToString();

        babySpeed = PlayerPrefs.GetFloat("BunnyBabySpeed");
        babySpeedIn.text = babySpeed.ToString();

        babyViewDist = PlayerPrefs.GetFloat("BunnyBabyViewDist");
        babyViewDistIn.text = babyViewDist.ToString();

        babyConsumTime = PlayerPrefs.GetFloat("BunnyBabyConsumTime");
        babyConsTimeIn.text = babyConsumTime.ToString();
    }

    //Methods to get values from input fields

    //Food & Thirst section
    #region Food&Thirst
    public void SetReducRate(string newReducRate)
    {
        float.TryParse(newReducRate, out float input);
        input = CheckInput(input);
        reducRate = input;
        reducRateIn.text = input.ToString();
    }

    public void SetHungerLim(string newHungerLim)
    {
        int.TryParse(newHungerLim, out int input);
        input = CheckInput(input);
        hungerLim = input;
        hungerLimIn.text = input.ToString();
    }

    public void SetThirstLim(string newThirstLim)
    {
        int.TryParse(newThirstLim, out int input);
        input = CheckInput(input);
        thirstLim = input;
        thirstLimIn.text = input.ToString();
    }

    public void SetHungerRepro(string newHungerRepro)
    {
        int.TryParse(newHungerRepro, out int input);
        input = CheckInput(input);
        hungerRepro = input;
        hungerReproIn.text = input.ToString();
    }

    public void SetThirstRepro(string newThirstRepro)
    {
        int.TryParse(newThirstRepro, out int input);
        input = CheckInput(input);
        thirstRepro = input;
        thirstReproIn.text = input.ToString();
    }

    public void SetFoodValue(string newFoodValue)
    {
        int.TryParse(newFoodValue, out int input);
        input = CheckInput(input);
        foodValue = input;
        foodValueIn.text = input.ToString();
    }


    public void SetWaterValue(string newWaterValue)
    {
        int.TryParse(newWaterValue, out int input);
        input = CheckInput(input);
        waterValue = input;
        waterValueIn.text = input.ToString();
    }

    #endregion

    //Carac section

    #region Carac

    public void SetSpeed(string newSpeed) 
    {
        float.TryParse(newSpeed, out float input);
        input = CheckInput(input);
        speed = input;
        speedIn.text = input.ToString();
    }


    public void SetViewDist(string newViewDist)
    {
        float.TryParse(newViewDist, out float input);
        input = CheckInput(input);
        viewDist = input;
        UpdateBunnyPreviewViewDist();
        viewDistIn.text = input.ToString();
    }

    public void SetConsumTime(string newConsumTime)
    {
        float.TryParse(newConsumTime, out float input);
        input = CheckInput(input);
        consumTime = input;
        consumTimeIn.text = input.ToString();
    }

    #endregion

    //Repro & child section
    #region Repro&Child

    public void SetGestTime(string newGestTime)
    {
        int.TryParse(newGestTime, out int input);
        input = CheckInput(input);
        gestTime = input;
        gestTimeIn.text = input.ToString();
    }

    public void SetGrowthTime(string newGrowthTime)
    {
        int.TryParse(newGrowthTime, out int input);
        input = CheckInput(input);
        growthTime = input;
        growthTimeIn.text = input.ToString();
    }

    public void SetBabyLitterMin(string newBabyLitterMin)
    {
        int.TryParse(newBabyLitterMin, out int input);
        input = CheckInput(input);
        babyLitterMin = input;
        babyLitterMinIn.text = input.ToString();
    }

    public void SetBabyLitterMax(string newBabyLitterMax)
    {
        int.TryParse(newBabyLitterMax, out int input);
        input = CheckInput(input);
        babyLitterMax = input;
        babyLitterMaxIn.text = input.ToString();
    }

    #endregion

    //baby section
    #region Baby

    public void SetBabyHungerLim(string newHungerLim)
    {
        int.TryParse(newHungerLim, out int input);
        input = CheckInput(input);
        babyHungerLim = input;
        babyHungerLimIn.text = input.ToString();
    }

    public void SetBabyThirstLim(string newThirstLim)
    {
        int.TryParse(newThirstLim, out int input);
        input = CheckInput(input);
        babyThirstLim = input;
        babyThirstLimIn.text = input.ToString();
    }

    public void SetBabyFoodValue(string newFoodValue)
    {
        int.TryParse(newFoodValue, out int input);
        input = CheckInput(input);
        babyFoodValue = input;
        babyFoodValueIn.text = input.ToString();
    }

    public void SetBabyWaterValue(string newWaterValue)
    {
        int.TryParse(newWaterValue, out int input);
        input = CheckInput(input);
        babyWaterValue = input;
        babyWaterValueIn.text = input.ToString();
    }

    public void SetBabySpeed(string newSpeed)
    {
        float.TryParse(newSpeed, out float input);
        input = CheckInput(input);
        babySpeed = input;
        babySpeedIn.text = input.ToString();
    }

    public void SetBabyViewDist(string newViewDist)
    {
        float.TryParse(newViewDist, out float input);
        input = CheckInput(input);
        babyViewDist = input;
        babyViewDistIn.text = input.ToString();
    }

    public void SetBabyConsTime(string newConsTime)
    {
        float.TryParse(newConsTime, out float input);
        input = CheckInput(input);
        babyConsumTime = input;
        babyConsTimeIn.text = input.ToString();
    }

    #endregion
}
