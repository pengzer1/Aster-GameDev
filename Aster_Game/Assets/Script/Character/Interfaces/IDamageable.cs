namespace AG.PlayerComponent.Interfaces
{
    public interface IDamageable
    {
        /// <summary> 데미지 추가 </summary>
        void ApplyDamage(int damage);
        /// <summary> ApplyDamage후 체력이 0 이하가 되면 이 함수 호출 </summary>
        void Die();
        /// <summary> 플레이어가 현재 죽었는지를 반환 </summary>
        bool Dead();
        /// <summary> 플레이어가 현재 이겼는지를 반환 </summary>
        bool Win();
    }
}