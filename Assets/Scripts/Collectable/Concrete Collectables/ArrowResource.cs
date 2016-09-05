public class ArrowResource : ConsumableAgent
{
    #region implemented abstract members of ConsumableAgent
    public override ConsumableType type
    {
        get
        {
            return ConsumableType.Arrow;
        }
    }
    #endregion
}