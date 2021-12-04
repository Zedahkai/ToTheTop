using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public Text errorText;

    /*
    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }
    */
    public void GoBack()
    {
        Client.instance.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoginPlayer()
    {
        if (!Client.instance.getConnected())
        {
            UIManager.instance.GetComponent<UIManager>().ConnectToServer();
        }
        ClientSend.LoginInfo();
        //Client.Disconnect();
    }

    public void ErrorText(string str)
    {
        errorText.text = str;
    }

    /*
    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;

        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
            UIManager.instance.GetComponent<UIManager>().ConnectToServer();
            Debug.Log("User login valid");
            www = null;
            form = null;
        }
        else
        {
            errorText.text = "User login failed. \n" + www.text;
            Debug.Log("User login failed. Error #" + www.text);
        }
    }
    */
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);

    }

    

}
