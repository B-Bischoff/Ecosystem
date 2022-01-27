using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SerializeField] public abstract class Animals : MonoBehaviour
{
    #region Variables

    public string State;

    public bool test;

    //Stats 
    [SerializeField] protected string _animal;
    [SerializeField] protected string _gender;
    [SerializeField] protected bool _baby;

    [SerializeField] protected float _hunger;
    [SerializeField] protected float _thirst;

    [SerializeField] protected float _updateFoodAndThirstSpeed;

    [SerializeField] protected int _foodValue;
    [SerializeField] protected int _waterValue;

    [SerializeField] protected int _hungerLimit;
    [SerializeField] protected int _thirstLimit;

    [SerializeField] protected int _hungerToReproduct;
    [SerializeField] protected int _thirstToReproduct;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _viewDistance;
    [SerializeField] protected float _timeToConsume;

    [SerializeField] protected float _searchingMateDelay;
    [SerializeField] protected bool _readyToReproduct;
    [SerializeField] protected bool _isPregnant;
    [SerializeField] protected float _gestationalTime;
    [SerializeField] protected Vector2 _litter;

    [SerializeField] protected bool _fleeing;


    [SerializeField] protected float _timeToGrow;

    [SerializeField] protected float[,] _parentsCarac = new float[2, 14];
    /* 
        * _parentsCarac description
        * 
        * dimension 0 : father
        * dimension 1 : mother
        * 
        * 0  - ReducRate
        * 1  - speed
        * 2  - viewDist
        * 3  - timeToConsum
        * 4  - hungerLim
        * 5  - thirstLim
        * 6  - hungerToRepro
        * 7  - thirstToRepro
        * 8  - foodValue
        * 9  - WaterValue
        * 10 - gestTime
        * 11 - timeToGrow
        * 12 - litterMin
        * 13 - litterMax
    */


    //Unity components
    protected NavMeshAgent agent;
    protected Animator animator;

    //Targets (food, water, mate)
    [SerializeField] protected GameObject _waterTarget, _foodTarget, _mateTarget;
    [SerializeField] protected Vector3 randomMovementPos;

    //Checkers
    protected bool _searchingFood, _searchingWater, _randomDeplacement;
    protected bool _waterNotFound, _foodNotFound, _mateNotFound;
    protected bool _eating, _drinking, _interactingWithMate;
    protected bool _mateFound;
    protected float _reproCooldown, _gestationalTimer, _growTimer;

    #endregion

    #region awake, start & update

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        GetParentsStats(); //Getting animals stats from parents or from stats entered in main menu
    }

    protected virtual void Start()
    {
        _thirst = 100;
        _hunger = 100;
        _growTimer = 0;
    }

    protected virtual void Update()
    {
        IsAlive();

        UpdateFoodAndThirst();
        UpdateReproductionState();

        if (_isPregnant)
            UpdatePregnancy();

        if (_baby)
            UpdateBaby();

        if (_thirst <= _thirstLimit && !_waterNotFound && !_fleeing && !_eating)
        {
            if (test)
                Debug.Log("water");

            if (!_searchingWater)
                SearchWater();
            else
                StartCoroutine(InteractWithWater());
        }
        else if(_hunger <= _hungerLimit && !_foodNotFound && !_fleeing)
        {
            if (test)
                Debug.Log("food");

            if (!_searchingFood)
                SearchFood(); //Search way to food
            else //If food reached
                StartCoroutine(InteractWithFood()); //Interact
        }
        else if((_readyToReproduct || _mateFound) && !_fleeing && !_mateNotFound)
        {
            if (_mateTarget == null && !_mateFound) //If bunny doesn't have a mate
            {
                if (test)
                    Debug.Log("search mate");
                SearchMate();
            }
            else if (_mateFound && _mateTarget != null) //If bunny already got a mate
            {
                if (test)
                    Debug.Log("mate found or mate target");
                if (!_interactingWithMate)
                    MoveToMate();
                if (agent.pathStatus == NavMeshPathStatus.PathComplete && !_interactingWithMate) //If mate reached -> interact
                    StartCoroutine(InteractWithMate());
            }

        }
        else if(!_fleeing)//Random movement
        {
            if(test)
                Debug.Log("random movement");
            RandomMovement();
        }
    }

    #endregion

    protected virtual void IsAlive()
    {
        if (_thirst <= 0)
        {
            Debug.Log(_animal + " died from dehydration");
        }
        else if (_hunger <= 0)
        {
            Debug.Log(_animal + "starved to death");
        }
    }

    protected void RandomMovement()
    {
        if (!_randomDeplacement) //if navigation accomplished
        {
            randomMovementPos = Vector3.zero;
            bool validCoord = false;

            while (!validCoord)
            {
                State = "Looking for direction";

                Vector3 destination = FindPosInCircle(transform.position, 3f);

                //out of map verification
                if (!(destination.x >= TerrainDatas.xSize / TerrainDatas.mapScale || destination.x < 0 || destination.z >= TerrainDatas.zSize / TerrainDatas.mapScale || destination.z < 0))
                {
                    //Water verification
                    if (TerrainDatas.heightMap[(int)(destination.z * TerrainDatas.mapScale), (int)(destination.x * TerrainDatas.mapScale)] >= .30f)
                    {
                        validCoord = true;
                        //retrieving yPos from the heightMap
                        float yPos = TerrainDatas.heightMap[(int)(destination.z * TerrainDatas.mapScale), (int)(destination.x * TerrainDatas.mapScale)] * TerrainDatas.heightMultiplier + .1f; //.1 = security

                        randomMovementPos = new Vector3(destination.x, yPos, destination.z);
                        agent.SetDestination(randomMovementPos);
                        SetMovementAnimation();

                        _randomDeplacement = true; 
                    }
                }
            }
        }
        else //if already moving 
        {
            State = "Random Walking";
            if (Vector3.Distance(randomMovementPos, transform.position) < .75f) //if random destination reached
            {
                _randomDeplacement = false;
                randomMovementPos = Vector3.zero;
                SetStandingAnimation();

                _foodNotFound = false;
                _waterNotFound = false;
                _mateNotFound = false;
                _fleeing = false;
            }
        }
    }

    protected Vector3 FindPosInCircle(Vector3 centre, float radius)
    {
        Vector3 pos = new Vector3();
        float angle = Random.value * 360;
        float margin = 1.5f;

        float norme = Random.Range(margin, radius);

        pos.x = centre.x + (norme * Mathf.Cos(angle));
        pos.z = centre.z + (norme * Mathf.Sin(angle));  

        return pos;
    }

    #region Food & Water

    private void UpdateFoodAndThirst()
    {
        _thirst -= Time.deltaTime * _updateFoodAndThirstSpeed;
        _hunger -= Time.deltaTime * _updateFoodAndThirstSpeed;
    }

    void SearchWater()
    {
        _searchingWater = true;
        _searchingFood = false;
        _foodTarget = null;
        _randomDeplacement = false;
        _waterTarget = null;
        float minDistance = float.MaxValue;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _viewDistance);
        foreach(Collider collider in hitColliders)
        {
            if (collider.name == "WaterSpot(Clone)")
            {
                if (Vector3.Distance(collider.transform.position, transform.position) < minDistance) //Distance inf than the previous waterSpot
                {
                    minDistance = Vector3.Distance(collider.transform.position, transform.position);
                    _waterTarget = collider.gameObject;
                }
            }
        }


        if (_waterTarget != null)
        {
            agent.ResetPath();
            agent.destination = _waterTarget.transform.position;
            SetMovementAnimation();
            State = "Moving to water";
        }
        else
        {
            _searchingWater = false;
            _waterNotFound = true;
        }
        
    }
    private IEnumerator InteractWithWater()
    {
        if (_waterTarget != null && !_drinking)
            if (Vector3.Distance(_waterTarget.transform.position, transform.position) < .5f)
            {
                State = "Drinking";
                _drinking = true;
                agent.ResetPath(); //Stop try to reach destination
                SetStandingAnimation();

                yield return new WaitForSeconds(_timeToConsume);

                _thirst = changeValues(_waterValue, _thirst);

                _searchingWater = false;
                _waterTarget = null;
                _waterNotFound = false;
                _drinking = false;
                _randomDeplacement = false;
                _mateNotFound = false;
            }
        else if (agent.destination != _waterTarget.transform.position)
            agent.destination = _waterTarget.transform.position;
    }


    protected abstract void SearchFood();
    protected abstract IEnumerator InteractWithFood();



    protected float changeValues(int amount, float stat) //Change hunger or thirst values
    {
        if (amount + stat > 100) //Clamp stat to 100
            stat = 100;
        else
            stat += amount;
        return stat;
    }

    #endregion

    #region Reproduction

    void UpdateReproductionState()
    {
        if (_hunger >= _hungerToReproduct && _thirst >= _thirstToReproduct) //Bunny with enough stats
        {
            if(!_isPregnant && !_baby && _reproCooldown <= 0)
                _readyToReproduct = true;
            /*
            if (_searchingMateTimer >= _searchingMateDelay && !_isPregnant && !_baby) //Check searching mate cooldown (to prevent spam) + pregnancy + baby
            {
                _readyToReproduct = true;
                _searchingMateTimer = 0;
            }
            else
                _readyToReproduct = false;
            */
        }
        else
            _readyToReproduct = false;

        if(_reproCooldown >= 0) //To not increment _searchingMateTime above _searchingMateDelay
            _reproCooldown -= Time.deltaTime;
    }

    protected abstract void SearchMate();

    protected abstract void MoveToMate();

    protected abstract IEnumerator InteractWithMate();

    #endregion

    #region Pregnancy

    protected void UpdatePregnancy()
    {
        if(_gestationalTimer >= _gestationalTime)
        {
            int _numberOfChild = Random.Range((int)_litter.x, (int)_litter.y);

            AnimalsManager.GiveBirth(gameObject, _numberOfChild);

            _isPregnant = false;
            _gestationalTimer = 0;
        }
        _gestationalTimer += Time.deltaTime;
    }

    #endregion

    #region Baby
    protected abstract void GetParentsStats();

    protected virtual void UpdateBaby()
    {
        if (_growTimer >= _timeToGrow)
        {
            _baby = false;
        }
        else
            _growTimer += Time.deltaTime;
    }

    #endregion

    #region Animation

    protected abstract void SetMovementAnimation();
    protected abstract void SetStandingAnimation();

    #endregion

    #region Getters & Setters

    public string getGender() { return _gender; }
    public float getHunger() { return _hunger; }
    public float getThirst() { return _thirst; }
    public bool getReadyToReproduct() { return _readyToReproduct; }
    public float getViewDistance() { return _viewDistance; }
    public bool getMateFound() { return _mateFound; }
    public bool getBaby() { return _baby; }
    public string GetState() { return State; }
    public float GetSpeed() { return _speed; }
    public float GetTimeToConsume() { return _timeToConsume; }
    public float GetThirstToReproduct() { return _thirstToReproduct; }
    public float GetHungerToReproduct() { return _hungerToReproduct; }
    public float GetHungerLimit() { return _hungerLimit; }
    public float GetThirstLimit() { return _thirstLimit; }
    public int GetFoodValue() { return _foodValue; }
    public int GetWaterValue() { return _waterValue; }
    public float GetGestTime() { return _gestationalTime; }
    public float GetTimeToGrow() { return _timeToGrow; }
    public bool GetPregnancy() { return _isPregnant; }
    public Vector2 GetLitter() { return _litter; }
    public float GetUpdateFoodAndThirst() { return _updateFoodAndThirstSpeed; }
    public float[,] GetParentsCarac() { return _parentsCarac; }

    public GameObject GetMateTarget() { return _mateTarget; }

    public void setGender(string value) => _gender = value;
    public void setMateTarget(GameObject gameObject) => _mateTarget = gameObject;
    public void setMateFound(bool value) => _mateFound = value;
    public void setBaby(bool value) => _baby = value;
    public void SetMateNotFound(bool value) => _mateNotFound = value;
    public void SetParentsCarac(float[,] newParentsCarac) => _parentsCarac = newParentsCarac;

    public void SetOutline(bool value) => GetComponent<QuickOutline>().enabled = value;

    public void SetInteractingWithMate(bool value) => _interactingWithMate = value;

    #endregion
}
