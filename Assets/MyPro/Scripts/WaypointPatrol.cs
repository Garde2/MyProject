using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyProjectL
{
    public class WayPointPatrol : MonoBehaviour
    {
        [SerializeField] private Player _player;
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;
        int m_CurrentWaypiontIndex;
        private bool _isNear;
        private bool _isOldNear;
        private bool _isPursue;


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
            //написать bool для преследования, если игрок входит в зону, то стопкорутина
            _isNear = Vector3.Distance(transform.position, _player.transform.position) < 4;
            
            if (_isNear)
            {
                navMeshAgent.SetDestination(_player.transform.position);                
            }
            else
            {
                if (_isOldNear)  //если раньше уже видели
                {
                    _isPursue = true;
                    navMeshAgent.SetDestination(transform.position);
                    Invoke(nameof(Waiting), 1f);
                }
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !_isPursue)    //написать bool для преследования, если игрок входит в зону, то стопкорутина
                                                                                        //если выходит - продолжаем корутину с патрулем
                {
                    m_CurrentWaypiontIndex = (m_CurrentWaypiontIndex + 1) % waypoints.Length;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypiontIndex].position);
                }
            }
            _isOldNear = _isNear;
        }

        private void Waiting()
        {
            _isPursue = false;

        }

        
    }
}
