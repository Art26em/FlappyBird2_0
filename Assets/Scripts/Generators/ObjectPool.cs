using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private GameObject container;
    [SerializeField] private int capacity;

    private Camera _camera;
    private readonly List<GameObject> _pool = new();

    protected void Initialize()
    {
        _camera = Camera.main;
        for (int i = 0; i < capacity; i++)
        {
            GameObject spawned = Instantiate(template, container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(0, 0.5f));
        
        foreach (var item in _pool)
        {
            if (!item.activeSelf) continue;
            if (item.transform.position.x < disablePoint.x )
            {
                item.SetActive(false);
            }
        }
    }
    
    public bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }
    
    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }
    
}
