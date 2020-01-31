using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum Element
    {
        WATER,
        FIRE,
        WIND,
        EARTH
    }

    [Header("Game Manager")]
    public Element WorldElement = Element.WATER;

    // Start is called before the first frame update
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
