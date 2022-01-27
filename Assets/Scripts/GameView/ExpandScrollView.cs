using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpandScrollView : MonoBehaviour
{
    public GameObject scrollView;
    public RawImage arrow;

    private bool _scrollViewShown;

    public void ToggleScrollView()
    {
        if(_scrollViewShown)
        {
            _scrollViewShown = false;
            scrollView.SetActive(false);
            arrow.transform.DORotate(new Vector3(0, 0, 90), 0);
        }
        else
        {
            _scrollViewShown = true;
            scrollView.SetActive(true);
            arrow.transform.DORotate(new Vector3(0, 0, 0), 0);
        }
    }
}
