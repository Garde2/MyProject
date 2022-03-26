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
            StartCoroutine(Spawner(5));   //если поставим без корутины - пройдет раз цикл, дойдет до ретурн вайт фор сек и выйдет из метода, не пойдет дальше
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                StopCoroutine(nameof(Spawner)); //останавливаем нашу конкретную корутину
            }
        }

        private IEnumerator Spawner(int count)   //в отличие от инвок рипитинг - использует цифровое значение, количество
        {
            //while (true)   //зайдет, создаст 5 мобов, подождет 10 сек, удалит, снова зайде  и создаст итд.
            //{
                for (int i = 0; i < count; i++)             // i < 1000
                {
                    //_enemies.Add(Instantiate(_enemyPrefab, transform.position, Quaternion.identity));
                    _enemies.Add((new GameObject()).GetComponent<Transform>());
                    yield return new WaitForSeconds(_timeCuldown);                            //указываем врем€. yeld return null - пропускаем только 1 кадр
                    //waitUntill ждет с фолс и работает с true, waitwhile - наоборот, когда фолс - выходит из ожидани€
                    //wait for endn frame - ждем пока сработает лейт апдейт, по фикседу ждет 0.2 сек
                    //waitForSecondыRealtime - 0 - пауза. а если хотим анимацию через код - используем это.

                    if (i == 4)         //чтоб экстренно остановить изнутри
                        yield break;
                }

                yield return new WaitForSeconds(4f);

                foreach (var enemy in _enemies)              //обращаетс€ к энемиес через енумератор
                {
                    enemy.Rotate(Vector3.up, Time.deltaTime);
                    Destroy(enemy.gameObject);
                    yield return new WaitForSeconds(_timeCuldown);
                }

                _enemies.Clear(); //потому что объекты удалили, а надо и ссылки на €чейки удалить, а то ошибка

                //можно наверное волны делать, через указанное врем€ делать указанные действи€(с)

            //}

        }
    }
}

