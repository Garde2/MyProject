using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProjectL;

public sealed class ShieldGenerator : WeaponFabric  //Нельзя унаследоваться, оптимиз
{
    [SerializeField] private int _level;

    public ShieldGenerator(int level, GameObject spawnPrefab, Transform spawnPoint) : base(spawnPrefab, spawnPoint)
    {
        _level = level;
    }

    public override GameObject Spawn()
    {
        //_isSpawnShield = false;
        var shieldObj = Object.Instantiate(_spawnPrefab, _spawnPoint.position, _spawnPoint.rotation); //получили ссылку на объект
        var shield = shieldObj.GetComponent<Shield>();
        shield.Init(10 * _level);
        shield.transform.SetParent(_spawnPoint);
        #region Note
        //получилили ссылку на экз класса. какой класс мы ищем - совершенно конкретный - в <> Ищем класс щит , находящийся на объекте, его экземпляр, созданный Instantiate через new, внутренний конструктор
        //если будет искать трансформ, а не щит, то найдет компонент трансформ
        //найти можем только то(по ссылке), что на объекте
        //чтобы найти на родственных - GetComponentInChildren
        //вспомнить про tag

        // gameObject.AddComponent();  можем создавать динамически без префабов
        //transform.GetChild - получаем дочерний по индексу (как в юнити в папочке  джона лемона они лежат)
        //transform.ChildCount - 4 компонента
        //можем сменить родителя shield.transfom.SetParent(spawnPosition) - станет дочерним к точке спауна
        #endregion
        return shieldObj;
    }
}


