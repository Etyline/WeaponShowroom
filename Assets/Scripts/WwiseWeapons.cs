using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseWeapons : MonoBehaviour
{
    public AK.Wwise.Event ShotEvent;
    public AK.Wwise.Event ReloadEvent;
    public AK.Wwise.Event ImpactEvent;
    public Action OnShoot;
    public Action OnReload;
    public Action OnImpact;

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

    private void WwiseImpactEvent()
    {
        ImpactEvent.Post(gameObject);
    }
}