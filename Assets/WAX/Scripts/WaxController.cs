using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtomicAssetsApiClient;
using AtomicAssetsApiClient.Assets;
using UnityEngine;
using UnityEngine.UI;
using UniversalAuthenticatorLibrary;
using UniversalAuthenticatorLibrary.Src.Canvas;

public class WaxController : MonoBehaviour
{
    [SerializeField] private UnityCanvasUAL _unityCanvasUAL;
    [SerializeField] private GameObject _nftPrefab;
    [SerializeField] private Transform _nftParent;
 
    void Start()
    {
        foreach(Transform child in _nftParent) Destroy(child.gameObject);
        _unityCanvasUAL.OnUserLogin += UserLogin;
        _unityCanvasUAL.Init();
    }

    private async void UserLogin(User user)
    {
        string accountName = await user.GetAccountName();
        Debug.Log($"User {accountName} has logged in.");
        await GetAllFromCollection(accountName,"platoevolved");
    }

    private async Task GetAllFromCollection(string accountName, string collectionName){
        var assetsParams = new AssetsUriParameterBuilder();
        assetsParams.WithOwner(accountName);//CHANGE!!!!!!!!!!!
        assetsParams.WithCollectionName(collectionName);
        var collectionData = await AtomicAssetsApiFactory.Version1.AssetsApi.Assets(assetsParams);
        for (int i = 0; i < collectionData.Data.Length; i++)
        {
            var data = collectionData.Data[i];
            string name = data.Template.ImmutableData.Name;
            string imageUrl = data.Template.ImmutableData.Image;
            Debug.Log(name + " " + imageUrl);
            //Use a UINFT prefab to create an instance of UINFT
            var nft = Instantiate(_nftPrefab,_nftParent);
            UI_NFT ui_NFT = nft.GetComponent<UI_NFT>();
            //call SetNFT with name and imageUrl
            ui_NFT.SetNFT(name,imageUrl);
            //Set up the event on the button click
            Button button = nft.GetComponent<Button>();
            button.onClick.AddListener(()=>{
                Debug.Log(name);
            });
        }
    }
}
