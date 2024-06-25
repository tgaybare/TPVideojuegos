using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    int Damage { get; set; }
    float Speed { get; }
    float LifeTime { get; }

    void Travel();
    
}
