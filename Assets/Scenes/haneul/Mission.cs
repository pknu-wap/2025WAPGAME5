using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> missionObjects = new List<GameObject>();

    private int lastMission = -1;

    private void Update()
    {
        if (GameManager.currentMission != lastMission)
        {
            UpdateMissionVisual();
            lastMission = GameManager.currentMission;
        }
    }


    private void UpdateMissionVisual()
    {
        for (int i = 0; i < missionObjects.Count; i++)
        {
            missionObjects[i].SetActive(i == GameManager.currentMission);
        }
    }
}
