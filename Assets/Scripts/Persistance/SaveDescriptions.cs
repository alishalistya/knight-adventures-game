using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDescriptions
{
  public List<Description> Descriptions = new() {
    null,
    null,
    null,
  };

  [System.Serializable]
  public class Description
  {
    public string Name;
    // in seconds
    public long Time;

    public override string ToString()
    {
      DateTime dateTime = DateTime.UnixEpoch.AddSeconds(Time);
      return $"{Name}\n{dateTime:dd/MM/yyyy HH:mm}";
    }
  }

  public void SetDummyData()
  {
    Descriptions.Clear();
    Descriptions.Add(new Description { Name = "Save 1", Time = 1715078854L });
    Descriptions.Add(new Description { Name = "Save 2", Time = 1715078851L });
    Descriptions.Add(new Description { Name = "Save 3", Time = 1715038342L });
  }
}