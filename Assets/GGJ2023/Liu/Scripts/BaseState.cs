using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState: MonoBehaviour
{
    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();

    public abstract int getCurrentStateIndex();
}
