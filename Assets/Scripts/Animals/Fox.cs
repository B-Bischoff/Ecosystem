using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : Animals
{
    [SerializeField] private float _huntMaxDuration;

    [SerializeField] private float _huntDurationCounter;
    private bool _updateFoodPath;

    protected override void Start()
    {
        base.Start();
        _animal = "Fox";
    }


    protected override void Update()
    {
        base.Update();

        if (_foodTarget != null && !_updateFoodPath)
        {
            StartCoroutine(UpdateFoodPath());

            if (_huntDurationCounter >= _huntMaxDuration) //Fox will hunt for a certain time
            {
                _huntDurationCounter = 0;
                _foodTarget = null;
            }
        }
        if (_foodTarget)
            _huntDurationCounter += Time.deltaTime;
        else if (_foodTarget == null)
            _huntDurationCounter = 0;
    }

    protected override void IsAlive()
    {
        base.IsAlive();

        if (_thirst <= 0)
        {
            if (_baby)
                AnimalsManager.babyFoxList.Remove(gameObject);
            else
                AnimalsManager.foxList.Remove(gameObject);

            AnimalsManager.foxDead++;
            AnimalsManager.foxDehydrated++;
            Destroy(gameObject);
        }
        else if (_hunger <= 0)
        {
            if (_baby)
                AnimalsManager.babyFoxList.Remove(gameObject);
            else
                AnimalsManager.foxList.Remove(gameObject);

            AnimalsManager.foxDead++;
            AnimalsManager.foxStarved++;
            Destroy(gameObject);
        }
    }

    #region Food

    protected override void SearchFood()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && !_searchingFood)
        {
            _searchingFood = true;
            _randomDeplacement = false;
            _updateFoodPath = false;
            _foodTarget = null;
            float minDistance = float.MaxValue;

            foreach (GameObject bunny in AnimalsManager.bunnyList) //Bunny list
                if (Vector3.Distance(bunny.transform.position, transform.position) < _viewDistance)
                    if (Vector3.Distance(bunny.transform.position, transform.position) < minDistance)
                    {
                        minDistance = Vector3.Distance(bunny.transform.position, transform.position);
                        _foodTarget = bunny;
                    }
            foreach (GameObject bunny in AnimalsManager.babyBunnyList) //Baby bunny list
                if (Vector3.Distance(bunny.transform.position, transform.position) < _viewDistance)
                    if (Vector3.Distance(bunny.transform.position, transform.position) < minDistance)
                    {
                        minDistance = Vector3.Distance(bunny.transform.position, transform.position);
                        _foodTarget = bunny;
                    }

            if (_foodTarget != null)
            {
                agent.ResetPath();
                agent.destination = _foodTarget.transform.position;
                SetMovementAnimation();
                State = "Moving to food";
            }
            else
            {
                _searchingFood = false;
                _foodNotFound = true;
            }
        }
    }

    private IEnumerator UpdateFoodPath() //Used to update the destination 
    {
        _updateFoodPath = true;
        agent.destination = _foodTarget.transform.position;

        yield return new WaitForSeconds(1f); //Refresh rate
        _updateFoodPath = false;
    }



    protected override IEnumerator InteractWithFood()
    {
        if (_foodTarget != null && !_eating)
        {
            if (Vector3.Distance(_foodTarget.transform.position, transform.position) < .75f)
            {
                int value;
                if (_foodTarget.GetComponent<Bunny>().getBaby())
                    value = _foodValue - 10;
                else
                    value = _foodValue;

                State = "Eating";
                _huntDurationCounter = 0;
                _eating = true;
                agent.ResetPath();

                _foodTarget.GetComponent<Bunny>().setFoxTimeToConsumme(_timeToConsume);
                _foodTarget.GetComponent<Bunny>().setIsEaten(true);

                SetStandingAnimation();

                yield return new WaitForSeconds(_timeToConsume);

                _hunger = changeValues(value, _hunger);


                _searchingFood = false;
                _foodTarget = null;
                _foodNotFound = false;
                _eating = false;
            }
            else if (agent.destination != _foodTarget.transform.position)
                agent.destination = _foodTarget.transform.position;
        }
        else if (_foodTarget == null) //Bunny consummed by another fox
        {
            agent.ResetPath();

            _searchingFood = false;
            _foodTarget = null;
            _foodNotFound = false;

            SetStandingAnimation();
        }
        else if (_eating)
        {
            //SetStandingAnimation();
            if(GetComponent<NavMeshAgent>() != null)
                agent.ResetPath();
        }

    }

    #endregion

    #region Reproduction
    protected override void SearchMate()
    {
        //Try to find a mate
        float minDistance = float.MaxValue;

        foreach (GameObject fox in AnimalsManager.foxList)
        {
            if (fox == null) continue; //Check if bunny did not die at the same time. "continue" -> skip new iteration

            if (Vector3.Distance(fox.transform.position, transform.position) <= fox.GetComponent<Fox>().getViewDistance() && fox != gameObject)//Near the bunny && bunny is not himself
            {
                if (fox.GetComponent<Fox>().getReadyToReproduct() && fox.GetComponent<Fox>().getGender() != _gender
                    && !fox.GetComponent<Fox>().getMateFound())//bunny target is ready to reproduct & gender check & bunny is free (doesn't already have a mate) 
                {
                    if (Vector3.Distance(fox.transform.position, transform.position) < minDistance) //Distance inf than the previous bunny
                    {
                        minDistance = Vector3.Distance(fox.transform.position, transform.position); //minDistance refresh
                        _mateTarget = fox; //Set mateTarget
                    }
                }
            }
        }
        if (_mateTarget != null) //If mate found
        {
            _mateFound = true;
            _mateNotFound = false;
            _randomDeplacement = false;
            _interactingWithMate = false;
            agent.ResetPath();

            _mateTarget.GetComponent<Fox>().SetMateNotFound(false);
            _mateTarget.GetComponent<Fox>().setMateFound(true); //Set mate info (mateFound & mateTarget)
            _mateTarget.GetComponent<Fox>().setMateTarget(gameObject);
            _mateTarget.GetComponent<Fox>().SetInteractingWithMate(false);
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
        if(animator.GetBool("Walking") == false)
            SetMovementAnimation();
    }

    protected override IEnumerator InteractWithMate()
    {
        if (Vector3.Distance(_mateTarget.transform.position, transform.position) < 1f) //Check distance between bunnys
        {
            State = "Interacting with mate";

            _interactingWithMate = true;
            agent.ResetPath();
            SetStandingAnimation();

            yield return new WaitForSeconds(4f);

            //SET FEMALE PREGNANT
            if (_gender == "Female")
            {
                _isPregnant = true;

                //Store male carac. so baby can have stats from both parents

                //Storing female carac
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

                //Storing male carac
                _parentsCarac[0, 0] = _mateTarget.GetComponent<Fox>().GetUpdateFoodAndThirst();
                _parentsCarac[0, 1] = _mateTarget.GetComponent<Fox>().GetSpeed();
                _parentsCarac[0, 2] = _mateTarget.GetComponent<Fox>().getViewDistance();
                _parentsCarac[0, 3] = _mateTarget.GetComponent<Fox>().GetTimeToConsume();
                _parentsCarac[0, 4] = _mateTarget.GetComponent<Fox>().GetHungerLimit();
                _parentsCarac[0, 5] = _mateTarget.GetComponent<Fox>().GetThirstLimit();
                _parentsCarac[0, 6] = _mateTarget.GetComponent<Fox>().GetHungerToReproduct();
                _parentsCarac[0, 7] = _mateTarget.GetComponent<Fox>().GetThirstToReproduct();
                _parentsCarac[0, 8] = _mateTarget.GetComponent<Fox>().GetFoodValue();
                _parentsCarac[0, 9] = _mateTarget.GetComponent<Fox>().GetWaterValue();
                _parentsCarac[0, 10] = _mateTarget.GetComponent<Fox>().GetGestTime();
                _parentsCarac[0, 11] = _mateTarget.GetComponent<Fox>().GetTimeToGrow();
                _parentsCarac[0, 12] = _mateTarget.GetComponent<Fox>().GetLitter().x;
                _parentsCarac[0, 13] = _mateTarget.GetComponent<Fox>().GetLitter().y;
            }

            //Reproduction over

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

            _updateFoodAndThirstSpeed = PlayerPrefs.GetFloat("FoxReducRate");
            _speed = PlayerPrefs.GetFloat("FoxSpeedB");
            agent.speed = PlayerPrefs.GetFloat("FoxSpeedB");
            _viewDistance = PlayerPrefs.GetFloat("FoxViewDistB");
            _timeToConsume = PlayerPrefs.GetFloat("FoxConsumTimeB");

            _hungerLimit = PlayerPrefs.GetInt("FoxHungerLimB");
            _thirstLimit = PlayerPrefs.GetInt("FoxThirstLimB");
            _hungerToReproduct = PlayerPrefs.GetInt("FoxHungerRepro");
            _thirstToReproduct = PlayerPrefs.GetInt("FoxThirstRepro");
            _foodValue = PlayerPrefs.GetInt("FoxFoodValueB");
            _waterValue = PlayerPrefs.GetInt("FoxWaterValueB");
            _gestationalTime = PlayerPrefs.GetInt("FoxGestTime");
            _timeToGrow = PlayerPrefs.GetInt("FoxGrowthTime");
            _litter.x = PlayerPrefs.GetInt("FoxBabyLitterMin");
            _litter.y = PlayerPrefs.GetInt("FoxBabyLitterMax");
        }
        else //get stats entered in main menu
        {
            _updateFoodAndThirstSpeed = PlayerPrefs.GetFloat("FoxReducRate");
            _speed = PlayerPrefs.GetFloat("FoxSpeedA");
            agent.speed = PlayerPrefs.GetFloat("FoxSpeedA");
            _viewDistance = PlayerPrefs.GetFloat("FoxViewDistA");
            _timeToConsume = PlayerPrefs.GetFloat("FoxConsumTimeA");

            _hungerLimit = PlayerPrefs.GetInt("FoxHungerLimA");
            _thirstLimit = PlayerPrefs.GetInt("FoxThirstLimA");
            _hungerToReproduct = PlayerPrefs.GetInt("FoxHungerRepro");
            _thirstToReproduct = PlayerPrefs.GetInt("FoxThirstRepro");
            _foodValue = PlayerPrefs.GetInt("FoxFoodValueA");
            _waterValue = PlayerPrefs.GetInt("FoxWaterValueA");
            _gestationalTime = PlayerPrefs.GetInt("FoxGestTime");
            _timeToGrow = PlayerPrefs.GetInt("FoxGrowthTime");
            _litter.x = PlayerPrefs.GetInt("FoxBabyLitterMin");
            _litter.y = PlayerPrefs.GetInt("FoxBabyLitterMax");
        }
    }

    protected override void UpdateBaby()
    {
        if (_growTimer >= _timeToGrow)
        {
            transform.localScale = new Vector3(.09f, .09f, .09f);

            //Updating stats
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
                gameObject.name = "FoxM (clone)";
            else
                gameObject.name = "FoxF (clone)";

            AnimalsManager.babyFoxList.Remove(gameObject);
            AnimalsManager.foxList.Add(gameObject);
            transform.parent = GameObject.Find("FoxManager").transform;



        }
        base.UpdateBaby();
    }

    #region Animations
    protected override void SetStandingAnimation() { animator.SetBool("Walking", false); animator.SetBool("EarsMoving", true); }

    protected override void SetMovementAnimation() { animator.SetBool("Walking", true); animator.SetBool("EarsMoving", false); }

    #endregion

}
