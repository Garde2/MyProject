using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 3;
        [SerializeField] private float _force = 3;
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Transform target, float lifeTime, float speed)
        {
            _target = target;
            _speed = speed;
            Destroy(gameObject, lifeTime);       //Destroy(this) - пуля останется, а компонент с неё удалится

            _rigidbody.AddForce(transform.forward * _force ); //сила и вращение + импульс            
        }
        //void FixedUpdate()
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
        //    MoveTovards - считает шаг персонажа

        //    transform.position += transform.forward * _speed * Time.fixedDeltaTime;

        //}

        private void OnCollisionEnter(Collision collision)  
        {
            // Debug.Log("Hit");
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //коллизия(класс) - точка соприк объектов физич
            {
                Debug.Log("HitBullet!");
                takeDamage.Hurt(_damage);
                Destroy(gameObject);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}