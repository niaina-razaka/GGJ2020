using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCube : MonoBehaviour
{
    public GameObject WATER;
    public GameObject FIRE;
    public GameObject WIND;
    public GameObject EARTH;

    private float duration = 0.25f;

    public GameManager.Element Element {
        get => _element;
        set
        {
            lastElement = _element;
            _element = value;
            if(Application.isPlaying)
                StartCoroutine(Animate());
        }
    }
    private GameManager.Element _element = GameManager.Element.EARTH;
    private GameManager.Element lastElement;

    // Start is called before the first frame update
    void Start()
    {
        Element = GameManager.Element.EARTH;
    }

    IEnumerator Animate()
    {
        WATER.SetActive(false);
        FIRE.SetActive(false);
        WIND.SetActive(false);
        EARTH.SetActive(false);
        switch (lastElement)
        {
            case GameManager.Element.EARTH:
                EARTH.SetActive(true);
                Scale(EARTH, 1);
                break;
            case GameManager.Element.FIRE:
                FIRE.SetActive(true);
                Scale(FIRE, 1);
                break;
            case GameManager.Element.WATER:
                WATER.SetActive(true);
                Scale(WATER, 1);
                break;
            case GameManager.Element.WIND:
                WIND.SetActive(true);
                Scale(WIND, 1);
                break;
        }
        switch (_element)
        {
            case GameManager.Element.EARTH:
                EARTH.SetActive(true);
                Scale(EARTH, 0);
                break;
            case GameManager.Element.FIRE:
                FIRE.SetActive(true);
                Scale(FIRE, 0);
                break;
            case GameManager.Element.WATER:
                WATER.SetActive(true);
                Scale(WATER, 0);
                break;
            case GameManager.Element.WIND:
                WIND.SetActive(true);
                Scale(WIND, 0);
                break;
        }
        float t = 0;
        //animate last
        while (t <= duration)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            switch (lastElement)
            {
                case GameManager.Element.EARTH:
                    Scale(EARTH, 1 - (t / duration));
                    break;
                case GameManager.Element.FIRE:
                    Scale(FIRE, 1 - (t / duration));
                    break;
                case GameManager.Element.WATER:
                    Scale(WATER, 1 - (t / duration));
                    break;
                case GameManager.Element.WIND:
                    Scale(WIND, 1 - (t / duration));
                    break;
            }
        }
        switch (lastElement)
        {
            case GameManager.Element.EARTH:
                Scale(EARTH, 0);
                break;
            case GameManager.Element.FIRE:
                Scale(FIRE, 0);
                break;
            case GameManager.Element.WATER:
                Scale(WATER, 0);
                break;
            case GameManager.Element.WIND:
                Scale(WIND, 0);
                break;
        }
        t = 0;
        //animate current
        while (t <= duration)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            switch (_element)
            {
                case GameManager.Element.EARTH:
                    Scale(EARTH, t / duration);
                    break;
                case GameManager.Element.FIRE:
                    Scale(FIRE, t / duration);
                    break;
                case GameManager.Element.WATER:
                    Scale(WATER, t / duration);
                    break;
                case GameManager.Element.WIND:
                    Scale(WIND, t / duration);
                    break;
            }
        }
        switch (_element)
        {
            case GameManager.Element.EARTH:
                Scale(EARTH, 1);
                break;
            case GameManager.Element.FIRE:
                Scale(FIRE, 1);
                break;
            case GameManager.Element.WATER:
                Scale(WATER, 1);
                break;
            case GameManager.Element.WIND:
                Scale(WIND, 1);
                break;
        }
    }

    private void Scale(GameObject target, float size)
    {
        target.transform.localScale = new Vector3(size, size, 1);
    }
}
