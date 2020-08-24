using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public float amountLiquid;
    private PlayerPlatformScript player;

    private void Start()
    {
        player = FindObjectOfType(typeof(PlayerPlatformScript)) as PlayerPlatformScript;
    }
    public override void UseItem()
    {
        UsePotion();
        RemoveItem();
    }

    public void UsePotion()
    {
        //Adicionar a função do player para adicionar a vida dele
    }
}
