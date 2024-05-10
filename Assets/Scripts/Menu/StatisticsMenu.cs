using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsMenu : MonoBehaviour
{
    private void OnEnable()
    {
        PersistanceManager.Instance.AssertInit();
        gameObject.GetComponentInChildren<StatisticsTable>().UpdateStatistics(PersistanceManager.Instance.GlobalStat);
    }
}
