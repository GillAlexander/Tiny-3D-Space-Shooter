using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Don't forget to:<para />
/// - use the correct namespaces<para />
/// - implement Start() method<para />
/// - create an enum for the different states of this context, and assign it to T<para />
/// - set the enum values to corresponding states in Awake()
/// </summary>
/// 
public class Context<T> : MonoBehaviour
{
    private State<T> state = null;

    protected Dictionary<T, State<T>> states = new Dictionary<T, State<T>>();

    public void ChangeState(T state)
    {
        Debug.Log($"Transition to {state}");
        this.TransitionTo(state);
    }

    public void TransitionTo(T state)
    {
        if (this.state != null)
        {
            this.state.OnStateExit();
        }
        this.state = states[state];
        FindObjectOfType<UiHandler>().currentStateDisplay.text = states[state].ToString();
        this.state.SetContext(this);
        this.state.OnStateEnter();
    }

    protected virtual void Start()
    {
        Debug.LogWarning($"You haven't implemented Start() in {this.name}");
    }

    private void Update()
    {
        if (state == null) return;
        state.Tick();
    }

    public void OnDisable()
    {
        state.OnStateExit();
        this.state = null;
    }
}
