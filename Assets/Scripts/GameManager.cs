using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coins = 0;

    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<Sprite> bowSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public Player player;
    public Weapon weapon;
    public BowArrow bow;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    public Text arrowCounter;
  
    public int experience;

    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //upgrade weapon
    public bool TryUpgradeWeapon() {
        //is weapon maxed out?
        if (weaponPrices.Count <= weapon.weaponLevel) {
            return false;
        }
        if (coins >= weaponPrices[weapon.weaponLevel]) {
             coins -= weaponPrices[weapon.weaponLevel];
             weapon.UpgradeWeapon();
             return true;
        }

        return false;
    }

    //healthbar
    public void OnHitpointChange() {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //experience system
    public int GetCurrentLevel() {
        int r = 0;
        int add = 0;

        while (experience >= add) {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) { //max level
                return r;
            }
        }

        return r;
    }

    public int GetXpToLevel(int level) {
        int r = 0;
        int xp = 0;

        while (r < level) {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp) {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel()) {
            OnLevelUp();
        }
    }

    public void OnLevelUp() {
        player.OnLevelUp();
        OnHitpointChange();
        ShowText("Level Up! Level: " + player.level.ToString(), 40, Color.yellow, new Vector3(player.transform.position.x + 0.4f, player.transform.position.y + 0.8f, 0), Vector3.zero, 3.0f);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode) {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //death menu and respawn
    public void Respawn() {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }

    public void SaveState() {
        string s = "";

        s += "0" + "|"; //preferred sprite
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString(); //weapon level


        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin
        coins = int.Parse(data[1]);

        //experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1) {
            player.SetLevel(GetCurrentLevel());
        }

        //change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));     
    }
}
