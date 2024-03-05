using System;
using System.Collections.Generic;
using UnityEngine;

public class CubesController : MonoBehaviour
{
    int playerIndex = 0;



    [SerializeField] GameObject playerCubePrefab;
    List<PlayerCube> controlledCubes = new List<PlayerCube>();

    public Action<int> OnComputeScore;

    public Action<PlayerCube> OnHitDifferentCubeSide;
    public Action<PlayerCube> OnHitSameCubeSide;
    internal List<PlayerCube> OnJumpedTogether = new List<PlayerCube>();

    CubeCreatorHandler cubeCreatorHandler;
    [SerializeField] GameObject cubeCreatorHandlerPrefab;


    bool canUpdateLogic = true;

    bool hasCubesLimit;
    int cubesLimit;

    void Start()
    {
        GameplayManager.Instance.OnDefeat += StopAction;
        OnComputeScore += GameplayManager.Instance.UpdateScore;
        OnComputeScore += GameplayManager.Instance.CheckVictoryCondition;
        OnHitDifferentCubeSide += TryCreateCube;
        OnHitSameCubeSide += ClearCubeCreateCubeHandler;
        hasCubesLimit = GameplayManager.Instance.HasCubesLimit();
        cubesLimit = GameplayManager.Instance.GetCubesLimit();

        CreateNewCube();
        OnComputeScore?.Invoke(playerIndex);

        canUpdateLogic = true;
    }

    void Update()
    {
        if (!canUpdateLogic || GameplayManager.Instance.gameFinished)
            return;

        for (int cubeIndex = 0; cubeIndex < controlledCubes.Count; cubeIndex++)
        {
            controlledCubes[cubeIndex].UpdateLogic();
        }

        CreateNewCubeCreatorHandler();
    }

    void StopAction(int playerIndex)
    {
        Debug.Log("Lose");
        canUpdateLogic = false;
    }

    void CreateNewCubeCreatorHandler()
    {
        if (OnJumpedTogether.Count <= 0)
            return;

        cubeCreatorHandler = Instantiate(cubeCreatorHandlerPrefab).GetComponent<CubeCreatorHandler>();
        for (int i = 0; i < OnJumpedTogether.Count; i++)
        {
            var _playerCube = OnJumpedTogether[i];
            cubeCreatorHandler.AddNewPossibleCubeCreator(_playerCube);
            _playerCube.SetCubeCreatorHandler(cubeCreatorHandler);
        }
        OnJumpedTogether.Clear();
        cubeCreatorHandler = null;
    }

    void ClearCubeCreateCubeHandler(PlayerCube playerCube)
    {
        var _createCubeHandler = playerCube.GetCreateCubeHandler();
        if (_createCubeHandler != null)
        {
            Debug.Log("ClearCubeCreateCubeHandler");
            _createCubeHandler.ClearCubeCreationByRound();
        }
    }

    void TryCreateCube(PlayerCube playerCube)
    {
        var _createCubeHandler = playerCube.GetCreateCubeHandler();
        if (_createCubeHandler != null)
        {
            Debug.Log("TryCreateCube");
            _createCubeHandler.ClearCubeCreationByRound();
            CreateNewCube();
        }
        Debug.Log($"_createCubeHandler : {_createCubeHandler}");
    }

    void CreateNewCube()
    {
        if (hasCubesLimit && controlledCubes.Count >= cubesLimit)
            return;
        GameObject _cubeRoot = new("PlayerCube");

        Vector3 maxRange = new Vector3(18, 0.5f, 18);
        Vector3 minRange = new Vector3(-18, 0.5f, -18);

        _cubeRoot.transform.position = Vector3RandomRange(minRange, maxRange);

        GameObject _instantiatedCube = Instantiate(playerCubePrefab, _cubeRoot.transform.position, Quaternion.identity, _cubeRoot.transform);
        _instantiatedCube.gameObject.GetComponent<Renderer>().material.SetColor("_Color", playerIndex == 0 ? Color.yellow : Color.red);
        PlayerCube _controlledCube = _instantiatedCube.GetComponent<PlayerCube>();
        _controlledCube.InitPlayer(this, _cubeRoot, _instantiatedCube);
        controlledCubes.Add(_controlledCube);

        GameplayManager.Instance.UpdateCubesNum(playerIndex);
    }

    public Vector3 Vector3RandomRange(Vector3 min, Vector3 max)
    {
        return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetPlayerIndex(int newPlayerIndex)
    {
        playerIndex = newPlayerIndex;
    }
}
