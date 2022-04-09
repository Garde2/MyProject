using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProjectL
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float _damage = 3f;
        [SerializeField] private float _lifeTime;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _maskMine;

        private void Awake()
        {
            _enemy = FindObjectOfType<Enemy>();
        }

        public void Init(float lifeTime)
        {
            _lifeTime = lifeTime;
            Destroy(gameObject, lifeTime);
            print("Mine Destroyed...Lifetime");
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 3))
            {
                Collider[] _col = Physics.OverlapSphere(hit.point, 3);

                foreach (var col in _col)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        //_enemy.Hurt(_damage);
                        //Debug.Log("Hit..Mine!");
                        col.GetComponent<Rigidbody>().AddForce(-(hit.point - col.transform.position) * 200);
                        print("...Explosion");
                        Destroy(gameObject);                        

                    }
                    else break;
                }
            }

        }

        private void OnCollisionEnter(Collision collision)  //float radius
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //��������(�����) - ����� ������ �������� �����
            {
                takeDamage.Hurt(_damage);
                Debug.Log("Hit..Mine!");
                Destroy(gameObject);                
            }
            /** ������ ���� � ����� - ����� ����
             Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, 3))
                {
                    Collider[] _col = Physics.OverlapSphere(hit.point, 3);

                    foreach (var col in _col)
                    {
                        if (col.CompareTag("Enemy"))
                        {
                            takeDamage.Hurt(_damage);
                            Debug.Log("Hit..Mine!");                     
                            col.GetComponent<Rigidbody>().AddForce(-(hit.point - col.transform.position) * 200);
                            Destroy(gameObject);
                            print("Mine Destroyed...Explosion");
                            
                        }
                        else break;
                    }
                } 
             **/
        }

        private void OnCollisionExit(Collision collision)
        {
            Destroy(gameObject);
        }
             
    }
}
