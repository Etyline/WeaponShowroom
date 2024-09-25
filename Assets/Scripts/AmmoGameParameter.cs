using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoGameParameter : MonoBehaviour
{
    public AK.Wwise.RTPC AmmoCount;
    public GunSystem ammonition;

    void Update()
    {
        AmmoCount.SetGlobalValue(ammonition.bulletsLeft);
    }
}
