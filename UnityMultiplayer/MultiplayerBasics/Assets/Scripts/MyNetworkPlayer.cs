using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] TMP_Text displayNameText = null;
    [SerializeField] Renderer displayColor = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    Color playerColor = new Color(0, 0, 0);

    #region Server
    [Server]
    public void SetDisplayName(string newName)
    {
        displayName = newName;
    }

    [Server]
    public void SetPlayerColor(Color newColor)
    {
        playerColor = newColor;
    }

    [Command]
    private void CmdSetDisplayNAme(string newName)
    {
        if (!ValidateName(newName))
        {
            return;
        }
        RpcLogNewName(newName);
        SetDisplayName(newName);
    }

    private bool ValidateName(string newName)
    {
        if (2 >= newName.Length)
        {
            return false;
        }

        return true;
    }

    #endregion

    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColor.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    public void SetMyNAme()
    {
        CmdSetDisplayNAme("My");
    }

    [ClientRpc]//TargetRpc is on the owner
    private void RpcLogNewName(string newName)
    {
        Debug.Log(newName);
    }

    #endregion
}
