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
    public bool IsAyam1Alive;
    public bool IsAyam2Alive;
    public bool IsAyam3Alive;
    public bool IsAyam4Alive;
    public bool IsAyam5Alive;

    public Difficulty Difficulty;

    public int BuffDamageTaken;

    public SaveData(GameStatistics statistics,
        int playerHealth,
        int playerGold, 
        int questNumber, 
        Vector3 position, 
        Vector3 rotation, 
        int weaponIndex, 
        StatusCheats cheats,
        Difficulty difficulty,
        int buffDamageTaken)
    {
        Statistics = statistics;
        PlayerHealth = playerHealth;
        PlayerGold = playerGold;
        QuestNumber = questNumber;
        Position = new float[] { position.x, position.y, position.z };
        Rotation = new float[] { rotation.x, rotation.y, rotation.z };
        WeaponIndex = weaponIndex;
        Cheats = cheats;
        Difficulty = difficulty;
        BuffDamageTaken = buffDamageTaken;
    }
}