using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MobileOnly : MonoBehaviour
{

  void Start()
  {
    if (!Application.isMobilePlatform)
    {
      Input.simulateMouseWithTouches = true;
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(false);
      }
    }
    else
    {
      Input.simulateMouseWithTouches = false;
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(true);
      }
    }
  }
}