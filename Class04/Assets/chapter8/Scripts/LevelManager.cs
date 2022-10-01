using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //싱글톤
    public static LevelManager instance;

    private int currentPoints = 0;
    private int highscore;

    public TMP_Text pointsText;

    public AudioSource pointAudio;
    public AudioSource specialAudio;
    public GameObject endMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            highscore = PlayerPrefs.GetInt("Highscore");

            GetAudioPreferences();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void GetAudioPreferences()
    {
        //지정된 사운드 볼륨 체크
        float appVolume = PlayerPrefs.GetFloat("Volume");
        appVolume = appVolume == 0f ? 0.5f : appVolume;

        specialAudio.volume = appVolume;
        pointAudio.volume = appVolume;
    }

    public void UpdatePoints()
    {
        currentPoints++;
        pointsText.text = currentPoints.ToString();

        //사운드
        if (currentPoints % 10 == 0)
        {
            specialAudio.Play();
        }
        else
        {
            pointAudio.Play();
        }
    }

    public void UpdateHighscore()
    {
        //갱신
        if (currentPoints > highscore)
        {
            highscore = currentPoints;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
    }

    public void OpenEndMenu()
    {
        //정지
        Time.timeScale = 0;

        //최고 점수 갱신
        UpdateHighscore();
        endMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
