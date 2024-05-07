using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public enum StatusCheats
{
  NO_DAMAGE = 1,
  ONE_HIT_KILL = 2,
  MOTHERLODE = 4,
  TWO_TIMES_SPEED = 8,
  FULL_HP_PET = 16
}

public class PlayerCheats : MonoBehaviour
{
  private StatusCheats _statusCheats = 0;
  private string compositeString = "";

  private float clearAfter = 1f;
  private Coroutine _clearTimer;

  private float _timeUntilClear;

  [SerializeField] TextMeshProUGUI toastText;

  private static readonly Dictionary<string, StatusCheats> cheatCodes = new() {
    {"fakhrikebal", StatusCheats.NO_DAMAGE},
    {"yonikerad", StatusCheats.ONE_HIT_KILL},
    {"alisharich", StatusCheats.MOTHERLODE},
    {"akbargece", StatusCheats.TWO_TIMES_SPEED},
    {"yonikebal", StatusCheats.FULL_HP_PET},
  };

  public bool IsCheat(StatusCheats statusCheats)
  {
    return (_statusCheats & statusCheats) != 0;
  }

  public void SetCheat(StatusCheats statusCheats, bool value = true)
  {
    if (value)
    {
      if (!IsCheat(statusCheats))
      {
      }
      showToast("Cheat activated: " + statusCheats.ToString(), 2);
      _statusCheats |= statusCheats;
    }
    else
    {
      _statusCheats &= ~statusCheats;
    }
  }

  private void OnEnable()
  {
    Keyboard.current.onTextInput += InputKey;
    _clearTimer = StartCoroutine(ClearTimer());
  }

  private void OnDisable()
  {
    StopCoroutine(_clearTimer);
    Keyboard.current.onTextInput -= InputKey;
  }

  private void InputKey(char inputchar)
  {
    compositeString += inputchar.ToString().ToLower();
    _timeUntilClear = 0;
    CheckCodes();
  }

  private void CheckCodes()
  {
    foreach (var code in cheatCodes)
    {
      if (compositeString.EndsWith(code.Key))
      {
        print("Cheat activated: " + code.Value);
        SetCheat(code.Value);
        compositeString = string.Empty;
      }
    }
  }

  private IEnumerator ClearTimer()
  {
    while (true)
    {
      if (compositeString == string.Empty)
        yield return null;

      _timeUntilClear += Time.deltaTime;

      if (_timeUntilClear >= clearAfter)
      {
        _timeUntilClear -= clearAfter;
        compositeString = string.Empty;
      }
      yield return null;
    }
  }

  void showToast(string text,
    int duration)
  {
    StartCoroutine(showToastCOR(text, duration));
  }

  private IEnumerator showToastCOR(string text,
      int duration)
  {
    if (toastText == null)
    {
      Debug.LogWarning("Please assign a Text object to the Toast Text field in the inspector");
      yield break;
    }
    Color orginalColor = toastText.color;

    toastText.text = text;
    toastText.enabled = true;

    //Fade in
    yield return fadeInAndOut(toastText, true, 0.5f);

    //Wait for the duration
    float counter = 0;
    while (counter < duration)
    {
      counter += Time.deltaTime;
      yield return null;
    }

    //Fade out
    yield return fadeInAndOut(toastText, false, 0.5f);

    toastText.enabled = false;
    toastText.color = orginalColor;
  }

  IEnumerator fadeInAndOut(TextMeshProUGUI targetText, bool fadeIn, float duration)
  {
    //Set Values depending on if fadeIn or fadeOut
    float a, b;
    if (fadeIn)
    {
      a = 0f;
      b = 1f;
    }
    else
    {
      a = 1f;
      b = 0f;
    }

    float counter = 0f;

    while (counter < duration)
    {
      counter += Time.deltaTime;
      float alpha = Mathf.Lerp(a, b, counter / duration);

      targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, alpha);
      yield return null;
    }
  }
}