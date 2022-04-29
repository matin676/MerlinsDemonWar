using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDropHandler
{
    public Image playerImage = null;
    public Image mirrorImage = null;
    public Image healthNumberImage = null;
    public Image glowImage = null;

    public int maxHealth = 5;
    public int health = 5;
    public int mana = 1;

    public bool isPlayer;
    public bool isFire; //wheather an enemy is a fire monster or not

    public GameObject[] manaBalls = new GameObject[5];

    private Animator animator = null;

    public AudioSource dealAudio = null;
    public AudioSource healAudio = null;
    public AudioSource mirrorAudio = null;
    public AudioSource smashAudio = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateHealth();
        UpdateManaBalls();
    }

    internal void PlayHitAnim()
    {
        if (animator != null)
            animator.SetTrigger("Hit");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameController.instance.isPlayable)
        {
            return;
        }
        GameObject Obj = eventData.pointerDrag;
        if (Obj != null)
        {
            Card card = Obj.GetComponent<Card>();
            if (card != null)
            {
                GameController.instance.UseCard(card, this, GameController.instance.playersHand);
            }
        }
    }

    internal void UpdateHealth()
    {
        if(health > 0 && health < GameController.instance.healthNumbers.Length)
        {
            healthNumberImage.sprite = GameController.instance.healthNumbers[health];
        }
        else
        {
            Debug.LogWarning("Health is not a valid number"+ health.ToString());
        }
    }

    internal void SetMirror(bool on)
    {
        mirrorImage.gameObject.SetActive(on);
    }

    internal bool HasMirror()
    {
        return mirrorImage.gameObject.activeInHierarchy;
    }

    internal void UpdateManaBalls()
    {
        for (int m = 0; m < 5; m++)
        {
            if (mana > m)
                manaBalls[m].SetActive(true);
            else
                manaBalls[m].SetActive(false);
        }
    }

    internal void PlayMirrorSound()
    {
        mirrorAudio.Play();
    }
    internal void PlayCardSound()
    {
        dealAudio.Play();
    }
    internal void PlayHealSound()
    {
        healAudio.Play();
    }
    internal void PlaySmashSound()
    {
        smashAudio.Play();
    }
}
