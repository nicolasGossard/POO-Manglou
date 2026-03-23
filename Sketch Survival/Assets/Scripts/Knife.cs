using UnityEngine;
using System.Collections;

public class Knife : Weapon
{
    [Header("Knife Settings")]
    [SerializeField] private int durabilityCostPerHit = 1;

    [Header("Knife Animation")]
    [SerializeField] private float thrustDistance = 0.3f;
    [SerializeField] private float thrustDuration = 0.08f;

    public override void Attack()
    {
        if (isAttacking)
        {
            return;
        }

        ReduceDurability(durabilityCostPerHit);
        StartCoroutine(DoKnifeAttackAnimation());
    }

    private IEnumerator DoKnifeAttackAnimation()
    {
        isAttacking = true;

        Vector3 startPos = initialHolderLocalPosition;
        Vector3 endPos = startPos + Vector3.right * thrustDistance;

        float time = 0f;

        // Aller
        while (time < thrustDuration)
        {
            time += Time.deltaTime;
            float t = time / thrustDuration;
            weaponHolder.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        time = 0f;

        // Retour
        while (time < thrustDuration)
        {
            time += Time.deltaTime;
            float t = time / thrustDuration;
            weaponHolder.localPosition = Vector3.Lerp(endPos, startPos, t);
            yield return null;
        }

        weaponHolder.localPosition = startPos;
        isAttacking = false;
    }
}