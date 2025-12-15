using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectMover
{
    private readonly float _moveSpeed;

    public ObjectMover(float speed)
    {
        _moveSpeed = speed;
    }

    public void StartObjectMoving(GameObject obj)
    {
        if (obj == null) return;
        _ = MoveObject(obj);    
    }
    
    private async UniTask MoveObject(GameObject obj)
    {
        while (obj != null && obj.activeSelf) 
        {
            obj.transform.Translate(Vector3.left * (_moveSpeed * Time.deltaTime));
            await UniTask.Yield(PlayerLoopTiming.Update);
        }    
    }
    
}
