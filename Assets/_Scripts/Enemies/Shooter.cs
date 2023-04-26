using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 360)] private float spreadAngle;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;

    private bool isShooting = false;

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        float startAngle, currentAngle, angleStep;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
            }

            currentAngle = startAngle;
            yield return new WaitForSeconds(timeBetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep); // recalculate the angle
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    /// <summary>
    /// This method makes the bullets shoot in a cone of influence.
    /// </summary>
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        // targetDirection takes the position of the player and subtracts the position of the enemy so that the enemy can shoot at the player.
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

        // targetAngle takes the targetDirection and converts it to an angle with the Atan2 method.
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // startAngle and endAngle are used to calculate the angle of the bullets.
        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;

        // if the spreadAngle is not 0, then the angleStep is calculated.
        if (spreadAngle != 0)
        {
            // angleStep is the angle between each bullet.
            angleStep = spreadAngle / (projectilesPerBurst - 1);

            // halfAngleSpread is the angle between the startAngle and the endAngle.
            halfAngleSpread = spreadAngle / 2f;

            // startAngle and endAngle are calculated considering the spreadAngle.
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    /// <summary>
    /// Bullets spawn depending on the angle.
    /// </summary>
    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        // x = x0 + r * cos(a)
        // y = y0 + r * sin(a)
        // they calculate the position of the bullet depending on the angle.
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
