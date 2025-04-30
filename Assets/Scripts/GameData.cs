using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int PlayerMaxHealth = 100;
    public static int PlayerBulletDamage = 1;
    public static int PlayerMaxEnergy = 5;
    public static int CoinSpawnCount = 1;
    [Range(0, 1)] public static float CoinSpawnChance = 0.15f;
    public static int PlayerCoins = 0;

    public static int PlayerMaxHealthLevel = 1;
    public static int PlayerBulletDamageLevel = 1;
    public static int PlayerMaxEnergyLevel = 1;
    public static int CoinSpawnCountLevel = 1;
    public static int CoinSpawnChanceLevel = 1;


    // Сохранение данных
    public static void Save()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth", PlayerMaxHealth);
        PlayerPrefs.SetInt("PlayerBulletDamage", PlayerBulletDamage);
        PlayerPrefs.SetInt("PlayerMaxEnergy", PlayerMaxEnergy);
        PlayerPrefs.SetInt("CoinSpawnCount", CoinSpawnCount);
        PlayerPrefs.SetFloat("CoinSpawnChance", CoinSpawnChance);
        PlayerPrefs.SetInt("PlayerMaxHealthLevel", PlayerMaxHealthLevel);
        PlayerPrefs.SetInt("PlayerBulletDamageLevel", PlayerBulletDamageLevel);
        PlayerPrefs.SetInt("PlayerMaxEnergyLevel", PlayerMaxEnergyLevel);
        PlayerPrefs.SetInt("CoinSpawnCountLevel", CoinSpawnCountLevel);
        PlayerPrefs.SetInt("CoinSpawnChanceLevel", CoinSpawnChanceLevel);
        PlayerPrefs.SetInt("PlayerCoins", PlayerCoins);
        PlayerPrefs.Save();
    }

    // Загрузка данных
    public static void Load()
    {
        PlayerMaxHealth = PlayerPrefs.GetInt("PlayerMaxHealth", 100);
        PlayerBulletDamage = PlayerPrefs.GetInt("PlayerBulletDamage", 1);
        PlayerMaxEnergy = PlayerPrefs.GetInt("PlayerMaxEnergy", 5);
        CoinSpawnCount = PlayerPrefs.GetInt("CoinSpawnCount", 1);
        CoinSpawnChance = PlayerPrefs.GetFloat("CoinSpawnChance", 0.1f);
        PlayerMaxHealthLevel = PlayerPrefs.GetInt("PlayerMaxHealthLevel", 1);
        PlayerBulletDamageLevel = PlayerPrefs.GetInt("PlayerBulletDamageLevel", 1);
        PlayerMaxEnergyLevel = PlayerPrefs.GetInt("PlayerMaxEnergyLevel", 1);
        CoinSpawnCountLevel = PlayerPrefs.GetInt("CoinSpawnCountLevel", 1);
        CoinSpawnChanceLevel = PlayerPrefs.GetInt("CoinSpawnChanceLevel", 1);
        PlayerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
    }
}
