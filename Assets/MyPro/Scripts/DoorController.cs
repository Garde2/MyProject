using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProjectL
{
    public class MyProject : MonoBehaviour
    {
        public float distance = 1f;

        List<Key> keyList;
        private readonly int IsOpen = Animator.StringToHash("IsOpen");
        [SerializeField] private Animator _anim;
        public GameObject TextNeedKey;        

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void Start()
        {
            keyList = new List<Key>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, distance))
                {
                    if (hit.collider.tag == "DoorCorridor")
                    {
                        DoorCorridor door = hit.collider.GetComponent<DoorCorridor>();
                        if (door._isLocked)
                        {
                            for (int i = 0; i < keyList.Count; i++)
                            {
                                if(keyList[i].id == door.id)
                                {
                                    door._isLocked = false;
                                    _anim.SetBool(IsOpen, true);
                                    //keyList.Remove(keyList[i]);   //удаляем ключ после использования
                                }
                                else
                                {
                                    TextNeedKey.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            _anim.SetBool(IsOpen, false);
                        }
                    }
                }

                if (hit.collider.GetComponent<Key>())
                {
                    Key key = hit.collider.GetComponent<Key>();
                    keyList.Add(key);
                    Debug.Log(keyList.Count);
                    Destroy(key.gameObject);
                    
                }
            }
        }
    }
   

}

