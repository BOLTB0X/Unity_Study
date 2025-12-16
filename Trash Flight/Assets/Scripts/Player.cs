using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval;
    private float lastShootTime = 0f;
    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");

        // Vector3 movoTo = new Vector3(horizontalInput, verticalInput, 0f);

        // transform.position += movoTo * moveSpeed * Time.deltaTime;

        Vector3 movoTo = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= movoTo;

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += movoTo;
        }

        Shoot();

        //Debug.Log(Input.mousePosition);
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // //Debug.Log(mousePos);
        // float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        // transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
    }

    void Shoot()
    {
        if (Time.time - lastShootTime <= shootInterval)
        {
            return;
        }
        Instantiate(weapon, shootTransform.position, Quaternion.identity);
        lastShootTime = Time.time;
    }
}
