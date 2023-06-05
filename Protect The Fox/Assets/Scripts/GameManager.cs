using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;


    [SerializeField] private bool isGameOver;
    [Space(2)]
    [Header("Lose Condition")]
    [SerializeField] private int lives = 5;

    [Header("Scores")] 
    [SerializeField] private int score;

    [Header("Cannon Places")] 
    [SerializeField] private Transform[] cannonPlaces;
    [SerializeField] private List<Cannon> cannonsSpawned;

    [SerializeField] private GameObject cannonObject;
    [SerializeField] private int howManyCannons;
    [SerializeField] private int cannonAdder;

    [Header("Balls")]
    [SerializeField] private Ball cannonBall;
    [SerializeField] private Ball[] ballsWithEffect;
    private int cannonAdderReset;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI scoreIndicator;
    [SerializeField] private TextMeshProUGUI healthIndicator;
    [SerializeField] private GameObject gameOverScreen;
    
    private bool isSpawning;
    public bool IsGameOver => isGameOver;
    protected void Awake()
    {
        Time.timeScale = 1;
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        cannonAdderReset = cannonAdder;
    }

    private void Start()
    {
        StartCoroutine(SpawnCannons());
    }

    private void Update()
    {
        if (lives <= 0)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
        ChangeUI();
        if (NoCannonsLeft)
        {
            if(!isSpawning) StartCoroutine(SpawnCannons());
        }
    }

    public void GiveScore() => score++;
    public void HitFox() => lives--;
    void ChangeUI()
    {
        scoreIndicator.text = $"{score}";
        healthIndicator.text = $"Fox Health: {lives}";
    }
    void SpawnCannon()
    {
        bool[] isTaken = new bool[cannonPlaces.Length];
        for (int i = 0; i < howManyCannons; i++)
        {
            int randomNumber = Random.Range(0, cannonPlaces.Length);
            while(isTaken[randomNumber]) randomNumber = Random.Range(0, cannonPlaces.Length);
            isTaken[randomNumber] = true;

            Cannon _instance = Instantiate(cannonObject, cannonPlaces[randomNumber]).GetComponentInChildren<Cannon>();
            _instance.transform.localPosition = Vector3.zero;
            
            if (i >= 3 &&i % 3 == 0)
            {
                int _ballEffect = Random.Range(0, ballsWithEffect.Length);
                _instance.loadedBall = ballsWithEffect[_ballEffect];
            }
            else _instance.loadedBall = cannonBall;
            
            cannonsSpawned.Add(_instance);


        }

        if (howManyCannons < cannonPlaces.Length)
        {
            cannonAdder--;
            if (cannonAdder <= 0)
            {
                howManyCannons++;
                cannonAdder = cannonAdderReset;
            }
        }
       
    }

    public void DestroyCannon(Cannon _cannon)
    {
        cannonsSpawned.Remove(_cannon);
        Destroy(_cannon.transform.parent.parent.gameObject,1);
    }

    IEnumerator SpawnCannons()
    {
        isSpawning = true;
        yield return new WaitForSeconds(1);
        SpawnCannon();
        isSpawning = false;
    }
    private bool NoCannonsLeft => cannonsSpawned.Count <= 0;
    
    public void RestartGame() => SceneManager.LoadScene(1);
    public void ExitGame() => SceneManager.LoadScene(0);
}
