using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float Life { get; }
    float MaxLife { get; }
    void TakeDamage(int damage);
}
