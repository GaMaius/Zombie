using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindFirstObjectByType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄알 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임오버 시 활성화할 UI

    private Text cheatModeText; // 치트 모드 표시용 텍스트

    private void Awake() {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        CreateCheatModeText();
    }

    private void CreateCheatModeText() {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null) return;

        GameObject textGo = new GameObject("CheatModeText");
        textGo.transform.SetParent(canvas.transform, false);

        cheatModeText = textGo.AddComponent<Text>();
        
        if (ammoText != null)
        {
            cheatModeText.font = ammoText.font;
        }

        cheatModeText.fontSize = 25;
        cheatModeText.fontStyle = FontStyle.Bold;
        cheatModeText.color = Color.red;
        cheatModeText.alignment = TextAnchor.UpperLeft;

        RectTransform rectTransform = cheatModeText.rectTransform;
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchoredPosition = new Vector2(20, -20);
        rectTransform.sizeDelta = new Vector2(300, 50);

        UpdateCheatModeText(false);
    }

    public void UpdateCheatModeText(bool isCheatMode) {
        if (cheatModeText == null) return;
        
        if (isCheatMode)
        {
            cheatModeText.text = "Cheat Mode ON";
            cheatModeText.color = Color.green;
        }
        else
        {
            cheatModeText.text = "Cheat Mode OFF";
            cheatModeText.color = Color.red;
        }
    }

    // 탄알 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo) {
        ammoText.text = magAmmo + " / " + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count) {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    // 게임오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}