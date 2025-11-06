using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    
    private RawImage _rawImage;
    private readonly float _step = 0.1f;
    private readonly float _defaultWidth = 1;
    private readonly float _defaultHeight = 1;
    
    private void Start()
    {
        _rawImage = GetComponent<RawImage>();
        _rawImage.uvRect = new Rect(0, 0, _defaultWidth, _defaultHeight);
    }

    private void Update()
    {
        var newRect = _rawImage.uvRect.x > 1 ? 
            new Rect(0, 0, _defaultWidth, _defaultHeight) : 
            new Rect(_rawImage.uvRect.x + (_step * speed * Time.deltaTime), 0, _defaultWidth, _defaultHeight);   
        _rawImage.uvRect = newRect;
    }
}
