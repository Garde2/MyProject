using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyProjectL
{
    public class WayPoint : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;
        int m_CurrentWaypiontIndex;

        public void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Start()
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }                
        void Update()
        {
               //�������� bool ��� �������������, ���� ����� ������ � ����, �� ������������

                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)    //�������� bool ��� �������������, ���� ����� ������ � ����, �� ������������
                //���� ������� - ���������� �������� � ��������
            {
                m_CurrentWaypiontIndex = (m_CurrentWaypiontIndex +1) % waypoints.Length;
                navMeshAgent.SetDestination (waypoints[m_CurrentWaypiontIndex].position);
            }
        }
    }

}
