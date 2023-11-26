using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeVfx : MonoBehaviour
{
    public List<Sprite> rocksprite;
    public List<Sprite> edmsprite;
    public Material rockvfx;
    public Material edmvfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(swapmechanic.instance.player1Active)
        {
            for(int i = 0; i < 2;  i++)
            {
                GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(i, rocksprite[i]);
            }
            GetComponent<ParticleSystemRenderer>().material = rockvfx;

        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(i, edmsprite[i]);
            }
            GetComponent<ParticleSystemRenderer>().material = edmvfx;
        }
    }
}
