using UnityEngine;
namespace Cavewood.Utils
{
    public static class Vector2ExtraMath
    {
        public static Vector2 NormalizeVector2(Vector2 vector){

            float _Magnitude = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
            Vector2 _NewVector = Vector2.zero; 

            if(vector != Vector2.zero)
                _NewVector = vector / _Magnitude;

            return _NewVector;
        }

        public static Vector2 TruncateVector2(Vector2 vector, float m_Limit){
            if(vector.x > m_Limit)
                vector.x = m_Limit;
                
            if(vector.y > m_Limit)
                vector.y = m_Limit;

            return vector;
        }
    }
}

