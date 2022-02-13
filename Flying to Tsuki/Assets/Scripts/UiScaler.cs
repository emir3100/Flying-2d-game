using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScaler : MonoBehaviour
{
    private float resoX;
    private float resoY;

    private CanvasScaler canvas;
    private void Start()
    {
        canvas = GetComponent<CanvasScaler>();
        SetInfo();
    }

    private void SetInfo()
    {
        resoX = (float)Screen.currentResolution.width;
        resoY = (float)Screen.currentResolution.height;

        canvas.referenceResolution = new Vector2(resoY, resoX);
    }
}
