using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BackButton : MonoBehaviour
{
    public enum BackAction
    {
        toMainMenu,
        toGallery,
        toQuit
    }
    public BackAction DoAction;
    private ButtonLoading buttonLoad;
    private void Start()
    {
        buttonLoad = GetComponent<ButtonLoading>();
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            switch (DoAction)
            {
                case BackAction.toGallery:
                    buttonLoad.LoadGalleryScene();
                    break;
                case BackAction.toMainMenu:
                    buttonLoad.LoadMainScene();
                    break;
                case BackAction.toQuit:
                    Application.Quit();
                    break;

            }
        }
    }
}
