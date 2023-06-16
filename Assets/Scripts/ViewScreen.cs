using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ViewScreen : MonoBehaviour
{
    private Image image;
    public static ViewScreen Instance;

    void Awake()
    {
        InitializeSingleton();
        image = GetComponent<Image>();
    }

    void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("More than one singleton ViewScreen on scene");
        }
    }
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
