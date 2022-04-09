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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(float lifeTime, float force)
        {                        
            Destroy(gameObject, lifeTime);       //Destroy(this) - ���� ���������, � ��������� � �� ��������
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse ); //���� � �������� + �������
            //Debug.Log("Spawn   Bullet!");
        }
        //void FixedUpdate()
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
        //    MoveTovards - ������� ��� ���������

        //    transform.position += transform.forward * _speed * Time.fixedDeltaTime;

        //}

        private void OnCollisionEnter(Collision collision)  
        {
            // Debug.Log("Hit");
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //��������(�����) - ����� ������ �������� �����
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