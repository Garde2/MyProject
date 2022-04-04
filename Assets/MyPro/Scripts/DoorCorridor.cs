using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class DoorCorridor : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _rotatePoint;
        [SerializeField] private bool _isStopped;
        
        public static bool _greenKey;
        [SerializeField] private bool _inTrigger;
        [SerializeField] private bool _inTrigger2;

        private readonly int IsOpen = Animator.StringToHash("IsOpen"); //экономит такты процессов

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {         

            if (_inTrigger)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _anim.SetBool(IsOpen, true);
                    Debug.Log("Open");
                    _inTrigger = false;
                }
            }            
            else
            {
                _inTrigger = false;
                _inTrigger2 = false;
            }
            
        }

        private void OnTriggerEnter(Collider other)  //ссылка на коллайдер который зашел в триггер
        {
            if (other.CompareTag("Player") && !_isStopped && _greenKey)
            {
                _inTrigger = true;
                _inTrigger2 = false;
            }
            else
            {
                _inTrigger2 = true;
                _inTrigger = false;
            }
            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _inTrigger2 = false;
                _inTrigger = false;
                _anim.SetBool(IsOpen, true);
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
            if (other.CompareTag("Player") && !_isStopped)
            {                
                _anim.SetBool(IsOpen, false);
                _inTrigger = false;
                _inTrigger2 = false;
            }

            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _anim.SetBool(IsOpen, false);
                _inTrigger = false;
                _inTrigger2 = false;
            }
        }

        private void OnTriggerStay(Collider other)  //аналог апдейта но для тех, кто в триггере
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.Keypad5))
                    _anim.enabled = false;
                _inTrigger = false;
                _inTrigger2 = false;
            }

            if (other.CompareTag("Enemy"))      
            {
                if (Input.GetKeyDown(KeyCode.Keypad6))
                    _anim.enabled = false;
                _inTrigger = false;
                _inTrigger2 = false;
            }
        }
        void OnGUI()
        {
            if (_inTrigger)
            {
                GUI.Box(new Rect(0, 60, 300, 30), "Нажми Z чтоб открыть дверь!");
            }
            if (_inTrigger2)
            {
                GUI.Box(new Rect(0, 0, 300, 30), "Вам нужен Зеленый Ключ!");
                GUI.Box(new Rect(0, 30, 350, 30), "Привидение: а мне не нужен ключ...");
            }

        }
    }
    

}