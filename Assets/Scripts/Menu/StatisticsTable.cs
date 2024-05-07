using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;

public class StatisticsTable : MonoBehaviour
{
    GameStatistics data;

    public void UpdateStatistics(GameStatistics statistics)
    {
        print("StatisticsTable UpdateStatistics");
        List<TextMeshProUGUI> texts = new();
        var i = 0;
        foreach (Transform child in transform)
        {
            if (i % 2 == 1)
            {
                texts.Add(child.GetComponent<TextMeshProUGUI>());
            }
            i++;
        }
        data = statistics;
        texts[0].text = data.TotalHits.ToString();
        texts[1].text = data.TotalShots.ToString();
        texts[2].text = data.DistanceTravelledString;
        texts[3].text = data.PlayTimeString;
        texts[4].text = data.TotalDamageDealt.ToString();
        texts[5].text = data.TotalDamageTaken.ToString();
        texts[6].text = data.TotalDeaths.ToString();
        texts[7].text = data.TotalKills.ToString();
    }
}
