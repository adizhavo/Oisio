public class ArrowResource : ResourceCollectable
{
    #region implemented abstract members of ResourceCollectable
    public override CollectableType type
    {
        get
        {
            return CollectableType.Arrow;
        }
    }
    #endregion
}