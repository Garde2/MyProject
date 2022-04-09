using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Key : MonoBehaviour
    {
        public int id;
        /** первый вариант
        [SerializeField] private bool _inTrigger;        
        public GameObject TextTakeKey;
        //[SerializeField] private float lifetime = 5f;
        public GameObject TextTookKey;        

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _inTrigger = true;                
            }

            if (other.CompareTag("Enemy"))
            {
                _inTrigger = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _inTrigger = false;
            }
            if (other.CompareTag("Enemy"))
            {
                _inTrigger = false;
            }
        }

        void Update()
        {
            if (_inTrigger)
            {
                TextTakeKey.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //Player._green_Key = true;  // != null
                    Destroy(this.gameObject);
                    TextTakeKey.SetActive(false);
                    TextTookKey.SetActive(true);                    
                }
            }
        }
        **/
    }
}