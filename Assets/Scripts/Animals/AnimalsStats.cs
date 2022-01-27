using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Animals stats data base 
 */

[System.Serializable]
public class AnimalsStats
{
    //Stats
    public string _animal;
    public bool _baby;

    public float _movementSpeed;
    public float _viewDistance;
    public float _timeToConsume;

    public float _hungerLimit, _waterLimit;

    public float _hungerToReproduct, _thirstToReproduct;
    public bool _readyToReproduct;

    public string _gender;

    public float _thirst, _hunger;

    //Bool managers
    public bool _searchingFood, _searchingWater, _randomDeplacement;
    public bool _waterNotFound, _foodNotFound;
    public bool _eating, _drinking, _interactingWithMate;
    public bool _mateFound, _mateTargeted, _matingOver;


    //Stats registered in BunnyCaracteristicsManager.cs
    public AnimalsStats(string animal, bool baby, string gender)
    {
        _animal = animal;
        _baby = baby;

        _thirst = 100;
        _hunger = 100;

        if (animal == "Bunny" && baby) //Baby bunny
            initBabyBunny(gender);
        else if (animal == "Bunny" && !baby) //Mature bunny
            initBunny(gender);
        else if (animal == "Fox" && baby) //Baby fox
            initBabyFox(gender);
        else
            initFox(gender);

        initBoolManager();


        //Complete for Fox with a FoxCaracteristicsManager.cs
    }

    //Manual stats entry
    public AnimalsStats(string animal, bool baby, float movementSpeed, float viewDistance, float timeToConsume, float hungerLimit, float waterLimit, string gender)
    {
        _animal = animal;
        _baby = baby;

        _movementSpeed = movementSpeed;
        _viewDistance = viewDistance;
        _timeToConsume = timeToConsume;
        _hungerLimit = hungerLimit;
        _waterLimit = waterLimit;
        _gender = gender;


        _thirst = 100;
        _hunger = 100;

        initBoolManager();
    }

    private void initBunny(string gender)
    {
        _movementSpeed = BunnyCaracteristicsManager.speed_B;
        _viewDistance = BunnyCaracteristicsManager.vision_B;
        _timeToConsume = BunnyCaracteristicsManager.timeToConsume_B;
        _hungerLimit = BunnyCaracteristicsManager.hungerLimit_B;
        _waterLimit = BunnyCaracteristicsManager.waterLimit_B;
        _hungerToReproduct = BunnyCaracteristicsManager.hungerToReproduct_B;
        _thirstToReproduct = BunnyCaracteristicsManager.thirstToReproduct_B;
        _readyToReproduct = false;
        _gender = gender;
    }

    private void initBabyBunny(string gender)
    {
        _movementSpeed = BunnyCaracteristicsManager.speed_BB;
        _viewDistance = BunnyCaracteristicsManager.vision_BB;
        _timeToConsume = BunnyCaracteristicsManager.timeToConsume_BB;
        _hungerLimit = BunnyCaracteristicsManager.hungerLimit_BB;
        _waterLimit = BunnyCaracteristicsManager.waterLimit_BB;
        _readyToReproduct = false;

        _gender = gender;
    }

    private void initFox(string gender)
    {

    }

    private void initBabyFox(string gender)
    {

    }

    private void initBoolManager()
    {
        _searchingFood = false;
        _searchingWater = false;
        _randomDeplacement = false;
        _waterNotFound = false;
        _foodNotFound = false;
        _eating = false;
        _drinking = false;
        _mateFound = false;
        _interactingWithMate = false;
        _matingOver = false;
    }
}
