using UnityEngine;

namespace Oisio.Game
{
    public static class GameConfig 
    {
        // Character
        public const int arrowInventorySize = 10;
        public const int smokeBombInvertorySize = 2;
        public const int initialArrows = 10;
        public const int initialBombs = 1;

        //Resources
        public const int ArrowCollectionAmount = 3;
        public const int BombCollectionAmount = 1;

        // Monster
        public const float maxAlertLevel = 1;
        public const float minAlertLevel = 0;
        public const string MONSTER_TAG = "Enemy";
        public const int MAX_TARGABLE_MONSTERS = 3;
               
        // Events   
        public const int projectilePriority = 1;
        public const int characterPriority = 2;
        public const int jumperAttackPriority = 3;
        public const int smokeBombPriotity = 4;
        public static LayerMask EVENT_OBSERVER_IGNORE_MASK = LayerMask.NameToLayer("Monster");
               
        public const float smokeBombEnableTime = 10;
        public const float monsterSmokeEscapeDistance = 2;

        // Pool
        public const string GAMEOBJECT_POOL_NAME = "GameObjectPool";
        public const string GAMEOBJECT_POOL_PATH = "GameObjectPool";

        // Camera
        public const string CAMERA_SHAKE_NAME = "CameraShakeConfig";
        public const string CAMERA_SHAKE_PATH = "CameraShakeConfig";

        // Animations
        public const string MONSTER_PREPARE_ATTACK_ANIM = "PrepareAttack";
        public const string MONSTER_RECOVER_ATTACK_ANIM = "RecoverAttack";

        // Trajectory gizmo
        public const string TRAJECOTRY_GIZMO_PATH = "Game/TrajectoryLineRenderer";
    }
}