using Fusion;

public class Player : NetworkBehaviour
{
    // Khai báo một biến để giữ tham chiếu đến NetworkCharacterControllerPrototype
    private NetworkCharacterControllerPrototype _cc;

    // Phương thức Awake() được gọi khi đối tượng được tạo
    private void Awake()
    {
        // Lấy thành phần NetworkCharacterControllerPrototype từ đối tượng hiện tại
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    // Ghi đè phương thức FixedUpdateNetwork() để tham gia vào vòng lặp mô phỏng của Fusion
    public override void FixedUpdateNetwork()
    {
        // Lấy dữ liệu đầu vào từ Fusion
        if (GetInput(out NetworkInputData data))
        {
            // Chuẩn hóa hướng di chuyển để tránh gian lận
            data.direction.Normalize();

            // Áp dụng chuyển động cho hình đại diện dựa trên dữ liệu đầu vào
            _cc.Move(5 * data.direction * Runner.DeltaTime);
        }
    }
}
