using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WeaponSelector : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPositionIndex(int index)
        {
            float x = 0f;
            if (index == 0) x = -100f;
            if (index == 1) x = 0f;
            if (index == 2) x = 100f;
            
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, this.GetComponent<RectTransform>().position.y);
        }
    }
}