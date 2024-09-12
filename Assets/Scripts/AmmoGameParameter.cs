using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoGameParameter : MonoBehaviour
{
    public AK.Wwise.RTPC AmmoCount;
    public Ammo ammunition;

    void Update()
    {
        AmmoCount.SetGlobalValue(ammunition.GetCurrentAmmo());
    }
}
