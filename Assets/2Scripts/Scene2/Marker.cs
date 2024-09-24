using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;
    [SerializeField] private AudioClip _drawSound; // 추가: 그릴 때 재생할 사운드

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _LastTouchRot;

    private AudioSource _audioSource; // 추가: 사운드 재생을 위한 AudioSource

    // Start는 첫 프레임 업데이트 전에 한 번 호출됩니다
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;

        // 추가: AudioSource 초기화
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = _drawSound;
    }

    // Update는 매 프레임 호출됩니다
    void Update()
    {
        Draw();
    }

    // Draw 함수는 화이트보드에 그리기 동작을 구현합니다
    private void Draw()
    {
        // 레이캐스트로 터치 지점을 검사합니다
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            // 터치된 객체가 화이트보드인지 확인합니다
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                // 화이트보드 참조를 가져옵니다
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // 그릴 위치를 계산합니다
                var x = (int)(_touchPos.x * _whiteboard.texturesize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.texturesize.y - (_penSize / 2));

                // 그릴 위치가 텍스처 범위를 벗어나는지 확인합니다
                if (y < 0 || y > _whiteboard.texturesize.y || x < 0 || x > _whiteboard.texturesize.x)
                    return;

                // 이전 프레임에서 터치된 경우, 선을 그립니다
                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    // 이전 터치 지점과 현재 지점을 선으로 연결합니다
                    for (float f = 0.01f; f < 1.00f; f += 0.001f) // f 값을 더 작게 설정
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _LastTouchRot;

                    _whiteboard.texture.Apply();

                    // 추가: 사운드 재생
                    if (_drawSound != null && !_audioSource.isPlaying)
                    {
                        _audioSource.Play();
                    }
                }

                // 마지막 터치 위치와 회전을 저장합니다
                _lastTouchPos = new Vector2(x, y);
                _LastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }
        // 터치가 없거나 화이트보드가 아닌 경우
        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
