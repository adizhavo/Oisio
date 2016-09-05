public class BombResource : ConsumableAgent
{
    #region implemented abstract members of ConsumableAgent
    public override ConsumableType Type
    {
        get
        {
            return ConsumableType.Bomb;
        }
    }
    #endregion
}