using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwisePlayer : MonoBehaviour
{

    public AK.Wwise.Event StepEvent;
    public AK.Wwise.Event JumpEvent;
    public AK.Wwise.Event LandEvent;
    public Action OnStep;
    public Action OnJump;
    public Action OnLand;

    void Start()
    {
        OnStep += WwiseStepEvent;
        OnJump += WwiseJumpEvent;
        OnLand += WwiseLandEvent;
    }

    private void WwiseStepEvent()
    {
        StepEvent.Post(gameObject);
    }

    private void WwiseJumpEvent()
    {
        JumpEvent.Post(gameObject);
    }

    private void WwiseLandEvent()
    {
        LandEvent.Post(gameObject);
    }
}