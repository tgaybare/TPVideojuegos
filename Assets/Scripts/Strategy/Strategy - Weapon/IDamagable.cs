using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float Life { get; }
    float MaxLife { get; }
    void TakeDamage(int damage);
}
