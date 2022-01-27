using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem_MainMenu : MonoBehaviour
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/SimulationPresets/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
            Directory.CreateDirectory(SAVE_FOLDER);
    }

    public static void Save(string saveString, int i)
    {
        File.WriteAllText(SAVE_FOLDER + "save_" + i + ".txt", saveString);
    }

    // Datas loaded from user custom saves
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

    // Data loaded from preset files (load only)
    public static string LoadPreset(int i)
    {
        if (File.Exists(SAVE_FOLDER + "preset_save_" + i + ".txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "preset_save_" + i + ".txt");
            return saveString;
        }
        else
            return null;
    }
}
