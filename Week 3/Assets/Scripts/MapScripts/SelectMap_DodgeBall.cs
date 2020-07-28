using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap_DodgeBall : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnClickMatrixBtn()
    {
        SceneManager.LoadScene("Matrix_DodgeBall");
        audioSource.Play();
    }

    public void clickAudio()
    {
        audioSource.Play();
    }

    

}
