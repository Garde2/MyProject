using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProjectL;

public class MineGenerator : WeaponFabric
{
    [SerializeField] private int _level;   


    public MineGenerator(int level, GameObject spawnPrefab, Transform spawnPoint) : base(spawnPrefab, spawnPoint)
    {
        _level = level;        
    }

    public override GameObject Spawn()
    {
        
        var mineObj = Object.Instantiate(_spawnPrefab, _spawnPoint.position, _spawnPoint.rotation); //получили ссылку на объект
        var mine = mineObj.GetComponent<Mine>();
        mineObj.layer = LayerMask.NameToLayer("Enemy");
        if (mine == null)
        {
            mine.Init(1 * 2 * _level);
        }
        
        //mine.transform.SetParent(_spawnPoint);
        #region Note
        //получилили ссылку на экз класса. какой класс мы ищем - совершенно конкретный - в <> »щем класс щит , наход€щийс€ на объекте, его экземпл€р, созданный Instantiate через new, внутренний конструктор
        //если будет искать трансформ, а не щит, то найдет компонент трансформ
        //найти можем только то(по ссылке), что на объекте
        //чтобы найти на родственных - GetComponentInChildren
        //вспомнить про tag

        // gameObject.AddComponent();  можем создавать динамически без префабов
        //transform.GetChild - получаем дочерний по индексу (как в юнити в папочке  джона лемона они лежат)
        //transform.ChildCount - 4 компонента
        //можем сменить родител€ shield.transfom.SetParent(spawnPosition) - станет дочерним к точке спауна
        #endregion
        return mineObj;
    }
}