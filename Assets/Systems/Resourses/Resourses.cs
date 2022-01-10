using UnityEngine;

public class Resourses : MonoBehaviour
{
    public Resourses_txt_coins coinsKey;
    public Resourses_txt_coins hairsKey;
    string coinKey;
    string hairKey;
    void Start()
    {
        Events_Resourses.Instance.On_CoinPickup += CoinPickup;
        Events_Barber.Instance.On_HairClipped += HairClipped;
        coinKey = coinsKey.this_key;
        hairKey = hairsKey.this_key;
    }

    void HairClipped(Vector2 pos, bool isPolice, bool PoliceHair) => IncValue(hairKey, 1);
    void CoinPickup(int amount) => IncValue(coinKey, amount);



    void IncValue(string key, int amount)
    {
        var value = PlayerPrefs.GetInt(key);
        value += amount;
        PlayerPrefs.SetInt(key, value);
    }
}
