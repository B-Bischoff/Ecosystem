using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject content;

    public void Start()
    {
        content.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(content.activeSelf) //escape menu already displayed
            {
                Time.timeScale = TimeSection.timeScale_s;
                content.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                content.SetActive(true);
            }
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseEscMenu()
    {
        Time.timeScale = TimeSection.timeScale_s;
        content.SetActive(false);
    }
}
