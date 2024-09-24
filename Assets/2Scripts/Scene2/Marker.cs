using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;
    [SerializeField] private AudioClip _drawSound; // �߰�: �׸� �� ����� ����

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _LastTouchRot;

    private AudioSource _audioSource; // �߰�: ���� ����� ���� AudioSource

    // Start�� ù ������ ������Ʈ ���� �� �� ȣ��˴ϴ�
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;

        // �߰�: AudioSource �ʱ�ȭ
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = _drawSound;
    }

    // Update�� �� ������ ȣ��˴ϴ�
    void Update()
    {
        Draw();
    }

    // Draw �Լ��� ȭ��Ʈ���忡 �׸��� ������ �����մϴ�
    private void Draw()
    {
        // ����ĳ��Ʈ�� ��ġ ������ �˻��մϴ�
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            // ��ġ�� ��ü�� ȭ��Ʈ�������� Ȯ���մϴ�
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                // ȭ��Ʈ���� ������ �����ɴϴ�
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // �׸� ��ġ�� ����մϴ�
                var x = (int)(_touchPos.x * _whiteboard.texturesize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.texturesize.y - (_penSize / 2));

                // �׸� ��ġ�� �ؽ�ó ������ ������� Ȯ���մϴ�
                if (y < 0 || y > _whiteboard.texturesize.y || x < 0 || x > _whiteboard.texturesize.x)
                    return;

                // ���� �����ӿ��� ��ġ�� ���, ���� �׸��ϴ�
                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    // ���� ��ġ ������ ���� ������ ������ �����մϴ�
                    for (float f = 0.01f; f < 1.00f; f += 0.001f) // f ���� �� �۰� ����
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _LastTouchRot;

                    _whiteboard.texture.Apply();

                    // �߰�: ���� ���
                    if (_drawSound != null && !_audioSource.isPlaying)
                    {
                        _audioSource.Play();
                    }
                }

                // ������ ��ġ ��ġ�� ȸ���� �����մϴ�
                _lastTouchPos = new Vector2(x, y);
                _LastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }
        // ��ġ�� ���ų� ȭ��Ʈ���尡 �ƴ� ���
        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
