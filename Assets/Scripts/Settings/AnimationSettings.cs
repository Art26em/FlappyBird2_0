public class AnimationSettings
{
    public float startAnimationDuration {get; private set;}
    public float blinkAnimationDuration {get; private set;}

    public AnimationSettings( 
        float startAnimationDuration, 
        float blinkAnimationDuration)
    {
        this.startAnimationDuration = startAnimationDuration;
        this.blinkAnimationDuration = blinkAnimationDuration;
    }
}