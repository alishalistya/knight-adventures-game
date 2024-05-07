using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsMenu : MonoBehaviour
{
    private void OnEnable()
    {
        PersistanceManager.Instance.LoadStatistics();
        gameObject.GetComponentInChildren<StatisticsTable>().UpdateStatistics(PersistanceManager.Instance.Statistics);
    }
}
