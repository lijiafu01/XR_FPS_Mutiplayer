using Fusion;
using multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : NetworkBehaviour
{
    [SerializeField]
    private NetworkCharacterControllerPrototype _characterController;
    [SerializeField]
    private float _speed = 5f;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputData data))
        {
            // Chuyển đổi từ không gian 2D của joystick sang không gian 3D của game
            var moveInput = new Vector3(data.MoveInput.x, 0, data.MoveInput.y);
            _characterController.Move(moveInput * _speed * Runner.DeltaTime);
        }
    }
}
