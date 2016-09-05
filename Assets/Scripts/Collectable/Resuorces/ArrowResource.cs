public class ArrowResource : ConsumableAgent
{
    #region implemented abstract members of ConsumableAgent
    public override ConsumableType Item
    {
        get
        {
            return ConsumableType.Arrow;
        }
    }
    #endregion
}