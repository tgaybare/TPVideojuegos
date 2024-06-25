using Assets.Scripts.Upgrades;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _title;
        [SerializeField] private Text _description;
        [SerializeField] private Button _button;
        [SerializeField] private UpgradeID _upgradeID = UpgradeID.NONE;

        void Start()
        {
            setComponentsIfNull();

            _button.onClick.AddListener(ApplyUpgrade);
        }

        private void setComponentsIfNull() {
            if (_image == null)
            {
                _image = transform.Find("Picture").GetComponent<Image>();
                if (_image == null)
                {
                    Debug.LogError("Image is null");
                }
            }

            if (_title == null)
            {
                _title = transform.Find("TitleText").GetComponent<Text>();
                if (_title == null)
                {
                    Debug.LogError("GetTitle is null");
                }
            }

            if (_description == null)
            {
                _description = transform.Find("DescriptionText").GetComponent<Text>();
                if (_description == null)
                {
                    Debug.LogError("GetDescription is null");
                }
            }

            if (_button == null)
            {
                _button = transform.Find("Button").GetComponent<Button>();
                if (_button == null)
                {
                    Debug.LogError("Button is null");
                }
            }
        }

        private void ApplyUpgrade()
        {
            UpgradeManager.instance.ApplyUpgrade(_upgradeID);
        }

        public void SetUpgradeInfo(IAppliableUpgrade upgrade)
        {
            _title.text = upgrade.GetTitle();
            _description.text = upgrade.GetDescription();
            _upgradeID = upgrade.GetUpgradeID();
            Debug.Log($"Setting upgrade info for {_title.text} - {_description.text} [{_upgradeID}]");
        }


    }
}