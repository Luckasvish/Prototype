using System.Collections;
using UnityEngine;

public class JumpingState : BaseState
{
    ControlledCubesData data;
    float rotationForce = 30f;
    bool canCheckGround = false;

    Vector3[] sideValue = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
    int sideToRotateIndex;

    public JumpingState(ControlledCubesData data)
    {
        this.data = data;
    }

    public override void OnInitialize()
    {
        canCheckGround = false;
        sideToRotateIndex = Random.Range(0, sideValue.Length - 1);

        data.PlayerCube.StartCoroutine(CanCheckGroundTimer());
    }

    public override void OnFinish()
    {
        var _playerCube = data.PlayerCube;
        var _cubesController = data.CubesController;
        var _rigidbody = _playerCube.GetRigidBody();
        _rigidbody.angularVelocity = Vector3.zero;

        var newSide = _playerCube.GetSideHittingGround();

        if (_playerCube.GetLastSideHittingGround() != newSide)
        {
            _playerCube.SetSideHittingGround(newSide);
            _cubesController.OnComputeScore?.Invoke(_cubesController.GetPlayerIndex());
            _cubesController.OnHitDifferentCubeSide?.Invoke(_playerCube);
        }
        else
            _cubesController.OnHitSameCubeSide?.Invoke(_playerCube);
    }

    public override void UpdateLogic()
    {
        if (!canCheckGround)
            return;

        var _playerCube = data.PlayerCube;
        if (_playerCube.IsCubeGrounded())
        {
            _playerCube.ChangeState(_playerCube.IdleState);
            return;
        }
        else
        {
            if (_playerCube.IsCubeOutOfArena())
            {
                _playerCube.ChangeState(_playerCube.IdleState);
                GameplayManager.Instance.OnDefeat?.Invoke(data.CubesController.GetPlayerIndex());
            }

        }

        var _rigidbody = data.PlayerCube.GetRigidBody();
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(sideValue[sideToRotateIndex] * rotationForce, ForceMode.Force);
    }

    IEnumerator CanCheckGroundTimer()
    {
        yield return new WaitForSeconds(0.2f);
        canCheckGround = true;
    }
}