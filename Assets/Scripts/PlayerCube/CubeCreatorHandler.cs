using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class CubeCreatorHandler : MonoBehaviour
{
    List<PlayerCube> cubesToHandleNewCubeCreation= new List<PlayerCube>();
    bool startCleaning;

    public void AddNewPossibleCubeCreator(PlayerCube playerCube) 
    {
        cubesToHandleNewCubeCreation.Add(playerCube);
    }

    private void Update()
    {
        if (startCleaning)
            Debug.Log("Error");
    }

    public void ClearCubeCreationByRound()
    {
        if (!startCleaning)
        {
            startCleaning = true;
            foreach (var item in cubesToHandleNewCubeCreation)
            {
                item.SetCubeCreatorHandler(null);
            }
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}