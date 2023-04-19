using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _delayBeforeMoving = 2f;
    
     private Transform _start;
     private Transform _end;

     private int _currentStartIndex = 0;
     private int _currentEndIndex = 1;

     private float _movingTime;
    private float _waitTime;


    private void Update()
    {
        _start = _points[_currentStartIndex];
        _end = _points[_currentEndIndex];
        
        var distance = Vector3.Distance(_start.position, _end.position);
        var travelTime = distance / _speed;

        if (_waitTime<_delayBeforeMoving)
        {
            _waitTime += Time.deltaTime;
            return;
        }

        if (_movingTime<travelTime)
        {
            _movingTime += Time.deltaTime;
            var progress = _movingTime / travelTime;
            var newPosition=Vector3.Lerp(_start.position, _end.position, progress);
            transform.position = newPosition;
        }
        
        else 
        {
            _movingTime = 0;
            _waitTime = 0;
            
            _currentStartIndex = FindNextIndex(_currentStartIndex,_points.Length);
            _currentEndIndex = FindNextIndex(_currentEndIndex,_points.Length);
        }
    }

    private int FindNextIndex(int currentIndex, int lenght)
    {
        if (currentIndex==lenght-1)
        {
            currentIndex= 0;
        }
        else if (currentIndex<lenght-1)
        {
            currentIndex++;
        }

        return currentIndex;
    } 
}
