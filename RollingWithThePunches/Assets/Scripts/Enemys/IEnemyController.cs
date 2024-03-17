using UnityEngine;

public interface IEnemyController
{
    void Stun(bool stund);
    void Death();
    void SetUpProcess(GameObject targ);
}