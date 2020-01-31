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

    private void LoadNextStory()
    {
        storyIndex++;
        storyIndex = Mathf.Clamp(storyIndex, 0, stories.Length-1);
        lblStory.text = stories[storyIndex].text;
        imgStory.sprite = stories[storyIndex].sprite;
    }

    [Serializable]
    public class Story
    {
        public Sprite sprite;
        public string text;
    }
}
