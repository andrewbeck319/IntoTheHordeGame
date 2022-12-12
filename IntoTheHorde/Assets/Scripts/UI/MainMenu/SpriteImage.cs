using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Menu
{
    public class SpriteImage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlayInAnimation()
        {
            this.GetComponent<Animator>().SetTrigger("In");
        }

        public void PlayOutAnimation()
        {
            this.GetComponent<Animator>().SetTrigger("Out");
        }
    }

}
