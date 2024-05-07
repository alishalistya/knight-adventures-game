using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
  GameStatistics statistics;
  int playerHealth;
  int playerGold;
  int sceneNumber;
  int questNumber;

  float[] position;
  float[] rotation;

  int weaponIndex;

  public SaveData(GameStatistics statistics, int playerHealth, int playerGold, int sceneNumber, int questNumber, Vector3 position, Vector3 rotation, int weaponIndex)
  {
    this.statistics = statistics;
    this.playerHealth = playerHealth;
    this.playerGold = playerGold;
    this.sceneNumber = sceneNumber;
    this.questNumber = questNumber;
    this.position = new float[] { position.x, position.y, position.z };
    this.rotation = new float[] { rotation.x, rotation.y, rotation.z };
    this.weaponIndex = weaponIndex;
  }
}