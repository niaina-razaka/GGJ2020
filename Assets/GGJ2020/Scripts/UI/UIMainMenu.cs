using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Menu")]
    public string initialScene;
    public Button btnPlay;
    public Image progressBar;

    [Header("Story")]
    public Text lblStory;
    public Image imgStory;
    public Button btnNextStory;
    public float storyDelay = 1.5f;
    public Story[] stories;

    private int storyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(Play);
        btnNextStory.onClick.AddListener(LoadNextStory);
        progressBar.gameObject.SetActive(false);
        storyIndex = -1;
        LoadNextStory();
    }

    private void Play()
    {
        btnPlay.gameObject.SetActive(false);
        StartCoroutine(LoadAscynchronosly(initialScene));
    }

    IEnumerator LoadAscynchronosly(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.fillAmount = operation.progress;
            yield return null;
        }
    }

    IEnumerator AnimateStory()
    {
        float t_delay = 0;
        lblStory.text = stories[storyIndex].text;
        imgStory.sprite = stories[storyIndex].sprite;
        Color transparant = new Color(255, 255, 255, 0);
        lblStory.color = transparant;
        imgStory.color = transparant;
        while (t_delay <= storyDelay)
        {
            t_delay += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            lblStory.color = Color32.Lerp(transparant, Color.white, t_delay/storyDelay);
            imgStory.color = Color32.Lerp(transparant, Color.white, t_delay / storyDelay);
        }
        lblStory.color = Color.white;
        imgStory.color = Color.white;
    }

    private void LoadNextStory()
    {
        storyIndex++;
        storyIndex = Mathf.Clamp(storyIndex, 0, stories.Length-1);
        StartCoroutine(AnimateStory());
    }

    [Serializable]
    public class Story
    {
        public Sprite sprite;
        public string text;
    }
}
