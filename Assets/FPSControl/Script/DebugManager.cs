using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private bool _fpsLock;
    [SerializeField] private int _fps;
    [SerializeField] private Text _fpsText;

    private float _timer = 1;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_fpsLock)
        {
            Application.targetFrameRate = _fps;
        }
        
        Debug.Log("hennkouekita");
    }

    private void Update()
    {
        if (_timer < 1)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _fpsText.text = (1 / Time.deltaTime).ToString("F");
            _timer = 0;
        }
    }
}
