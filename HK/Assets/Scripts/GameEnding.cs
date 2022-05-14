using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public static GameEnding instance;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    float m_Timer;
    bool m_IsPlayerAtExit = false;
    bool m_IsPlayerCaught=false;
    private void Awake()
    {
        if(instance!=null)
        {
            return;
        }
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_IsPlayerAtExit=true;
            return;
        }
    }
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
    private void Update()
    {

        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false);
        }
        else if(m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup,true);
        }
    }
    void EndLevel(CanvasGroup canvasGroupValue,bool doRestart)
    {
        m_Timer += Time.deltaTime;
        canvasGroupValue.alpha = m_Timer / fadeDuration;
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
