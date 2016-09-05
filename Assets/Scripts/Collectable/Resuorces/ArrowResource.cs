public class ArrowResource : ConsumableAgent
{
    #region implemented abstract members of ConsumableAgent
    public override ConsumableType Type
    {
        get
        {
            return ConsumableType.Arrow;
        }
    }
    #endregion
}