using UnityEngine;
using TMPro;
using System.Reflection;
using TraningMode;
public class TraningMissionUI : MonoBehaviour
{
    public TrainerUI trainerUI;

    static int HITNUM = 0;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI missionText;
    // Static biến để giữ instance duy nhất của UIController
    public static TraningMissionUI Instance { get; private set; }

    private void Awake()
    {
        // Kiểm tra nếu Instance đã tồn tại và không phải là 'this'
        if (Instance != null && Instance != this)
        {
            // Nếu có một instance khác, hủy đối tượng này
            Destroy(gameObject);
        }
        else
        {
            // Đặt instance này là instance duy nhất (singleton) và đảm bảo nó không bị hủy khi load scene mới
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ShowMissionCurrent(int missionCurrent)
    {
        if (missionCurrent < 10)
        {
            missionText.text = "Mission 0" + missionCurrent.ToString();
        }
        else
        {
            missionText.text = "Mission " + missionCurrent.ToString();
        }
    }
    public void ShowMissionProgress(int targetTotal, int hitNum)
    {
        HITNUM += hitNum;
        progressText.text = "Target(" + HITNUM.ToString() + "/" + targetTotal.ToString() + ")";
    }
    public void SetMissionProgress(int targetTotal)
    {
        HITNUM = 0;
        progressText.text = "Target(" + HITNUM.ToString() + "/" + targetTotal.ToString() + ")";
    }
    // Thêm các phương thức quản lý UI vào đây
    public void ShowMainMenu()
    {
        // Logic để hiển thị main menu
    }

    public void HideMainMenu()
    {
        // Logic để ẩn main menu
    }

    // Ví dụ: UpdateScore, ShowGameOverScreen, v.v.
}
