using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public InputField verifyPasswordField;

    public Button submitButton;

    public Text passwordVerification;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //Method to register user with inputs from inputfields. Once registered, user is sent to main menu.
    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }

    //To check if inputs are valid before submitting.
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8) && (passwordField.text == verifyPasswordField.text);
        if(passwordField.text == verifyPasswordField.text)
        {
            passwordVerification.text = "";
        }
        else
        {
            passwordVerification.text = "Password inputs do not match!";
        }
    }
}
