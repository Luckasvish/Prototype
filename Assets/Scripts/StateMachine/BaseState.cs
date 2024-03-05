using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    abstract public void OnInitialize();
    abstract public void UpdateLogic();
    abstract public void OnFinish();
}
