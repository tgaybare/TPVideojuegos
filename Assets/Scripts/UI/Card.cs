using Assets.Scripts.Upgrades;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI
{
    public class Card : MonoBehaviour
    {
        public Image Image { get => _image; set => _image = value;}
        [SerializeField] private Image _image;

        public Text Title { get => _title; set => _title = value; }
        [SerializeField] private Text _title;

        public Text Description { get => _description; set => _description = value; }
        [SerializeField] private Text _description;

        public UpgradeID UpgradeID { get => _upgradeID; set => _upgradeID = value; }
        [SerializeField] private UpgradeID _upgradeID = UpgradeID.NONE;

        void Start()
        {
            if (_image == null)
            {
                _image = transform.Find("Picture").GetComponent<Image>();
                if (_image == null)
                {
                    Debug.LogError("Image is null");
                }
            }

            if(_title == null)
            {
                _title = transform.Find("TitleText").GetComponent<Text>();
                if (_title == null)
                {
                    Debug.LogError("Title is null");
                }
            }

            if (_description == null)
            {
                _description = transform.Find("DescriptionText").GetComponent<Text>();
                if (_description == null)
                {
                    Debug.LogError("Description is null");
                }
            }

            _title.text = "TITULARDOOO";
            _description.text = "DESCRIPCION";
        }


    }
}