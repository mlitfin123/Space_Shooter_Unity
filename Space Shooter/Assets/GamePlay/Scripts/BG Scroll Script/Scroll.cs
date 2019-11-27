using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed; //indicates the speed for the background to scroll
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _renderer.material.mainTextureOffset = new Vector2(Time.time * speed, 0);
    }
}
