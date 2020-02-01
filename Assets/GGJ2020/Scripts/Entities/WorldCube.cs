using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCube : MonoBehaviour
{
    public GameObject BONE;
    public GameObject BLOOD_VESSEL;
    public GameObject BRAIN;
    public GameObject HEART;

    private float duration = 0.25f;

    public GameManager.HumanPart Element {
        get => _element;
        set
        {
            lastElement = _element;
            _element = value;
            /*BONE.SetActive(_element == GameManager.HumanPart.BONE);
            BLOOD_VESSEL.SetActive(_element == GameManager.HumanPart.BLOOD_VESSEL);
            BRAIN.SetActive(_element == GameManager.HumanPart.BRAIN);
            HEART.SetActive(_element == GameManager.HumanPart.HEART);//*/
        }
    }
    private GameManager.HumanPart _element;
    private GameManager.HumanPart lastElement;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        //yield return new WaitForSeconds(1);
        BONE.SetActive(false);
        BLOOD_VESSEL.SetActive(false);
        BRAIN.SetActive(false);
        HEART.SetActive(false);
        switch (lastElement)
        {
            case GameManager.HumanPart.HEART:
                HEART.SetActive(true);
                Scale(HEART, 1);
                break;
            case GameManager.HumanPart.BLOOD_VESSEL:
                BLOOD_VESSEL.SetActive(true);
                Scale(BLOOD_VESSEL, 1);
                break;
            case GameManager.HumanPart.BONE:
                BONE.SetActive(true);
                Scale(BONE, 1);
                break;
            case GameManager.HumanPart.BRAIN:
                BRAIN.SetActive(true);
                Scale(BRAIN, 1);
                break;
        }
        switch (_element)
        {
            case GameManager.HumanPart.HEART:
                HEART.SetActive(true);
                Scale(HEART, 0);
                break;
            case GameManager.HumanPart.BLOOD_VESSEL:
                BLOOD_VESSEL.SetActive(true);
                Scale(BLOOD_VESSEL, 0);
                break;
            case GameManager.HumanPart.BONE:
                BONE.SetActive(true);
                Scale(BONE, 0);
                break;
            case GameManager.HumanPart.BRAIN:
                BRAIN.SetActive(true);
                Scale(BRAIN, 0);
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
                case GameManager.HumanPart.HEART:
                    Scale(HEART, 1 - (t / duration));
                    break;
                case GameManager.HumanPart.BLOOD_VESSEL:
                    Scale(BLOOD_VESSEL, 1 - (t / duration));
                    break;
                case GameManager.HumanPart.BONE:
                    Scale(BONE, 1 - (t / duration));
                    break;
                case GameManager.HumanPart.BRAIN:
                    Scale(BRAIN, 1 - (t / duration));
                    break;
            }
        }
        switch (lastElement)
        {
            case GameManager.HumanPart.HEART:
                Scale(HEART, 0);
                break;
            case GameManager.HumanPart.BLOOD_VESSEL:
                Scale(BLOOD_VESSEL, 0);
                break;
            case GameManager.HumanPart.BONE:
                Scale(BONE, 0);
                break;
            case GameManager.HumanPart.BRAIN:
                Scale(BRAIN, 0);
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
                case GameManager.HumanPart.HEART:
                    Scale(HEART, t / duration);
                    break;
                case GameManager.HumanPart.BLOOD_VESSEL:
                    Scale(BLOOD_VESSEL, t / duration);
                    break;
                case GameManager.HumanPart.BONE:
                    Scale(BONE, t / duration);
                    break;
                case GameManager.HumanPart.BRAIN:
                    Scale(BRAIN, t / duration);
                    break;
            }
        }
        switch (_element)
        {
            case GameManager.HumanPart.HEART:
                Scale(HEART, 1);
                break;
            case GameManager.HumanPart.BLOOD_VESSEL:
                Scale(BLOOD_VESSEL, 1);
                break;
            case GameManager.HumanPart.BONE:
                Scale(BONE, 1);
                break;
            case GameManager.HumanPart.BRAIN:
                Scale(BRAIN, 1);
                break;
        }
    }

    private void Scale(GameObject target, float size)
    {
        target.transform.localScale = new Vector3(size, size, 1);
    }
}
