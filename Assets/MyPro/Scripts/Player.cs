using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProjectL
{
    public class Player : MonoBehaviour  //у монобих конструктор закрыт, наследника не вызвать. содает экземпляр.
    {
        //public KeyCode keySpell1;
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _cooldown = 1;
        [SerializeField] private bool _isFire;
        [SerializeField] private UnityEvent _event;

        [SerializeField] private float _jumpForce = 10f;

        [SerializeField] private GameObject _mine; // Наша мина
        [SerializeField] private Transform mineSpawnPlace; // точка, где создается мина
        [SerializeField] private bool _isSpawnMine;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
       

        public GameObject shieldPrefab;
        public Transform spawnPosition;

        [SerializeField] private bool _isSpawnShield;
        
        [HideInInspector] public int level = 1;  //так бы видели в юнити, но спрятали в инспекторе

        [SerializeField] private Vector3 _direction;    //x - право, z - вперед, y - вверх
        [SerializeField] private float speed = 2f;
        [SerializeField] private float _speedRotate = 200f;           //для поворота мышкой
        [SerializeField] private bool _isSprint;
        [SerializeField] private UnityEvent _event2;


        private void Start()
        {
            _enemy = FindObjectOfType<Enemy>();

            _health += Time.deltaTime;
            if (Mathf.Approximately(_health, 0))    //+ - эпсилон, сравнивает
            {
                Debug.Log("Health = 0");
                enabled = false;
            }
            /**
            var point1 = new Vector3(1f, 5f, 1434f);
            var point2 = new Vector3(15f, 5f, 143f);

            var dist = Vector3.Distance(point1, point2);
            Debug.Log(dist);
            Debug.Log(Vector3.one.magnitude);
            **/
        }

        void Update() //привязан к фпс                 
        {
            Ray ray = new Ray(_spawnPosition.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 4))
            {
                Debug.DrawRay(_spawnPosition.position, transform.forward * hit.distance, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.magenta);

                if (hit.collider.CompareTag("Enemy"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_isFire)
                            Fire();
                    }
                }                
            }           

            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _isSprint = Input.GetKey(KeyCode.C);      //Input.GetButton("Sprint");

            if (Input.GetKeyDown(KeyCode.Space))
                GetComponent<Rigidbody>().AddForce(Vector3.up
                    * _jumpForce, ForceMode.Impulse);
            
            // Если нажата кнопка  
            if (Input.GetKey(KeyCode.F))
                _isSpawnMine = true;

           // if (Input.GetKey(KeyCode.C)) _isSprint = true;   //так он не прекращает носиться, но если while - я поставила мину и игра встала. хотя в коде ничего не предвещало)

            if (Input.GetKey(KeyCode.R))     //if (Input.GetMouseButtonDown(0)) кнопки мышки
                _isSpawnShield = true;


            /** движение длинное
             if (Input.GetKey(KeyCode.W))
                _direction.z = 1;                    //назад
            else if (Input.GetKey(KeyCode.S))
                _direction.z = -1;
            else
                _direction.z = 0;                //если не нажата - стоим
            */

        }
        public void FixedUpdate()
        {
            var direction = _enemy.transform.position - transform.position;

            var pr = Vector3.Dot(transform.forward, direction);
            var abs = Mathf.Abs(pr);
            var rad = Mathf.Sin(abs);
            var deg = rad * Mathf.Rad2Deg;

            
            if (_isSpawnShield)
            {
                _isSpawnShield = false;
                SpawnShield();
            }
            
            if (_isSpawnMine)
            {
                _isSpawnMine = false;
                SpawnMine();                
            }           

            Move(Time.fixedDeltaTime);   //тк стабильно 0.2   а если в Update дельтатайм - будет постоянно изменяться

            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * _speedRotate * Time.fixedDeltaTime);
            //transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speedRotate * Time.fixedDeltaTime, 0));  //поворачиваем через мышку
            //по Х - самого игрока, по У - от первого лица камеру
        }

        public void SpawnMine()
        {
            _isSpawnMine = false;
            var mineObj = Instantiate(_mine, mineSpawnPlace.position, mineSpawnPlace.rotation);
           var mine = mineObj.GetComponent<Mine>();
           mine.Init(3);            
        }

        public void SpawnShield()
        {
            _isSpawnShield = false;
            var shieldObj = Instantiate(shieldPrefab, spawnPosition.position, spawnPosition.rotation); //получили ссылку на объект
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(10 * level);
            _event?.Invoke();  //проверка на нуль (?)
            shield.transform.SetParent(spawnPosition);

            #region Note
            //получилили ссылку на экз класса. какой класс мы ищем - совершенно конкретный - в <> Ищем класс щит , находящийся на объекте, его экземпляр, созданный Instantiate через new, внутренний конструктор
            //если будет искать трансформ, а не щит, то найдет компонент трансформ
            //найти можем только то(по ссылке), что на объекте
            //чтобы найти на родственных - GetComponentInChildren
            //вспомнить про tag

            // gameObject.AddComponent();  можем создавать динамически без префабов
            //transform.GetChild - получаем дочерний по индексу (как в юнити в папочке  джона лемона они лежат)
            //transform.ChildCount - 4 компонента
            //можем сменить родителя shield.transfom.SetParent(spawnPosition) - станет дочерним к точке спауна
            #endregion
        }

        public void Move(float delta)
        {
            var fixedDirection = transform.TransformDirection(_direction.normalized);
            transform.position += (_isSprint ? speed * 2 : speed) * delta * fixedDirection;   //переменная режет отрезок на маленькие, чтоб можно и в апдейте и в фикседапдейте использоватью вектор = направление * скорость

            //+= потому что мы к текущей позиции прибавляем прирост
            var parent = transform.parent;
        }
        private void Fire()
        {
            _isFire = false;
            var shieldObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Bullet>();
            shield.Init(_enemy.transform, 10, 1.2f);
            //_event?.Invoke();
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }
        public void Hurt(float _damage)
        {
            print("OuchLemon: " + _damage);

            _health -= _damage; ;

            if (_health <= 0)
            {                
                print("OuchLemon: " + "Dead....");
            }
        }
    }
}

