using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
  public GameStatistics Statistics;
  public int PlayerHealth;
  public int PlayerGold;
  public int QuestNumber;
  public float[] Position;
  public float[] Rotation;
  public int WeaponIndex;
  public StatusCheats Cheats;

  public SaveData(GameStatistics statistics, int playerHealth, int playerGold, int questNumber, Vector3 position, Vector3 rotation, int weaponIndex, StatusCheats cheats)
  {
    Statistics = statistics;
    PlayerHealth = playerHealth;
    PlayerGold = playerGold;
    QuestNumber = questNumber;
    Position = new float[] { position.x, position.y, position.z };
    Rotation = new float[] { rotation.x, rotation.y, rotation.z };
    WeaponIndex = weaponIndex;
    Cheats = cheats;
  }
}