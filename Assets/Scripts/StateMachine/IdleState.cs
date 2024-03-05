using System.Collections;
using UnityEngine;

public class IdleState : BaseState
{
    ControlledCubesData data;

    public IdleState(ControlledCubesData data)
    {
        this.data = data;
    }

    public override void OnInitialize() { }
    public override void OnFinish() { }

    public override void UpdateLogic()
    {
        var _inputs = ControllerManager.Instance.GetPlayerInputManager(data.CubesController.GetPlayerIndex()).GetInputs();
        if (_inputs.SouthButtonPressed && data.PlayerCube.GetRigidBody().velocity == Vector3.zero)
        {
            data.PlayerCube.ChangeState(data.PlayerCube.JumpState);
            OnFinish();
        }
    }
}