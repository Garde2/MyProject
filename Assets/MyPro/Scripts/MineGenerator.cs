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
        
        var mineObj = Object.Instantiate(_spawnPrefab, _spawnPoint.position, _spawnPoint.rotation); //�������� ������ �� ������
        var mine = mineObj.GetComponent<Mine>();
        mineObj.layer = LayerMask.NameToLayer("Enemy");
        if (mine == null)
        {
            mine.Init(1 * 2 * _level);
        }
        
        //mine.transform.SetParent(_spawnPoint);
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
        return mineObj;
    }
}