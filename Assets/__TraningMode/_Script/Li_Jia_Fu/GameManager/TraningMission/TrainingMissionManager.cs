using UnityEngine;
using TraningMode;

public class TrainingMissionManager : MonoBehaviour
{
    public static TrainingMissionManager Instance { get; private set; }
    [SerializeField] private WeaponTraining[] weaponTrainings;
    private WeaponType trainingWeapon;
    private WeaponTraining _weaponTraining;
    public WeaponTraining WeaponTraining => _weaponTraining;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Makes the object not be destroyed automatically when loading a new scene.
        }
        else
        {
            Destroy(gameObject); // Ensures that there is only one instance of this object in the game.
        }
    }
    private void Start()
    {
        //TraningMissionUI.Instance.trainerUI.UpdateCurrentMission(0);
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
                _weaponTraining = training;
                training.gameObject.SetActive(true); // Kích hoạt GameObject tương ứng
                training.StartNextMission(); // Bắt đầu nhiệm vụ tiếp theo
            }
            else
            {
                training.gameObject.SetActive(false); // Vô hiệu hóa các GameObject không liên quan
            }
        }
    }


    public void CompleteTraining(WeaponType weaponType, int missionId)
    {
        WeaponTraining training = System.Array.Find(weaponTrainings, w => w.weaponType == weaponType);
        training.CompleteMission(missionId);
    }
}
