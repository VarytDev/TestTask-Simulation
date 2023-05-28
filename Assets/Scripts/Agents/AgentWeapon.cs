using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;

    private int weaponDamage = 1;

    public void InitializeWeapon(int _weaponDamage)
    {
        weaponDamage = _weaponDamage;

        IsInitialized = true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent(out IDamagable _foundDamagable) == false)
        {
            return;
        }

        _foundDamagable.OnDamageTaken(weaponDamage);
    }
}
