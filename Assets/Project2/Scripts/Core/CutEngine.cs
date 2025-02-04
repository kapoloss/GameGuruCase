using UnityEngine;

public static class CutEngine
{
    public static CutPlatformResult CutPlatform(Platform basePlatform,
                                                Platform cuttingPlatform,
                                                float minAllowedOverlap,
                                                Vector3 initialScale)
    {
        float xDif = cuttingPlatform.transform.position.x - basePlatform.transform.position.x;
        
        float basePlatformWidth = MeshHelper.GetMeshXSize(basePlatform.GetComponent<MeshFilter>().mesh);
        float cuttingPlatformWidth = MeshHelper.GetMeshXSize(cuttingPlatform.GetComponent<MeshFilter>().mesh);

        float overlapWidth = cuttingPlatformWidth - Mathf.Abs(xDif);
        
        if (overlapWidth < minAllowedOverlap || Mathf.Abs(xDif) > basePlatformWidth)
        {
            GameObject cuttingPlatformMock = Object.Instantiate(cuttingPlatform.gameObject, cuttingPlatform.transform.position, Quaternion.identity);
            cuttingPlatform.gameObject.SetActive(false);
            float torqueRandomness = 20;
            cuttingPlatformMock.AddComponent<Rigidbody>().AddTorque(
                new Vector3(Random.Range(-torqueRandomness, torqueRandomness), Random.Range(-torqueRandomness, torqueRandomness), Random.Range(-torqueRandomness, torqueRandomness)));
            Object.Destroy(cuttingPlatformMock,2);
            
            return new CutPlatformResult(false, null, null,xDif);
        }

        Vector3 nextPlatformNewScale = initialScale;
        nextPlatformNewScale.x = overlapWidth;

        cuttingPlatform.transform.position += Vector3.left * (xDif / 2f);

        MeshHelper.ScaleMeshToDimensions(cuttingPlatform.GetComponent<MeshFilter>().mesh, nextPlatformNewScale);

        GameObject fallenPart = CreateFallenPart(initialScale, xDif, overlapWidth, cuttingPlatform.transform.position,cuttingPlatform.meshRenderer.material);
        
        cuttingPlatform.ResizeCollider();

        return new CutPlatformResult(true, fallenPart, cuttingPlatform,xDif);
    }

    private static GameObject CreateFallenPart(Vector3 baseScale,
                                              float xDif,
                                              float overlapWidth,
                                              Vector3 cuttingPlatformPosition,
                                              Material baseMaterial)
    {
        GameObject fallenPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallenPart.SetActive(true);
        fallenPart.GetComponent<MeshRenderer>().material = baseMaterial;

        float fallenWidth = Mathf.Abs(xDif);
        fallenPart.transform.localScale = new Vector3(fallenWidth, baseScale.y, baseScale.z);

        float signOfCut = Mathf.Sign(xDif);
        float halfOverlap = overlapWidth / 2f;
        float halfFallen = fallenWidth / 2f;

        float finalNextPlatformX = cuttingPlatformPosition.x;
        float fallenPartX = finalNextPlatformX + signOfCut * (halfOverlap + halfFallen);

        Vector3 fallenPos = cuttingPlatformPosition;
        fallenPos.x = fallenPartX;
        fallenPart.transform.position = fallenPos;

        Rigidbody rb = fallenPart.AddComponent<Rigidbody>();
        rb.mass = 1f;

        Object.Destroy(fallenPart, 2f);

        return fallenPart;
    }
}