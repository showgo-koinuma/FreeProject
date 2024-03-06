using UnityEngine;

public class RecoilTest : MonoBehaviour
{
    [SerializeField, Tooltip("視点移動Script")] private POVController _povController;
    [SerializeField, Tooltip("弾痕オブジェクト")] private GameObject _bulletMarkObject;
    [SerializeField, Tooltip("射撃間隔")] private float _fireRate = 0.12f;
    [SerializeField, Tooltip("リコイルパターン")] private Vector2[] _recoilPattern;
    [SerializeField, Tooltip("ランダムリコイルの範囲")] private Vector2 _randomRecoil;

    private float _fireTimer;
    private int _recoilPatternIndex;

    private void Update()
    {
        if (_fireTimer < _fireRate)
        {
            _fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            if (_fireTimer >= _fireRate)
            {
                _fireTimer = 0;

                if (_recoilPatternIndex < _recoilPattern.Length)
                {
                    _povController.Recoil(_recoilPattern[_recoilPatternIndex]);
                    _recoilPatternIndex++;
                }
                else // 一定以降はランダムとか
                {
                    _povController.Recoil(new Vector2(Random.Range(-_randomRecoil.x, _randomRecoil.x), _randomRecoil.y));
                }
                
                // 着弾地点が分かるようにオブジェクト生成
                Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit);
                Destroy(Instantiate(_bulletMarkObject, hit.point, Quaternion.identity), 5);
            }
        }
        else
        {
            _recoilPatternIndex = 0;
        }
    }
}
