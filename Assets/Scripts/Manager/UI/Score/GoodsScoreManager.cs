using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goods
{
    public double RebirthStone;
    public double Coin;
    public double Diamond;
    public double AutoStone;
}

public class GoodsScoreManager : MonoBehaviour
{
    public Goods goods = new Goods();
}
