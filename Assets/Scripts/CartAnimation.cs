using System.Collections;
using UnityEngine;

public class cartAnimation : MonoBehaviour
{
    [SerializeField] private tokenCollected tokenUI;
    [SerializeField] private GameObject cart;
    [SerializeField] private GameObject cartUI;


    private Animator animator;


    void Start()
    {
        animator = cartUI.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cart.GetComponent<SpriteRenderer>().enabled = false;
            animator.SetTrigger("moveCart");
            StartCoroutine(DestroyCartAfterAnimation());
        }
    }


    private IEnumerator DestroyCartAfterAnimation()
    {
        // Replace with the actual length of your "moveCart" animation
        // AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float waitTime = 3.0f;
        Debug.Log(waitTime);

        yield return new WaitForSeconds(waitTime);
        Destroy(cartUI);
        animator.SetTrigger("StopCart");
        Destroy(cartUI);
        tokenUI.ShowCount();
        Destroy(cart);
    }
}
