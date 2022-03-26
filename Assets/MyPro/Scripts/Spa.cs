using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyProjectL
{
    public class Spa : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyFrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _timeCuldown;


        [SerializeField] private bool[] _fillPoints;
        [SerializeField] private int _counterPoint = 0;
        [SerializeField] private int _countDie;
        [SerializeField] private List<GameObject> _enemys;

        [SerializeField] private float _timer;

        //[SerializeField] private GameObject _prefabAst;
        //[SerializeField] private float _radiusAst;

        private void Awake()  //инициализируем поля
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
            if (Input.GetKeyDown(KeyCode.K) && _countDie < _spawnPoints.Length) // _enemys.Count > 0)  //  _countDie < _spawnPoints.Length)
            {
                Destroy(_enemys[_counterPoint]);
                _fillPoints[_counterPoint] = false;    //ячейка опустела
                _counterPoint++;//прибавили единичку но как только станет 5 (по кол-ву точек спавна) - ошибка превышения
                _countDie++;
                
                if (_countDie > _spawnPoints.Length)
                    _countDie = _spawnPoints.Length;
                _counterPoint %= _spawnPoints.Length;// поэтому наше значение 5 / длину 5 = и остаток | равен 0
                                                     //5 / 4 = 1 | 1, //7 / 4 = 1 | 3 итд
                _timer = 0;
            }

            if (_timer < _timeCuldown)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                if (_countDie > 0)
                    Spawn(_countDie);  //тут досоздаем необх кол-во мобов исходя из счетчика мертвых                
            }            
        }
        private void Spawn()
        {
            /**var count = Random.Range(_min, _max + 1);            если надо рандом
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity); //! берем поворот что в префабе , можем не Q а можно _enemyPrefab.transform.rotation)

            Vector3 point = Random.insideUnitSphere;

            Instantiate(_prefabAst, transform.position + point * _radiusAst, Random.rotation); // создали в точке спавнера+ случ точка умнож на радиус области и случайный поворот
            **/
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _enemys.Add(Instantiate(_enemyFrefab, _spawnPoints[i].position, Quaternion.identity));
                _fillPoints[i] = true;
            }
            _countDie = 0;
        }
        private void Spawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                /**var freePoint = _fillPoints.FirstOrDefault(point => !point); перебираем массив, берет фипойнтс и подставляет их, проверяем их не пустой ли.
                по условию получаем первый объект удовлетв условию
                **/
                for (var j = 0; j < _fillPoints.Length; j++)
                {
                    if (!_fillPoints[j])
                    {
                        _enemys[j] = Instantiate(_enemyFrefab, _spawnPoints[j].position, Quaternion.identity);
                        _fillPoints[j] = true;
                        break;
                    }                        
                }   
            }
            _countDie = 0;
        }
    }
}