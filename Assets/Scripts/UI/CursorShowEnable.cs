using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CursorShowEnable : MonoBehaviour
{
  private void OnEnable()
  {
    Cursor.visible = true;
  }

  private void OnDisable()
  {
    Cursor.visible = false;
  }
}