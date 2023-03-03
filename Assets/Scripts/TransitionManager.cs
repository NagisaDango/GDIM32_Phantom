using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    public int startScene;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool isFade;
    private bool canTransition;

    private void OnEnable()
    {
        //EventHandler.StartGameEvent += OnStartNewGameEvent;
    }


    private void OnDisable()
    {
        //EventHandler.StartGameEvent -= OnStartNewGameEvent;
    }


    private void OnStartNewGameEvent(int obj)
    {
        StartCoroutine(TransitionToScene(0, 1));
    }

    public void Transition(int from, int to)
    {
        if (!isFade && canTransition)
            StartCoroutine(TransitionToScene(from, to));
    }


    private IEnumerator TransitionToScene(int from, int to)
    {
        //yield return Fade(1);

        //yield return SceneManager.UnloadSceneAsync(from);

        yield return SceneManager.LoadSceneAsync(to);

        //设置新场景为激活场景
        //Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        //SceneManager.SetActiveScene(newScene);

        //yield return Fade(0);
    }


    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }

}
