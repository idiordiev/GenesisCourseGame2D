using System;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader : IEntityInputSource
{
    public float Direction
    {
        get
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }

    public bool Jump { get; private set; }
    public bool Attack { get; private set; }

    public void OnUpdate()
    {
        if (!IsPointerOverUi() && Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        
        if (!IsPointerOverUi() && Input.GetButtonDown("Fire1"))
        {
            Attack = true;
        }
    }

    private bool IsPointerOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void ResetOneTimeActions()
    {
        Jump = false;
        Attack = false;
    }
}