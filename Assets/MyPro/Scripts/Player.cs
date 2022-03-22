using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProjectL
{
    public class Player : MonoBehaviour  //� ������� ����������� ������, ���������� �� �������. ������ ���������.
    {
        //public KeyCode keySpell1;
        [SerializeField] private float _health = 200f;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isFire;
        [SerializeField] private UnityEvent _event;

        [SerializeField] private float _jumpForce = 10f;

        [SerializeField] private GameObject _mine; // ���� ����
        [SerializeField] private Transform mineSpawnPlace; // �����, ��� ��������� ����
        private bool _isSpawnMine;

        [SerializeField] private Enemy _enemy;
        
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
       

        public GameObject shieldPrefab;
        public Transform spawnPosition;

        private bool _isSpawnShield;
        
        [HideInInspector] public int level = 1;  //��� �� ������ � �����, �� �������� � ����������

        private Vector3 _direction;    //x - �����, z - ������, y - �����
        public float speed = 2f;
        public float _speedRotate = 200f;           //��� �������� ������
        private bool _isSprint;

        private void Start()
        {
            _enemy = FindObjectOfType<Enemy>();

            /**
            var point1 = new Vector3(1f, 5f, 1434f);
            var point2 = new Vector3(15f, 5f, 143f);

            var dist = Vector3.Distance(point1, point2);
            Debug.Log(dist);
            Debug.Log(Vector3.one.magnitude);
            **/
        }


        void Update() //�������� � ���                 //���� �� ��������, ������ ��� ���������� ������ ���� ���� ��� ��������. �� ��������� ����������� ����?
        {
            if (Input.GetKey(KeyCode.R))     //if (Input.GetMouseButtonDown(2)) ������ �����
                _isSpawnShield = true;

            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _isSprint = Input.GetButton("Sprint");

            if (Input.GetKeyDown(KeyCode.Space))
                GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            //if (Input.GetMouseButtonDown(1))           // ��� ��������� ����� ����
            //    _isSpawnShield = true;

            // ���� ������ ������  
            if (Input.GetKey(KeyCode.F))
                _isSpawnMine = true;

            if (Input.GetMouseButtonDown(0))
            {
                if (_isFire)
                    Fire();
            }

            //_isFire = true;                 ��� ��� ��� - ����� �� ������ ������ ����� �� ���. �� 20:43:10 �� ����� �������.

            /** �������� �������
             if (Input.GetKey(KeyCode.W))
                _direction.z = 1;                    //�����
            else if (Input.GetKey(KeyCode.S))
                _direction.z = -1;
            else
                _direction.z = 0;                //���� �� ������ - �����
            */

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
            }

            Move(Time.fixedDeltaTime);   //�� ��������� 0.2   � ���� � Update ���������� - ����� ��������� ����������


            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * _speedRotate * Time.fixedDeltaTime);
            //transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speedRotate * Time.fixedDeltaTime, 0));  //������������ ����� �����
            //�� � - ������ ������, �� � - �� ������� ���� ������
        }


        public void SpawnMine()
        {
           var mineObj = Instantiate(_mine, mineSpawnPlace.position, mineSpawnPlace.rotation);
           var mine = mineObj.GetComponent<Mine>();
           mine.Init(3);            
        }

        public void SpawnShield()
        {
            var shieldObj = Instantiate(shieldPrefab, spawnPosition.position, spawnPosition.rotation); //�������� ������ �� ������
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(10 * level);

            shield.transform.SetParent(spawnPosition);

            #region Note
            //���������� ������ �� ��� ������. ����� ����� �� ���� - ���������� ���������� - � <> ���� ����� ��� , ����������� �� �������, ��� ���������, ��������� Instantiate ����� new, ���������� �����������
            //���� ����� ������ ���������, � �� ���, �� ������ ��������� ���������
            //����� ����� ������ ��(�� ������), ��� �� �������
            //����� ����� �� ����������� - GetComponentInChildren
            //��������� ��� tag

            // gameObject.AddComponent();  ����� ��������� ����������� ��� ��������
            //transform.GetChild - �������� �������� �� ������� (��� � ����� � �������  ����� ������ ��� �����)
            //transform.ChildCount - 4 ����������
            //����� ������� �������� shield.transfom.SetParent(spawnPosition) - ������ �������� � ����� ������
            #endregion
        }

        public void Move(float delta)
        {
            var fixedDirection = transform.TransformDirection(_direction.normalized);
            transform.position += (_isSprint ? speed * 2 : speed) * delta * fixedDirection;   //���������� ����� ������� �� ���������, ���� ����� � � ������� � � ������������� ������������� ������ = ����������� * ��������

            //+= ������ ��� �� � ������� ������� ���������� �������
            var parent = transform.parent;
        }
        private void Fire()
        {
            _isFire = false;
            var shieldObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Bullet>();
            shield.Init(_enemy.transform, 10, 0.6f);
            _event?.Invoke();
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }
        public void Hurt(float _damage)
        {
            print("Ouch: " + _damage);

            _health -= _damage; ;

            if (_health <= 0)
            {
                Destroy(gameObject);  //��� ������� ����� Die
            }
        }
    }
}

