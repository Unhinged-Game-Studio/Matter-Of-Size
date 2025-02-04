using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAnimations : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] windows;
    [SerializeField] CanvasGroup matter;
    [SerializeField] CanvasGroup of;
    [SerializeField] CanvasGroup size;
    [SerializeField] Boolean mainMenu;
    private bool transitioning = false;

    private enum windowType
    {
        TITLE,
        MARKSR,
        MARKSL,
        PLAYBUTTON,
        OPTIONSBUTTON,
        CREDITSBUTTON,
        CREDITS,
        OPTIONS,
        TUTORIAL,
        CREDITSBACKBUTTON,
        OPTIONSBACKBUTTON,
        PLAYBLOCK
    };

    void Start()
    {
        if (mainMenu)
        {
            hideEverthing();
            titleAnimation();
            buttonsAnimation();
            showInstructions();
        }
    }

    void hideEverthing()
    {
        if (mainMenu)
        {
            for (int i = 0; i < 12; i++)
                windows[i].SetActive(false);
            windows[(int)windowType.OPTIONS].GetComponent<CanvasGroup>().LeanAlpha(0f, 0f);
            windows[(int)windowType.CREDITS].GetComponent<CanvasGroup>().LeanAlpha(0f, 0f);
            windows[(int)windowType.TUTORIAL].GetComponent<CanvasGroup>().LeanAlpha(0f, 0f);
        }
    }

    void titleAnimation()
    {
        windows[(int)windowType.TITLE].SetActive(true);
        LeanTween.moveLocal(windows[(int)windowType.TITLE], new Vector3(0f, 140f, 0f), 0.7f).setEase(LeanTweenType.easeInCirc);
        LeanTween.scale(windows[(int)windowType.TITLE], new Vector3(1.0f, 1.0f, 1.0f), 1f).setDelay(1f).setEase(LeanTweenType.easeInCirc).setOnComplete(marksAnimation);
    }

    void buttonsAnimation()
    {
        for (int i = 4; i < 6; i++)
            windows[i].SetActive(true);
        LeanTween.moveLocal(windows[(int)windowType.PLAYBUTTON], new Vector3(0f, -100f, 0f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeInCirc);
        LeanTween.moveLocal(windows[(int)windowType.OPTIONSBUTTON], new Vector3(0f, -100f, 0f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeInCirc);
        LeanTween.moveLocal(windows[(int)windowType.CREDITSBUTTON], new Vector3(0f, -100f, 0f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeInCirc);
        LeanTween.moveLocal(windows[(int)windowType.PLAYBUTTON], new Vector3(0f, -30f, 0f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeInCirc);
        LeanTween.moveLocal(windows[(int)windowType.CREDITSBUTTON], new Vector3(0f, -170f, 0f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeInCirc);
        for (int i = 3; i < 6; i++)
            LeanTween.scale(windows[i], new Vector3(1.6f, 1.6f, 1.6f), 1f).setDelay(3.5f).setEase(LeanTweenType.easeInCirc);
        windows[(int)windowType.OPTIONSBACKBUTTON].SetActive(true);
        windows[(int)windowType.CREDITSBACKBUTTON].SetActive(true);
    }

    void showInstructions()
    {
        windows[(int)windowType.TUTORIAL].SetActive(true);
        windows[(int)windowType.PLAYBLOCK].SetActive(true);
        windows[(int)windowType.TUTORIAL].GetComponent<CanvasGroup>().LeanAlpha(1f, 1f).setDelay(3.5f);
    }

    void Update()
    {
        if (mainMenu)
        {
            if (windows[(int)windowType.PLAYBLOCK].transform.localScale.x > 2.0f
                && (Vector2.Distance(windows[(int)windowType.PLAYBLOCK].GetComponent<RectTransform>().anchoredPosition, windows[(int)windowType.PLAYBUTTON].GetComponent<RectTransform>().anchoredPosition) < 10.0f))
            {
                windows[(int)windowType.PLAYBUTTON].SetActive(true);
                windows[(int)windowType.PLAYBLOCK].SetActive(false);
            }
        }
    }

    void marksAnimation()
    {
        windows[(int)windowType.MARKSR].SetActive(true);
        windows[(int)windowType.MARKSL].SetActive(true);
        matter.LeanAlpha(1f, 0.5f);
        matter.LeanAlpha(0f, 0.5f).setDelay(0.5f);
        of.LeanAlpha(1f, 0.5f).setDelay(0.5f);
        of.LeanAlpha(0f, 0.5f).setDelay(1f);
        size.LeanAlpha(1f, 0.5f).setDelay(1.5f);
        LeanTween.moveLocal(windows[(int)windowType.MARKSR], new Vector3(0f, 0f, 0f), 2f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(windows[(int)windowType.MARKSL], new Vector3(0f, 0f, 0f), 2f).setEase(LeanTweenType.easeOutQuint);
    }

    public void playChosen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void restartChosen(string sceneName)
    {
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void exitChosen(string sceneName)
    {
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void optionsChosen()
    {
        if (transitioning == false)
        {
            transitioning = true;
            LeanTween.scale(windows[(int)windowType.OPTIONSBUTTON], new Vector3(0.2f, 0.8f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
            LeanTween.moveLocal(windows[(int)windowType.OPTIONSBUTTON], new Vector3(700f, -100f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeInBack);
            this.GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f).setDelay(2f);
            windows[(int)windowType.TUTORIAL].GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f).setDelay(1.5f);
            windows[(int)windowType.OPTIONS].SetActive(true);
            windows[(int)windowType.OPTIONS].GetComponent<CanvasGroup>().LeanAlpha(1f, 1f).setDelay(1.5f);
            LeanTween.moveLocal(windows[(int)windowType.OPTIONSBACKBUTTON], new Vector3(-400f, -150f, 0f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.scale(windows[(int)windowType.OPTIONSBACKBUTTON], new Vector3(0.8f, 1f, 0f), 0.5f).setDelay(3.5f).setEase(LeanTweenType.easeInCirc);
        }
    }

    public void optionsBackChosen()
    {
        LeanTween.scale(windows[(int)windowType.OPTIONSBACKBUTTON], new Vector3(0.2f, 0.8f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
        LeanTween.moveLocal(windows[(int)windowType.OPTIONSBACKBUTTON], new Vector3(-750f, -150f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutBounce);
        windows[(int)windowType.OPTIONS].GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f).setDelay(1.5f);
        this.GetComponent<CanvasGroup>().LeanAlpha(1f, 0.5f).setDelay(2f);
        windows[(int)windowType.TUTORIAL].GetComponent<CanvasGroup>().LeanAlpha(1f, 0.5f).setDelay(1.5f);
        StartCoroutine(settingInactive(3.5f, (int)windowType.OPTIONS));
        LeanTween.moveLocal(windows[(int)windowType.OPTIONSBUTTON], new Vector3(0f, -100f, 0f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(windows[(int)windowType.OPTIONSBUTTON], new Vector3(1.6f, 1.6f, 1.6f), 1f).setDelay(3.5f).setEase(LeanTweenType.easeInCirc);
        transitioning = false;
    }

    public void creditsChosen()
    {
        if (transitioning == false)
        {
            transitioning = true;
            LeanTween.scale(windows[(int)windowType.CREDITSBUTTON], new Vector3(0.2f, 0.8f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
            LeanTween.moveLocal(windows[(int)windowType.CREDITSBUTTON], new Vector3(-700f, -170f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeInBack);
            this.GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f).setDelay(2f);
            windows[(int)windowType.TUTORIAL].GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f).setDelay(2f);
            windows[(int)windowType.CREDITS].SetActive(true);
            windows[(int)windowType.CREDITS].GetComponent<CanvasGroup>().LeanAlpha(1f, 1f).setDelay(2f);
            LeanTween.moveLocal(windows[(int)windowType.CREDITSBACKBUTTON], new Vector3(250f, -150f, 0f), 0.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.scale(windows[(int)windowType.CREDITSBACKBUTTON], new Vector3(0.8f, 1f, 0f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeInCirc);
        }
    }

    public void creditsBackChosen()
    {
        LeanTween.scale(windows[(int)windowType.CREDITSBACKBUTTON], new Vector3(0.2f, 0.8f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
        LeanTween.moveLocal(windows[(int)windowType.CREDITSBACKBUTTON], new Vector3(750f, -150f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutBounce);
        windows[(int)windowType.CREDITS].GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f).setDelay(1.5f);
        this.GetComponent<CanvasGroup>().LeanAlpha(1f, 0.5f).setDelay(2f);
        windows[(int)windowType.TUTORIAL].GetComponent<CanvasGroup>().LeanAlpha(1f, 0.5f).setDelay(1.5f);
        StartCoroutine(settingInactive(3.5f, (int)windowType.CREDITS));
        LeanTween.moveLocal(windows[(int)windowType.CREDITSBUTTON], new Vector3(0f, -170f, 0f), 0.5f).setDelay(3f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(windows[(int)windowType.CREDITSBUTTON], new Vector3(1.6f, 1.6f, 1.6f), 1f).setDelay(3.5f).setEase(LeanTweenType.easeInCirc);
        transitioning = false;
    }


    IEnumerator settingInactive(float delay, int type)
    {
        yield return new WaitForSeconds(delay);
        windows[type].SetActive(false);
    }
}
