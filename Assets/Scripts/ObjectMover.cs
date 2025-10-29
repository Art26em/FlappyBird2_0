using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private float _moveSpeed;

    public void Initialize(float speed)
    {
        _moveSpeed = speed;
    }

    public void StartObjectMoving(GameObject obj)
    {
        StartCoroutine(MoveObject(obj));    
    }
    
    private IEnumerator MoveObject(GameObject obj)
    {
        while (obj.activeSelf) 
        {
            obj.transform.Translate(Vector3.left * (2 * Time.deltaTime));
            yield return null;
        }    
    }
    
}
