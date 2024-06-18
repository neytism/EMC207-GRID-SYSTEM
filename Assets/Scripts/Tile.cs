using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _currentPlayerTile = false;
    [SerializeField] private bool _target = false;
    [SerializeField] private bool _selectable = false;

    public Color isPlayerColor = Color.blue;
    public Color isTargetColor = Color.green;
    public Color isSelectableColor = Color.white;
    
    private Renderer _rend;
    
    // Start is called before the first frame update
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPlayerTile)
        {
           _rend.material.color = isPlayerColor;
        }
        else if (_target)
        {
            _rend.material.color = isTargetColor;
        }
        else
        {
            _rend.material.color = isSelectableColor;
        }
    }
}
