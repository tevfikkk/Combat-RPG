using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // If the particle system is not null and it's not alive, then destroy the particle system.
        if (ps && !ps.IsAlive())
        {
            DestroySelfAnimEvent();
        }
    }

    /// <summary>
    /// Destroys the slash animation
    /// </summary>
    public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }
}
