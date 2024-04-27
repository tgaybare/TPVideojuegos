using UnityEngine;

namespace Controllers
{
    public interface IMoveable
    {
        void Move(Vector3 direction);

        void RotateTowards(Ray ray);
    }
}