using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Main : MonoBehaviour
{
    PlayableDirector director;
    bool isPlayingTimeline = false;

    void Awake()
    {
        director = FindObjectOfType<PlayableDirector>();
        director.stopped += EnterBattleScene;
        director.stopped += director => isPlayingTimeline = false;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return) && !isPlayingTimeline)
        //{
        //    PlayTimeLine();
        //    isPlayingTimeline = true;
        //}
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
