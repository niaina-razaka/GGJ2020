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

    protected Player playerInstance;

    // Start is called before the first frame update
    protected void Start()
    {
        //init player
        playerInstance = Instantiate(playerPrefab);
        playerInstance.name = "HAFA";
        playerInstance.transform.position = spawnPlayer.position;
        cinemachine.Follow = playerInstance.transform;
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
}
