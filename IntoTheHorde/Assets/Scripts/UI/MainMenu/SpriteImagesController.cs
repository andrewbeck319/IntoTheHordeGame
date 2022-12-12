using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

public class SpriteImagesController : MonoBehaviour
{
    public List<SpriteImage> SpriteImages = new List<SpriteImage>();

    private int _currentSpriteIndex;
    // Start is called before the first frame update
    void Start()
    {
        // start first
        this.SpriteImages[0].PlayInAnimation();
        
        InvokeRepeating("Next", 1f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        StartCoroutine(this._next());
    }

    private IEnumerator _next()
    {
        this.SpriteImages[this._currentSpriteIndex].PlayOutAnimation();
        yield return new WaitForSeconds(1.6f);
        this._currentSpriteIndex++;
        if (this._currentSpriteIndex >= this.SpriteImages.Count) this._currentSpriteIndex = 0;
        this.SpriteImages[this._currentSpriteIndex].PlayInAnimation();
    }
}
