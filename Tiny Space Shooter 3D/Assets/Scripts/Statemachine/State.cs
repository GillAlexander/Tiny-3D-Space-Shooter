using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Don't forget to set the T(StateMachine enum I belong to)
/// </summary>

public abstract class State<T>
{
    protected Context<T> context;

    public void SetContext(Context<T> context)
    {
        this.context = context;
    }

    public abstract void Tick();
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
}
