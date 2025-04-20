using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public PlayerModel playerModel;
    public UIManager uiManager;

    public int lifeCost = 100;

    public void BuyLife()
    {
        if (scoreManager.GetScore() >= lifeCost)
        {
            scoreManager.SpendScore(lifeCost);
            scoreManager.AddLife();
            uiManager.UpdateUI();
        }
    }

    //public void NextSkin() => playerModel.EnableNextSkin();
    //public void PreviousSkin() => playerModel.EnablePreviousSkin();
}