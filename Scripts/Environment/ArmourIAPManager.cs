// ==================================
// ArmourIAPManager.cs
// Handles IAP purchases for premium armours & costumes
// ==================================

using UnityEngine;
using UnityEngine.Purchasing;

namespace ExoPioneer.Monetization
{
    public class ArmourIAPManager : MonoBehaviour, IStoreListener
    {
        private static IStoreController storeController;
        private static IExtensionProvider storeExtensionProvider;

        [Header("Product IDs (Google Play / App Store)")]
        public string product_armourPack = "exopioneer_armour_pack";
        public string product_costumePack = "exopioneer_costume_pack";

        void Start()
        {
            if (storeController == null)
                InitializePurchasing();
        }

        public void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(product_armourPack, ProductType.NonConsumable);
            builder.AddProduct(product_costumePack, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyArmourPack() => BuyProductID(product_armourPack);
        public void BuyCostumePack() => BuyProductID(product_costumePack);

        void BuyProductID(string productId)
        {
            if (storeController != null)
            {
                Product product = storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                    storeController.InitiatePurchase(product);
                else
                    Debug.Log("Product not available: " + productId);
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (args.purchasedProduct.definition.id == product_armourPack)
            {
                Debug.Log("Armour Pack Purchased!");
                UnlockArmourPack();
            }
            else if (args.purchasedProduct.definition.id == product_costumePack)
            {
                Debug.Log("Costume Pack Purchased!");
                UnlockCostumePack();
            }

            return PurchaseProcessingResult.Complete;
        }

        void UnlockArmourPack()
        {
            var costumeManager = FindObjectOfType<ExoPioneer.PlayerSystem.CostumeManager>();
            if (costumeManager != null)
            {
                costumeManager.UnlockCostume("Dragon Armour");
                costumeManager.UnlockCostume("NanoShield Suit");
            }
        }

        void UnlockCostumePack()
        {
            var costumeManager = FindObjectOfType<ExoPioneer.PlayerSystem.CostumeManager>();
            if (costumeManager != null)
            {
                costumeManager.UnlockCostume("Shadow Outfit");
                costumeManager.UnlockCostume("Phoenix Robe");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            storeExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("IAP Init Failed: " + error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            Debug.Log("Purchase failed: " + reason);
        }
    }
}
