using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    public void TakeDmg(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
