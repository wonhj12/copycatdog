using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject control;

    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void Control()
    {
        control.SetActive(!control.activeSelf);
    }
}
