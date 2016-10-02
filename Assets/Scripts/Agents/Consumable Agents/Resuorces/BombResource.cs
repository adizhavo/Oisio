using Oisio.Game;

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

        public override int CollectableAmount
        {
            get
            {
                return GameConfig.BombCollectionAmount;
            }
        }
        #endregion
    }
}