using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class DisplayCarac : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI state;
    public TextMeshProUGUI thirst, hunger, speed, vision, timeToConsume, thirstToReproduct, hungerToReproduct, hungerToEat, thirstToDrink;

    [Header("2D Textures")]
    public Texture2D bunnyRaceTexture;
    public Texture2D foxRaceTexture, femaleTexture, maleTexture, babyTeatTexture, babyTeatCrossedTexture, pregnancyTexture, pregnancyCrossedTexture;

    [Header("Raw images")]
    public RawImage raceImage;
    public RawImage genderImage, babyImage, pregnantImage, foodImage, waterImage, speedImage, visionImage, timeToConsumeImage;

    [Header("Defined colors")]
    public Color red;
    public Color green, foxMaleColor, foxFemaleColor, bunnyMaleColor, bunnyFemaleColor;


    [SerializeField] private Material bunnyMaleMat,bunnyFemaleMat, foxMaleMat, foxFemaleMat;

    [SerializeField] private GameObject _animalTarget;

    private Animals _anim;

    // Strings for translation 
    private string _isPregnant, _isNotPregnant, _state, _hungerLim, _hungerMate, _thirstLim, _thirstMate, _bunny, _fox, _male, _female ,_baby, _adult;

    private void Awake()
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
        {
            _isPregnant = "Cet animal est enceinte";
            _isNotPregnant = "Cet animal n'est pas enceinte";
            _state = "Activité : \n";
            _hungerLim = "Faim limite : ";
            _hungerMate = "\nFaim pour se reproduire : ";
            _thirstLim = "Hydratation limite : ";
            _thirstMate = "\nHydratation pour se reproduire : ";
            _bunny = "Lapin";
            _fox = "Renard";
            _male = "Mâle";
            _female = "Femelle";
            _baby = "Bébé";
            _adult = "Adulte";

            genderImage.GetComponent<TooltipTrigger>().header = "Genre";
            pregnantImage.GetComponent<TooltipTrigger>().header = "Grossesse";
            foodImage.GetComponent<TooltipTrigger>().header = "Nourriture";
            waterImage.GetComponent<TooltipTrigger>().header = "Hydratation";
            speedImage.GetComponent<TooltipTrigger>().header = "Vitesse de déplacement";
            visionImage.GetComponent<TooltipTrigger>().header = "Champs de vision";
            timeToConsumeImage.GetComponent<TooltipTrigger>().header = "Temps de consommation";
            timeToConsumeImage.GetComponent<TooltipTrigger>().content = "(en seconde)";
        }
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
        {
            _isPregnant = "This animal is pregnant";
            _isNotPregnant = "This animal is not pregnant";
            _state = "State : \n";
            _hungerLim = "Hunger to eat : ";
            _hungerMate = "\nHunger to mate : ";
            _thirstLim = "Thirst to drink : ";
            _thirstMate = "\nThirst to mate : ";
            _bunny = "Bunny";
            _fox = "Fox";
            _male = "Male";
            _female = "Female";
            _baby = "Baby";
            _adult = "Adult";

            genderImage.GetComponent<TooltipTrigger>().header = "Gender";
            pregnantImage.GetComponent<TooltipTrigger>().header = "Pregnancy";
            foodImage.GetComponent<TooltipTrigger>().header = "Food";
            waterImage.GetComponent<TooltipTrigger>().header = "Thirst";
            speedImage.GetComponent<TooltipTrigger>().header = "Movement speed";
            visionImage.GetComponent<TooltipTrigger>().header = "Field of view";
            timeToConsumeImage.GetComponent<TooltipTrigger>().header = "Consumption time";
            timeToConsumeImage.GetComponent<TooltipTrigger>().content = "(in second)";
        }
    }

    public void Start()
    {
        // Getting color properties from materials base map
        bunnyMaleColor = bunnyMaleMat.color;
        bunnyFemaleColor = bunnyFemaleMat.color;

        // Getting color properties from shader graph
        foxMaleColor = foxMaleMat.GetColor("Color_AAC4E4DA");
        foxFemaleColor = foxFemaleMat.GetColor("Color_50B22DC6");
    }

    public void Update()
    {
        if(_animalTarget != null)
        {
            DisplayStateHungerAndThirst(_anim);
            DisplayAnimalPregnancy(_anim);
            DisplayAnimalMaturationState(_anim);
        }
    }

    public void DisplayAnimalCarac()
    {
        // Getting Animals componant as _anim
        _anim = _animalTarget.GetComponent<Animals>();

        // Display one time only 
        DisplayAnimaleGenderAndRace(_anim);
        DisplayAnimalSpeedViewDistTimeConsumption(_anim);
        DisplayHungerAndThirstThresholds(_anim);
    }

    private void DisplayAnimaleGenderAndRace(Animals anim)
    {
        // Race and gender
        if (anim.GetComponent<Bunny>() != null)
        {
            raceImage.texture = bunnyRaceTexture;
            raceImage.GetComponent<TooltipTrigger>().content = _bunny;

            // Gender
            if (anim.getGender() == "Female")
            {
                genderImage.texture = femaleTexture;
                genderImage.color = bunnyFemaleColor;
                genderImage.GetComponent<TooltipTrigger>().content = _female;
            }
            else
            {
                genderImage.texture = maleTexture;
                genderImage.color = bunnyMaleColor;
                genderImage.GetComponent<TooltipTrigger>().content = _male;
            }
        }
        else
        {
            raceImage.texture = foxRaceTexture;
            raceImage.GetComponent<TooltipTrigger>().content = _fox;

            // Gender
            if (anim.getGender() == "Female")
            {
                genderImage.texture = femaleTexture;
                genderImage.color = foxFemaleColor;
                genderImage.GetComponent<TooltipTrigger>().content = _female;
            }
            else
            {
                genderImage.texture = maleTexture;
                genderImage.color = foxMaleColor;
                genderImage.GetComponent<TooltipTrigger>().content = _male;
            }
        }
    }

    private void DisplayAnimalPregnancy(Animals anim)
    {
        // Pregnancy
        if (anim.GetPregnancy())
        {
            pregnantImage.texture = pregnancyTexture;
            pregnantImage.color = green;
            pregnantImage.GetComponent<TooltipTrigger>().content = _isPregnant;
        }
        else
        {
            pregnantImage.texture = pregnancyCrossedTexture;
            pregnantImage.color = red;
            pregnantImage.GetComponent<TooltipTrigger>().content = _isNotPregnant;
        }
    }

    private void DisplayAnimalMaturationState(Animals anim)
    {
        // Baby
        if (anim.getBaby())
        {
            babyImage.texture = babyTeatTexture;
            babyImage.color = green;
            babyImage.GetComponent<TooltipTrigger>().content = _baby;
        }
        else
        {
            babyImage.texture = babyTeatCrossedTexture;
            babyImage.color = red;
            babyImage.GetComponent<TooltipTrigger>().content = _adult;
        }
    }

    private void DisplayStateHungerAndThirst(Animals anim)
    {
        if (LocalizationSettings.SelectedLocale.ToString() == "French (fr)")
            state.text = _state + TranslateState(_anim.GetState());
        else if (LocalizationSettings.SelectedLocale.ToString() == "English (en)")
            state.text = _state + _anim.GetState();

        thirst.text = _anim.getThirst().ToString("f1");
        hunger.text = _anim.getHunger().ToString("f1");
    }

    private void DisplayHungerAndThirstThresholds(Animals anim)
    {
        // Hunger 
        string hungerText = _hungerLim + anim.GetHungerLimit().ToString() + _hungerMate + anim.GetHungerToReproduct().ToString();
        foodImage.GetComponent<TooltipTrigger>().content = hungerText;

        // Thirst 
        string thirstText = _thirstLim + anim.GetThirstLimit().ToString() + _thirstMate + anim.GetThirstToReproduct().ToString();
        waterImage.GetComponent<TooltipTrigger>().content = thirstText;
    }

    private void DisplayAnimalSpeedViewDistTimeConsumption(Animals anim)
    {
        // Speed
        speed.text = anim.GetSpeed().ToString();

        // View distance
        vision.text = anim.getViewDistance().ToString();

        // Time to consume
        timeToConsume.text = anim.GetTimeToConsume().ToString();
    }

    private string TranslateState(string state)
    {
        switch (state)
        {
            case "Looking for direction": return "Cherche une direction";
            case "Random Walking": return "Déplacement aléatoire";
            case "Moving to water": return "Se déplace vers de l'eau";
            case "Drinking": return "En train de boire";
            case "Moving to food": return "Se déplace vers de la nourriture";
            case "Eating": return "En train de manger";
            case "Fleeing": return "En train de fuir";
            case "Moving to mate": return "Se dirige vers son partenaire";
            case "Interacting with mate": return "En train de se reproduire";
        }

        return "";
    }


    public void SetAnimalTarget(GameObject animal) { _animalTarget = animal; }
}
