using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
  public GameStatistics statistics;
  public int playerHealth;
  public int playerGold;
  public int sceneNumber;
  public int questNumber;
  public float[] position;
  public float[] rotation;
  public int weaponIndex;
  public StatusCheats cheats;

  public SaveData(GameStatistics statistics, int playerHealth, int playerGold, int sceneNumber, int questNumber, Vector3 position, Vector3 rotation, int weaponIndex, StatusCheats cheats)
  {
    this.statistics = statistics;
    this.playerHealth = playerHealth;
    this.playerGold = playerGold;
    this.sceneNumber = sceneNumber;
    this.questNumber = questNumber;
    this.position = new float[] { position.x, position.y, position.z };
    this.rotation = new float[] { rotation.x, rotation.y, rotation.z };
    this.weaponIndex = weaponIndex;
    this.cheats = cheats;
  }
}