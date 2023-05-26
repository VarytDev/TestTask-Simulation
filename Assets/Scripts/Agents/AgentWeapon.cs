using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent(out IDamagable _foundDamagable) == false)
        {
            return;
        }

        _foundDamagable.OnDamageTaken(1);
    }
}
