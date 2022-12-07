using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerLayer;
    public GameObject tabMenuList1, tabMenuListFolder1;
    public GameObject tabMenuList2, tabMenuListFolder2;

    [Header("rules")]
    public int minutes;
    public float secodnds = 0;
    public Text minutesUi;

    [Header("players")]
    public int playerAmount = 5;
    public GameObject AiPlayer;
    public GameObject spawnPoints;
    private GameObject[] spawnPointsArray;
    private GameObject[] aiPlayers;
    public GameObject PlayerGameObject;
    public GameObject playerSpawnPosition;

    [Header("ui panels")]
    public GameObject playerUI;
    public GameObject pauseUI;
    public GameObject tabUI;

    public GameObject AiContainer;

    public bool paused = false, pauseFlag;

    private void Awake()
    {
        secodnds = minutes * 60;
        spawnPointsArray = new GameObject[spawnPoints.transform.childCount];
        aiPlayers = new GameObject[spawnPoints.transform.childCount];

        for (int i = 0; i < spawnPointsArray.Length; i++)
        {
            spawnPointsArray[i] = spawnPoints.transform.GetChild(i).gameObject;
        }

        PlayerLayer = GameObject.FindGameObjectWithTag("Player").transform.gameObject;

        pauseGame();
        resumeGame();
        createPauseUI();
        createTabUI();
        spawnAiPlayers();
    }

    void FixedUpdate()
    {
        if (PlayerLayer == null) PlayerLayer = FindObjectOfType<PlayerController>().transform.gameObject;

        secodnds -= Time.deltaTime;

        minutes = (int)secodnds / 60;

        minutesUi.text = minutes.ToString("0") + ":" + (secodnds % 60).ToString("0");

        if (secodnds <= 0) endGame();

        if (Input.GetKeyDown(KeyCode.Escape)) pauseGame();
        if (Input.GetKey(KeyCode.Tab)) tabUI.SetActive(true);
        else tabUI.SetActive(false);
    }

    public void endGame()
    {
        for (int i = 0; i < aiPlayers.Length; i++)
        {
            Destroy(aiPlayers[i]);
        }
    }

    private void pauseGame()
    {
        //pauseUI.transform.localScale = Vector3.Lerp(pauseUI.transform.localScale, new Vector3(1, 1, 1), lerpTime * Time.deltaTime);
        if (pauseFlag) return;
        PlayerLayer.transform.gameObject.GetComponent<InputManager>().enabled = false;
        playerUI.SetActive(false);
        pauseUI.SetActive(true);
        pauseFlag = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        //pauseUI.transform.localScale = Vector3.Lerp(pauseUI.transform.localScale, new Vector3(0, 0, 0), lerpTime * Time.deltaTime);
        if (!pauseFlag) return;
        Time.timeScale = 1;
        PlayerLayer.transform.gameObject.GetComponent<InputManager>().enabled = true;
        playerUI.SetActive(true);
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseFlag = false;
    }

    private void createPauseUI()
    {
        float yPosition = 105;
        for (int i = 0; i < 5; i++)
        {
            GameObject sample = Instantiate(tabMenuList1);
            sample.transform.parent = tabMenuListFolder1.transform;
            sample.transform.localScale = new Vector3(1, 1, 1);
            sample.transform.localPosition = new Vector3(0, yPosition, 0);
            sample.transform.gameObject.GetComponent<TabListPrefab>().setUpPlayerList("player" + i, i * 4, i * 2);
            yPosition -= 52;
        }
    }    

    private void createTabUI()
    {
        float yPosition = 105;
        for (int i = 0; i < 5; i++)
        {
            GameObject sample = Instantiate(tabMenuList2);
            sample.transform.parent = tabMenuListFolder2.transform;
            sample.transform.localScale = new Vector3(1, 1, 1);
            sample.transform.localPosition = new Vector3(0, yPosition, 0);
            sample.transform.gameObject.GetComponent<TabListPrefab>().setUpPlayerList("player" + i, i * 4, i * 2);
            yPosition -= 52;
        }
    }

    private void spawnAiPlayers()
    {
        for (int i = 0; i < playerAmount; i++)
        {
            GameObject player = Instantiate(AiPlayer, spawnPointsArray[i].transform.position, Quaternion.identity);
            aiPlayers[i] = player;
            player.transform.parent = AiContainer.transform;
        }
    }

    public void deadPlayer(GameObject g)
    {
        if (g.tag == "AI")
        {
            GameObject r = Instantiate(AiPlayer, spawnPointsArray[Random.Range(0, 8)].transform.position, Quaternion.identity);
            r.transform.parent = AiContainer.transform;
            Destroy(g);
        }
        else
        {
            GameObject r = Instantiate(PlayerGameObject, playerSpawnPosition.transform.position, Quaternion.identity);
            Destroy(g);
        }
    }
}
