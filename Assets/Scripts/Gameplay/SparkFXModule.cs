using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    public sealed class SparkFXModule : BaseFXModule<ParticleSystem, GameObjectEnum>
    {
        public SparkFXModule(GameController gameController, Player player, GameSetup gameSetup) : base(gameController)
        {
            SetFxKey(GameObjectEnum.SparkEffect);
            SetFxPool(new SparksPool(gameSetup));
            player.FXTriggerAction += InvokeFXInPoint;
        }

        public override bool CheckCurrentFX(ParticleSystem objectFX)
        {
            return objectFX.isStopped;
        }

        public override void InvokeFXInPoint(Vector3 point)
        {
            var spark = GetFX();
            spark.transform.position = point;
            spark.Play();
        }
    }
}
