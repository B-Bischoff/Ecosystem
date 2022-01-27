using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListsManager : MonoBehaviour
{
    public static List<int> carrotNumber = new List<int>();
    public static List<int> bunnyNumber = new List<int>();
    public static List<int> bunnyDead = new List<int>();
    public static List<int> bunnyDehydrated = new List<int>();
    public static List<int> bunnyStarved = new List<int>();

    public static List<int> bunnyEaten = new List<int>();
    public static List<int> foxNumber = new List<int>();
    public static List<int> foxDead = new List<int>();
    public static List<int> foxDehydrated = new List<int>();
    public static List<int> foxStarved = new List<int>();

    public static string[] listsName = new string[11];

    public static void ClearLists()
    {
        AnimalsManager.bunnyDead = 0;
        AnimalsManager.bunnyDehydrated = 0;
        AnimalsManager.bunnyStarved = 0;
        AnimalsManager.bunnyEaten = 0;
        AnimalsManager.foxDead = 0;
        AnimalsManager.foxDehydrated = 0;
        AnimalsManager.foxStarved = 0;

        carrotNumber.Clear();
        bunnyNumber.Clear();
        bunnyDead.Clear();
        bunnyDehydrated.Clear();
        bunnyStarved.Clear();
        bunnyEaten.Clear();
        foxNumber.Clear();
        foxDead.Clear();
        foxDehydrated.Clear();
        foxStarved.Clear();
    }

    public static void initListsName()
    {
        listsName[0] = "No data";
        listsName[1] = "Bunny Number";
        listsName[2] = "Bunny Dead";
        listsName[3] = "Bunny Dehydrated";
        listsName[4] = "Bunny Starved";
        listsName[5] = "Bunny Eaten";
        listsName[6] = "Carrot Number";
        listsName[7] = "Fox Number";
        listsName[8] = "Fox Dead";
        listsName[9] = "Fox Dehydrated";
        listsName[10] = "Fox Starved";
    }

    public static void UpdateBunnyList() => bunnyNumber.Add(AnimalsManager.bunnyList.Count + AnimalsManager.babyBunnyList.Count);
    public static void UpdateCarrotList() => carrotNumber.Add(NatureManager.carrotsList.Count);
    public static void UpdateBunnyDead() => bunnyDead.Add(AnimalsManager.bunnyDead);
    public static void UpdateBunnyDehydrated() => bunnyDehydrated.Add(AnimalsManager.bunnyDehydrated);
    public static void UpdateBunnyStarved() => bunnyStarved.Add(AnimalsManager.bunnyStarved);
    public static void UpdateBunnyEaten() => bunnyEaten.Add(AnimalsManager.bunnyEaten);
    public static void UpdateFoxNumber() { foxNumber.Add(AnimalsManager.foxList.Count + AnimalsManager.babyFoxList.Count); }
    public static void UpdateFoxDead() => foxDead.Add(AnimalsManager.foxDead);
    public static void UpdateFoxDehydrated() => foxDehydrated.Add(AnimalsManager.foxDehydrated);
    public static void UpdateFoxStarved() => foxStarved.Add(AnimalsManager.foxStarved);
}
