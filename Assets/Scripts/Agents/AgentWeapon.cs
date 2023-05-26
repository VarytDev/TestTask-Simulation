using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    //TODO This is a hack. It should be rewritten
    private IDamagable damagableToIgnore = null;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent(out IDamagable _foundDamagable) == false || _foundDamagable == damagableToIgnore)
        {
            return;
        }

        if (damagableToIgnore == null)
        {
            damagableToIgnore = _foundDamagable;
            return;
        }

        _foundDamagable.OnDamageTaken(1);
    }
}
