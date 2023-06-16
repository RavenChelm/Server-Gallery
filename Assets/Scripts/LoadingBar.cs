using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingBar : MonoBehaviour
{
    public static LoadingBar Instance { get; private set; }
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressText;

    void Awake()
    {
        InitializeSingleton();
    }
    void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("More than one singleton LoadingBar on scene");
        }
    }
    public void UpdateLoadingBar(float progress)
    {
        progressBar.value = progress;
        progressText.SetText((progress * 100).ToString("F1"));
    }
}
