using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public SceneFader sceneFaderPrefab;


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
