using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class PlayerLogin : MonoBehaviour
{
    public GameObject LoginPanel, RegisterPanel, LoginCanvas;
    public TMP_Text LoginErr, RegErr,UsernameText,CoinsText;
    public TMP_InputField Lusername, Lpassword, Remail, Rusername, Rpassword;
    public int coins;
    public GameObject ConnectWallet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchPanel()
    {
        if (LoginPanel.activeInHierarchy)
        {
            LoginPanel.SetActive(false);
            RegisterPanel.SetActive(true);
        }
        else
        {
            LoginPanel.SetActive(true);
            RegisterPanel.SetActive(false);
        }
    }
    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Rusername.text);
        form.AddField("password", Rpassword.text);
        form.AddField("email", Remail.text);
        WWW www = new WWW("https://havenverse.world/Middleware/register.php", form);
        yield return www;
        if (www.text == "1")
        {
            Debug.Log("user created succefully");
            RegErr.text = "Account registered sucessfully";
            RegErr.color = Color.green;
            SwitchPanel();
        }
        else if (www.text == "2")
        {
            Debug.Log("Registeration failed");
            RegErr.text = "username already registered";
            RegErr.color = Color.red;
        }
        else if (www.text == "3")
        {
            Debug.Log("Registeration failed");
            RegErr.text = "email already registered";
            RegErr.color = Color.red;
        }
        else
        {
            Debug.Log("Registeration failed");
            RegErr.text = "Registration failed, try again later";
            RegErr.color = Color.red;
        }
    }
    public void CallLogin()
    {
        StartCoroutine(Login());
    }
    IEnumerator Login() 
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Lusername.text);
        form.AddField("password", Lpassword.text);
        WWW www = new WWW("https://havenverse.world/Middleware/login.php", form);
        yield return www;
        if (www.text[0] == '1')
        {
            Debug.Log("Logged in succefully");
            LoginErr.text = "Loggeed in sucessfully";
            LoginErr.color = Color.green;
            string[] responseParts = www.text.Split(' ');
            coins = int.Parse(responseParts[1]);
            LoginCanvas.SetActive(false);
            ConnectWallet.SetActive(true);
            UsernameText.text = Lusername.text;
            CoinsText.text = coins.ToString();
            PlayerPrefs.SetString("username", Lusername.text);
            PhotonNetwork.NickName = Lusername.text;
        }
        else if (www.text[0] == '2')
        {
            Debug.Log("login failed");
            LoginErr.text = "username dosen't exist";
            LoginErr.color = Color.red;
        }
        else if (www.text[0] == '3')
        {
            Debug.Log("login failed");
            LoginErr.text = "incorrect password";
            LoginErr.color = Color.red;
        }
        else
        {
            Debug.Log("login failed");
            LoginErr.text = "Incorrect username or password";
            LoginErr.color = Color.red;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
