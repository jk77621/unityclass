using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerLayer;
    public GameObject pauseMenuList, pauseMenuListFolder;
    public GameObject tabMenuList, tabMenuListFolder;
    public GameObject kilroggMenuList, kilroggMenuListFolder;

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
        spawnAiPlayers();
        createPauseUI();
        createTabUI();
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
        GameObject sample1 = Instantiate(pauseMenuList);
        PlayerController pController = PlayerGameObject.GetComponent<PlayerController>();
        sample1.transform.parent = pauseMenuListFolder.transform;
        sample1.transform.localScale = new Vector3(1, 1, 1);
        sample1.transform.localPosition = new Vector3(0, yPosition, 0);
        sample1.transform.gameObject.GetComponent<TabListPrefab>().setUpPlayerList(pController.playerName, 0, 0);

        for (int i = 0; i < playerAmount; i++)
        {
            GameObject sample2 = Instantiate(pauseMenuList);
            AiController aController = aiPlayers[i].GetComponent<AiController>();
            sample2.transform.parent = pauseMenuListFolder.transform;
            sample2.transform.localScale = new Vector3(1, 1, 1);
            sample2.transform.localPosition = new Vector3(0, yPosition - (52 * (i + 1)), 0);
            sample2.transform.gameObject.GetComponent<TabListPrefab>().setUpPlayerList(aController.playerName, 0, 0);
        }
    }

    private void createTabUI()
    {
        float yPosition = 105;
        GameObject sample1 = Instantiate(tabMenuList);
        PlayerController pController = PlayerGameObject.GetComponent<PlayerController>();
        sample1.transform.parent = tabMenuListFolder.transform;
        sample1.transform.localScale = new Vector3(1, 1, 1);
        sample1.transform.localPosition = new Vector3(0, yPosition, 0);
        sample1.transform.gameObject.GetComponent<TabListPrefab>().setUpPlayerList(pController.playerName, 0, 0);

        for (int i = 0; i < playerAmount; i++)
        {
            GameObject sample2 = Instantiate(tabMenuList);
            AiController aController = aiPlayers[i].GetComponent<AiController>();
            sample2.transform.parent = tabMenuListFolder.transform;
            sample2.transform.localScale = new Vector3(1, 1, 1);
            sample2.transform.localPosition = new Vector3(0, yPosition - (52 * (i + 1)), 0);
            sample2.transform.gameObject.GetComponent<TabListPrefab>().setUpPlayerList(aController.playerName, 0, 0);
        }
    }

    private void spawnAiPlayers()
    {
        for (int i = 0; i < playerAmount; i++)
        {
            GameObject player = Instantiate(AiPlayer, spawnPointsArray[i].transform.position, Quaternion.identity);
            AiController contoller = player.GetComponent<AiController>();
            player.transform.parent = AiContainer.transform;
            contoller.playerName = "Ai" + i;
            aiPlayers[i] = player;
        }
    }

    public void deadPlayer(GameObject shooter, GameObject hitPerson)
    {
        string shooterName;
        string hitPersonName;

        if (shooter.tag == "AI")
        {
            AiController controller = shooter.GetComponent<AiController>();
            shooterName = controller.playerName;
        }
        else
        {
            PlayerController controller = shooter.GetComponent<PlayerController>();
            shooterName = controller.playerName;
        }



        if (hitPerson.tag == "AI")
        {
            AiController controller1 = hitPerson.GetComponent<AiController>();
            hitPersonName = controller1.playerName;

            GameObject player = Instantiate(AiPlayer, spawnPointsArray[Random.Range(0, 8)].transform.position, Quaternion.identity);
            AiController contoller2 = player.GetComponent<AiController>();
            player.transform.parent = AiContainer.transform;
            contoller2.playerName = controller1.playerName;
        }
        else
        {
            PlayerController controller1 = hitPerson.GetComponent<PlayerController>();
            hitPersonName = controller1.playerName;

            GameObject player = Instantiate(PlayerGameObject, playerSpawnPosition.transform.position, Quaternion.identity);
            PlayerController contoller2 = player.GetComponent<PlayerController>();
            contoller2.playerName = controller1.playerName;
        }


        Debug.Log(shooterName + " // " + hitPersonName);

        setUpKillDeath(shooterName, hitPersonName);
        StartCoroutine(createKilroggUI(shooterName, hitPersonName));

        Destroy(hitPerson);
    }

    IEnumerator createKilroggUI(string shooterName, string hitPersonName)
    {
        float yPosition = 0;
        GameObject sample1 = Instantiate(kilroggMenuList);
        sample1.transform.parent = kilroggMenuListFolder.transform;
        sample1.transform.localScale = new Vector3(1, 1, 1);
        sample1.transform.localPosition = new Vector3(0, yPosition, 0);
        sample1.transform.gameObject.GetComponent<KilroggListPrefab>().setUpKilroggList(shooterName, hitPersonName);

        int j = kilroggMenuListFolder.transform.childCount - 1;
        for (int i = 0; i < kilroggMenuListFolder.transform.childCount; i++)
        {
            GameObject sample2 = kilroggMenuListFolder.transform.GetChild(i).gameObject;
            sample2.transform.localPosition = new Vector3(0, yPosition - (32 * j), 0);
            j--;
        }

        yield return new WaitForSeconds(2.0f);

        Destroy(sample1);
    }


    private void setUpKillDeath(string shooterName, string hitPersonName)
    {
        for (int i = 0; i < pauseMenuListFolder.transform.childCount; i++)
        {
            GameObject sample = pauseMenuListFolder.transform.GetChild(i).gameObject;
            if (sample.GetComponent<TabListPrefab>().playerName.text == shooterName)
                sample.GetComponent<TabListPrefab>().setUpKill();
            if (sample.GetComponent<TabListPrefab>().playerName.text == hitPersonName)
                sample.GetComponent<TabListPrefab>().setUpDeath();
        }

        for (int i = 0; i < tabMenuListFolder.transform.childCount; i++)
        {
            GameObject sample = tabMenuListFolder.transform.GetChild(i).gameObject;
            if (sample.GetComponent<TabListPrefab>().playerName.text == shooterName)
                sample.GetComponent<TabListPrefab>().setUpKill();
            if (sample.GetComponent<TabListPrefab>().playerName.text == hitPersonName)
                sample.GetComponent<TabListPrefab>().setUpDeath();
        }
    }

}
