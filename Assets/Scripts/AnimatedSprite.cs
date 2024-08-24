using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    //tạo ra hiệu ứng hoạt hình
    // 3 DAY and small vs big

    public Sprite[] sprites;

    //tần số khung hình cho hoạt hình 
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;
    // Start is called before the first0 frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Phương thức OnEnble, được gọi khi đối tượng được kích hoạt
    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }


    // Phương thức OnDisable, được gọi khi đối tượng bị vô hiệu hóa
    private void OnDisable()
    {
        CancelInvoke();
    }

    //  biến thay đổi sprite theo khung hình
    private void Animate()
    {
        frame++;

        if(frame >= sprites.Length)
        {
            frame = 0;
        }

        if(frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
