using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    public Text m_playerHealthText;
    public Text m_playerScoreText;
    public Text m_gunAmmoCount;

    private Player m_player;
    private EnemySpawner m_enemySpawner;
    private GunController m_gunController;

    private void Awake()
    {
        m_player = FindObjectOfType<Player>();
        m_gunController = m_player.GetComponent<GunController>();
        m_enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        m_playerHealthText.text = "Health: " + m_player.GetHealth();
        m_playerScoreText.text = "Enemies: " + m_enemySpawner.GetEnemiesAlive();
        m_gunAmmoCount.text = m_gunController.GetGunAmmoCount().ToString();
    }


}
