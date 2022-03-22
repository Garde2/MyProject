using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class Door : MonoBehaviour
    {

        [SerializeField] private Transform _rotatePoint;

        private bool _isStopped;

        private void OnTriggerEnter(Collider other)  //ссылка на коллайдер который зашел в триггер
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, 90);
                //transform.rotation = Quaternion.LookRotation(transfrom.position + Vector3.left);
                //_rotatePoint = Quaternion.LookRotation(transfrom.position + Vector3.left);
            }
            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, 90);  //повернуться вокруг мировой оси на 90 градусов
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, -90);       //повернуться вокруг мировой оси на - 90 градусов
            }
            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, -90);
            }
        }

        private void OnTriggerStay(Collider other)  //аналог апдейта но для тех, кто в триггере
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                    _isStopped = true;
            }
            if (other.CompareTag("Enemy"))      //если не прописать энеми везде - дверь будет крутиться на следующий шаг
                                                //вокруг оси при каждом вхождении в триггер
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    _isStopped = true;
            }
        }
    }
}