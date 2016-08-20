public class BombResource : ResourceCollectable
{
    #region implemented abstract members of ResourceCollectable
    public override CollectableType type
    {
        get
        {
            return CollectableType.Bomb;
        }
    }
    #endregion
}