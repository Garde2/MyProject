using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Events;


namespace MyProjectL
{
    public class Player : MonoBehaviour, ITakeDamage  //у монобих конструктор закрыт, наследника не вызвать. содает экземпляр.
    {
        //public KeyCode keySpell1;
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _cooldownTime1;
        [SerializeField] private float _cooldownTime2;
        [SerializeField] private bool _cooldown1;
        [SerializeField] private bool _cooldown2;
        [SerializeField] private bool _isFire;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private GameObject _mine; // Наша мина
        [SerializeField] private Transform mineSpawnPlace; // точка, где создается мина
        [SerializeField] private bool _isSpawnMine;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Vector3 _direction;    //x - право, z - вперед, y - вверх
        [SerializeField] private Vector3 _direction2;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float _speedRotate = 200f;           //для поворота мышкой
        [SerializeField] private bool _isSprint;
        //[SerializeField] private UnityEvent _event2;
        [SerializeField] private bool _isSpawnShield;        
        [SerializeField] private bool _isAlive;
        [SerializeField] private Animator _anim;

        [SerializeField] private ShieldGenerator _shieldGenerator;
        [SerializeField] private Gun _gun;

        //[SerializeField] public NavMeshAgent JohnNavigation; //агент это для юнитов в стратегии, стандартная система навигации юнити не используем агенты. меш 2.0 почитать.

        public GameObject shieldPrefab;
        public Transform spawnShieldPosition;
        public GameObject bulletPrefab;
        public Transform spawnBulletPosition;

        [HideInInspector] public int level = 1;  //так бы видели в юнити, но спрятали в инспекторе
                
        private void Awake()
        {            
            _enemy = FindObjectOfType<Enemy>();
            _anim = GetComponent<Animator>();
            _gun = new Gun(bulletPrefab, spawnBulletPosition);
            _shieldGenerator =  new ShieldGenerator(10, shieldPrefab, spawnShieldPosition);
        }

        private void Start()
        {    
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
            /** RayCast
             * Ray ray = new Ray(spawnBulletPosition.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 4))
            {
                Debug.DrawRay(spawnBulletPosition.position, transform.forward * hit.distance, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.magenta);

               if (hit.collider.CompareTag("Enemy"))
               {
                    if (Input.GetMouseButtonDown(0) && _cooldown2)
                    {
                        _isFire = true;                        
                    }
               }                 
            }
            **/

            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _isSprint = Input.GetButton("Sprint");
            _direction2.x = Input.GetAxis("Horizontal2");   // q, e

            _anim.SetBool("IsWalking", _direction != Vector3.zero);

            if (Input.GetKeyDown(KeyCode.Space))                       //_direction.y = Input.GetAxis("Jump");
                GetComponent<Rigidbody>().AddForce(Vector3.up
                    * _jumpForce, ForceMode.Impulse);
            
            // Если нажата кнопка  
            if (Input.GetKeyDown(KeyCode.F))
                _isSpawnMine = true;

            if (Input.GetButtonDown("Sprint"))
                _isSprint = true;
            //if (Input.GetKeyDown(KeyCode.C))
            //_isSprint = true;   //так он не прекращает носиться, но если while - я поставила мину и игра встала. хотя в коде ничего не предвещало)

            if (Input.GetKeyDown(KeyCode.R) && _cooldown1)     //if (Input.GetMouseButtonDown(0)) кнопки мышки
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
            /**старый код
             * var direction = _enemy.transform.position - transform.position;
            var pr = Vector3.Dot(transform.forward, direction);
            var abs = Mathf.Abs(pr);
            var rad = Mathf.Sin(abs);
            var deg = rad * Mathf.Rad2Deg;
            **/
            
            if (_isSpawnShield)
            {
                _isSpawnShield = false;
                _cooldown1 = false;
                StartCoroutine(Cooldown1(_cooldownTime1, 1));
                _shieldGenerator.Spawn();
            }
            
            if (_isSpawnMine)  //добавить по аналогии про мину
            {
                _isSpawnMine = false;
                SpawnMine();                
            }

            if (_isFire)  
            {
                _isFire = false;
                _cooldown2 = false;
                StartCoroutine(Cooldown1(_cooldownTime2, 2));
                _gun.Spawn();
            }                

            Move(Time.fixedDeltaTime);   //тк стабильно 0.2   а если в Update дельтатайм - будет постоянно изменяться            
            
        }

        public void SpawnMine()
        {
            _isSpawnMine = false;
            var mineObj = Instantiate(_mine, mineSpawnPlace.position, mineSpawnPlace.rotation);
            var mine = mineObj.GetComponent<Mine>();
            mine.Init(3);          
        }

        /**public void SpawnShield()
        {
            _isSpawnShield = false;
            var shieldObj = Instantiate(shieldPrefab, spawnPosition.position, spawnPosition.rotation); //получили ссылку на объект
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(10 * level);            
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
        **/

        public void Move(float delta)
        {
            var fixedDirection = transform.TransformDirection(_direction.normalized);
            transform.position += (_isSprint ? speed * 2 : speed) * delta * fixedDirection;   //переменная режет отрезок на маленькие, чтоб можно и в апдейте и в фикседапдейте использоватью вектор = направление * скорость


            /** старые куски
             * += потому что мы к текущей позиции прибавляем прирост
            var parent = transform.parent;       
            **/
        }
        private IEnumerator Cooldown1(float time, int numSpell)
        {
            yield return new WaitForSeconds(time);
            switch (numSpell)
            {
                case 1:
                    _cooldown1 = true;
                    break;
                case 2:
                    _cooldown2 = true;
                    break;
            }
        }

        /** private void Fire()
        {
            _isFire = false;
            //transform.rotation = Quaternion.identity;
            var bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, Quaternion.identity);  // ревьюер исправила Quaternion.identity! теперь стреляет спиной.
            var bullet = bulletObj.GetComponent<Bullet>();
            bullet.Init(_enemy.transform, 10, 0.6f);
            //_event2?.Invoke();
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }
        **/
        public void Hurt(float _damage)
        {
            print("OuchLemon: " + _damage);

            _health -= _damage;

            if (_health <= 0)
            {                
                print("OuchLemon: " + "Dead....");
            }
        }
    }
}

