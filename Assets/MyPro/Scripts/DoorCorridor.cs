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
        [SerializeField] private bool _greenKey = false;
        //[SerializeField] private GameObject greenKeyPrefab;

        private readonly int IsOpen = Animator.StringToHash("IsOpen"); //экономит такты процессов

        private void Awake()
        {
            _anim = GetComponent<Animator>();           

        }

        /** попытка открыть по коллизии
         *void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag("RedKey"))
            {
                _redkey = true;
                _anim.SetBool(IsOpen, true);
                Debug.Log("Open");
            }
            
        }
        **/

        private void OnTriggerEnter(Collider other)  //ссылка на коллайдер который зашел в триггер
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                if (other.CompareTag("GreenKey") == true)
                {
                    Debug.Log("Open");
                    _anim.SetBool(IsOpen, true);
                }
            }

            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _anim.SetBool(IsOpen, true);                 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _anim.SetBool(IsOpen, false);               
            }

            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _anim.SetBool(IsOpen, false);                
            }
        }

        private void OnTriggerStay(Collider other)  //аналог апдейта но для тех, кто в триггере
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.Keypad5))
                    _anim.enabled = false;                
            }

            if (other.CompareTag("Enemy"))      
            {
                if (Input.GetKeyDown(KeyCode.Keypad6))
                    _anim.enabled = false;                
            }
        }
    }
}