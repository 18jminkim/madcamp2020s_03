using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap_Anounymous : MonoBehaviour
{
    public AudioSource audioSource;
    public void OnClickForestBtn()
    {
        SceneManager.LoadScene("Forest_Anounymous");
        audioSource.Play();
    }

    public void OnClickTownBtn()
    {
        SceneManager.LoadScene("Town_Anounymous");
        audioSource.Play();
    }

    public void OnClickMatrixBtn()
    {
        SceneManager.LoadScene("Matrix_Anounymous");
        audioSource.Play();
    }

    public void clickAudio()
    {
        audioSource.Play();
    }

    

}
