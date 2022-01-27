using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyCaracteristicsManager : MonoBehaviour
{
    // ---------------------- UNITY EDITOR VARIABLES ----------------------

    [Header("Bunny Stats :")]
    public float speed;
    public float vision, timeToConsume, searchingMateDelay, waterLimit, hungerLimit, hungerToReproduct, thirstToReproduct, gestationnalTime;


    [Header("Baby Bunny Stats :")]
    public float speed_baby;
    public float vision_baby, timeToConsume_baby, waterLimit_baby, hungerLimit_baby, timeToGrow;

    // ---------------------- STATIC VARIABLES ----------------------

    //Bunny
    public static float speed_B, vision_B, timeToConsume_B, searchingMateDelay_B, waterLimit_B, hungerLimit_B, hungerToReproduct_B, thirstToReproduct_B, gestationnalTime_B;
    //Baby Bunny
    public static float speed_BB, vision_BB, timeToConsume_BB, waterLimit_BB, hungerLimit_BB, timeToGrow_BB;

    // ---------------------- SCRIPTS VARIABLES ----------------------

    [Header("Commands :")]
    [SerializeField] private bool _updateVariables;

    private void Awake()
    {
        _updateVariables = false;
        UpdateDatas();
    }

    private void Update()
    {
        if (_updateVariables)
            UpdateDatas();
    }

    private void UpdateDatas()
    {
        _updateVariables = false;

        //Bunny
        speed_B = speed;
        vision_B = vision;
        timeToConsume_B = timeToConsume;
        searchingMateDelay_B = searchingMateDelay;
        waterLimit_B = waterLimit;
        hungerLimit_B = hungerLimit;
        hungerToReproduct_B = hungerToReproduct;
        thirstToReproduct_B = thirstToReproduct;
        gestationnalTime_B = gestationnalTime;

        //Baby Bunny
        speed_BB = speed_baby;
        vision_BB = vision_baby;
        timeToConsume_BB = timeToConsume_baby;
        waterLimit_BB = waterLimit_baby;
        hungerLimit_BB = hungerLimit_baby;
        timeToGrow_BB = timeToGrow;
    }
}
