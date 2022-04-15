using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Main : MonoBehaviour
{
    PlayableDirector director;

    void Awake()
    {
        director = FindObjectOfType<PlayableDirector>();
        director.stopped += EnterBattleScene;
    }

    void Update()
    {

    }

    public void PlayTimeLine()
    {
        director.Play();
    }

    void EnterBattleScene(PlayableDirector obj)
    {
        SceneController.instance.TransitionToBattleScene();
    }

}
