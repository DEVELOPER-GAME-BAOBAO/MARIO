using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// DAY 5
/// </summary>
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer; // DAY 6 AND POWERUP of script

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider; // DAY 6 and
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool startpower { get; private set; } // DAY 6 

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    public void Hit()
    {
        if (!dead && !startpower)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
        /*if (big)
        {
            Shrink();
        }
        else
        {
            Death();
        }*/
    }

    public void Grow() // DAY 6
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer; // DAY 6

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    private void Shrink() // DAY 6 
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer; // DAY 6

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    private IEnumerator ScaleAnimation() // DAY 6
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 4 == 0)
            {

                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }


        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;

    }

    public void Startpower(float duration = 10f)
    {
        StartCoroutine(StartpowerAnimation(duration));

    }

    private IEnumerator StartpowerAnimation(float duration) // DAY 6
    {
        startpower = true;

        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 4 == 0)
            {
                activeRenderer.SpriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.SpriteRenderer.color = Color.white;
        startpower = false;
    }
}
