using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDescriptions
{
  public List<Description> Descriptions = new();

  [System.Serializable]
  public class Description
  {
    public string Name;
    public float Time;
  }
}