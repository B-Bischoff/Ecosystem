using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bunny : Animals
{
    private bool _isEaten, _callOnce, _randomFleeing;
    private float _foxTimeToConsume;

    [SerializeField] private GameObject _foxFlown;
    [SerializeField] float _distanceToFlee;
    [SerializeField] Vector3 fleePos;

    protected override void Start()
    {
        base.Start();
        _animal = "Bunny";
    }

    protected override void Update()
    {
        if(CheckFlee())
        {
            if(FleeImpossible())
            {
                FleeRandomPosition();
            }
            else
            {
                FleeOpposite();
            }
        } //No need to flee, execute Animals.Update()

        base.Update();
    }

    protected override void IsAlive()
    {
        base.IsAlive();

        if (_thirst <= 0)
        {
            if (_baby)
                AnimalsManager.babyBunnyList.Remove(gameObject);
            else
                AnimalsManager.bunnyList.Remove(gameObject);

            AnimalsManager.bunnyDead++;
            AnimalsManager.bunnyDehydrated++;
            Destroy(gameObject);
        }
        else if (_hunger <= 0)
        {
            if (_baby)
                AnimalsManager.babyBunnyList.Remove(gameObject);
            else
                AnimalsManager.bunnyList.Remove(gameObject);

            AnimalsManager.bunnyDead++;
            AnimalsManager.bunnyStarved++;
            Destroy(gameObject);
        }
        else if (_isEaten)
        {
            agent.ResetPath(); //Make sure bunny doesn't move while being eaten
            if(!_callOnce)
                StartCoroutine(IsEaten(_foxTimeToConsume));
        }
    }

    #region Flee

    private void FleeRandomPosition()
    {
        if (!_randomFleeing) //if navigation accomplished
        {
            randomMovementPos = Vector3.zero;
            bool validCoord = false;

            while (!validCoord)
            {
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

                        _randomFleeing = true;
                    }
                }
            }
        }
        else //if already moving 
        {
            State = "Fleeing";
            if (Vector3.Distance(randomMovementPos, transform.position) < .75f) //if random destination reached
            {
                _randomFleeing = false;
                randomMovementPos = Vector3.zero;
                SetStandingAnimation();

                _foodNotFound = false;
                _waterNotFound = false;
                _fleeing = false;
            }
        }
    }

    private void FleeOpposite()
    {
        State = "Fleeing";
        _fleeing = true;
        _randomFleeing = false;

        float yPos = TerrainDatas.heightMap[(int)(fleePos.z * TerrainDatas.mapScale), (int)(fleePos.x * TerrainDatas.mapScale)] + .1f; //.1 = security
        fleePos.y = yPos;

        agent.destination = fleePos;
        SetMovementAnimation();
    }

    private bool FleeImpossible()
    {
        // Fox relative coordinates (bunny is the reference)
        Vector3 foxDistance = transform.position - _foxFlown.transform.position;

        // Resetting flee destination
        fleePos = Vector3.zero;

        _randomDeplacement = false;

        //Clamping flee destination (To make sure the bunny doesn't want to run outside the map)
        if (foxDistance.x > 0)
            foxDistance.x = 1f;
        else if (foxDistance.x < 0)
            foxDistance.x = -1f;

        if (foxDistance.z > 0)
            foxDistance.z = 1f;
        else if (foxDistance.z < 0)
            foxDistance.z = -1;

        fleePos = transform.position + foxDistance;

        //out of map verification
        if (!(fleePos.x >= TerrainDatas.xSize / TerrainDatas.mapScale || fleePos.x < 0 || fleePos.z >= TerrainDatas.zSize / TerrainDatas.mapScale || fleePos.z < 0))
        {
            //Water verification
            if (TerrainDatas.heightMap[(int)(fleePos.z * TerrainDatas.mapScale), (int)(fleePos.x * TerrainDatas.mapScale)] >= .30f)
            {
                return false; //Flee in the opposite fox direction
            }
            else
                return true; //Cannot flee in the opposite fox direction (water blocked)
        }
        else
            return true; //Cannot flee in the opposite fox direction (out of map blocked)
    }

    private bool CheckFlee()
    {
        _foxFlown = null;
        float nearestFoxDistance = float.MaxValue;
        float foxDistance;

        // Check foxs distance
        foreach (GameObject fox in AnimalsManager.foxList)
        {
            if (fox == null) continue; // Checking if fox still exist (he could have died at the exact same moment)

            // Calculating distance between fox and bunny
            foxDistance = Vector3.Distance(fox.transform.position, transform.position);

            if (foxDistance < _distanceToFlee)
                if (foxDistance < nearestFoxDistance)
                {
                    nearestFoxDistance = foxDistance;
                    _foxFlown = fox;
                }
        }
        foreach (GameObject fox in AnimalsManager.babyFoxList) {
            if (fox == null) continue;

            // Calculating distance between fox and bunny
            foxDistance = Vector3.Distance(fox.transform.position, transform.position);

            if (foxDistance < _distanceToFlee)
                if (foxDistance < nearestFoxDistance)
                {
                    nearestFoxDistance = foxDistance;
                    _foxFlown = fox;
                }
        }

        // If a fox is near
        if (_foxFlown != null )
        {
            _fleeing = true;
        }
        else
        {
            fleePos = Vector3.zero;
            _fleeing = false;

            if(agent.destination == fleePos)
            {
                agent.ResetPath();
            }
        }
        
        return _fleeing;
    }

    #endregion

    #region Food

    protected override void SearchFood()
    {
        _searchingFood = true;
        _randomDeplacement = false;
        _foodTarget = null;
        float minDistance = float.MaxValue;

        int layerMask = 1 << 8; //Layer 8

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _viewDistance, layerMask);
        foreach (Collider collider in hitColliders)
        {
            if (Vector3.Distance(collider.transform.position, transform.position) < minDistance) //Distance inf than the previous carrot
            {
                minDistance = Vector3.Distance(collider.transform.position, transform.position);
                _foodTarget = collider.gameObject;
            }
        }

        if (_foodTarget != null)
        {
            agent.ResetPath();
            agent.destination = _foodTarget.transform.position;
            SetMovementAnimation();
            State = "Moving to food";
        }
        else //did not find any food
        {
            _searchingFood = false;
            _foodNotFound = true;
        }
    }

    protected override IEnumerator InteractWithFood()
    {
        if (_foodTarget != null && !_eating)
        {
            if (Vector3.Distance(_foodTarget.transform.position, transform.position) < .5f)
            {
                State = "Eating";

                _eating = true;

                if (GetComponent<NavMeshAgent>() != null)
                    agent.ResetPath(); //Stop try to reach destination
                SetStandingAnimation();

                yield return new WaitForSeconds(_timeToConsume);

                if (_foodTarget != null && TerrainDatas.natureMap[_foodTarget.GetComponent<Carrot>().GetIndex()])
                    TerrainDatas.natureMap[_foodTarget.GetComponent<Carrot>().GetIndex()] = false; //Removing carrot from the natureMap

                NatureManager.carrotsList.Remove(_foodTarget); //Remove carrot from list
                Destroy(_foodTarget); //Remove carrot physically

                _hunger = changeValues(_foodValue, _hunger);

                _searchingFood = false;
                _foodTarget = null;
                _foodNotFound = false;
                _eating = false;
                _mateNotFound = false;
            }
            else if (agent.destination != _foodTarget.transform.position)
                agent.destination = _foodTarget.transform.position;
        }
        else if (_foodTarget == null) //Carrot might have been consumed by another bunny
        {
            if(GetComponent<NavMeshAgent>() != null)
                agent.ResetPath(); //Stop try to reach destination

            _searchingFood = false;
            _foodTarget = null;
            _foodNotFound = false;
            _randomDeplacement = false;

            SetStandingAnimation();
        }
    }

    public IEnumerator IsEaten(float time)
    {
        _isEaten = true;
        _callOnce = true;
        agent.ResetPath();

        yield return new WaitForSeconds(time);

        if(_baby)
        {
            //ADD REMOVE LISTS + COUNTER
            AnimalsManager.babyBunnyList.Remove(gameObject);
        }
        else
        {
            AnimalsManager.bunnyList.Remove(gameObject);
        }
        AnimalsManager.bunnyEaten++;
        AnimalsManager.bunnyDead++;
        Destroy(gameObject);
    }

    #endregion

    #region Reproduction

    protected override void SearchMate()
    {
        //Try to find a mate
        float minDistance = float.MaxValue;

        foreach (GameObject bunny in AnimalsManager.bunnyList)
        {
            if (bunny == null) continue; //Check if bunny did not die at the exact same time. "continue" -> skip to the next iteration

            if (Vector3.Distance(bunny.transform.position, transform.position) <= bunny.GetComponent<Bunny>().getViewDistance() && bunny != gameObject)//Near the bunny && bunny is not himself
            {
                if (bunny.GetComponent<Bunny>().getReadyToReproduct() && bunny.GetComponent<Bunny>().getGender() != _gender 
                    && (bunny.GetComponent<Bunny>().GetMateTarget() == gameObject || bunny.GetComponent<Bunny>().GetMateTarget() == null))//bunny target is ready to reproduct & gender check & bunny is doesn't already have a mate
                {
                    if (Vector3.Distance(bunny.transform.position, transform.position) < minDistance) //Distance inf than the previous bunny
                    {
                        minDistance = Vector3.Distance(bunny.transform.position, transform.position); //minDistance refresh
                        _mateTarget = bunny; //Set mateTarget
                    }
                }
            }
        }

        if (_mateTarget != null) //If mate found
        {
            _randomDeplacement = false;
            _mateFound = true;
            _mateNotFound = false;
            _interactingWithMate = false;
            agent.ResetPath();

            _mateTarget.GetComponent<Bunny>().SetMateNotFound(false);
            _mateTarget.GetComponent<Bunny>().setMateFound(true); //Set mate info (mateFound & mateTarget)
            _mateTarget.GetComponent<Bunny>().setMateTarget(gameObject);
            _mateTarget.GetComponent<Bunny>().SetInteractingWithMate(false);
            _mateTarget.GetComponent<NavMeshAgent>().ResetPath();
        }
        else
        {
            _randomDeplacement = false;
            _mateNotFound = true;
        }
    }

    protected override void MoveToMate()
    {
        State = "Moving to mate";
        agent.destination = _mateTarget.transform.position;
        if(animator.GetBool("Moving") == false)
            SetMovementAnimation();
    }

    protected override IEnumerator InteractWithMate()
    {
        if (Vector3.Distance(_mateTarget.transform.position, transform.position) < .5f) //Check distance between bunnys
        {
            State = "Interacting with mate";

            _interactingWithMate = true;
            agent.ResetPath();
            SetJumpingAnimation();

            yield return new WaitForSeconds(3f);

            // Female pregnancy
            if (_gender == "Female")
            {
                _isPregnant = true;

                // Store male carac. so baby can have stats from both parents

                // Storing female carac
                _parentsCarac[1, 0] = _updateFoodAndThirstSpeed;
                _parentsCarac[1, 1] = _speed;
                _parentsCarac[1, 2] = _viewDistance;
                _parentsCarac[1, 3] = _timeToConsume;
                _parentsCarac[1, 4] = _hungerLimit;
                _parentsCarac[1, 5] = _thirstLimit;
                _parentsCarac[1, 6] = _hungerToReproduct;
                _parentsCarac[1, 7] = _thirstToReproduct;
                _parentsCarac[1, 8] = _foodValue;
                _parentsCarac[1, 9] = _waterValue;
                _parentsCarac[1, 10] = _gestationalTime;
                _parentsCarac[1, 11] = _timeToGrow;
                _parentsCarac[1, 12] = _litter.x;
                _parentsCarac[1, 13] = _litter.y;

                // Storing male carac
                _parentsCarac[0, 0] = _mateTarget.GetComponent<Bunny>().GetUpdateFoodAndThirst();
                _parentsCarac[0, 1] = _mateTarget.GetComponent<Bunny>().GetSpeed();
                _parentsCarac[0, 2] = _mateTarget.GetComponent<Bunny>().getViewDistance();
                _parentsCarac[0, 3] = _mateTarget.GetComponent<Bunny>().GetTimeToConsume();
                _parentsCarac[0, 4] = _mateTarget.GetComponent<Bunny>().GetHungerLimit();
                _parentsCarac[0, 5] = _mateTarget.GetComponent<Bunny>().GetThirstLimit();
                _parentsCarac[0, 6] = _mateTarget.GetComponent<Bunny>().GetHungerToReproduct();
                _parentsCarac[0, 7] = _mateTarget.GetComponent<Bunny>().GetThirstToReproduct();
                _parentsCarac[0, 8] = _mateTarget.GetComponent<Bunny>().GetFoodValue();
                _parentsCarac[0, 9] = _mateTarget.GetComponent<Bunny>().GetWaterValue();
                _parentsCarac[0, 10] = _mateTarget.GetComponent<Bunny>().GetGestTime();
                _parentsCarac[0, 11] = _mateTarget.GetComponent<Bunny>().GetTimeToGrow();
                _parentsCarac[0, 12] = _mateTarget.GetComponent<Bunny>().GetLitter().x;
                _parentsCarac[0, 13] = _mateTarget.GetComponent<Bunny>().GetLitter().y;
            }

            // Reproduction over
            if (_gender == "Male")
                _reproCooldown = 5f;

            _mateTarget = null;
            _interactingWithMate = false;
            _mateFound = false;
            _readyToReproduct = false;
            _randomDeplacement = false;
            SetStandingAnimation();
        }
    }



    #endregion

    #region baby
    protected override void GetParentsStats()
    {
        if (_baby == true) //get stats from parents
        {
            /*
            //Get Stats from parents
            _updateFoodAndThirstSpeed = _parentsCarac[0,0];
            agent.speed = _parentsCarac[0, 1] * (PlayerPrefs.GetFloat("BunnyBabySpeed",100)); 
            _viewDistance = _parentsCarac[0, 2] * (PlayerPrefs.GetFloat("BunnyBabyViewDist",100)); 
            _timeToConsume = _parentsCarac[0, 3] * (PlayerPrefs.GetFloat("BunnyBabyConsumTime",100)); 

            _hungerLimit = (int)_parentsCarac[0, 4];
            _thirstLimit = (int)_parentsCarac[0, 5];
            _hungerToReproduct = PlayerPrefs.GetInt("BunnyHungerRepro", 80);
            _thirstToReproduct = PlayerPrefs.GetInt("BunnyThirstRepro", 80);
            _foodValue = PlayerPrefs.GetInt("BunnyBabyFoodValue");
            _waterValue = PlayerPrefs.GetInt("BunnyBabyWaterValue");
            _gestationalTime = _parentsCarac[0, 10];
            _timeToGrow = _parentsCarac[0, 11];
            _litter.x = _parentsCarac[0, 12];
            _litter.y = _parentsCarac[0, 13];
            */

            _updateFoodAndThirstSpeed = PlayerPrefs.GetFloat("BunnyReducRate");
            _speed = PlayerPrefs.GetFloat("BunnyBabySpeed");
            agent.speed = PlayerPrefs.GetFloat("BunnyBabySpeed");
            _viewDistance = PlayerPrefs.GetFloat("BunnyBabyViewDist");
            _timeToConsume = PlayerPrefs.GetFloat("BunnyBabyConsumTime");

            _hungerLimit = PlayerPrefs.GetInt("BunnyBabyHungerLim");
            _thirstLimit = PlayerPrefs.GetInt("BunnyBabyThirstLim");
            _hungerToReproduct = PlayerPrefs.GetInt("BunnyHungerRepro");
            _thirstToReproduct = PlayerPrefs.GetInt("BunnyThirstRepro");
            _foodValue = PlayerPrefs.GetInt("BunnyBabyFoodValue");
            _waterValue = PlayerPrefs.GetInt("BunnyBabyWaterValue");
            _gestationalTime = PlayerPrefs.GetInt("BunnyGestTime");
            _timeToGrow = PlayerPrefs.GetInt("BunnyGrowthTime");
            _litter.x = PlayerPrefs.GetInt("BunnyBabyLitterMin");
            _litter.y = PlayerPrefs.GetInt("BunnyBabyLitterMax");
        }
        else //get stats entered in main menu
        {
            _updateFoodAndThirstSpeed = PlayerPrefs.GetFloat("BunnyReducRate");
            _speed = PlayerPrefs.GetFloat("BunnySpeed");
            agent.speed = PlayerPrefs.GetFloat("BunnySpeed");
            _viewDistance = PlayerPrefs.GetFloat("BunnyViewDist");
            _timeToConsume = PlayerPrefs.GetFloat("BunnyConsumTime");

            _hungerLimit = PlayerPrefs.GetInt("BunnyHungerLim");
            _thirstLimit = PlayerPrefs.GetInt("BunnyThirstLim");
            _hungerToReproduct = PlayerPrefs.GetInt("BunnyHungerRepro");
            _thirstToReproduct = PlayerPrefs.GetInt("BunnyThirstRepro");
            _foodValue = PlayerPrefs.GetInt("BunnyFoodValue");
            _waterValue = PlayerPrefs.GetInt("BunnyWaterValue");
            _gestationalTime = PlayerPrefs.GetInt("BunnyGestTime");
            _timeToGrow = PlayerPrefs.GetInt("BunnyGrowthTime");
            _litter.x = PlayerPrefs.GetInt("BunnyBabyLitterMin");
            _litter.y = PlayerPrefs.GetInt("BunnyBabyLitterMax");
        }
    }
    protected override void UpdateBaby()
    {
        if (_growTimer >= _timeToGrow && _timeToGrow != 0)
        {
            transform.localScale = new Vector3(.05f, .05f, .05f); //Resize bunny

            //Updating stats
            _updateFoodAndThirstSpeed = _parentsCarac[0, 0];
            agent.speed = _parentsCarac[0, 1];
            _speed = _parentsCarac[0, 1];
            _viewDistance = _parentsCarac[0, 2];
            _timeToConsume = _parentsCarac[0, 3];

            _hungerLimit = (int)_parentsCarac[0, 4];
            _thirstLimit = (int)_parentsCarac[0, 5];
            _hungerToReproduct = (int)_parentsCarac[0, 6];
            _thirstToReproduct = (int)_parentsCarac[0, 7];
            _foodValue = (int)_parentsCarac[0, 8];
            _waterValue = (int)_parentsCarac[0, 9];
            _gestationalTime = _parentsCarac[0, 10];
            _timeToGrow = _parentsCarac[0, 11];
            _litter.x = _parentsCarac[0, 12];
            _litter.y = _parentsCarac[0, 13];

            if (_gender == "Male")
                gameObject.name = "BunnyM (clone)";
            else
                gameObject.name = "BunnyF (clone)";

            AnimalsManager.babyBunnyList.Remove(gameObject);
            AnimalsManager.bunnyList.Add(gameObject);
            transform.parent = GameObject.Find("BunnyMananger").transform;

        }
        base.UpdateBaby();
    }

    #endregion


    #region animations
    protected override void SetMovementAnimation() { animator.SetBool("Moving", true); animator.SetBool("EarsMoving", false); animator.SetBool("Jumping", false); }
    protected override void SetStandingAnimation() { animator.SetBool("Moving", false); animator.SetBool("EarsMoving", true); animator.SetBool("Jumping", false); }
    protected void SetJumpingAnimation() { animator.SetBool("Moving", false); animator.SetBool("EarsMoving", false); animator.SetBool("Jumping", true); }

    #endregion

    public void setIsEaten(bool value) => _isEaten = value;
    public void setFoxTimeToConsumme(float value) => _foxTimeToConsume = value;
}
