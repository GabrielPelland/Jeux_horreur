﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public Image image;
    public Image image2;

    public void Restart()
    {
        SceneManager.LoadScene("Niveau_emile_hehe");
    }
}
