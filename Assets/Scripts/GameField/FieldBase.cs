using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class FieldBase : MonoBehaviour, ICreatable, ISwitchTetromino
{
    [Header("Collection Tetromino")]
    [SerializeField]
    private GameObject[] tetrominoCollection;

    [Header("Field size")]
    [SerializeField]
    private protected int fieldWidth;
    [SerializeField]
    private protected int fieldHeight;

    [Header("Tetromino positions")]
    [SerializeField]
    private protected Vector2 spawnPosition;
    [SerializeField]
    private protected Vector2 stashPosition;
    [SerializeField]
    private protected Vector2 queuePosition;

    [Header("Game queue")]
    [SerializeField]
    private protected int queueSize;
    [SerializeField]
    private protected float queueShift;

    [Header("Score panel")]
    [SerializeField]
    private TextMeshPro scorePanel;
    [SerializeField]
    private protected TextMeshProUGUI loseScorePanel;
    [SerializeField]
    private protected GameObject losePanel;



    private protected GameObject currentTetrominoInGame;
    private protected Stash gameStash;
    private protected QueueField gameQueue;
    private protected LineDetector gameLineDetector;
    private protected Score gameScore;

    private protected abstract void SpawnTetromino();

    public void Create()
    {
        GameObject currentTetromino = Instantiate(RandomFigureSelection(this.tetrominoCollection), queuePosition, Quaternion.identity);
        gameQueue.AddObject(currentTetromino);
        BusEvent.OnCreateTetrominoEvent?.Invoke(currentTetromino);
    }

    private GameObject RandomFigureSelection(GameObject[] tetrominoCollection)
    {
        System.Random randomValue = new();
        int index = randomValue.Next(0, tetrominoCollection.Length);
        return tetrominoCollection[index];
    }

    private protected void OnQueueFull()
    {
        SpawnTetromino();
        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnQueueFull;
    }

    public void SwitchTetromino(KeyCode keyCode, float _)
    {
        if (keyCode != KeyCode.F)
            return;
        if (gameStash.isItSwithcable)
        {
            GameObject newTetromino = gameStash.SwitchTetromino(this.currentTetrominoInGame);
            this.currentTetrominoInGame = newTetromino;
            if (newTetromino == null)
                SpawnTetromino();
        }
    }

    private protected void StartDestroyAnimation(RaycastHit2D[] detectedObject)
    {
        foreach (RaycastHit2D cell in detectedObject)
        {
            cell.transform.GetComponent<Animator>().SetTrigger("Break");
        }
    }
    private protected void StartDestroyAnimation(List<Transform> detectedObject)
    {
        foreach (Transform cell in detectedObject)
        {
            cell.GetComponent<Animator>().SetTrigger("Break");
        }

    }

    private protected void OnDeleteTetromino(GameObject fig)
    {
        SpawnTetromino();
    }
    private protected void OnLoseGame()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        losePanel.SetActive(true);
        loseScorePanel.text = $"{gameScore.Point}";
    }
    public bool IsLose(Collider2D collider)
    {
        return collider.CompareTag("Figure");
    }
    private protected void IsPaused(bool isPaused)
    {
        Rigidbody2D tetromino = this.currentTetrominoInGame.GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = tetromino.velocity;
        if (isPaused)
        {
            tetromino.isKinematic = true;
            tetromino.velocity = new Vector2(0, 0);
        }
        else
        {
            tetromino.isKinematic = false;
            tetromino.velocity = currentVelocity;
        }
    }
    private protected void OnAddScore(int point)
    {
        scorePanel.text = $"{point}";
    }

}
