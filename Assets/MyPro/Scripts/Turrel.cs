using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProjectL
{
    public class Turrel : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _speedRotate;
        [SerializeField] private LayerMask _maskBullet; //enemy player
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _health = 10;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isFire;

        //[SerializeField] private UnityEvent _event;

        void Awake()
        {
            _player = FindObjectOfType<Player>();

        }
        private void Update()   
        {
            Ray ray = new Ray(_spawnPosition.position, transform.forward);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 4))
            {
                Debug.DrawRay(_spawnPosition.position, transform.forward * hit.distance, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.magenta);

                if (hit.collider.CompareTag("Player"))
                {
                    if (_isFire)
                        Fire();
                }

                if (hit.collider.CompareTag("Shield"))
                {
                    if (_isFire)
                        Fire();
                }
            }
            /**
            if (Vector3.Distance(transform.position, _player.transform.position) < 3)
            {
                //if (Input.GetMouseButtonDown(1))  //по-моему это надо на игрока прицепить
                    Fire();
                    //_isFire = true;
                    
            }
            **/
        }
       
        void FixedUpdate()
        {
            //динамическое определение типов. если magnitude - а float автоматически, не для полей.
            //а внутри методов можно
            var direction =
                _player.transform.position - transform.position;

            var pr = Vector3.Dot(
                transform.forward,
                direction);
            var abs = Mathf.Abs(pr);
            var rad = Mathf.Sin(abs);
            var deg = rad * Mathf.Rad2Deg;  //*57.3                        

            direction.Set(direction.x, 0, direction.z);
            var stepRotate = Vector3.RotateTowards(transform.forward,
                    direction,
                    _speedRotate * Time.fixedDeltaTime,
                    0f);

            transform.rotation = Quaternion.LookRotation(stepRotate);
        }

        private void Fire()
        {
            _isFire = false;

            var bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var bullet = bulletObj.GetComponent<Bullet>();
            bulletObj.layer = LayerMask.NameToLayer("Player"); //или   bulletObj.layer = _maskBullet;
            bullet.Init(10, 0.6f);
            //_event?.Invoke();
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }

        public void Hurt(float _damage)
        {
            print("OuchTurrel: " + _damage);

            _health -= _damage;

            if (_health <= 0)
            {
                print("DeadTurrelHurt...");
                Destroy(gameObject);                
            }
        }       
    }
}
