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

    public int FieldWidth
    {
        get { return fieldWidth; }
    }

    public int FieldHeigh
    {
        get { return fieldHeight; }
    }

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

    public void StartDestroyAnimation(RaycastHit2D[] detectedObject)
    {
        foreach (RaycastHit2D cell in detectedObject)
        {
            cell.transform.GetComponent<Animator>().SetTrigger("Break");
        }
    }

    public void StartDestroyAnimation(List<Transform> detectedObject)
    {
        BusEvent.OnStartDestroyAnimationEvent?.Invoke();
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

    private protected void OnAddScore(int point)
    {
        scorePanel.text = $"{point}";
    }
}
