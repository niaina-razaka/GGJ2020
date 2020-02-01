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

    // Start is called before the first frame update
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
