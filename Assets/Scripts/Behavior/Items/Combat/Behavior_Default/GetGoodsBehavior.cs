using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGoodsBehavior : MonoBehaviour
{
    public GoodsScoreManager goodsManager;
    public string tagOfGoods;
    public Transform targetPoint;
    public float speed = 5f;
    public int upgradeCost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagOfGoods))
        {
            StartCoroutine(MoveAndDestroy(other.gameObject));
        }
    }

    private IEnumerator MoveAndDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);

        while (Vector3.Distance(obj.transform.position, targetPoint.position) > 0.1f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPoint.position, speed * Time.deltaTime);
            yield return null;
        }
        CheckValueType();
        Destroy(obj);
    }

    public void CheckValueType()
    {
        if (tagOfGoods == "Coin")
        {
            goodsManager.goods.Coin++;
            goodsManager.goods.Coin += upgradeCost;
        }

        if (tagOfGoods == "Diamond")
        {
            goodsManager.goods.Diamond++;
        }

        if (tagOfGoods == "RebirthStone")
        {
            goodsManager.goods.RebirthStone++;
        }
    }
}
