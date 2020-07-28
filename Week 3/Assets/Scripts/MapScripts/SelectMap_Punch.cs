using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap_Punch : MonoBehaviour
{
    public AudioSource audioSource;
    public void OnClickForestBtn()
    {
        SceneManager.LoadScene("Forest");
        audioSource.Play();
        Debug.Log("BTN_FOREST");
    }

    public void OnClickTownBtn()
    {
        SceneManager.LoadScene("Town");
        audioSource.Play();
        Debug.Log("BTN_FOREST");
    }

    public void OnClickMatrixBtn()
    {
        SceneManager.LoadScene("Matrix");
        audioSource.Play();
        Debug.Log("BTN_FOREST");
    }

    public void clickAudio()
    {
        audioSource.Play();
    }

    

}
