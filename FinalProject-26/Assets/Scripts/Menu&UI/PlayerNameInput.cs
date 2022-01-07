using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
    {
        [Header("EnterNameUI")]
        [SerializeField] public TMP_InputField nameInputField = null;
        [SerializeField] private Button Confirm= null;
        public static string Getname;
        private const string PlayerNameKey = "PlayerName"; //Defaltname if player didn't enter any name 

         private void Start() { GetInputField(); }

        private void GetInputField()
        {
            nameInputField.text = PlayerPrefs.GetString(PlayerNameKey);

            SetName(nameInputField.text);
        }

        public void SetName(string name)
        {
            Confirm.interactable = !string.IsNullOrEmpty(name); //check the button is only interacable when field input is not null
        }

        public void SaveName()
        {
            Getname = nameInputField.text; //Pass the name string into the static string
            PlayerPrefs.SetString(PlayerNameKey, nameInputField.text);
            //Debug.Log(Getname);
        }
}
