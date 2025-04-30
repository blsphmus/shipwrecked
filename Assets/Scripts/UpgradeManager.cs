using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text energyText;
    public TMP_Text coinCountText;
    public TMP_Text coinChanceText;

    [System.Serializable]
    public class UpgradeButton
    {
        public Button button;
        public Image buttonImage;
        public TMP_Text priceText;
        public Sprite availableSprite;
        public Sprite lockedSprite;
        public Sprite maxLevelSprite;
        public bool isBlocked;
    }

    public UpgradeButton healthUpgrade;
    public UpgradeButton damageUpgrade;
    public UpgradeButton energyUpgrade;
    public UpgradeButton coinCountUpgrade;
    public UpgradeButton coinChanceUpgrade;

    public TMP_Text healthLevelText;
    public TMP_Text damageLevelText;
    public TMP_Text energyLevelText;
    public TMP_Text coinCountLevelText;
    public TMP_Text coinChanceLevelText;

    public int basePrice = 5;
    public int maxLevel = 15;



    void Start()
    {
        GameData.Load();
        SetupButtons();
        UpdateUI();
    }

    void SetupButtons()
    {
        healthUpgrade.button.onClick.RemoveAllListeners();
        damageUpgrade.button.onClick.RemoveAllListeners();
        energyUpgrade.button.onClick.RemoveAllListeners();
        coinCountUpgrade.button.onClick.RemoveAllListeners();
        coinChanceUpgrade.button.onClick.RemoveAllListeners();

        healthUpgrade.button.onClick.AddListener(UpgradeHealth);
        damageUpgrade.button.onClick.AddListener(UpgradeDamage);
        energyUpgrade.button.onClick.AddListener(UpgradeEnergy);
        coinCountUpgrade.button.onClick.AddListener(UpgradeCoinCount);
        coinChanceUpgrade.button.onClick.AddListener(UpgradeCoinChance);
    }

    void UpdateButtonState(UpgradeButton button, int currentLevel)
    {
        int price = CalculatePrice(currentLevel);
        bool isMaxLevel = currentLevel >= maxLevel;
        bool canUpgrade = !isMaxLevel && GameData.PlayerCoins >= price;

        button.priceText.text = isMaxLevel ? " " : price.ToString(); // Устанавливаем текст цены

        // Выбираем спрайт в зависимости от состояния
        if (isMaxLevel) button.buttonImage.sprite = button.maxLevelSprite;
        else button.buttonImage.sprite = canUpgrade ? button.availableSprite : button.lockedSprite;

        // Блокируем кнопку если достигнут максимум или недостаточно монет
        button.button.interactable = !isMaxLevel && canUpgrade;
    }

    private int CalculatePrice(int level)
    {
        return basePrice * (int)Mathf.Pow(2, level);
    }

    //private bool CanUpgrade(int currentLevel, int price)
    //{
    //    return currentLevel < maxLevel && GameData.PlayerCoins >= price;
    //}

    //public void UpgradeHealth()
    //{
    //    int price = CalculatePrice(GameData.PlayerMaxHealthLevel);
    //    if (CanUpgrade(GameData.PlayerMaxHealthLevel, price))
    //    {
    //        GameData.PlayerCoins -= price;
    //        GameData.PlayerMaxHealth += 10;
    //        GameData.PlayerMaxHealthLevel++;
    //        GameData.Save();
    //        UpdateUI();
    //    }
    //}

    //public void UpgradeDamage()
    //{
    //    int price = CalculatePrice(GameData.PlayerBulletDamageLevel);
    //    if (CanUpgrade(GameData.PlayerBulletDamageLevel, price))
    //    {
    //        GameData.PlayerCoins -= price;
    //        GameData.PlayerBulletDamage++;
    //        GameData.PlayerBulletDamageLevel++;
    //        GameData.Save();
    //        UpdateUI();
    //    }
    //}

    //public void UpgradeEnergy()
    //{
    //    int price = CalculatePrice(GameData.PlayerMaxEnergyLevel);
    //    if (CanUpgrade(GameData.PlayerMaxEnergyLevel, price))
    //    {
    //        GameData.PlayerCoins -= price;
    //        GameData.PlayerMaxEnergy++;
    //        GameData.PlayerMaxEnergyLevel++;
    //        GameData.Save();
    //        UpdateUI();
    //    }
    //}

    //public void UpgradeCoinCount()
    //{
    //    int price = CalculatePrice(GameData.CoinSpawnCountLevel);
    //    if (CanUpgrade(GameData.CoinSpawnCountLevel, price))
    //    {
    //        GameData.PlayerCoins -= price;
    //        GameData.CoinSpawnCount++;
    //        GameData.CoinSpawnCountLevel++;
    //        GameData.Save();
    //        UpdateUI();
    //    }
    //}

    //public void UpgradeCoinChance()
    //{
    //    int price = CalculatePrice(GameData.CoinSpawnChanceLevel);
    //    if (CanUpgrade(GameData.CoinSpawnChanceLevel, price))
    //    {
    //        GameData.PlayerCoins -= price;
    //        GameData.CoinSpawnChance += 0.05f;
    //        GameData.CoinSpawnChanceLevel++;
    //        GameData.Save();
    //        UpdateUI();
    //    }
    //}

    void TryUpgradeHealth(ref int level, UpgradeButton button)
    {
        //Debug.Log($"Начало улучшения. Текущий уровень: {level}");
        int price = CalculatePrice(level);
        if (button.isBlocked || level >= maxLevel || GameData.PlayerCoins < CalculatePrice(level))
            return;

        StartCoroutine(BlockButtonTimer(button)); // Запускаем таймер блокировки
        GameData.PlayerCoins -= price;
        GameData.PlayerMaxHealth += 10;
        level++;
        GameData.Save();
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.UpdateUI();
        }
        UpdateUI();
        //Debug.Log($"Уровень после улучшения: {level}");
    }

    void TryUpgradeBulletDamage(ref int level, UpgradeButton button)
    {
        //Debug.Log($"Начало улучшения. Текущий уровень: {level}");
        int price = CalculatePrice(level);
        if (button.isBlocked || level >= maxLevel || GameData.PlayerCoins < CalculatePrice(level))
            return;

        StartCoroutine(BlockButtonTimer(button)); // Запускаем таймер блокировки
        GameData.PlayerCoins -= price;
        GameData.PlayerBulletDamage++;
        level++;
        GameData.Save();
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.UpdateUI();
        }
        UpdateUI();
        //Debug.Log($"Уровень после улучшения: {level}");
    }

    void TryUpgradePlayerMaxEnergy(ref int level, UpgradeButton button)
    {
        //Debug.Log($"Начало улучшения. Текущий уровень: {level}");
        int price = CalculatePrice(level);
        if (button.isBlocked || level >= maxLevel || GameData.PlayerCoins < CalculatePrice(level))
            return;

        StartCoroutine(BlockButtonTimer(button)); // Запускаем таймер блокировки
        GameData.PlayerCoins -= price;
        level++;
        GameData.PlayerMaxEnergy++;
        GameData.Save();
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.UpdateUI();
        }
        UpdateUI();
        //Debug.Log($"Уровень после улучшения: {level}");
    }

    void TryUpgradeCoinSpawnCount(ref int level, UpgradeButton button)
    {
        //Debug.Log($"Начало улучшения. Текущий уровень: {level}");
        int price = CalculatePrice(level);
        if (button.isBlocked || level >= maxLevel || GameData.PlayerCoins < CalculatePrice(level))
            return;

        StartCoroutine(BlockButtonTimer(button)); // Запускаем таймер блокировки
        GameData.PlayerCoins -= price;
        level++;
        GameData.CoinSpawnCount++;
        GameData.Save();
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.UpdateUI();
        }
        UpdateUI();
        //Debug.Log($"Уровень после улучшения: {level}");
    }

    void TryUpgradeCoinSpawnChance(ref int level, UpgradeButton button)
    {
        //Debug.Log($"Начало улучшения. Текущий уровень: {level}");
        int price = CalculatePrice(level);
        if (button.isBlocked || level >= maxLevel || GameData.PlayerCoins < CalculatePrice(level))
            return;

        StartCoroutine(BlockButtonTimer(button)); // Запускаем таймер блокировки
        GameData.PlayerCoins -= price;
        level++;
        GameData.CoinSpawnChance += 0.05f;
        GameData.Save();
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.UpdateUI();
        }
        UpdateUI();
        //Debug.Log($"Уровень после улучшения: {level}");
    }

    public void UpgradeHealth() => TryUpgradeHealth(ref GameData.PlayerMaxHealthLevel, healthUpgrade);
    public void UpgradeDamage() => TryUpgradeBulletDamage(ref GameData.PlayerBulletDamageLevel, damageUpgrade);
    public void UpgradeEnergy() => TryUpgradePlayerMaxEnergy(ref GameData.PlayerMaxEnergyLevel, energyUpgrade);
    public void UpgradeCoinCount() => TryUpgradeCoinSpawnCount(ref GameData.CoinSpawnCountLevel, coinCountUpgrade);
    public void UpgradeCoinChance() => TryUpgradeCoinSpawnChance(ref GameData.CoinSpawnChanceLevel, coinChanceUpgrade);

    public void UpdateUI()
    {
        healthText.text = "Здоровье: " + GameData.PlayerMaxHealth;
        damageText.text = "Урон: " + GameData.PlayerBulletDamage;
        energyText.text = "Энергия: " + GameData.PlayerMaxEnergy;
        coinCountText.text = "Количество выпадающих монет: " + GameData.CoinSpawnCount;
        coinChanceText.text = "Шанс выпадения монет: " + GameData.CoinSpawnChance;

        healthLevelText.text = "Уровень: " + GameData.PlayerMaxHealthLevel;
        damageLevelText.text = "Уровень: " + GameData.PlayerBulletDamageLevel;
        energyLevelText.text = "Уровень: " + GameData.PlayerMaxEnergyLevel;
        coinCountLevelText.text = "Уровень: " + GameData.CoinSpawnCountLevel;
        coinChanceLevelText.text = "Уровень: " + GameData.CoinSpawnChanceLevel;

        UpdateButtonState(healthUpgrade, GameData.PlayerMaxHealthLevel);
        UpdateButtonState(damageUpgrade, GameData.PlayerBulletDamageLevel);
        UpdateButtonState(energyUpgrade, GameData.PlayerMaxEnergyLevel);
        UpdateButtonState(coinCountUpgrade, GameData.CoinSpawnCountLevel);
        UpdateButtonState(coinChanceUpgrade, GameData.CoinSpawnChanceLevel);
    }

    IEnumerator BlockButtonTimer(UpgradeButton button)
    {
        button.isBlocked = true;
        button.button.interactable = false;
        button.buttonImage.sprite = button.lockedSprite;

        yield return new WaitForSeconds(1f); // Ждем 1 секунду

        button.isBlocked = false;
        UpdateButtonState(button, GetCurrentLevel(button)); // Восстанавливаем состояние
    }

    int GetCurrentLevel(UpgradeButton button)
    {
        if (button == healthUpgrade) return GameData.PlayerMaxHealthLevel;
        if (button == damageUpgrade) return GameData.PlayerBulletDamageLevel;
        if (button == energyUpgrade) return GameData.PlayerMaxEnergyLevel;
        if (button == coinCountUpgrade) return GameData.CoinSpawnCountLevel;
        return GameData.CoinSpawnChanceLevel;
    }

    public void ResetLevels()
    {
        GameData.PlayerMaxHealth = 100;
        GameData.PlayerBulletDamage = 1;
        GameData.PlayerMaxEnergy = 5;
        GameData.CoinSpawnCount = 1;
        GameData.CoinSpawnChance = 0.15f;

        GameData.PlayerMaxHealthLevel = 0;
        GameData.PlayerBulletDamageLevel = 0;
        GameData.PlayerMaxEnergyLevel = 0;
        GameData.CoinSpawnCountLevel = 0;
        GameData.CoinSpawnChanceLevel = 0;
    }
}
