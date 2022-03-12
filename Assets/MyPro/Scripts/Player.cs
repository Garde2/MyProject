using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MyProjectL
{
    public class Player : MonoBehaviour  //у монобих конструктор закрыт, наследника не вызвать. содает экземпляр.
    {
        [SerializeField] private GameObject _mine; // Наша мина
        [SerializeField] private Transform _mineSpawnPlace; // точка, где создается мина


        public GameObject _shieldPrefab;
        public Transform _spawnPosition;

        private bool _isSpawnShield;
        private bool _isSpawnMine;
        [HideInInspector] public int level = 1;  //так бы видели в юнити, но спрятали в инспекторе

        private Vector3 _direction;    //x - право, z - вперед, y - вверх
        public float speed = 2f;
        private bool _isSprint;
                
        void Update() //привязан к фпс                 //пули по аналогии, только как просчитать пролет мимо щита для удаления. по положению геймобжекту шилд?
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _isSprint = Input.GetButton("Sprint");

            if (Input.GetMouseButtonDown(1))           // для мобильных тапов тоже
                _isSpawnShield = true;
            
            // Если нажата кнопка  
            if (Input.GetMouseButtonDown(0))
                _isSpawnMine = true;
            
            /** движение длинное
             if (Input.GetKey(KeyCode.W))
                _direction.z = 1;                    //назад
            else if (Input.GetKey(KeyCode.S))
                _direction.z = -1;
            else
                _direction.z = 0;                //если не нажата - стоим
            */

        }

        public void SpawnMine()
        {
           var mineObj = Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
           var mine = mineObj.GetComponent<Mine>();
                 
        }

        public void SpawnShield()
        {
            var shieldObj = Instantiate(_shieldPrefab, _spawnPosition.position, _spawnPosition.rotation); //получили ссылку на объект\
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(10 * level);

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

        public void FixedUpdate()
        {
            if (_isSpawnShield)
            {
                _isSpawnShield = false;
                SpawnShield();
            }
            if (_isSpawnMine)
            {
                _isSpawnMine = false;
                SpawnMine();
                Destroy(gameObject, 3f); //3 sec
                
            }

            Move(Time.fixedDeltaTime);   //тк стабильно 0.2   а если в Update дельтатайм - будет постоянно изменяться
        }

        public void Move(float delta)
        {
            transform.position += _direction * (_isSprint ? speed * 2 : speed) * delta;   //переменная режет отрезок на маленькие, чтоб можно и в апдейте и в фикседапдейте использоватью вектор = направление * скорость

            //+= потому что мы к текущей позиции прибавляем прирост

        }

    }
}

