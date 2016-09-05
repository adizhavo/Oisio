public class BombResource : ConsumableAgent
{
    #region implemented abstract members of ConsumableAgent
    public override ConsumableType type
    {
        get
        {
            return ConsumableType.Bomb;
        }
    }
    #endregion
}