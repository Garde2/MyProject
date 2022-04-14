using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public interface IStorage
    {
        public bool IsGetKey(int id); //метод
        public void AddKey(int id);

    }

}
