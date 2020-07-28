using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    int REDPOINT;
    int BLUEPOINT;
    public GameObject sub;
    public GameObject redWin;
    public GameObject blueWin;
    public Text Red;
    public Text Blue;
    bool activeCheck;
    void start()
    {
        sub.SetActive(false);
        blueWin.SetActive(false);
        redWin.SetActive(false);
        activeCheck = false;
        Red.text = "0";
        Blue.text = "0";
        REDPOINT = 0;
        BLUEPOINT = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("키가 눌렸습니다.");
            if(!activeCheck)
            {
                sub.SetActive(true);
                activeCheck = true;
            }
            else
            {
                sub.SetActive(false);
                activeCheck = false;
            }
        }

        if(BLUEPOINT == 3)
        {
            blueWin.SetActive(true);
        }

        if(REDPOINT == 3)
        {
            redWin.SetActive(true);
        }
    }

    public void OnClickContinue()
    {
        if(activeCheck)
        {
        sub.SetActive(false);
        activeCheck = false;
        }
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Game Menu");
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void setRedPoint(int redPoint)
    {
        Red.text = redPoint.ToString();
        REDPOINT++;
    }

    public void setBluePoint(int bluePoint)
    {
        Blue.text = bluePoint.ToString();
        BLUEPOINT++;
    }
}
