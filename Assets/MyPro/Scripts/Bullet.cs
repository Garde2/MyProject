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
            Destroy(gameObject, lifeTime);       //Destroy(this) - ���� ���������, � ��������� � �� ��������
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse ); //���� � �������� + �������            
        }
        //void FixedUpdate()
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
        //    MoveTovards - ������� ��� ���������

        //    transform.position += transform.forward * _speed * Time.fixedDeltaTime;

        //}

        private void OnCollisionEnter(Collision collision)  
        {
            var particle = Instantiate(_sparksPrefab); //�������� PS - �������� PS, � �� GO
            particle.transform.position = collision.contacts[0].point; //����������
            particle.transform.rotation = Quaternion.Euler(collision.contacts[0].normal); // ���������� �� ����� ���������
            var lifetime = particle.main.duration + particle.main.startLifetimeMultiplier;
            Destroy(particle.gameObject, lifetime);

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