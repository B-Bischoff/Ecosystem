using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/GraphPresets/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
            Directory.CreateDirectory(SAVE_FOLDER);
    }

    public static void Save(string saveString, int i)
    {
        File.WriteAllText(SAVE_FOLDER + "save_" + i + ".txt", saveString);
    }

    public static string Load(int i)
    {
        if (File.Exists(SAVE_FOLDER + "save_" + i + ".txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save_" + i + ".txt");
            return saveString;
        }
        else
            return null;

    }
}
