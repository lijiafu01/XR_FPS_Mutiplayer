using UnityEngine;
using TMPro;
using System.Reflection;
public class UIController : MonoBehaviour
{
    static int HITNUM = 0;
    public TextMeshProUGUI scoreText;
    // Static biến để giữ instance duy nhất của UIController
    public static UIController Instance { get; private set; }

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
    public void ShowMissionProgress(int targetTotal,int hitNum)
    {
        HITNUM += hitNum;
        scoreText.text = "Target(" + HITNUM.ToString() + "/" + targetTotal.ToString() + ")";
    }
    public void SetMissionProgress(int targetTotal)
    {
        HITNUM = 0;
        scoreText.text = "Target(" + HITNUM.ToString() + "/" + targetTotal.ToString() + ")";
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
