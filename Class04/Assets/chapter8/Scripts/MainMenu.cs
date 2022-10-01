using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text hightscoreCount;

    // Start is called before the first frame update
    void Awake()
    {
        //Playerprefs는 유니티 내부 저장 데이터
        int highscore = PlayerPrefs.GetInt("Highscore");
        //int highscore = 0;
        hightscoreCount.text = highscore.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
    }
}
