using Oisio.Game;

namespace Oisio.Agent
{
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

        public override int CollectableAmount
        {
            get
            {
                return GameConfig.ArrowCollectionAmount;
            }
        }
        #endregion
    }
}