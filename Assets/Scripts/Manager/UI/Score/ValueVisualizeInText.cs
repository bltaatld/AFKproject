using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ValueVisualize // Ŭ���� ���� ����
{
    static readonly string[] CurrencyUnits = new string[] {
        "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
        "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az",
        "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
        "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx"
    };

    public static string ToCurrencyString(this double number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }

        if (double.IsInfinity(number))
        {
            return "Infinity";
        }

        // ��ȣ ��� ���ڿ�
        string significant = (number < 0) ? "-" : string.Empty;

        // ���밪���� ó��
        number = System.Math.Abs(number);

        // ������ �ܼ�ȭ ��Ű�� ���� ������ ���� ǥ�������� ������ �� ó��
        string[] partsSplit = number.ToString("E").Split('E');

        // ����
        if (partsSplit.Length < 2)
        {
            return "0";
        }

        // ���� (�ڸ��� ǥ��)
        if (!int.TryParse(partsSplit[1], out int exponent))
        {
            Debug.LogWarningFormat("Failed - ToCurrencyString({0}) : partSplit[1] = {1}", number, partsSplit[1]);
            return "0";
        }

        // ���� ���ڿ� �ε���
        int quotient = exponent / 3;

        // �������� ������ �ڸ��� ��꿡 ���(10�� �ŵ������� ���)
        int remainder = exponent % 3;

        // 10�� �ŵ������� ���ؼ� �ڸ��� ǥ������ ����� �ش�.
        var temp = double.Parse(partsSplit[0]) * System.Math.Pow(10, remainder);

        // �Ҽ� ��°�ڸ������� ����Ѵ�.
        string showNumber = temp.ToString("F1").Replace(".0", "");

        string unityString = CurrencyUnits[quotient];

        return string.Format("{0}{1}{2}", significant, showNumber, unityString);
    }

    public static void LogCurrencyValue(double number)
    {
        string currencyString = number.ToCurrencyString();
        Debug.Log(currencyString);
    }
}

// ��� ����
public class ValueVisualizeInText : MonoBehaviour
{
    public GoodsScoreManager goodsScoreManage;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI diamondText;

    private void Update()
    {
        double stoneValue = goodsScoreManage.goods.RebirthStone;
        double coinValue = goodsScoreManage.goods.Coin;
        double diamondValue = goodsScoreManage.goods.Diamond;

        stoneText.text = ValueVisualize.ToCurrencyString(stoneValue);
        coinText.text = ValueVisualize.ToCurrencyString(coinValue);
        diamondText.text = ValueVisualize.ToCurrencyString(diamondValue);
    }
}
