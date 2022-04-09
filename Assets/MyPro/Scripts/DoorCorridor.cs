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
        //[SerializeField] private float lifetime = 5f;
        public GameObject TextOpenDoor;      
        public bool _isLocked;
        public int id;


        private readonly int IsOpen = Animator.StringToHash("IsOpen"); //экономит такты процессов

        private void Awake()
        {
            _anim = GetComponent<Animator>();            
        }


        private void OnTriggerExit(Collider other)
        {            
            if (other.CompareTag("Player") && !_isStopped)
            {                
                _anim.SetBool(IsOpen, false);                
                TextOpenDoor.SetActive(false);                
            }

            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _anim.enabled = false;               
            }
        }

        private void OnTriggerStay(Collider other)  //аналог апдейта но для тех, кто в триггере
        {
            if (other.CompareTag("Player"))
            {                
                if (!_isLocked)                          
                {                    
                    OnDoor();
                    TextOpenDoor.SetActive(false);
                }                               
            }

            if (other.CompareTag("Enemy"))      
            {
                _anim.enabled = true;                
            }
        }
        
        void OnDoor()
        {
            if (Input.GetKey(KeyCode.X))
            {
                //if (Player._greenKey){}
                TextOpenDoor.SetActive(false);
                _anim.SetBool(IsOpen, false);
                Debug.Log("Open");

            }
        }
    }
    

}