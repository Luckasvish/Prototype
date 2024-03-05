using System.Collections;
using UnityEngine;

public enum Side { Left, Right, Up, Down, Foward, Back }


public class CollisionChecker
{

    ControlledCubesData controlledCubesData;
    float lineCastSize = 1.2f;
    Side lastSideHit;

    Vector3[] sideValue = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.up, Vector3.down };

    public CollisionChecker(ControlledCubesData data)
    {
        controlledCubesData = data;
    }

    public bool IsOutOfArena()
    {
        bool _isOutOfArena = false;
        for (int sideIndex = 0; sideIndex < sideValue.Length; sideIndex++)
        {
            _isOutOfArena = Physics.Raycast(controlledCubesData.ControlledCube.transform.position, controlledCubesData.ControlledCube.transform.rotation * sideValue[sideIndex], lineCastSize, LayerMask.GetMask("OutOfArena"));
            if (_isOutOfArena)
                break;
        }
        return _isOutOfArena;
    }

    public bool IsGrounded()
    {
        bool _isGrounded = false;
        int hitSide = 0;
        for (int sideIndex = 0; sideIndex < sideValue.Length; sideIndex++)
        {
            _isGrounded = Physics.Raycast(controlledCubesData.ControlledCube.transform.position, controlledCubesData.ControlledCube.transform.rotation * sideValue[sideIndex], lineCastSize, LayerMask.GetMask("Ground"));

            if (_isGrounded)
            {
                hitSide = sideIndex;
                break;
            }

        }

        if(_isGrounded)
            lastSideHit = (Side)hitSide;


        return _isGrounded;
    }

    public Side GetHitSide()
    {
        return lastSideHit;
    }

}