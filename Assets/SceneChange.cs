﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int scene;

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }

}
