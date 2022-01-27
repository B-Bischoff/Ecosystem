using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject windowGraph;

    private bool _windowGraphShown;

    void Start()
    {
        _windowGraphShown = false;

        windowGraph.SetActive(false);

    }

    public void DisplayWindowGraph(bool display){ windowGraph.SetActive(display); }
}
