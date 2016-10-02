namespace Oisio.Agent
{
    public class BombResource : ConsumableAgent
    {
        #region implemented abstract members of ConsumableAgent
        public override ConsumableType Item
        {
            get
            {
                return ConsumableType.Bomb;
            }
        }
        #endregion
    }
}