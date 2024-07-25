using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    
    public enum RESOURCE_TYPE
    {
        GOLD, WOOD, MEAT
    }

    public RESOURCE_TYPE resourceType = RESOURCE_TYPE.GOLD; // �ڿ� ����
    public int resourceAmount = 100; // �ڿ� ��

    // �ڿ��� ĳ���� �� ȣ��Ǵ� �޼���
    public int Gather(int amount)
    {

        resourceAmount -= amount;
        if (resourceAmount <= 0)
        {
            this.gameObject.SetActive(false); // �ڿ��� �� ĳ���� ������Ʈ ��Ȱ��ȭ
            return resourceAmount;
        }
        return amount;
    }
}
