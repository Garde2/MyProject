using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class DoorLevel2 : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _rotatePoint;
        [SerializeField] private bool _isStopped;        
        public GameObject TextOpenDoorV;
        public GameObject TextNeedKeyV;
        public GameObject TextCongrats;
        public bool _isLocked;
        public int id;


        private readonly int IsOpen = Animator.StringToHash("IsOpen");

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _isLocked)
            {
                if (other.TryGetComponent(out IStorage storage))
                {
                    if (storage.IsGetKey(id))
                    {
                        TextOpenDoorV.SetActive(true);
                    }
                    else
                    {
                        TextNeedKeyV.SetActive(true);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                TextOpenDoorV.SetActive(false);
                TextNeedKeyV.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && _isLocked)
            {
                if (other.TryGetComponent(out IStorage storage))
                {
                    if (storage.IsGetKey(id))
                    {
                        OnDoor();
                    }

                }
            }
        }

        void OnDoor()
        {

            if (Input.GetKey(KeyCode.X))
            {
                _isLocked = false;
                //if (Player._greenKey){}
                TextOpenDoorV.SetActive(false);
                TextNeedKeyV.SetActive(false);
                _anim.SetBool(IsOpen, true);
                Debug.Log("Open");
                TextCongrats.SetActive(true);
            }
        }
    }


}