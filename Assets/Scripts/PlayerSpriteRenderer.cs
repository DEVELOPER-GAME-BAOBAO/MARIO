using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{

    // 3 day

    public SpriteRenderer SpriteRenderer { get; private set; }
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;
    //public Sprite run;

    // Start is called before the first frame update
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SpriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        SpriteRenderer.enabled = false;
        run.enabled = false; //DAY 5
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;
        if(movement.jumping)
        {
            SpriteRenderer.sprite = jump;
        } else if (movement.sliding)
        {
            SpriteRenderer.sprite = slide;
        } 
        else if(!movement.running)
        {
            SpriteRenderer.sprite = idle;
        }

    }


}
