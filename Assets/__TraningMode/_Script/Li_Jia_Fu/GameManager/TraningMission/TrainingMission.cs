using UnityEngine;

public class TrainingMission : MonoBehaviour
{
    public string missionId;
    public bool isActive;
    public bool isCompleted;

    private int _hitTargetNum = 0;
    public int targetTotal;
    private void Start()
    {
        targetTotal = transform.childCount;
        UIController.Instance.SetMissionProgress(targetTotal);
    }
    public void UpdateMissionProgress(int hitTargetNum = 1)
    {
        
        _hitTargetNum += hitTargetNum;
        Debug.Log("dev "+_hitTargetNum);
        if (_hitTargetNum == targetTotal)
        {
            
            WeaponTraining weaponTraining = transform.GetComponentInParent<WeaponTraining>();
            weaponTraining.CompleteMission(missionId);
            Debug.Log("dev next ");
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
        Debug.Log("dev check completed");
        isCompleted = true;
        isActive = false;
        gameObject.SetActive(false);
        Debug.Log($"dev Mission {missionId} completed.");
    }
}
