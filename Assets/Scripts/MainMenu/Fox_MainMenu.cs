using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fox_MainMenu : MonoBehaviour
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

    [Header("FoxPreview")] public GameObject foxSphereView; //Visualize fox view distance

    //Values will be stored in these variables 
    [HideInInspector] public float reducRate, speedA, speedB, viewDistA, viewDistB, consumTimeA, consumTimeB;
    [HideInInspector] public int hungerLimA, hungerLimB, thirstLimA, thirstLimB, hungerRepro, thirstRepro, foodValueA, foodValueB, waterValueA, waterValueB;
    [HideInInspector] public int gestTime, growthTime, babyLitterMin, babyLitterMax;

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
        if (adultStatToggle.isOn && !_adultCaracShown)//Enable adult input fields
        {
            _adultCaracShown = true;

            speedIn.interactable = true;
            viewDistIn.interactable = true;
            consumTimeIn.interactable = true;
        }
        else if (!adultStatToggle.isOn && _adultCaracShown)//Disable adult input fields
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

        if (reproToggle.isOn && !_reproShown)
        {
            _reproShown = true;

            hungerReproIn.interactable = true;
            thirstReproIn.interactable = true;
            gestTimeIn.interactable = true;
            babyLitterMinIn.interactable = true;
            babyLitterMaxIn.interactable = true;
        }
        else if (!reproToggle.isOn && _reproShown)
        {
            _reproShown = false;

            hungerReproIn.interactable = false;
            thirstReproIn.interactable = false;
            gestTimeIn.interactable = false;
            babyLitterMinIn.interactable = false;
            babyLitterMaxIn.interactable = false;

            SetDefaultRepro();
        }

        if (foodToggle.isOn && !_foodShown)
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
        else if (!foodToggle.isOn && _foodShown)
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
        speedIn.text = PlayerPrefs.GetFloat("FoxSpeedA", .9f).ToString();
        speedA = PlayerPrefs.GetFloat("FoxSpeedA", .9f);
        viewDistIn.text = PlayerPrefs.GetFloat("FoxViewDistA", 15).ToString();
        viewDistA = PlayerPrefs.GetFloat("FoxViewDistA", 15);
        consumTimeIn.text = PlayerPrefs.GetFloat("FoxConsumTimeA", 2.5f).ToString();
        consumTimeA = PlayerPrefs.GetFloat("FoxConsumTimeA", 2.5f);
    }

    private void SetDefaultBabyCarac()
    {
        babySpeedIn.text = PlayerPrefs.GetFloat("FoxSpeedB", .7f).ToString();
        speedB = PlayerPrefs.GetFloat("FoxSpeedB", .7f);
        babyViewDistIn.text = PlayerPrefs.GetFloat("FoxViewDistB", 10).ToString();
        viewDistB = PlayerPrefs.GetFloat("FoxViewDistB",10);
        babyConsTimeIn.text = PlayerPrefs.GetFloat("FoxConsumTimeB", 3.5f).ToString();
        consumTimeB = PlayerPrefs.GetFloat("FoxConsumTimeB", 3.5f);
        growthTimeIn.text = PlayerPrefs.GetInt("FoxGrowthTime", 45).ToString();
        growthTime = PlayerPrefs.GetInt("FoxGrowthTime", 10);
    }

    private void SetDefaultRepro()
    {
        hungerReproIn.text = PlayerPrefs.GetInt("FoxHungerRepro", 70).ToString();
        hungerRepro = PlayerPrefs.GetInt("FoxHungerRepro", 70);
        thirstReproIn.text = PlayerPrefs.GetInt("FoxThirstRepro", 70).ToString();
        thirstRepro = PlayerPrefs.GetInt("FoxThirstRepro", 70);
        gestTimeIn.text = PlayerPrefs.GetInt("FoxGestTime", 60).ToString();
        gestTime = PlayerPrefs.GetInt("FoxGestTime", 60);
        babyLitterMinIn.text = PlayerPrefs.GetInt("FoxBabyLitterMin", 1).ToString();
        babyLitterMin = PlayerPrefs.GetInt("FoxBabyLitterMin", 1);
        babyLitterMaxIn.text = PlayerPrefs.GetInt("FoxBabyLitterMax", 2).ToString();
        babyLitterMax = PlayerPrefs.GetInt("FoxBabyLitterMax", 2);
    }
    private void SetDefaultFood()
    {
        reducRateIn.text = PlayerPrefs.GetFloat("FoxReducRate", 1.25f).ToString();
        reducRate = PlayerPrefs.GetFloat("FoxReducRate", 1.25f);
        foodValueIn.text = PlayerPrefs.GetInt("FoxFoodValueA", 30).ToString();
        foodValueA = PlayerPrefs.GetInt("FoxFoodValueA", 30);
        babyFoodValueIn.text = PlayerPrefs.GetInt("FoxFoodValueB", 40).ToString();
        foodValueB = PlayerPrefs.GetInt("FoxFoodValueB", 40);
        waterValueIn.text = PlayerPrefs.GetInt("FoxWaterValueA", 30).ToString();
        waterValueA = PlayerPrefs.GetInt("FoxWaterValueA", 30);
        babyWaterValueIn.text = PlayerPrefs.GetInt("FoxWaterValueB", 40).ToString();
        waterValueB = PlayerPrefs.GetInt("FoxWaterValueB", 40);
        hungerLimIn.text = PlayerPrefs.GetInt("FoxHungerLimA", 50).ToString();
        hungerLimA = PlayerPrefs.GetInt("FoxHungerLimA", 50);
        babyHungerLimIn.text = PlayerPrefs.GetInt("FoxHungerLimB", 60).ToString();
        hungerLimB = PlayerPrefs.GetInt("FoxHungerLimB", 60);
        thirstLimIn.text = PlayerPrefs.GetInt("FoxThirstLimA", 50).ToString();
        thirstLimA = PlayerPrefs.GetInt("FoxThirstLimA", 50);
        babyThirstLimIn.text = PlayerPrefs.GetInt("FoxThirstLimB", 50).ToString();
        thirstLimB = PlayerPrefs.GetInt("FoxThirstLimB", 50);
    }

    private void UpdateFoxPreviewViewDist() //Change the preview (sphere gameObject) of the fox view distance
    {
        //NOTE : Scale : 0.09 => to find coef : 1 (scale wanted) divided by 0.09 (current scale)

        float size = viewDistA * (1 / .09f);
        foxSphereView.transform.localScale = new Vector3(size, size, size);
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
        PlayerPrefs.SetFloat("FoxReducRate", reducRate);
        PlayerPrefs.SetFloat("FoxSpeedA", speedA);
        PlayerPrefs.SetFloat("FoxSpeedB", speedB);
        PlayerPrefs.SetFloat("FoxViewDistA", viewDistA);
        PlayerPrefs.SetFloat("FoxViewDistB", viewDistB);
        PlayerPrefs.SetFloat("FoxConsumTimeA", consumTimeA);
        PlayerPrefs.SetFloat("FoxConsumTimeB", consumTimeB);

        PlayerPrefs.SetInt("FoxHungerLimA", hungerLimA);
        PlayerPrefs.SetInt("FoxHungerLimB", hungerLimB);
        PlayerPrefs.SetInt("FoxThirstLimA", thirstLimA);
        PlayerPrefs.SetInt("FoxThirstLimB", thirstLimB);
        PlayerPrefs.SetInt("FoxHungerRepro", hungerRepro);
        PlayerPrefs.SetInt("FoxThirstRepro", thirstRepro);
        PlayerPrefs.SetInt("FoxFoodValueA", foodValueA);
        PlayerPrefs.SetInt("FoxFoodValueB", foodValueB);
        PlayerPrefs.SetInt("FoxWaterValueA", waterValueA);
        PlayerPrefs.SetInt("FoxWaterValueB", waterValueB);
        PlayerPrefs.SetInt("FoxGestTime", gestTime);
        PlayerPrefs.SetInt("FoxGrowthTime", growthTime);
        PlayerPrefs.SetInt("FoxBabyLitterMin", babyLitterMin);
        PlayerPrefs.SetInt("FoxBabyLitterMax", babyLitterMax);
    }

    public void LoadPlayerPrefs()
    {
        reducRate = PlayerPrefs.GetFloat("FoxReducRate");
        reducRateIn.text = reducRate.ToString();

        speedA = PlayerPrefs.GetFloat("FoxSpeedA");
        speedIn.text = speedA.ToString();

        speedB = PlayerPrefs.GetFloat("FoxSpeedB");
        babySpeedIn.text = speedB.ToString();

        viewDistA = PlayerPrefs.GetFloat("FoxViewDistA");
        viewDistIn.text = viewDistA.ToString();

        viewDistB = PlayerPrefs.GetFloat("FoxViewDistB");
        babyViewDistIn.text = viewDistB.ToString();

        consumTimeA = PlayerPrefs.GetFloat("FoxConsumTimeA");
        consumTimeIn.text = consumTimeA.ToString();

        consumTimeB = PlayerPrefs.GetFloat("FoxConsumTimeB");
        babyConsTimeIn.text = consumTimeB.ToString();

        hungerLimA = PlayerPrefs.GetInt("FoxHungerLimA");
        hungerLimIn.text = hungerLimA.ToString();

        hungerLimB = PlayerPrefs.GetInt("FoxHungerLimB");
        babyHungerLimIn.text = hungerLimB.ToString();

        thirstLimA = PlayerPrefs.GetInt("FoxThirstLimA");
        thirstLimIn.text = thirstLimA.ToString();

        thirstLimB = PlayerPrefs.GetInt("FoxThirstLimB");
        babyThirstLimIn.text = thirstLimB.ToString();

        hungerRepro = PlayerPrefs.GetInt("FoxHungerRepro");
        hungerReproIn.text = hungerRepro.ToString();

        thirstRepro = PlayerPrefs.GetInt("FoxThirstRepro");
        thirstReproIn.text = thirstRepro.ToString();

        foodValueA = PlayerPrefs.GetInt("FoxFoodValueA");
        foodValueIn.text = foodValueA.ToString();

        foodValueB = PlayerPrefs.GetInt("FoxFoodValueB");
        babyFoodValueIn.text = foodValueB.ToString();

        waterValueA = PlayerPrefs.GetInt("FoxWaterValueA");
        waterValueIn.text = waterValueA.ToString();

        waterValueB = PlayerPrefs.GetInt("FoxWaterValueB");
        babyWaterValueIn.text = waterValueB.ToString();

        gestTime = PlayerPrefs.GetInt("FoxGestTime");
        gestTimeIn.text = gestTime.ToString();

        growthTime = PlayerPrefs.GetInt("FoxGrowthTime");
        growthTimeIn.text = growthTime.ToString();

        babyLitterMin = PlayerPrefs.GetInt("FoxBabyLitterMin");
        babyLitterMinIn.text = babyLitterMin.ToString();

        babyLitterMax = PlayerPrefs.GetInt("FoxBabyLitterMax");
        babyLitterMaxIn.text = babyLitterMax.ToString();
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

    public void SetHungerLimA(string newHungerLimA)
    {
        int.TryParse(newHungerLimA, out int input);
        input = CheckInput(input);
        hungerLimA = input;
        hungerLimIn.text = input.ToString();
    }

    public void SetHungerLimB(string newHungerLimB)
    {
        int.TryParse(newHungerLimB, out int input);
        input = CheckInput(input);
        hungerLimB = input;
        babyHungerLimIn.text = input.ToString();
    }

    public void SetThirstLimA(string newThirstLimA)
    {
        int.TryParse(newThirstLimA, out int input);
        input = CheckInput(input);
        thirstLimA = input;
        thirstLimIn.text = input.ToString();
    }

    public void SetThirstLimB(string newThirstLimB)
    {
        int.TryParse(newThirstLimB, out int input);
        input = CheckInput(input);
        thirstLimB = input;
        babyThirstLimIn.text = input.ToString();
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

    public void SetFoodValueA(string newFoodValueA)
    {
        int.TryParse(newFoodValueA, out int input);
        input = CheckInput(input);
        foodValueA = input;
        foodValueIn.text = input.ToString();
    }

    public void SetFoodValueB(string newFoodValueB)
    {
        int.TryParse(newFoodValueB, out int input);
        input = CheckInput(input);
        foodValueB = input;
        babyFoodValueIn.text = input.ToString();
    }

    public void SetWaterValueA(string newWaterValueA)
    {
        int.TryParse(newWaterValueA, out int input);
        input = CheckInput(input);
        waterValueA = input;
        waterValueIn.text = input.ToString();
    }

    public void SetWaterValueB(string newWaterValueB)
    {
        int.TryParse(newWaterValueB, out int input);
        input = CheckInput(input);
        waterValueB = input;
        babyWaterValueIn.text = input.ToString();
    }
    #endregion

    //Carac section

    #region Carac

    public void SetSpeedA(string newSpeedA)
    {
        float.TryParse(newSpeedA, out float input);
        input = CheckInput(input);
        speedA = input;
        speedIn.text = input.ToString();
    }

    public void SetSpeedB(string newSpeedB)
    {
        float.TryParse(newSpeedB, out float input);
        input = CheckInput(input);
        speedB = input;
        babySpeedIn.text = input.ToString();
    }

    public void SetViewDistA(string newViewDistA)
    {
        float.TryParse(newViewDistA, out float input);
        input = CheckInput(input);
        viewDistA = input;
        viewDistIn.text = input.ToString();
        UpdateFoxPreviewViewDist();
    }

    public void SetViewDistB(string newViewDistB)
    {
        float.TryParse(newViewDistB, out float input);
        input = CheckInput(input);
        viewDistB = input;
        babyViewDistIn.text = input.ToString();
    }

    public void SetConsumTimeA(string newConsumTimeA)
    {
        float.TryParse(newConsumTimeA, out float input);
        input = CheckInput(input);
        consumTimeA = input;
        consumTimeIn.text = input.ToString();
    }

    public void SetConsumTimeB(string newConsumTimeB)
    {
        float.TryParse(newConsumTimeB, out float input);
        input = CheckInput(input);
        consumTimeB = input;
        babyConsTimeIn.text = input.ToString();
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


}
