using UnityEngine;
using TraningMode;
public enum ControllerHand
{
    Left,
    Right
}
public class InputManager : MonoBehaviour
{
    // Singleton instance
    public static InputManager Instance { get; private set; }
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
        }
    }
    private void Update()
    {
        // Check the inputs each frame
        CheckControllerInputs();
        GetRightGripReleased();


    }

    
    public Vector3 GetControllerPosition(ControllerHand hand)
    {
        if (hand == ControllerHand.Right)
        {
            Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            return rightControllerPosition;
        }
        else
        {
            Vector3 leftControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            return leftControllerPosition;
        }
    }
    // Thêm phương thức mới để lấy lực vung của controller
    public Vector3 GetControllerVelocity(bool isRightHand)
    {
        OVRInput.Controller controller = isRightHand ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
        return OVRInput.GetLocalControllerVelocity(controller);
    }

    public Vector3 GetControllerAngularVelocity(bool isRightHand)
    {
        OVRInput.Controller controller = isRightHand ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
        return OVRInput.GetLocalControllerAngularVelocity(controller);
    }
    // Phương thức mới để kiểm tra nút Grip phải
    public bool IsRightGripPressed()
    {
        // Sử dụng OVRInput từ Oculus SDK để kiểm tra nút Grip phải
        return OVRInput.Get(OVRInput.RawButton.RHandTrigger);
    }
    public bool GetRightGripReleased()
    {
        // Trả về true nếu nút Grip phải được thả
        return OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger);
    }
    private void CheckControllerInputs()
    {
        // Check for button presses
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("Button One (A) pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("Button Two (B) pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Button Three (X) pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Debug.Log("Button Four (Y) pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Debug.Log("Button Start pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            Debug.Log("Primary Thumbstick pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            Debug.Log("Secondary Thumbstick pressed");
        }

    }

    public bool IsButtonPressed(string buttonName)
    {
        // You can map button names to KeyCode or use Unity's new Input System
        // Example for the old Input system:
        switch (buttonName)
        {
            case "ButtonOne":
                return Input.GetKey(KeyCode.JoystickButton0);
                // Add cases for other buttons here
        }

        return false;
    }
    public bool GetTriggerPressed()
    {
        // Lấy giá trị nút trigger bên trái
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

        // Lấy giá trị nút trigger bên phải
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        // Kiểm tra xem có nút trigger nào được nhấn qua một ngưỡng nhất định không
        // Ví dụ, sử dụng ngưỡng 0.1f để coi như là nhấn nút
        return leftTrigger > 0.1f || rightTrigger > 0.1f;
    }

}
