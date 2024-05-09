using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;




public class ShopTimer : MonoBehaviour

{
    public float duration = 20f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private UI_Shop uiShop;
    // private UI_Shop uiShop;

    // Start is called before the first frame update
    void Start()
    {
        ShopEvents.OnTimerStarted += OnShopTimerStarted;
    }

    private void OnShopTimerStarted()
    {
        Debug.Log("ShopTimer onShopTimerStarted");
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        Debug.Log("ShopTimer Timer");
        timerText.gameObject.SetActive(true);
        uiShop.SetShopOpen(true);
        uiShop.SetShopHadBeenOpened(true);
        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time left until shop closed: " + timeLeft.ToString("F2");
            yield return null;
        }
        uiShop.Hide();
        uiShop.SetShopOpen(false);
        timerText.text = "Shop is closed";
        yield return new WaitForSeconds(5f);
        timerText.text = "";
        timerText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
