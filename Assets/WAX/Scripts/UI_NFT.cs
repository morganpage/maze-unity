using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_NFT : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;

    public async void SetNFT(string name, string ipfsID)
    {
        _name.text = name;
        _image.sprite = await GetSpriteAsync("https://gateway.pinata.cloud/ipfs/" + ipfsID);
    }

    async Task<Sprite> GetSpriteAsync(string imageUrl){
        var request = UnityWebRequestTexture.GetTexture(imageUrl);
        await request.SendWebRequest();
        while(!request.isDone) await Task.Yield();
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        Sprite sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.one * 0.5f);
        return sprite;
    }

}
