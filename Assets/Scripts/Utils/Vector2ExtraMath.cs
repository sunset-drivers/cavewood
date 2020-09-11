using UnityEngine;
namespace Cavewood.Utils
{
    public static class Vector2ExtraMath
    {
        public static Vector2 Normalize(Vector2 Vector){            
            Vector2 _NewVector = Vector2.zero; 
            if(Vector != _NewVector)
                _NewVector = Vector / Vector.magnitude;

            return _NewVector;
        }
    }
}

