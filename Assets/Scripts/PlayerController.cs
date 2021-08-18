using System.Collections;
using UnityEngine;
// ReSharper disable All

public class PlayerController : MonoBehaviour
{
    public GameObject powerUpIndicator;
    public float speed = 5f;
    public bool hasPowerUp = false; 
    private Rigidbody playerRb;

    private GameObject focalPoint;

    private float powerUpForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        var forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        
        var horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed);

        powerUpIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)  // Is Trigger 체크
    {
        if (other.CompareTag("PowerUp"))
        {
            powerUpIndicator.gameObject.SetActive(true);
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(nameof(CountDownPowerUp));
        }
    }

    IEnumerator CountDownPowerUp()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)  // 물리 관련한 충돌 
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            var rigidbody = other.gameObject.GetComponent<Rigidbody>();
            var direction = other.gameObject.transform.position - transform.position;
            rigidbody.AddForce(direction * powerUpForce, ForceMode.Impulse);
            // Debug.Log($"power up collied with {other.gameObject}");
        }
    }
}
