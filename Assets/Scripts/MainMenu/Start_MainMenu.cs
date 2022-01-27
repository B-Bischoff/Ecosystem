using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Start_MainMenu : MonoBehaviour
{
    private int bunnyNumber, foxNumber;
    public TMP_InputField bunnyInput, foxInput;

    public void SetBunnyNumber(string newBunnyNumber)
    {
        int.TryParse(newBunnyNumber, out int input);
        
        input = CheckInput(input);

        bunnyNumber = input;
        bunnyInput.text = input.ToString();
    }

    public void SetFoxNumber(string newFoxNumber)
    {
        int.TryParse(newFoxNumber, out int input);

        input = CheckInput(input);

        foxNumber = input;
        foxInput.text = input.ToString();
    }

    private int CheckInput(int input)
    {
        if (input < 0 && input != 0)
            return -input;
        else
            return input;
    }

    public void StartSimulation()
    {
        gameObject.GetComponent<Terrain_MainMenu>().SavePlayerPrefs();
        gameObject.GetComponent<Bunny_MainMenu>().SavePlayerPrefs();
        gameObject.GetComponent<Fox_MainMenu>().SavePlayerPrefs();
        SavePlayerPrefs();

        SceneManager.LoadScene("Simulation");
    }



    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("StartBunnyNumber", bunnyNumber);
        PlayerPrefs.SetInt("StartFoxNumber", foxNumber);
    }

    public void LoadPlayerPrefs()
    {
        bunnyNumber = PlayerPrefs.GetInt("StartBunnyNumber");
        bunnyInput.text = bunnyNumber.ToString();

        foxNumber = PlayerPrefs.GetInt("StartFoxNumber");
        foxInput.text = foxNumber.ToString();
    }
}
