using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TraningMode;
public class WeaponTraining : MonoBehaviour
{
    public WeaponType weaponType;
    private List<TrainingMission> missions = new List<TrainingMission>();
    private int completedMissionsCount = 0;
    private int totalMissionsCount = 0;

    void Awake()
    {
        InitializeMissionsList();
    }

    private void InitializeMissionsList()
    {
        // Tìm tất cả các components TrainingMission trên GameObject này và các con của nó
        TrainingMission[] missionsArray = GetComponentsInChildren<TrainingMission>(true); // true để bao gồm cả các GameObject không hoạt động

        // Thêm tất cả các TrainingMission tìm được vào danh sách missions
        missions.AddRange(missionsArray);

        totalMissionsCount = missions.Count;
        // Tùy chọn: In log số lượng missions được thêm
        Debug.Log($"Added {missions.Count} TrainingMission components to the list.");

    }

    public void StartNextMission()
    {
        TrainingMission nextMission = missions.Find(m => !m.isCompleted && !m.isActive);
        nextMission?.StartMission();
    }

    public void CompleteMission(int missionId)
    {
        
        TrainingMission mission = missions.Find(m => m.missionId == missionId && m.isActive);
        if (mission != null)
        {
            mission.CompleteMission();
            completedMissionsCount++;
            if (completedMissionsCount == totalMissionsCount)
            {
                Debug.Log($"{weaponType} training completed.");
            }
            else
            {
                Invoke("StartNextMission", 1f);
            }
        }
    }
}
