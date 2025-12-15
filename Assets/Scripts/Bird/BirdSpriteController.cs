using UnityEngine;

public class BirdSpriteController
{
    private Sprite _deadSprite;
    private Sprite _normalSprite;
    
    public BirdSpriteController(Sprite normalSprite, Sprite deadSprite)
    {
        _normalSprite = normalSprite;
        _deadSprite = deadSprite;
    }

    public void SetNormalSprite(ref SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sprite = _normalSprite;
    }
    
    public void SetDeadSprite(ref SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sprite = _deadSprite;
    }
    
}