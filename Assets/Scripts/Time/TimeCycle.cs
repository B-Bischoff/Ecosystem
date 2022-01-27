using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public int cycleTime;
    public static int nbMeasurePerCycle;

    [SerializeField] public float _timeCounter, _measureCounter;
    [SerializeField] public double _realTimeCounter;
    private int _cycleCounter;

    private void Awake()
    {
        // Default value 
        if (nbMeasurePerCycle <= 0)
            nbMeasurePerCycle = 6;
    }

    public void Start()
    {
        ListsManager.ClearLists();
        ListsManager.initListsName();
        _cycleCounter = 1;
        _timeCounter = 0;
    }

    public void Update()
    {
        _timeCounter += Time.deltaTime;
        _measureCounter += Time.deltaTime;
        _realTimeCounter += Time.deltaTime;

        if (_timeCounter >= cycleTime) //Increase cycle
        {
            _timeCounter = 0;
            _cycleCounter++;

            NatureManager.spawnCarrot_s = true;
        }

        if (_measureCounter >= (cycleTime / nbMeasurePerCycle))
        {
            _measureCounter = 0;

            ListsManager.UpdateBunnyList();
            ListsManager.UpdateBunnyDead();
            ListsManager.UpdateCarrotList();
            ListsManager.UpdateBunnyDehydrated();
            ListsManager.UpdateBunnyStarved();
            ListsManager.UpdateBunnyEaten();
            ListsManager.UpdateFoxNumber();
            ListsManager.UpdateFoxDehydrated();
            ListsManager.UpdateFoxStarved();
            ListsManager.UpdateFoxDead();
        }
    }

    public double GetRealTimeCounter() { return _realTimeCounter; }
    public int GetCycleCounter() { return _cycleCounter; }
}
