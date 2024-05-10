using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    public float duration = 10f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] string sceneName;
    [SerializeField] GameObject healthUI;
    [SerializeField] GameObject goldUI;
    [SerializeField] GameObject questUI;

    void Start()
    {

        gameObject.GetComponentInChildren<StatisticsTable>().UpdateStatistics(GameManager.Instance.Statistics);

        healthUI.SetActive(false);
        goldUI.SetActive(false);
        questUI.SetActive(false);

        startGameOverTimer();
        PersistanceManager.Instance.SaveStatistics();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene(sceneName);
        
    }

    public void startGameOverTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time left: " + timeLeft.ToString("F2");
            if (timeLeft <= 0)
            {
                timerText.text = "Returning to main menu...";
                MainMenu();
                yield return new WaitForSeconds(5f);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }

}
