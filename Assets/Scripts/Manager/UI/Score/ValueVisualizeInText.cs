using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ValueVisualize // 클래스 정적 선언
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

        // 부호 출력 문자열
        string significant = (number < 0) ? "-" : string.Empty;

        // 절대값으로 처리
        number = System.Math.Abs(number);

        // 패턴을 단순화 시키기 위해 무조건 지수 표현식으로 변경한 후 처리
        string[] partsSplit = number.ToString("E").Split('E');

        // 예외
        if (partsSplit.Length < 2)
        {
            return "0";
        }

        // 지수 (자릿수 표현)
        if (!int.TryParse(partsSplit[1], out int exponent))
        {
            Debug.LogWarningFormat("Failed - ToCurrencyString({0}) : partSplit[1] = {1}", number, partsSplit[1]);
            return "0";
        }

        // 몫은 문자열 인덱스
        int quotient = exponent / 3;

        // 나머지는 정수부 자릿수 계산에 사용(10의 거듭제곱을 사용)
        int remainder = exponent % 3;

        // 10의 거듭제곱을 구해서 자릿수 표현값을 만들어 준다.
        var temp = double.Parse(partsSplit[0]) * System.Math.Pow(10, remainder);

        // 소수 둘째자리까지만 출력한다.
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

// 사용 예제
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
