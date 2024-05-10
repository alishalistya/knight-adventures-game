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

      Disable();
    }
    else
    {
      Input.simulateMouseWithTouches = false;
      Enable();
    }
  }

  private void OnEnable()
  {
    GameManager.Instance.OnGameStateChange += OnGameStateChange;
  }

  private void OnDisable()
  {
    GameManager.Instance.OnGameStateChange -= OnGameStateChange;
  }

  private void OnGameStateChange(GameState state)
  {
    if (!Application.isMobilePlatform) return;
    if (state == GameState.CUTSCENE)
    {
      Disable();
    }
    else
    {
      Enable();
    }
  }

  private void Disable()
  {
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }
  }

  private void Enable()
  {
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(true);
    }
  }
}