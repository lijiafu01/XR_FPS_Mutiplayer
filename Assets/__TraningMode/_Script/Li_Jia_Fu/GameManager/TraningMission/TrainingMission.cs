using UnityEngine;

public class TrainingMission : MonoBehaviour
{
    public string missionId;
    public bool isActive;
    public bool isCompleted;

    private int _hitTargetNum = 0;
    private int totalTarget;
    private void Start()
    {
        totalTarget = transform.childCount;
    }
    public void Setup(string id)
    {
        missionId = id;
        isActive = false;
        isCompleted = false;
    }
    public void UpdateMissionProgress(int hitTargetNum = 1)
    {
        _hitTargetNum += hitTargetNum;
        if (_hitTargetNum == totalTarget)
        {
            
            WeaponTraining weaponTraining = transform.GetComponentInParent<WeaponTraining>();
            weaponTraining.CompleteMission(missionId);
        }
    }
    public void StartMission()
    {
        isActive = true;
        gameObject.SetActive(true);
        Debug.Log($"Mission {missionId} started.");
    }

    public void CompleteMission()
    {
        isCompleted = true;
        isActive = false;
        gameObject.SetActive(false);
        Debug.Log($"Mission {missionId} completed.");
    }
}
