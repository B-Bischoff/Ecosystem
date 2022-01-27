using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    private int _index; //Used to update natureMap (stored in terrainDatas) properly 

    public void SetIndex(int newIndex) => _index = newIndex;
    public int GetIndex() { return _index; }
}
