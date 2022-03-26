using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        //[SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Transform> _enemies;

        [SerializeField] private float _timeCuldown = 4;

        private void Awake()
        {
            _enemies = new List<Transform>();
        }

        void Start()
        {
            StartCoroutine(Spawner(5));   //���� �������� ��� �������� - ������� ��� ����, ������ �� ������ ���� ��� ��� � ������ �� ������, �� ������ ������
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                StopCoroutine(nameof(Spawner)); //������������� ���� ���������� ��������
            }
        }

        private IEnumerator Spawner(int count)   //� ������� �� ����� �������� - ���������� �������� ��������, ����������
        {
            //while (true)   //������, ������� 5 �����, �������� 10 ���, ������, ����� �����  � ������� ���.
            //{
                for (int i = 0; i < count; i++)             // i < 1000
                {
                    //_enemies.Add(Instantiate(_enemyPrefab, transform.position, Quaternion.identity));
                    _enemies.Add((new GameObject()).GetComponent<Transform>());
                    yield return new WaitForSeconds(_timeCuldown);                            //��������� �����. yeld return null - ���������� ������ 1 ����
                    //waitUntill ���� � ���� � �������� � true, waitwhile - ��������, ����� ���� - ������� �� ��������
                    //wait for endn frame - ���� ���� ��������� ���� ������, �� ������� ���� 0.2 ���
                    //waitForSecond�Realtime - 0 - �����. � ���� ����� �������� ����� ��� - ���������� ���.

                    if (i == 4)         //���� ��������� ���������� �������
                        yield break;
                }

                yield return new WaitForSeconds(4f);

                foreach (var enemy in _enemies)              //���������� � ������� ����� ����������
                {
                    enemy.Rotate(Vector3.up, Time.deltaTime);
                    Destroy(enemy.gameObject);
                    yield return new WaitForSeconds(_timeCuldown);
                }

                _enemies.Clear(); //������ ��� ������� �������, � ���� � ������ �� ������ �������, � �� ������

                //����� �������� ����� ������, ����� ��������� ����� ������ ��������� ��������(�)

            //}

        }
    }
}

