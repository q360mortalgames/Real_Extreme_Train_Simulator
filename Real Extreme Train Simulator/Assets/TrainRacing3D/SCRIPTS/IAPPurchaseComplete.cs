using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPPurchaseComplete : MonoBehaviour
{
	public void OnPurchaseComplete(Product product)
	{
		Debug.Log($"OnPurchaseComplete: {product.definition.id}");

		switch (product.definition.id)
		{
			case "com.eurotrain.noads":
				AdsManager.Instance.OnPurchasedRemoveAds();
				break;

			case "com.eurotrain.unlockalllevels":
				GameManager.Instance.UnlockAllLevels();
				break;

			case "com.eurotrain.unlockalltrains":
				GameManager.Instance.UnlockAllTrains();
				break;

			case "com.eurotrain.unlockalltrainsandkevels":
				GameManager.Instance.UnlockAllTrains();
				GameManager.Instance.UnlockAllLevels();
				break;

			case "com.eurotrain.coinpack01":
				GameManager.Instance.AddCoins(125000);
				break;

			case "com.eurotrain.coinpack02":
				GameManager.Instance.AddCoins(250000);
				break;

			default:
				break;
		}
	}
}
