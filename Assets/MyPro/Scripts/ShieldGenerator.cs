using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProjectL;

public sealed class ShieldGenerator : WeaponFabric  //������ ��������������, �������
{
    [SerializeField] private int _level;

    public ShieldGenerator(int level, GameObject spawnPrefab, Transform spawnPoint) : base(spawnPrefab, spawnPoint)
    {
        _level = level;
    }

    public override GameObject Spawn()
    {
        //_isSpawnShield = false;
        var shieldObj = Object.Instantiate(_spawnPrefab, _spawnPoint.position, _spawnPoint.rotation); //�������� ������ �� ������
        var shield = shieldObj.GetComponent<Shield>();
        shield.Init(10 * _level);
        shield.transform.SetParent(_spawnPoint);
        #region Note
        //���������� ������ �� ��� ������. ����� ����� �� ���� - ���������� ���������� - � <> ���� ����� ��� , ����������� �� �������, ��� ���������, ��������� Instantiate ����� new, ���������� �����������
        //���� ����� ������ ���������, � �� ���, �� ������ ��������� ���������
        //����� ����� ������ ��(�� ������), ��� �� �������
        //����� ����� �� ����������� - GetComponentInChildren
        //��������� ��� tag

        // gameObject.AddComponent();  ����� ��������� ����������� ��� ��������
        //transform.GetChild - �������� �������� �� ������� (��� � ����� � �������  ����� ������ ��� �����)
        //transform.ChildCount - 4 ����������
        //����� ������� �������� shield.transfom.SetParent(spawnPosition) - ������ �������� � ����� ������
        #endregion
        return shieldObj;
    }
}


