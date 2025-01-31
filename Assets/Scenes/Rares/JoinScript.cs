using UnityEngine;

public class JoinScript : MonoBehaviour
{
    public int downSpeed = 10;
    public int upSpeed = 20;

    public GameObject[] Player_M = new GameObject[4]; // Array for Player_M models
    public GameObject[] Player_L = new GameObject[4]; // Array for Player_L locations

    private int currentPlayerIndex = 0; // Tracks which player to spawn next
    private bool isMovingDown = false;
    private bool isMovingUp = false;
    private float targetY;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Press S to enable next player
        {
            if (currentPlayerIndex < Player_M.Length)
            {
                EnablePlayerModel();
            }
        }

        if (isMovingDown)
        {
            MoveDown(Player_M[currentPlayerIndex - 1], Player_L[currentPlayerIndex - 1]);
        }

        if (isMovingUp)
        {
            MoveUp(Player_M[currentPlayerIndex - 1], Player_L[currentPlayerIndex - 1]);
        }

        if (Input.GetKey(KeyCode.W))
        {
            StartGame();
        }
    }

    void EnablePlayerModel()
    {
        if (Player_M[currentPlayerIndex] != null && Player_L[currentPlayerIndex] != null)
        {
            Player_M[currentPlayerIndex].SetActive(true); // Enable the current Player_M
            targetY = Player_M[currentPlayerIndex].transform.position.y - 3.5f; // Set the stop position
            isMovingDown = true;
            currentPlayerIndex++; // Move to the next player for the next S press
        }
    }

    void MoveDown(GameObject model, GameObject location)
    {
        if (model != null && location != null)
        {
            model.transform.position = Vector3.MoveTowards(model.transform.position,
                                                           new Vector3(model.transform.position.x, targetY, model.transform.position.z),
                                                           downSpeed * Time.deltaTime);
            location.transform.position = Vector3.MoveTowards(location.transform.position,
                                                              new Vector3(location.transform.position.x, targetY, location.transform.position.z),
                                                              downSpeed * Time.deltaTime);

            if (Mathf.Approximately(model.transform.position.y, targetY))
            {
                isMovingDown = false;
            }
        }
    }

    void MoveUp(GameObject model, GameObject location)
    {
        if (model != null && location != null)
        {
            model.transform.position = Vector3.MoveTowards(model.transform.position,
                                                           new Vector3(model.transform.position.x, targetY + 3.5f, model.transform.position.z),
                                                           upSpeed * Time.deltaTime);
            location.transform.position = Vector3.MoveTowards(location.transform.position,
                                                              new Vector3(location.transform.position.x, targetY + 3.5f, location.transform.position.z),
                                                              upSpeed * Time.deltaTime);

            // If the player reaches the target, move to the next player
            if (Mathf.Approximately(model.transform.position.y, targetY + 3.5f))
            {
                isMovingUp = false;
                currentPlayerIndex++; // Move to the next player
            }
        }
    }

    void StartGame()
    {
        if (currentPlayerIndex == Player_M.Length) // Ensure all players are spawned
        {
            isMovingUp = true; // Begin moving players up
            currentPlayerIndex = 0; // Start with Player1
        }
    }
}
