using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _rotatePoint;
        [SerializeField] private bool _isStopped;
        private readonly int IsOpen = Animator.StringToHash("IsOpen"); //экономит такты процессов

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)  //ссылка на коллайдер который зашел в триггер
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _anim.SetBool(IsOpen, true);

                //_rotatePoint.Rotate(Vector3.up, 90);
                //transform.rotation = Quaternion.LookRotation(transfrom.position + Vector3.left);
                //_rotatePoint = Quaternion.LookRotation(transfrom.position + Vector3.left);
            }

            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _anim.SetBool(IsOpen, true);  //не только строку, но и ID (числовой параметр), лучше строки не юзать. ридер позволяет кэшировать строку, получая число
                //_rotatePoint.Rotate(Vector3.up, 90);  //повернуться вокруг мировой оси на 90 градусов
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _anim.SetBool(IsOpen, false);
                //_rotatePoint.Rotate(Vector3.up, -90);       //повернуться вокруг мировой оси на - 90 градусов
            }

            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _anim.SetBool(IsOpen, false);
                //_rotatePoint.Rotate(Vector3.up, -90);
            }
        }

        private void OnTriggerStay(Collider other)  //аналог апдейта но для тех, кто в триггере
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.Keypad5))
                    _anim.enabled = false;
                    //_isStopped = true;
            }

            if (other.CompareTag("Enemy"))      //если не прописать энеми везде - дверь будет крутиться на следующий шаг
                                                //вокруг оси при каждом вхождении в триггер
            {
                if (Input.GetKeyDown(KeyCode.Keypad6))
                    _anim.enabled = false;
                //_isStopped = true;
            }
        }
    }
}