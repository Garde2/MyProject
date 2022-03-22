using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyProjectL
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private float _timeCuldown;

        [SerializeField] private bool[] _fillPoints;
        [SerializeField] private int _counterPoint = 0;
        [SerializeField] private int _countDie;
        [SerializeField] private List<GameObject> _enemys;
        [SerializeField] private float _timer;

        [SerializeField] private int _min;
        [SerializeField] private int _max;

        private void Awake()
        {
            _enemys = new List<GameObject>();
            _fillPoints = new bool[_spawnPoints.Length];
        }

        private void Start()
        {
            Spawn();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && _countDie < _spawnPoints.Length)
            {
                Destroy(_enemys[_counterPoint]);
                _fillPoints[_counterPoint] = false;
                _counterPoint++;
                _countDie++;
                if (_countDie > _spawnPoints.Length)
                    _countDie = _spawnPoints.Length;
                _counterPoint %= _spawnPoints.Length;
                //4 / 4 = 1 | 0
                //5 / 4 = 1 | 1                   //это мне пока не нужно, потом сделаю рандомный спаун  с привидениями в куске большого зала 
                //6 / 4 = 1 | 2
                //7 / 4 = 1 | 3
                _timer = 0;
            }

            if (_timer < _timeCuldown)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                if (_countDie > 0)
                    Spawn(_countDie);
            }
        }

        private void Spawn()
        {
            var count = Random.Range(_min, _max + 1);            //если надо рандом

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _enemys.Add(Instantiate(_enemyPrefab, _spawnPoints[i].position, Quaternion.identity));
                _fillPoints[i] = true;
            }

            _countDie = 0;
        }

        private void Spawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                //var freePoint = _fillPoints.FirstOrDefault(point => !point);
                for (var j = 0; j < _fillPoints.Length; j++)
                {
                    if (!_fillPoints[j])
                    {
                        _enemys[j] = Instantiate(_enemyPrefab, _spawnPoints[j].position, Quaternion.identity);
                        _fillPoints[j] = true;
                        break;
                    }
                }
            }
            _countDie = 0;
        }
    }
}
