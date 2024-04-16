using UnityEngine;

public class TrainingMissionManager : MonoBehaviour
{
    public WeaponTraining[] weaponTrainings;
    private WeaponType trainingWeapon;
    /*void Start()
    {

        foreach (var training in weaponTrainings)
        {
            GameObject weaponObj = new GameObject($"{training.weaponType}Training");
            weaponObj.transform.SetParent(this.transform);
            var weaponTraining = weaponObj.AddComponent<WeaponTraining>();
            weaponTraining.weaponType = training.weaponType;
        }
    }*/
    private void Start()
    {
        trainingWeapon = WeaponManager.Instance.CurrentWeapon;
        StartTraining(trainingWeapon);
    }
    public void StartTraining(WeaponType weaponType)
    {
        foreach (WeaponTraining training in weaponTrainings)
        {
            // Kiểm tra nếu training match với loại vũ khí được yêu cầu
            if (training.weaponType == weaponType)
            {
                training.gameObject.SetActive(true); // Kích hoạt GameObject tương ứng
                training.StartNextMission(); // Bắt đầu nhiệm vụ tiếp theo
            }
            else
            {
                training.gameObject.SetActive(false); // Vô hiệu hóa các GameObject không liên quan
            }
        }
    }


    public void CompleteTraining(WeaponType weaponType, string missionId)
    {
        WeaponTraining training = System.Array.Find(weaponTrainings, w => w.weaponType == weaponType);
        training.CompleteMission(missionId);
    }
}
