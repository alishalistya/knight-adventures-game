using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDescriptions
{
  public List<Description> Descriptions = new() {
    new Description("", false),
    new Description("", false),
    new Description("", false),
  };

  [System.Serializable]
  public class Description
  {
    public bool IsUsed = true;
    public string Name;
    // in seconds
    public long Time;

    public override string ToString()
    {
      DateTime dateTime = DateTime.UnixEpoch.AddSeconds(Time);
      return $"{Name}\n{dateTime:dd/MM/yyyy HH:mm}";
    }

    public Description(string name, bool isUsed = true)
    {
      Name = name;
      IsUsed = isUsed;
      Time = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
  }
}