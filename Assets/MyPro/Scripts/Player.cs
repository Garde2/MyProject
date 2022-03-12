using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MyProjectL
{
    public class Player : MonoBehaviour  //� ������� ����������� ������, ���������� �� �������. ������ ���������.
    {
        [SerializeField] private GameObject _mine; // ���� ����
        [SerializeField] private Transform _mineSpawnPlace; // �����, ��� ��������� ����


        public GameObject _shieldPrefab;
        public Transform _spawnPosition;

        private bool _isSpawnShield;
        private bool _isSpawnMine;
        [HideInInspector] public int level = 1;  //��� �� ������ � �����, �� �������� � ����������

        private Vector3 _direction;    //x - �����, z - ������, y - �����
        public float speed = 2f;
        private bool _isSprint;
                
        void Update() //�������� � ���                 //���� �� ��������, ������ ��� ���������� ������ ���� ���� ��� ��������. �� ��������� ����������� ����?
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            _isSprint = Input.GetButton("Sprint");

            if (Input.GetMouseButtonDown(1))           // ��� ��������� ����� ����
                _isSpawnShield = true;
            
            // ���� ������ ������  
            if (Input.GetMouseButtonDown(0))
                _isSpawnMine = true;
            
            /** �������� �������
             if (Input.GetKey(KeyCode.W))
                _direction.z = 1;                    //�����
            else if (Input.GetKey(KeyCode.S))
                _direction.z = -1;
            else
                _direction.z = 0;                //���� �� ������ - �����
            */

        }

        public void SpawnMine()
        {
           var mineObj = Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
           var mine = mineObj.GetComponent<Mine>();
                 
        }

        public void SpawnShield()
        {
            var shieldObj = Instantiate(_shieldPrefab, _spawnPosition.position, _spawnPosition.rotation); //�������� ������ �� ������\
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(10 * level);

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

            Move(Time.fixedDeltaTime);   //�� ��������� 0.2   � ���� � Update ���������� - ����� ��������� ����������
        }

        public void Move(float delta)
        {
            transform.position += _direction * (_isSprint ? speed * 2 : speed) * delta;   //���������� ����� ������� �� ���������, ���� ����� � � ������� � � ������������� ������������� ������ = ����������� * ��������

            //+= ������ ��� �� � ������� ������� ���������� �������

        }

    }
}

