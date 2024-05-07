using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatistics
{
  private int totalShots = 0;
  private int totalHits = 0;

  private float distanceTravelled = 0;

  private float playTime = 0;

  private float totalDamageDealt = 0;
  private float totalDamageTaken = 0;
  private float totalDeaths = 0;
  private float totalKills = 0;

  public int TotalShots { get => totalShots; }
  public int TotalHits { get => totalHits; }
  public float DistanceTravelled { get => distanceTravelled; }

  public string DistanceTravelledString
  {
    get
    {
      return (distanceTravelled / 1000f).ToString("F") + " km";
    }
  }
  public float PlayTime { get => playTime; }

  public string PlayTimeString
  {
    get
    {
      TimeSpan time = TimeSpan.FromSeconds(playTime);
      return time.ToString(@"hh\:mm\:ss");
    }
  }
  public float TotalDamageDealt { get => totalDamageDealt; }
  public float TotalDamageTaken { get => totalDamageTaken; }
  public float TotalDeaths { get => totalDeaths; }
  public float TotalKills { get => totalKills; }

  public void AddTotalShot()
  {
    totalShots++;
  }

  public void AddTotalHit()
  {
    totalHits++;
  }

  public void AddDistance(float distance)
  {
    if (distance < 0)
    {
      throw new System.ArgumentException("Distance cannot be negative");
    }
    distanceTravelled += distance;
  }

  public void AddPlayTime(float time)
  {
    if (time < 0)
    {
      throw new System.ArgumentException("Time cannot be negative");
    }
    playTime += time;
  }

  public void AddDamageDealt(float damage)
  {
    if (damage < 0)
    {
      throw new System.ArgumentException("Damage cannot be negative");
    }
    totalDamageDealt += damage;
  }

  public void AddDamageTaken(float damage)
  {
    if (damage < 0)
    {
      throw new System.ArgumentException("Damage cannot be negative");
    }
    totalDamageTaken += damage;
  }

  public void AddDeath()
  {
    totalDeaths++;
  }

  public void AddKill()
  {
    totalKills++;
  }
}
