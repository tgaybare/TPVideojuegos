using UnityEngine;

namespace Strategy.Strategy___Movement
{
    public interface IMoveable
    {
        void Move(Vector3 direction);

        void RotateTowards(Ray ray);
        
        void Dodge(int duration);
    }
}