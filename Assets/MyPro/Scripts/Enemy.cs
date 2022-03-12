using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Enemy : MonoBehaviour
    {
        [HideInInspector] public int level = 1;
                
        public GameObject ghostPrefab;
        public Transform spawnPositionGhost;
        
        public int maxenemy = 3;
        public float TimerSpawn = 5f;
        private bool _isSpawnGhost;

        void Start()
        {
            SpawnGhost();
        }

        void FixedUpdate() //����� � ���� ��� ���� ������ - ������ �� ������ � ������ ������. ���� �� ������������. ������?
        {
            int i;
            for (i = 0; i <= maxenemy; i++)
            {

                if (TimerSpawn > 0)
                {
                    TimerSpawn -= Time.deltaTime;
                }
                else if (TimerSpawn <= -1)    // ���� ������� ���� - ��� ��������� ���������� ��������.
                {
                    SpawnGhost();

                }
            }
                

            
        }
        void SpawnGhost()
        {
            
                var ghostObj = Instantiate(ghostPrefab, spawnPositionGhost.position, spawnPositionGhost.rotation);
                var ghost = ghostObj.GetComponent<Ghost>();

                ghost.Hurt(10 * level);
            
           
        }
        
    }
}

