namespace Cavewood.Models
{
    public interface IEnemy {    
        void Attack();
        void Move();    
        void Die();
        void TakeDamage(float value);
    }
}