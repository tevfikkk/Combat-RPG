using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 20f;

    private bool isGrowing = true;
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            isGrowing = false; // stop growing when hits indestructible object such like walls
        }
    }

    /// <summary>
    /// Updates the laser's range
    /// </summary>
    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    /// <summary>
    /// Increases the laser's length over time
    /// </summary>
    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linerT = timePassed / laserGrowTime;

            // sprite size
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linerT), 1f);

            // increase the collider size and offset at x axis
            capsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linerT), capsuleCollider.size.y);
            capsuleCollider.offset = new Vector2(Mathf.Lerp(1f, laserRange, linerT) / 2, capsuleCollider.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    /// <summary>
    /// Rotates the laser to face the mouse
    /// </summary>
    private void LaserFaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = transform.position - mousePos;
        transform.right = -direction;
    }
}
