using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : GameManager
{
    [Header("Level Manager")]
    public float animationDelay = 0.15f;
    public List<WorldCube> cubes = new List<WorldCube>();

    new private void Start()
    {
        base.Start();
        cubes = FindObjectsOfType<WorldCube>().ToList();
        //order cubes
        cubes = cubes.OrderBy(x => x.transform.localPosition.x).ThenBy(x => x.transform.position.y).ToList();
    }

    new private void Update()
    {
        base.Update();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 25), "WATER"))
        {
            ChangeElement(Element.WATER);
        }
        if (GUI.Button(new Rect(0, 25, 100, 25), "FIRE"))
        {
            ChangeElement(Element.FIRE);
        }
        if (GUI.Button(new Rect(0, 50, 100, 25), "WIND"))
        {
            ChangeElement(Element.WIND);
        }
        if (GUI.Button(new Rect(0, 75, 100, 25), "EARTH"))
        {
            ChangeElement(Element.EARTH);
        }
    }

    public void ChangeElement(Element element)
    {
        WorldElement = element;
        StartCoroutine(DynamicAnimateCubes(WorldElement));
    }

    IEnumerator DynamicAnimateCubes(Element element)
    {
        int count = 0;
        while (count < cubes.Count)
        {
            yield return new WaitForSeconds(animationDelay);
            cubes[count].Element = element;
            count++;
        }
    }
}
