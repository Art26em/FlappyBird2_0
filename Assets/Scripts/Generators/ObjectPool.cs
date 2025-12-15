using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool<T> where T : Component
{
    private int _capacity;
    private readonly Camera _camera;
    private readonly List<T> _pool;

    public ObjectPool(int capacity)
    {
        _camera = Camera.main;
        _pool = new List<T>(capacity);
    }
    
    public void Add(T obj)
    {
        _pool.Add(obj);       
    }
    
    public void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(0, 0.5f));

        foreach (var item in _pool.
                     Where(item => item.gameObject.activeSelf).
                     Where(item => item.gameObject.transform.position.x < disablePoint.x))
        {
            
            item.gameObject.SetActive(false);
        }
    }

    public bool TryGetObject(out T result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
        return result != null;
    }
    
    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.gameObject.SetActive(false);
        }
    }
    
}
