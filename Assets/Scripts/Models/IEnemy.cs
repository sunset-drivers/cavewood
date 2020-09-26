using UnityEngine;
namespace Cavewood.Models
{
    public interface IEnemy {    
        void Attack();
        void Move(Transform Target);    
        void Die();
        void TakeDamage(float value);
    }
}