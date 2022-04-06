using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public SceneFader sceneFaderPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator LoadScene(string scene)
    {
        if (scene == "") yield break;

        SceneFader fader = Instantiate(sceneFaderPrefab);

        yield return StartCoroutine(fader.FadeOut(2.5f));
        yield return SceneManager.LoadSceneAsync(scene);

        yield return StartCoroutine(fader.FadeIn(2.5f));
    }

    public void TransitionToBattleScene()
    {
        StartCoroutine(LoadScene("Battle"));
    }
}
