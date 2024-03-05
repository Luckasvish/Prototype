using System.Collections;
using UnityEngine;


public class PlayerCube : MonoBehaviour
{
    CubesController cubesController;
    CubeCreatorHandler createCubeHandler;

    GameObject instantiatedCubeRoot;

    CollisionChecker collisionChecker;

    [SerializeField] Rigidbody rigidBody;

    internal BaseState IdleState;
    internal BaseState JumpState;
    internal BaseState JumpingState;
    BaseState currentState;

    Side lastSideHittingGround;


    public void InitPlayer(CubesController cubesController, GameObject cubeRoot, GameObject cube)
    {
        instantiatedCubeRoot = cubeRoot;
        this.cubesController = cubesController;

        InitializeCube(cube);
    }

    public Rigidbody GetRigidBody()
    {
        return rigidBody;
    }

    void InitializeCube(GameObject cube)
    {
        var data = new ControlledCubesData
        {
            ControlledCube = cube,
            CubeRoot = instantiatedCubeRoot,
            PlayerCube = this,
            CubesController = cubesController
        };

        collisionChecker = new(data);
        IdleState = new IdleState(data);
        JumpState = new JumpState(data);
        JumpingState = new JumpingState(data);
        ChangeState(IdleState);
    }


    public void ChangeState(BaseState newState)
    {
        if(currentState != null)
            currentState.OnFinish();

        currentState = newState;
        currentState.OnInitialize();
    }

    public void UpdateLogic()
    {
        if (currentState != null)
            currentState.UpdateLogic();

        if (collisionChecker.IsOutOfArena())
            GameplayManager.Instance.OnDefeat?.Invoke(cubesController.GetPlayerIndex());
    }

    public void SetCubeCreatorHandler(CubeCreatorHandler createCubeHandler) => this.createCubeHandler = createCubeHandler;

    public CubeCreatorHandler GetCreateCubeHandler()
    {
        return createCubeHandler;
    }


    /// <summary>
    /// Return if the player is grounded
    /// </summary>
    /// <returns></returns>
    public bool IsCubeOutOfArena()
    {
        return collisionChecker.IsOutOfArena();
    }    
    
    public bool IsCubeGrounded()
    {
        return collisionChecker.IsGrounded();
    }

    /// <summary>
    /// Return the new side of the cube that hit the ground
    /// </summary>
    /// <returns></returns>
    public Side GetSideHittingGround()
    {
        return collisionChecker.GetHitSide();
    }

    /// <summary>
    /// Return the last side of the cube that hit the ground
    /// </summary>
    /// <returns></returns>
    public Side GetLastSideHittingGround()
    {
        return lastSideHittingGround;
    }    
    
    public void SetSideHittingGround(Side newSide) => lastSideHittingGround = newSide;
} 