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
			case "com.q360mortalgames.noads":
				AdsManager.Instance.OnPurchasedRemoveAds();
				break;

			case "com.q360mortalgames.unlockalllevels":
				GameManager.Instance.UnlockAllLevels();
				break;

			case "com.q360mortalgames.unlockalltrains":
				GameManager.Instance.UnlockAllTrains();
				break;

			case "com.q360mortalgames.unlockalltrainsandkevels":
				GameManager.Instance.UnlockAllTrains();
				GameManager.Instance.UnlockAllLevels();
				break;

			case "com.q360mortalgames.coinpack01":
				GameManager.Instance.AddCoins(125000);
				break;

			case "com.q360mortalgames.coinpack02":
				GameManager.Instance.AddCoins(250000);
				break;

			default:
				break;
		}
	}
}
