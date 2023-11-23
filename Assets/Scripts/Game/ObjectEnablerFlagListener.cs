public class ObjectEnablerFlagListener : BaseFlagListener
{
    protected override void FlagChecked(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}