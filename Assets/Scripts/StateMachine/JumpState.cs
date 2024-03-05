using System.Collections;
using UnityEngine;

public class JumpState : BaseState
{
    ControlledCubesData data;
    Rigidbody rigidbody;
    float jumpForce = 10f;

    public JumpState(ControlledCubesData data)
    {
        this.data = data;
    }

    public override void OnInitialize()
    {
        var _playerCube = data.PlayerCube;

        data.CubesController.OnJumpedTogether.Add(_playerCube);
        rigidbody = _playerCube.GetRigidBody();
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        _playerCube.ChangeState(_playerCube.JumpingState);
    }

    public override void OnFinish()
    {

    }

    public override void UpdateLogic()
    {

    }
}