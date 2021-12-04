using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Renderer : MonoBehaviour
{
    public Material RtRendererMaterial;
    public Transform RtRendererTransform;
    
    public Camera OverworldCamera;
    public Camera WaterCamera;
    public Camera FinalCamera;
    
    public Transform WaterLinePoint;

    public float Size;

    private Vector2Int prevRes;
    private RenderTexture rt;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTexturesAndCameras(new Vector2Int(Screen.width, Mathf.FloorToInt(Screen.height / 2f)));
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector2Int currentRes = new Vector2Int(Screen.width, Mathf.FloorToInt(Screen.height));
        if (/*prevRes != currentRes*/true) // always do it instead
        {
            float cameraTopPos = transform.position.y + Size/2f;
            float cameraBottomPos = transform.position.y - Size/2f;
            float waterUnitHeight = Mathf.Clamp01(Mathf.InverseLerp(cameraBottomPos*2f, cameraTopPos*2f, WaterLinePoint.position.y));
            float overworldUnitHeight = 1f - waterUnitHeight;

            if (waterUnitHeight > 0)
            {
                FinalCamera.aspect = WaterCamera.aspect = (1f / waterUnitHeight) * ((float)currentRes.x / (float)currentRes.y);
                FinalCamera.rect = new Rect(0f, 0f, 1f, waterUnitHeight);
                WaterCamera.rect = new Rect(0f, 0f, 1f, 1f);
                FinalCamera.enabled = true;
                WaterCamera.enabled = true;
                FinalCamera.orthographicSize = WaterCamera.orthographicSize = waterUnitHeight * Size;
                WaterCamera.transform.position = new Vector3(
                    transform.position.x,
                    cameraBottomPos*2f + waterUnitHeight * Size,
                    WaterCamera.transform.position.z
                    );

            }
            else
            {
                FinalCamera.enabled = false;
                WaterCamera.enabled = false;
            }
            if (overworldUnitHeight > 0)
            {
                OverworldCamera.aspect = (1f / overworldUnitHeight) * ((float)currentRes.x / (float)currentRes.y);
                OverworldCamera.rect = new Rect(0, waterUnitHeight, 1f, overworldUnitHeight);
                OverworldCamera.enabled = true;
                OverworldCamera.orthographicSize = Size * overworldUnitHeight;
                OverworldCamera.transform.position = new Vector3(
                    transform.position.x,
                    cameraTopPos*2f - overworldUnitHeight * Size,
                    OverworldCamera.transform.position.z
                    );


            }
            else
            {
                OverworldCamera.enabled = false;
            }



            // setup new render texture
            if (rt != null)
            {
                rt.Release();
            }
            if (waterUnitHeight > 0)
            {
                rt = new RenderTexture(currentRes.x, Mathf.RoundToInt(currentRes.y * waterUnitHeight), 0, RenderTextureFormat.ARGBFloat);
                WaterCamera.targetTexture = rt;
                RtRendererMaterial.SetTexture("_RT", rt);
                RtRendererTransform.localScale = new Vector3(WaterCamera.orthographicSize * WaterCamera.aspect * 2f, WaterCamera.orthographicSize * 2f, 1f);
            }


            // setup sizes of cameras

            prevRes = currentRes;
        }
    }
    private void UpdateTexturesAndCameras(Vector2Int res)
    {
        
    }
}
