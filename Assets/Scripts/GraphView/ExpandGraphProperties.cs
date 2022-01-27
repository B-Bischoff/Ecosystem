using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpandGraphProperties : MonoBehaviour
{
    public RectTransform Graph1, Graph2, Graph3, Graph4;
    public GameObject param1, param2, param3, param4;
    public RawImage ExpandImg1, ExpandImg2, ExpandImg3, ExpandImg4;

    public float expandTime;

    private bool _graph1expanded, _graph2expanded, _graph3expanded, _graph4expanded;
    private bool _graph1expanding, _graph2expanding, _graph3expanding, _graph4expanding;

    public void Start()
    {
        param1.SetActive(false);
        param2.SetActive(false);
        param3.SetActive(false);
        param4.SetActive(false);
    }

    public void ExpandGraphicProperties(int graphNumber)
    {
        if (Time.timeScale != 0) // Make sure time is not frozen
        {
            switch (graphNumber)
            {
                case 1:
                    if (!_graph1expanded & !_graph1expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph1expanded = true;

                        Graph2.DOAnchorPos(new Vector2(30, Graph2.anchoredPosition.y - param1.GetComponent<RectTransform>().sizeDelta.y), expandTime);
                        Graph3.DOAnchorPos(new Vector2(30, Graph3.anchoredPosition.y - param1.GetComponent<RectTransform>().sizeDelta.y), expandTime);
                        Graph4.DOAnchorPos(new Vector2(30, Graph4.anchoredPosition.y - param1.GetComponent<RectTransform>().sizeDelta.y), expandTime);

                        ExpandImg1.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), expandTime);
                        param1.SetActive(true);
                    }
                    else if (!_graph1expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph1expanded = false;

                        Graph2.DOAnchorPos(new Vector2(30, Graph2.anchoredPosition.y + param1.GetComponent<RectTransform>().sizeDelta.y), expandTime);
                        Graph3.DOAnchorPos(new Vector2(30, Graph3.anchoredPosition.y + param1.GetComponent<RectTransform>().sizeDelta.y), expandTime);
                        Graph4.DOAnchorPos(new Vector2(30, Graph4.anchoredPosition.y + param1.GetComponent<RectTransform>().sizeDelta.y), expandTime);

                        ExpandImg1.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 90), expandTime);
                        param1.SetActive(false);
                    }
                    break;

                case 2:
                    if (!_graph2expanded && !_graph2expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph2expanded = true;

                        Graph3.DOAnchorPos(new Vector2(30, Graph3.anchoredPosition.y - param2.GetComponent<RectTransform>().sizeDelta.y), expandTime);
                        Graph4.DOAnchorPos(new Vector2(30, Graph4.anchoredPosition.y - param2.GetComponent<RectTransform>().sizeDelta.y), expandTime);

                        ExpandImg2.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), expandTime);
                        param2.SetActive(true);
                    }
                    else if (!_graph2expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph2expanded = false;

                        Graph3.DOAnchorPos(new Vector2(30, Graph3.anchoredPosition.y + param2.GetComponent<RectTransform>().sizeDelta.y), expandTime);
                        Graph4.DOAnchorPos(new Vector2(30, Graph4.anchoredPosition.y + param2.GetComponent<RectTransform>().sizeDelta.y), expandTime);

                        ExpandImg2.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 90), expandTime);
                        param2.SetActive(false);
                    }
                    break;

                case 3:
                    if (!_graph3expanded && !_graph3expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph3expanded = true;

                        Graph4.DOAnchorPos(new Vector2(30, Graph4.anchoredPosition.y - param3.GetComponent<RectTransform>().sizeDelta.y), expandTime);

                        ExpandImg3.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), expandTime);
                        param3.SetActive(true);
                    }
                    else if (!_graph3expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph3expanded = false;

                        Graph4.DOAnchorPos(new Vector2(30, Graph4.anchoredPosition.y + param3.GetComponent<RectTransform>().sizeDelta.y), expandTime);

                        ExpandImg3.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 90), expandTime);
                        param3.SetActive(false);

                    }
                    break;

                case 4:
                    if (!_graph4expanded && !_graph4expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph4expanded = true;

                        ExpandImg4.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), expandTime);
                        param4.SetActive(true);

                    }
                    else if (!_graph4expanding)
                    {
                        StartCoroutine(delaySecurity(graphNumber));
                        _graph4expanded = false;

                        ExpandImg4.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 90), expandTime);
                        param4.SetActive(false);
                    }
                    break;
            }
        }
    }

    IEnumerator delaySecurity(int graphNumber)
    {
        switch(graphNumber)
        {
            case 1: _graph1expanding = true; break;
            case 2: _graph2expanding = true; break;
            case 3: _graph3expanding = true; break;
            case 4: _graph4expanding = true; break;
        }

        yield return new WaitForSeconds(expandTime);

        switch (graphNumber)
        {
            case 1: _graph1expanding = false; break;
            case 2: _graph2expanding = false; break;
            case 3: _graph3expanding = false; break;
            case 4: _graph4expanding = false; break;
        }
    }
       
}
