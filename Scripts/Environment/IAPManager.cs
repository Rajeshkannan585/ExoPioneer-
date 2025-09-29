// ==================================
// IAPManager.cs
// Handles In-App Purchases for powerful weapons & pets
// ==================================

using UnityEngine;
using UnityEngine.Purchasing;

namespace ExoPioneer.Monetization
{
    public class IAPManager : MonoBehaviour, IStoreListener
    {
        private static IStoreController storeController;
        private static IExtensionProvider storeExtensionProvider;

        [Header("Product IDs (Google Play / App Store)")]
        public string product_weaponPack = "exopioneer_weapon_pack";
        public string product_petPack = "exopioneer_pet_pack";

        void Start()
        {
            if (storeController == null)
            {
                InitializePurchasing();
            }
        }

        public void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Register products
            builder.AddProduct(product_weaponPack, ProductType.NonConsumable);
            builder.AddProduct(product_petPack, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyWeaponPack()
        {
            BuyProductID(product_weaponPack);
        }

        public void BuyPetPack()
        {
            BuyProductID(product_petPack);
        }

        void BuyProductID(string productId)
        {
            if (storeController != null)
            {
                Product product = storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    storeController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Product not found or not available");
                }
            }
        }

        // Called when purchase is successful
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (args.purchasedProduct.definition.id == product_weaponPack)
            {
                Debug.Log("Weapon Pack purchased!");
                UnlockWeaponPack();
            }
            else if (args.purchasedProduct.definition.id == product_petPack)
            {
                Debug.Log("Pet Pack purchased!");
                UnlockPetPack();
            }
            return PurchaseProcessingResult.Complete;
        }

        public void UnlockWeaponPack()
        {
            // Add powerful weapons to inventory
            var inv = FindObjectOfType<ExoPioneer.PlayerSystem.InventorySystem>();
            if (inv != null)
            {
                inv.AddItem("Plasma Rifle", 1);
                inv.AddItem("Thunder Blade", 1);
                inv.AddItem("Alien Rocket Launcher", 1);
            }
        }

        public void UnlockPetPack()
        {
            // Spawn premium pets
            Debug.Log("Premium Pets Unlocked!");
            // TODO: instantiate pets with PetEvolution script
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            storeExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("IAP Initialization Failed: " + error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("Purchase Failed: " + failureReason);
        }
    }
}
