using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameStatistics
{
  public int TotalShots = 0;
  public int TotalHits = 0;
  public float DistanceTravelled = 0;

  public string DistanceTravelledString
  {
    get
    {
      return (DistanceTravelled / 1000f).ToString("F") + " km";
    }
  }
  public float PlayTime = 0;

  public string PlayTimeString
  {
    get
    {
      TimeSpan time = TimeSpan.FromSeconds(PlayTime);
      return time.ToString(@"hh\:mm\:ss");
    }
  }
  public float TotalDamageDealt = 0;
  public float TotalDamageTaken = 0;
  public float TotalDeaths = 0;
  public float TotalKills = 0;

  public void AddTotalShot()
  {
    TotalShots++;
  }

  public void AddTotalHit()
  {
    TotalHits++;
  }

  public void AddDistance(float distance)
  {
    if (distance < 0)
    {
      throw new System.ArgumentException("Distance cannot be negative");
    }
    DistanceTravelled += distance;
  }

  public void AddPlayTime(float time)
  {
    if (time < 0)
    {
      throw new System.ArgumentException("Time cannot be negative");
    }
    PlayTime += time;
  }

  public void AddDamageDealt(float damage)
  {
    if (damage < 0)
    {
      throw new System.ArgumentException("Damage cannot be negative");
    }
    TotalDamageDealt += damage;
  }

  public void AddDamageTaken(float damage)
  {
    if (damage < 0)
    {
      throw new System.ArgumentException("Damage cannot be negative");
    }
    TotalDamageTaken += damage;
  }

  public void AddDeath()
  {
    TotalDeaths++;
  }

  public void AddKill()
  {
    TotalKills++;
  }
}
