using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Building building = null;
    [SerializeField] LayerMask floorMask = new LayerMask();
    [SerializeField] Image iconImage = null;
    [SerializeField] TMP_Text priceText = null;

    Camera mainCamera;
    RTSPlayer player;
    GameObject buildingPreviewInstance;
    Renderer buildingRenderInstance;
    BoxCollider buildingCollider;

    private void Start()
    {
        mainCamera = Camera.main;

        iconImage.sprite = building.GetIcon();
        priceText.text = building.GetPrice().ToString();

        player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();

        buildingCollider = building.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        
        if (buildingPreviewInstance == null)
        {
            return;
        }

        UpdateBuildingPreview();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        buildingPreviewInstance = Instantiate(building.GetBuildingPreview());
        buildingRenderInstance = buildingPreviewInstance.GetComponentInChildren<Renderer>();

        buildingPreviewInstance.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance == null)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            player.CmdTryPlaceBuilding(building.GetID(), hit.point);
        }

        Destroy(buildingPreviewInstance);
    } 

    void UpdateBuildingPreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            return;
        }

        buildingPreviewInstance.transform.position = hit.point;
        if (!buildingPreviewInstance.activeSelf)
        {
            buildingPreviewInstance.SetActive(true);
        }
    }
}
