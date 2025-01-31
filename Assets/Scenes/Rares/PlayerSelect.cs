using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public int downSpeed = 5;
    public int upSpeed = 20;
    public GameObject Player; // Assign your player prefab in the Inspector
    public GameObject ReferenceModel; // Assign a reference model (e.g., platform)

    private GameObject spawnedPlayer; // Store the instantiated player
    private bool isMovingDown = false; // Track if movement should happen

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * upSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * downSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to spawn the player
        {
            SpawnPlayerModel();
        }

        if (isMovingDown && spawnedPlayer != null)
        {
            MovePlayerDown();
        }
    }

    void SpawnPlayerModel()
    {
        if (Player != null && ReferenceModel != null)
        {
            // Get the reference model's position
            Vector3 spawnPosition = ReferenceModel.transform.position + new Vector3(0, 10f, 0);

            // Spawn the player 10 units above the reference model
            spawnedPlayer = Instantiate(Player, spawnPosition, Quaternion.identity);

            // Assign a random color
            Renderer renderer = spawnedPlayer.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value);
            }

            // Start moving down immediately
            isMovingDown = true;
        }
        else
        {
            Debug.LogWarning("Player prefab or ReferenceModel is not assigned!");
        }
    }

    void MovePlayerDown()
    {
        if (spawnedPlayer.transform.position.y > -3.8f)
        {
            spawnedPlayer.transform.position += Vector3.down * downSpeed * Time.deltaTime;
        }
        else
        {
            isMovingDown = false; // Stop moving when it reaches -3.8 Y
        }
    }
}
