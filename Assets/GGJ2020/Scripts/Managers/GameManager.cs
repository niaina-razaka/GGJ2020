using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum HumanPart
    {
        BONE,
        BLOOD_VESSEL,
        BRAIN,
        HEART
    }

    public static GameManager Instance { get; private set; }

    [Header("Game Manager")]
    public HumanPart humanPart = HumanPart.BONE;
    public Player playerPrefab;
    public Transform spawnPlayer;
    public CinemachineVirtualCamera cinemachine;
    public UIManager ui;
    [Range(1,100)]
    public int enemySpawnPercentage = 100;
    public Transform bg_heart;
    public Transform bg_blood_vessel;
    public Transform bg_bone;
    public Transform bg_brain;
    [HideInInspector] public List<WorldCube> cubes = new List<WorldCube>();
    [HideInInspector] public List<AI> inGameAI = new List<AI>();
    public Image imgLoadScene;

    public AI boss = null;

    public int targetEnemyKilled = 2;
    public int targetEnemyKilledIncremetation = 10;
    public int currentEnemyKilled = 0;

    protected Player playerInstance;

    // Start is called before the first frame update
    protected void Start()
    {
        imgLoadScene.gameObject.SetActive(false);
        Instance = this;
        //init player
        playerInstance = Instantiate(playerPrefab);
        playerInstance.name = "HAFA";
        playerInstance.transform.position = spawnPlayer.position;
        cinemachine.Follow = playerInstance.transform;
        ui.player = playerInstance.gameObject;
        //init background
        ChangeElement(humanPart);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //click action
        }
    }

    protected void LateUpdate()
    {
        
    }

    public void EndGame()
    {
        Debug.Log("END GAME");
        AudioManager.Instance.GameOver();
        AudioManager.Instance.PlaySound("game over");
        Time.timeScale = 1F;
        StartCoroutine(ToMainMenu());
    }

    IEnumerator ToMainMenu()
    {
        imgLoadScene.gameObject.SetActive(true);
        Color transparent = Color.black;
        transparent.a = 0;
        imgLoadScene.color = transparent;
        float t = 0;
        while (t <= 5)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            imgLoadScene.color = Color.Lerp(transparent, Color.black, t/5);
        }
        SceneManager.LoadScene(0);
    }
    /*
    protected void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 110, 25), "BONE"))
        {
            ChangeElement(HumanPart.BONE);
        }
        if (GUI.Button(new Rect(0, 25, 110, 25), "BLOOD_VESSEL"))
        {
            ChangeElement(HumanPart.BLOOD_VESSEL);
        }
        if (GUI.Button(new Rect(0, 50, 110, 25), "BRAIN"))
        {
            ChangeElement(HumanPart.BRAIN);
        }
        if (GUI.Button(new Rect(0, 75, 110, 25), "HEART"))
        {
            ChangeElement(HumanPart.HEART);
        }
    }
    //*/

    public void ChangeElement(HumanPart element)
    {
        humanPart = element;
        bg_blood_vessel.gameObject.SetActive(humanPart == HumanPart.BLOOD_VESSEL);
        bg_bone.gameObject.SetActive(humanPart == HumanPart.BONE);
        bg_brain.gameObject.SetActive(humanPart == HumanPart.BRAIN);
        bg_heart.gameObject.SetActive(humanPart == HumanPart.HEART);
    }

    internal void KillEnemy(AI target)
    {
        LevelManager lvl = (LevelManager)GameManager.Instance;
        if (boss != null && target == boss)
        {
            AudioManager.Instance.SwitchToLevel();
            lvl.destroyFarBlocks = true;
            lvl.blockBossEnd.SetActive(false);
            targetEnemyKilled += targetEnemyKilledIncremetation;
            //Debug.LogWarning("Animation interface boss killed");
            UIManager.Instance.bossLifeBar.gameObject.SetActive(false);
            switch (humanPart)
            {
                case HumanPart.BRAIN:
                    ChangeElement(HumanPart.BLOOD_VESSEL);
                    break;
                case HumanPart.BLOOD_VESSEL:
                    ChangeElement(HumanPart.BONE);
                    break;
                case HumanPart.BONE:
                    ChangeElement(HumanPart.HEART);
                    break;
                case HumanPart.HEART:
                    StartCoroutine(ToMainMenu());
                    break;
            }
        }
        else
        {
            currentEnemyKilled++;
            inGameAI.Remove(target);
            if (currentEnemyKilled >= targetEnemyKilled)
            {
                currentEnemyKilled = 0;
                AudioManager.Instance.SwitchToBoss();
                lvl.PopBossTerrain();
                foreach (AI ai in inGameAI)
                {
                    Destroy(ai.gameObject);
                }
                inGameAI.Clear();
            }
        }
    }
}
