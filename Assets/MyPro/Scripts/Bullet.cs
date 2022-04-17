using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 3;              
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private ParticleSystem _sparksPrefab;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(float lifeTime, float force)
        {                        
            Destroy(gameObject, lifeTime);       //Destroy(this) - пуля останется, а компонент с неё удалится
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse ); //сила и вращение + импульс            
        }
        //void FixedUpdate()
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
        //    MoveTovards - считает шаг персонажа

        //    transform.position += transform.forward * _speed * Time.fixedDeltaTime;

        //}

        private void OnCollisionEnter(Collision collision)  
        {
            var particle = Instantiate(_sparksPrefab); //передали PS - получили PS, а не GO
            particle.transform.position = collision.contacts[0].point; //разместили
            particle.transform.rotation = Quaternion.Euler(collision.contacts[0].normal); // развернули от точки соприкосн
            var lifetime = particle.main.duration + particle.main.startLifetimeMultiplier;
            Destroy(particle.gameObject, lifetime);

            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //коллизия(класс) - точка соприк объектов физич
            {
                takeDamage.Hurt(_damage);
                Debug.Log("HitBullet!");
                Destroy(gameObject);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}