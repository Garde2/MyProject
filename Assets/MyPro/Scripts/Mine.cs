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
        //[SerializeField] private UnityEvent _event;

        private void Update()
        {
            Ray ray = new Ray(transform.position, transform.forward);                               
        }

        public void Init(float lifeTime)
        {
            _lifeTime = lifeTime;
            Destroy(gameObject, lifeTime);
            print("Mine Destroyed...Lifetime");

        }
        private void OnCollisionEnter(Collision collision)  //float radius
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //коллизия(класс) - точка соприк объектов физич
            {
                takeDamage.Hit(_damage);
                Debug.Log("HitMine!");
                Destroy(gameObject);
                print("Mine Destroyed...Hitting");
            }                       
        }

        private void OnTriggerEnter(Collider other)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (other.gameObject.CompareTag("Enemy"))
            {
                _enemy.Hurt(_damage);

                if (Physics.Raycast(ray, out RaycastHit hit, 3))
                {
                    Collider[] _col = Physics.OverlapSphere(hit.point, 3);

                    foreach (var col in _col)
                    {
                        if (col.GetComponent<Rigidbody>())
                        {                            
                            //NavMeshAgent.enabled = false;
                            col.GetComponent<Rigidbody>().AddForce(-(hit.point - col.transform.position) * 200);                            
                            Destroy(gameObject);
                            print("DMine Destroyed...Explosion");
                        }                        
                    }
                }
            }            
        }        
    }
}
