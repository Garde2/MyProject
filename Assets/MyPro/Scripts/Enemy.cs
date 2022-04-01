using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Events;

namespace MyProjectL
{
    public class Enemy : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
        
        [SerializeField] private float _health = 10f;
        [SerializeField] private float _speedRotate = 200f;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isFire;
        //[SerializeField] private UnityEvent _event;  //������� �� �����
        [SerializeField] private Color color;
        [SerializeField] private bool _isVisible;
        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private Vector3 direction;
        [SerializeField] private NavMeshAgent _agent;
        
        public enum OffMeshLinkMoveMethod   
        {
            Teleport,
            NormalSpeed,
            Parabola
        }

        [RequireComponent(typeof(NavMeshAgent))]

        public class AgentLinkMover : MonoBehaviour
        {
            public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;

            IEnumerator Start()
            {
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.autoTraverseOffMeshLink = false;
                while (true)
                {
                    if (agent.isOnOffMeshLink)
                    {
                        if (method == OffMeshLinkMoveMethod.NormalSpeed)
                            yield return StartCoroutine(NormalSpeed(agent));   // null - ������� ����� � update, for Second/Fixed upd, ��� ������ corutine
                        else if (method == OffMeshLinkMoveMethod.Parabola)
                            yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));  //���� ��������, � 0.5f ��������
                        agent.CompleteOffMeshLink();
                    }
                    yield return null;
                }
            }

            IEnumerator NormalSpeed(NavMeshAgent agent)   //����� ������ ��� JumpSpeed ��� �������� ������ (����������/����� = �������� � ������)
            {
                OffMeshLinkData data = agent.currentOffMeshLinkData;
                Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
                while (agent.transform.position != endPos)
                {
                    agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
                    yield return null;
                }
            }

            IEnumerator Parabola(NavMeshAgent agent, float height, float duration)   //� duration ��������
            {
                OffMeshLinkData data = agent.currentOffMeshLinkData;
                Vector3 startPos = agent.transform.position;
                Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
                float normalizedTime = 0.0f;
                while (normalizedTime < 1.0f)
                {
                    float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                    agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                    normalizedTime += Time.deltaTime / duration;
                    yield return null;
                }
            }
        }      

        private void Awake()
        {
            _player = FindObjectOfType<Player>();          //var q = new Quaternion(1, 1, 1, 1);               // x, y, z, w
            _agent = GetComponent<NavMeshAgent>();
        }
        //private void Start()
        //{
            //var direction = _player.transform.position - transform.position;
            //_agent.SetDestination(_player.transform.position);
            //reviewer �������, ��� ���� ������, ������ ���� � �������, ���� ������ ���������
            //� ����������� � ����� ����� ��������, �� ������ ���� �������, ��� ��� ����� - ���� � Awake, ������������ � Start. 
            //������, ������� ������ ��������.
        //}

        private void Update()
        {                       
            Ray ray = new Ray(_spawnPosition.transform.position, transform.forward);  //������ ���� � ����������� 

            if (Physics.Raycast(ray, out RaycastHit hit, 4))  //���, ������������ ����������, ���������(����� ������), ���� (����� ������� �� �����),
                                                              //������� � ���� ��� ��� � � ����������
                                                              //������ ������ � ���� �����. RaycastAll - ��� ������� � ���� � ��� �� ���������� - ������
            {
                Debug.DrawRay(_spawnPosition.position, transform.forward * hit.distance, Color.blue);//����� ��� �����������������
                Debug.DrawRay(hit.point, hit.normal, Color.magenta); //�����, �����������

                if (_isVisible == true)
                {                    
                    _agent.SetDestination(_player.transform.position);

                    if (hit.collider.CompareTag("Player"))  //����� �������� �����������, ������� - � ����� ������� ���� ���������
                                                            //(�������� ������� � ������ �������), ����, ����� - ����������(����� ����������)
                    {
                        if (_isFire)
                            Fire();
                    }

                    if (hit.collider.CompareTag("Shield"))
                    {
                        if (_isFire)
                            Fire();
                    }
                }                                
            }

            //if (NavMesh.SamplePosition(_agent.transform.position, out NavMeshHit navMeshHit, 1f, NavMesh.AllAreas))
            //    print(NavMesh.GetAreaCost((int)Mathf.Log(navMeshHit.mask, 4)));
            /**  ��� ���� �������� � Areas �� �������� �������� �����������, � ������������ ������ ��� �������� � �������. 
            ������� ���� ����� ����� �� ������� �������� 0.2f, ��� �������. � ��������� ��� � �������� - ������� ���������� ������ � �������� ��� ��� ���.
            ���� ����� ������� - ������ ��� - �������� ����: �����, ���� ������� (� float) � areas, �� �� ��� ��������� �������� 3,4,5 ����, �� ����
            ���� ������. � ����� ���������� �������� ���.
            ����� ��������� �������� � ���� �������.
             **/
            //int[] values = new int[5];
            //var value = values[Random.Range(0, values.Length)];
        }

        private void FixedUpdate()
        {            
            var direction =
                _player.transform.position - transform.position;

            var pr = Vector3.Dot(
                transform.forward,
                direction);
            var abs = Mathf.Abs(pr);
            var rad = Mathf.Sin(abs);
            var deg = rad * Mathf.Rad2Deg;  //*57.3
                        
            direction.Set(direction.x, 0, direction.z);
            var stepRotate = Vector3.RotateTowards(transform.forward,
                    direction,
                    _speedRotate * Time.fixedDeltaTime,
                    0f);

            transform.rotation = Quaternion.LookRotation(stepRotate);
             
            //Move2(Time.fixedDeltaTime);
            if (Vector3.Distance(transform.position, _player.transform.position) < 4)    // ��� ���� ������ ��� ������� � �������
            {
                if (_isFire)
                    Fire();
            }
        }

        public void Hurt(float _damage)
        {
            print("OuchGhost: " + _damage);

            _health -= _damage;

            if (_health <= 0)
            {
                print("DeadGhost...");
                Destroy(gameObject);  
            }
        }

        private void Fire()
        {
            _isFire = false;
            var shieldObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Bullet>();
            shield.Init(10, 0.6f);
            //event?.Invoke();  //�������� �� ���� (?)
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _isFire = true;
        }

        private void Move2(float delta)
        {
            var direction =
                _player.transform.position - transform.position;

            var fixedDirection = transform.TransformDirection(direction.normalized);
            transform.position += _speed * delta * fixedDirection;   //���������� ����� ������� �� ���������, ���� ����� �
                                                                     //� ������� � � ������������� ������������� ������ = ����������� * ��������
            //+= ������ ��� �� � ������� ������� ���������� �������
            var parent = transform.parent;
        }
    }
}
