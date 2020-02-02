using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public List<GameObject> listOfHearth;
    public GameObject hearthPrefab;
    public GameObject hearthPart;
    public Image bossLifeBar;

    private int nbHearth = 0;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CreateHearthList();
        bossLifeBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        VerifyHearthPlayer();
    }
    void CreateHearthList()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            nbHearth = player.GetComponent<PlayerController>().Life;
            int x = 70;
            for (int i = 0; i < nbHearth; i++)
            {
                GameObject clone = Instantiate(hearthPrefab, hearthPart.transform);
                if (i != 0)
                {
                    Vector3 vector = new Vector3(clone.GetComponent<RectTransform>().position.x + x, clone.GetComponent<RectTransform>().position.y, clone.GetComponent<RectTransform>().position.z);
                    clone.GetComponent<RectTransform>().position = vector;
                    x += 70;
                }
                listOfHearth.Add(clone);

            }
        }
       
    }
    void VerifyHearthPlayer()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        int hearthPlayer = player.GetComponent<PlayerController>().Life;
        if (hearthPlayer != nbHearth)
        {
            ResetList();
            CreateHearthList();
        }
    }
    void ResetList()
    {
        // listOfHearth.RemoveRange(0,listOfHearth.Count-1);
        for (int n = 0; n < listOfHearth.Count; n++)
        {
            Destroy(listOfHearth[n]);
        }
    }
}
