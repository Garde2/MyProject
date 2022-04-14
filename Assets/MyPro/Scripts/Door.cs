using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyProjectL
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _rotatePoint;
        [SerializeField] private bool _isStopped;
        private readonly int IsOpen = Animator.StringToHash("IsOpen"); //экономит такты процессов
        [SerializeField] private string[] tags;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach (string tag in tags)
            {
                if (other.CompareTag(tag) && !_isStopped)
                {
                    _anim.SetBool(IsOpen, true);
                    //_rotatePoint.Rotate(Vector3.up, -90);       //повернуться вокруг мировой оси на - 90 градусов
                    break;
                }                
            }            
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (string tag in tags)
            {
                if (other.CompareTag(tag) && !_isStopped)
                {
                    _anim.SetBool(IsOpen, false);
                    //_rotatePoint.Rotate(Vector3.up, -90);       //повернуться вокруг мировой оси на - 90 градусов
                    break;
                }
            }
        }        
    }
}