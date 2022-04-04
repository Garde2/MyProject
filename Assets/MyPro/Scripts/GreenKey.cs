using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class GreenKey : MonoBehaviour
    {
        [SerializeField] private bool _inTrigger;

        void OnTriggerEnter(Collider other)
        {
            _inTrigger = true;
        }

        void OnTriggerExit(Collider other)
        {
            _inTrigger = false;
        }

        void Update()
        {
            if (_inTrigger)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    DoorCorridor._greenKey = true;
                    Destroy(this.gameObject);
                }
            }
        }

        void OnGUI()
        {
            if (_inTrigger)
            {
                GUI.Box(new Rect(0, 90, 300,30), "Нажми Z чтоб взять Зеленый ключ!");
                Debug.Log("НАЖМИИИИИ");
            }
        }
    }
}