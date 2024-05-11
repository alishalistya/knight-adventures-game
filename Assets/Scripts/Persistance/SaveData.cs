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
    public bool PlayerHasKnight;
    public bool PlayerHasMage;

    public float[] Position;
    public float[] Rotation;
    public int WeaponIndex;
    public StatusCheats Cheats;
    public bool[] IsAyamAlive = new bool[] { true, true, true, true, true};
    public Difficulty Difficulty;
    public int BuffDamageTaken;
    public int[] GoalProgress = new int[] { 0, 0, 0, 0, 0 };

    public SaveData(GameStatistics statistics,
        int playerHealth,
        int playerGold,
        int questNumber,
        Vector3 position,
        Vector3 rotation,
        int weaponIndex,
        StatusCheats cheats,
        Difficulty difficulty,
        int buffDamageTaken,
        bool[] isAyamAlive,
        int[] goalProgress,
        bool playerHasKnight,
        bool playerHasMage
        )
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
        IsAyamAlive = isAyamAlive;
        GoalProgress = goalProgress;
        PlayerHasKnight = playerHasKnight;
        PlayerHasMage = playerHasMage;
    }
}