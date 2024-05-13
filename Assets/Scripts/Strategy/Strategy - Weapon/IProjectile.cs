using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    int Damage { get; }
    float Speed { get; }
    float LifeTime { get; }

    void Travel();
    
}
