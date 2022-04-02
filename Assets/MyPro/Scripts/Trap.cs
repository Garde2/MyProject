using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private float _damage = 3f;
        [SerializeField] private bool _isHide;

        void Start()
        {
            InvokeRepeating(nameof(Move1), 1f, _cooldown);   //������ ��� ����� ������, �������� - �� �����
        }

        private void Move1()
        {
            if (_isHide)
            {
                transform.position = new Vector3(transform.position.x, -1, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }

            _isHide = !_isHide;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {                
                var enemy = other.GetComponent<Player>();
                enemy.Hurt(_damage);
            }
            if (other.gameObject.CompareTag("Shield"))
            {                
                var enemy = other.GetComponent<Shield>();
                enemy.Hurt(_damage);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                _damage = 0;
                var enemy = other.GetComponent<Enemy>();
                enemy.Hurt(_damage);                
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))   // � � ������� ����� ����� ���� ������� � ������ 1, ���� ���������, �������� ���� ��� �������� - "����� �� ���������"
            {
                CancelInvoke(nameof(Move1));
                Debug.Log("��������� Trap");
                transform.position = new Vector3(transform.position.x, -1, transform.position.z);
            }
        }                 

        private void OnCollisionEnter(Collision collision)                                                                
        {
            // Debug.Log("Hit");
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //��������(�����) - ����� ������ �������� �����
            {
                takeDamage.Hurt(_damage);
                Debug.Log("HitTrap!");
            }
        }
    }
}
