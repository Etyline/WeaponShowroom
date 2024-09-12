using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseShot : MonoBehaviour
{
    public AK.Wwise.Event ShotEvent;
    public Action Shot;

    private void Start()
    {
        Shot += ShotSound;
    }

    private void ShotSound()
    {
        ShotEvent.Post(gameObject);
    }

}