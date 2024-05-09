using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    public float duration = 5f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        startGameOverTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // gameObject.SetActive(false);
        
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
