using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseWeapons : MonoBehaviour
{
    public AK.Wwise.Event ShotEvent;
    public AK.Wwise.Event ReloadEvent;
    public Action OnShoot;
    public Action OnReload;

    private void Start()
    {
        OnShoot += WwiseShotEvent;
        OnReload += WwiseReloadEvent;
    }

    private void WwiseShotEvent()
    {
        ShotEvent.Post(gameObject);
    }

    private void WwiseReloadEvent()
    {
        ReloadEvent.Post(gameObject);
    }
}