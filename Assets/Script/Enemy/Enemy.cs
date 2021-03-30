using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent nmagent;

    //количество жизней
    public int heals;
    public int maxHeals;

    //номер точки назначения
    int numberWay;

    //Маршрут противника
    Vector3[] way;

    public UnityAction<Enemy> Die;
    public UnityAction<Enemy> Finish;

    public void SetDamage(int damage)
    {
        heals -= damage;

        if(heals < 1)
        {
            Die?.Invoke(this); // TODO: 
        }
    }

    public float getDistanation(out int numberWay)
    {
        numberWay = this.numberWay;
        return nmagent.remainingDistance;
    }

    public void Initialize(Vector3 spawnPosition, Quaternion rotation, Vector3[] way, int Heals)
    {
        this.way = way;
        this.heals = Heals;
        this.maxHeals = Heals;
        this.transform.position = spawnPosition;
        this.transform.rotation = rotation;
        this.numberWay = 0;
    }

    void Update()
    {
        if (nmagent.remainingDistance <= 1f && way.Length - 1 != numberWay)
            nmagent.destination = way[++numberWay];
        else if (nmagent.remainingDistance <= 1f)
            Finish(this);
    }
}
