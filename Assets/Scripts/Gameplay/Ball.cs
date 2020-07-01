using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public float speed = 30;
    public Rigidbody2D rb;
    public SpriteRenderer skinHolder;
    public Sprite[] skins;
    private void OnEnable()
    {
        skinHolder.sprite = skins[PlayerPrefs.GetInt(Constant.skinId, 0)];
        SetSpeedAndSize();
        StartCoroutine(StartMoveCoroutine());
    }

    void SetSpeedAndSize()
    {
        speed = Random.Range(20, 35);
        transform.localScale = Vector3.one * Random.Range(0.75f, 1.5f);
    }

    IEnumerator StartMoveCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = ((Random.value >0.5f? Vector2.up : Vector2.down)+new Vector2(Random.Range(-0.5f,0.5f),0)) * speed;
    }

    public void Restart()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "RacketDown")
        {
            float x = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rb.velocity = dir * speed;
        }

        if (col.gameObject.name == "RacketUp")
        {
            float x = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, -1).normalized;

            rb.velocity = dir * speed;
        }
        if (col.gameObject.tag == Constant.racket)
        {
            ScoreUI.instance.UpdateScore();
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.x - racketPos.x) / racketHeight;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == Constant.gate)
        {
            GameController.instance.Lose();
        }
    }}
