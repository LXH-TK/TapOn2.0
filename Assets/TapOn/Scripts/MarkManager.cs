﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapOn.Models.DataModels;
using TapOn.Api;
using TapOn.Constants;
using System.IO;

public class MarkManager : MonoBehaviour
{
    public float Saturation = 0.6f;
    public float Brightness = 0.8f;
    public SnapShotLoad snapShotLoad;

    public void AddMark(Mark mark)
    {
        Vector3 pos = Prefabs.instance.mapController.ConvertCoordinateToWorld(mark.coordinate);

        GameObject tempMark = (GameObject)Instantiate(Prefabs.instance.marker, GetComponent<MarkManager>().transform, true);
        tempMark.transform.position = pos + 0.5f * new Vector3(0, 0, tempMark.transform.localScale.y * tempMark.GetComponent<SpriteRenderer>().bounds.size.y);
        tempMark.GetComponent<Renderer>().sortingOrder = 1;

        //// 计算颜色 HSV->RGB
        string[] temp = mark.date.Split(' ');
        string[] temp2 = temp[1].Split(':');
        float Hue = (int.Parse(temp2[0]) * 15 + int.Parse(temp2[1]) / 4) * 1.0f / 360;
        tempMark.GetComponent<Renderer>().material.color = Color.HSVToRGB(Hue, Saturation, Brightness);

        StartCoroutine(snapShotLoad.LoadSnapShot(mark.url, mark.fileName, "picture", tempMark));

    }
    
}
