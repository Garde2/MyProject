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

        private void OnTriggerEnter(Collider other)  //������ �� ��������� ������� ����� � �������
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, 90);
                //transform.rotation = Quaternion.LookRotation(transfrom.position + Vector3.left);
                //_rotatePoint = Quaternion.LookRotation(transfrom.position + Vector3.left);
            }
            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, 90);  //����������� ������ ������� ��� �� 90 ��������
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, -90);       //����������� ������ ������� ��� �� - 90 ��������
            }
            if (other.CompareTag("Enemy") && !_isStopped)
            {
                _rotatePoint.Rotate(Vector3.up, -90);
            }
        }

        private void OnTriggerStay(Collider other)  //������ ������� �� ��� ���, ��� � ��������
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                    _isStopped = true;
            }
            if (other.CompareTag("Enemy"))      //���� �� ��������� ����� ����� - ����� ����� ��������� �� ��������� ���
                                                //������ ��� ��� ������ ��������� � �������
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    _isStopped = true;
            }
        }
    }
}