using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseShot : MonoBehaviour
{
    public AK.Wwise.Event ShotEvent;
    public Action OnShoot;

    private void Start()
    {
        OnShoot += WwiseEvent;
    }

    private void WwiseEvent()
    {
        ShotEvent.Post(gameObject);
    }

}