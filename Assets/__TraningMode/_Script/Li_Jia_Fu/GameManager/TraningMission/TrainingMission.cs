using UnityEngine;
using TraningMode;
public class TrainingMission : MonoBehaviour
{
    public int missionId;
    public bool isActive;
    public bool isCompleted;

    private int _hitTargetNum = 0;
    public int targetTotal;
    private void Start()
    {
        targetTotal = transform.childCount;
        TraningMissionUI.Instance.ShowMissionCurrent(missionId);
        TraningMissionUI.Instance.SetMissionProgress(targetTotal);
    }
    public void UpdateMissionProgress(int hitTargetNum = 1)
    {
        _hitTargetNum += hitTargetNum;
        if (_hitTargetNum == targetTotal)
        {
            TraningMissionUI.Instance.trainerUI.UpdateCurrentMission(missionId,false);
            /*WeaponTraining weaponTraining = transform.GetComponentInParent<WeaponTraining>();
            weaponTraining.CompleteMission(missionId);*/
        }
        TraningMissionUI.Instance.ShowMissionProgress(targetTotal, 1);
    }
    public void StartMission()
    {
        isActive = true;
        gameObject.SetActive(true);
        TraningMissionUI.Instance.trainerUI.UpdateCurrentMission(missionId, true);
    }

    public void CompleteMission()
    {
        isCompleted = true;
        isActive = false;
        gameObject.SetActive(false);
    }
}
