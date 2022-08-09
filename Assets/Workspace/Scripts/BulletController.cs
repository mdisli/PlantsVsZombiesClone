using DG.Tweening;
using UnityEngine;

namespace Workspace.Scripts
{
    public class BulletController : MonoBehaviour
    {
        #region Variables
    
        public int damage;
        public Transform target;

        public float speed;

        #endregion


        public void PrepareBullet(Transform target, int damage)
        {
            this.target = target;
            this.damage = damage;
        }

        public void Shoot()
        {
            var distance = Vector3.Distance(transform.position, target.position);
            
            transform.DOMove(target.position, distance / speed)
                .SetEase(Ease.Linear)
                .SetId(21)
                .OnComplete(() =>
                {
                    ObjectPool.instance.AddBullet(gameObject);
                });
        }

        #region Triggers

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                // damage
                DOTween.Kill(transform);
                ObjectPool.instance.AddBullet(gameObject);

                var enemy = col.GetComponent<EnemyItem>();
                
                enemy.TakeDamage(damage);
            }
        }

        #endregion
    
    }
}
