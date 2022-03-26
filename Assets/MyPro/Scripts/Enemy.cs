using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MyProjectL
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _health = 10f;
        [SerializeField] private float _speedRotate = 200f;
        [SerializeField] private bool _isFire;
        [SerializeField] private UnityEvent _event;  //делегат от юнити
        [SerializeField] private Color color;
        [SerializeField] private bool _isVisible;
        [SerializeField] private float _speed = 1.5f;

        //[SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Vector3 direction;

        private void Awake()
        {
            _player = FindObjectOfType<Player>(); //var q = new Quaternion(1, 1, 1, 1);               // x, y, z, w
            //_agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            
            //_agent.SetDestination(_player.transform.position);
        }

        private void Update()
        {
            Ray ray = new Ray(_spawnPosition.position, transform.forward);  //откуда идет и направление  -работает с  - transform.position, _player.transform.position

            if (Physics.Raycast(ray, out RaycastHit hit, 4))  //луч, возвращаемая переменная, дистанция(можно убрать), слой (можем триггер со слоем),
                                                              //сталкив с колл или еще и с триггерами
                                                              //первый объект в луче берем. RaycastAll - все объекты в луче с кот он столкнулся - массив
            {
                Debug.DrawRay(_spawnPosition.position, transform.forward * hit.distance, Color.blue);//можно еще продолжительность
                Debug.DrawRay(hit.point, hit.normal, Color.magenta); //старт, направление

                if (hit.collider.CompareTag("Player"))  //можем дистанию фактическую, нормаль - в какую сторону напр плоскость
                                                        //(разворач спецэфф в нужную сторону), удар, пойнт - координаты(можем спецэффект)
                {
                    if (_isFire)
                        Fire();
                }

                if (hit.collider.CompareTag("Shield"))
                {
                    if (_isFire)
                        Fire();
                }

                if (Vector3.Distance(transform.position, _player.transform.position) < 4)    // без луча только эта радость в апдейте
                {
                    if (_isFire)
                        Fire();
                }

                //if (NavMesh.SamplePosition(_agent.transform.position, out NavMeshHit navMeshHit, 0.2f, NavMesh.AllAreas))
                //    print(NavMesh.GetAreaCost((int)Mathf.Log(navMeshHit.mask, 2)));


                // int[] values = new int[5];
                // var value = values[Random.Range(0, values.Length)];                   

                /**
                if (Vector3.Distance(transform.position, _player.transform.position) < 3)
                {
                    //if (Input.GetMouseButtonDown(1))  //по-моему это надо на игрока прицепить
                        Fire();
                        //_isFire = true;
                        
                }
                **/
            }
        }
        private void FixedUpdate()
        {
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

            Move2(Time.fixedDeltaTime);                         

        }
        public void Hurt(float _damage)
        {
            print("OuchGhost: " + _damage);

            _health -= _damage; ;

            if (_health <= 0)
            {
                Destroy(gameObject);  //или сделать метод Die
            }
        }

        private void Fire()
        {
            _isFire = false;
            var shieldObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Bullet>();
            shield.Init(_player.transform, 10, 0.6f);
            _event?.Invoke();  //проверка на нуль (?)
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }
        private void Move2(float delta)
        {
            var direction =
                _player.transform.position + transform.position;
            

            var fixedDirection = transform.TransformDirection(direction.normalized);
            transform.position += _speed * delta * fixedDirection;   //переменная режет отрезок на маленькие, чтоб можно и в апдейте и в фикседапдейте использоватью вектор = направление * скорость

            //+= потому что мы к текущей позиции прибавляем прирост
            var parent = transform.parent;
        }
    }
}
