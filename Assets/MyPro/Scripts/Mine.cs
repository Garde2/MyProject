using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float _damage = 3f;
        [SerializeField] private float _lifeTime = 5f;

        public void Init(float lifeTime)
        {
            _lifeTime = lifeTime;  
            Destroy(gameObject, lifeTime); 

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _damage = 0;
                var enemy = other.GetComponent<Player>();
                enemy.Hurt(_damage);

            }
            if (other.gameObject.CompareTag("Shield"))
            {
                _damage = 0;
                var enemy = other.GetComponent<Shield>();
                enemy.Hurt(_damage);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();
                enemy.Hurt(_damage);
                Destroy(gameObject);
            }
            
        }
        private void OnCollisionEnter(Collision collision)   //по уроку 4 сделаnm взрыв для мины через SphereCast/OverlapSphere
            //float radius
        {
            // Debug.Log("Hit");
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //коллизия(класс) - точка соприк объектов физич
            {
                Debug.Log("Hit!");
                takeDamage.Hit(_damage);
                Destroy(gameObject);
            }

        }


    }
}
