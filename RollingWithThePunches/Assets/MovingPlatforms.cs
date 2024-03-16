using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private enum PlatformType { Automatic, GeneratorController };
    [SerializeField] private PlatformType platformType;
    [SerializeField] private Vector2 endPosition;
    [SerializeField] float moveTime;

    private Vector2 startPos, endPos;
    private GameObject player;

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(transform.position.x + endPosition.x, transform.position.y + endPosition.y);
        if (platformType == PlatformType.Automatic)
        {
            AutomaticMovement();
        }
    }

    private void AutomaticMovement()
    {
        transform.LeanMove(endPos, moveTime).setLoopPingPong();
    }
    private void OnDrawGizmosSelected()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Vector2 pos = new Vector2(transform.position.x + endPosition.x, transform.position.y + endPosition.y);
        Gizmos.DrawWireCube(pos, spr.bounds.size);
    }
    
    public void MoveToEndPosition()
    {
        LeanTween.cancel(gameObject);
        float distance = Vector2.Distance(transform.position, endPos);
        float duration = distance/moveTime;
        transform.LeanMove(endPos, duration); 
    }

    public void MoveToStartPosition()
    {
        LeanTween.cancel(gameObject);
        float distance = Vector2.Distance(transform.position, startPos);
        float duration = distance/moveTime;
        transform.LeanMove(startPos, duration); 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.SetParent(transform); // Set player as child of the platform
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(null); // Release player from being a child of the platform
            player = null;
        }
    }
}
