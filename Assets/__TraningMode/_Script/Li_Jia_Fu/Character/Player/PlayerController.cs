using TraningMode;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Định nghĩa các biến private
    private PlayerAttack _playerAttack;
    private PlayerMovement _playerMovement;
    private PlayerWeapon _playerWeapon;
    private PlayerTeleport _playerTeleport;

    // Cung cấp truy cập chỉ đọc thông qua properties
    public PlayerAttack PlayerAttack => _playerAttack;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerWeapon PlayerWeapon => _playerWeapon;
    public PlayerTeleport PlayerTeleport => _playerTeleport;

    // Singleton instance
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            AssignComponents();
        }
    }

    // Phương thức gán các components từ các GameObject con
    private void AssignComponents()
    {
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        _playerMovement = GetComponentInChildren<PlayerMovement>();
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();
        _playerTeleport = GetComponentInChildren<PlayerTeleport>();

        // Kiểm tra xem các component đã được gán đúng chưa
        Debug.Assert(_playerAttack != null, "PlayerAttack component is missing!");
        Debug.Assert(_playerMovement != null, "PlayerMovement component is missing!");
        Debug.Assert(_playerWeapon != null, "PlayerWeapon component is missing!");
        Debug.Assert(_playerTeleport != null, "PlayerTeleport component is missing!");
    }
}
