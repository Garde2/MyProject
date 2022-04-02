using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class BigTrap : MonoBehaviour
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private float _damage = 3f;
        [SerializeField] private bool _isHide;

        void Start()
        {
            InvokeRepeating(nameof(Move2), 1f, _cooldown);
        }

        private void Move2()
        {
            if (_isHide)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -1, transform.position.z);
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


        //trap на родителе, а в нем ссылки на шипы  ими управляет
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")) // или можно без кнопки, просто если зашел игрок
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    CancelInvoke(nameof(Move2));
                    Debug.Log("Выключили большой Trap");
                    transform.position = new Vector3(transform.position.x, -1, transform.position.z);
                }
                else
                {
                    //Debug.Log("Пиликаем");
                }
            }
            
        }                   

        private void OnCollisionEnter(Collision collision)
        {
            // Debug.Log("Hit");
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //коллизия(класс) - точка соприк объектов физич
            {                
                takeDamage.Hurt(_damage);
                Debug.Log("HitBigTrap!");
            }
        }
    }
}
