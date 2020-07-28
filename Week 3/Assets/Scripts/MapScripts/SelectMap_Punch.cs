﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap_Punch : MonoBehaviour
{
    public AudioSource audioSource;
    public void OnClickForestBtn()
    {
        SceneManager.LoadScene("Forest_Score");
        audioSource.Play();
    }

    public void OnClickTownBtn()
    {
        SceneManager.LoadScene("Town_Score");
        audioSource.Play();
    }

    public void OnClickMatrixBtn()
    {
        SceneManager.LoadScene("Matrix_Score");
        audioSource.Play();
    }

    public void clickAudio()
    {
        audioSource.Play();
    }

    

}
