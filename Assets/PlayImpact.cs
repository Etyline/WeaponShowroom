using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayImpact : MonoBehaviour
{
    public AK.Wwise.Event impactEvent;

    public void PlaySound()
    {
        impactEvent.Post(gameObject);
    }
}
