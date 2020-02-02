using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public enum HumanPart
    {
        BONE,
        BLOOD_VESSEL,
        BRAIN,
        HEART
    }

    [Header("Game Manager")]
    public HumanPart humanPart = HumanPart.BONE;
    public Player playerPrefab;
    public Transform spawnPlayer;
    public CinemachineVirtualCamera cinemachine;
    [Range(1,100)]
    public int enemySpawnPercentage = 100;
    public Transform bg_heart;
    public Transform bg_blood_vessel;
    public Transform bg_bone;
    public Transform bg_brain;
    [HideInInspector] public List<WorldCube> cubes = new List<WorldCube>();
    [HideInInspector] public List<AI> inGameAI = new List<AI>();

    protected Player playerInstance;

    // Start is called before the first frame update
    protected void Start()
    {
        //init player
        playerInstance = Instantiate(playerPrefab);
        playerInstance.name = "HAFA";
        playerInstance.transform.position = spawnPlayer.position;
        cinemachine.Follow = playerInstance.transform;

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
    }

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

    public void ChangeElement(HumanPart element)
    {
        humanPart = element;
        bg_blood_vessel.gameObject.SetActive(humanPart == HumanPart.BLOOD_VESSEL);
        bg_bone.gameObject.SetActive(humanPart == HumanPart.BONE);
        bg_brain.gameObject.SetActive(humanPart == HumanPart.BRAIN);
        bg_heart.gameObject.SetActive(humanPart == HumanPart.HEART);
    }
}
