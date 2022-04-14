using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyProjectL
{
    public class Inventory : MonoBehaviour, IStorage
    {
        private List<int> _inventory;
        [SerializeField] private Text _textGreenKey;
        [SerializeField] private Text _textVioletKey;

        public void AddKey(int id)
        {
            if (!_inventory.Contains(id))
            {
                _inventory.Add(id);
                switch (id)
                {
                    case 0:
                        _textGreenKey.text = "1";
                        break;
                    case 1:
                        _textVioletKey.text = "1";
                        break;
                }
            }
        }

        public bool IsGetKey(int id)
        {
            foreach (var item in _inventory)
            {
                if (id == item)
                {
                    return true;
                }
            }
            return false;
        }

        void Awake()
        {
            _inventory = new List<int>();
        }        
    }
}

