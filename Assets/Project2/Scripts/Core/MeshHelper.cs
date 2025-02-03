using UnityEngine;

public static class MeshHelper
{
    public static Mesh CreateCuboidMesh(float width, float height, float depth)
    {
        Mesh mesh = new Mesh();


        Vector3[] vertices = new Vector3[8]
        {
            new Vector3(0,      0,      0),
            new Vector3(width,  0,      0),
            new Vector3(width,  height, 0),
            new Vector3(0,      height, 0),

            new Vector3(0,      0,      depth),
            new Vector3(width,  0,      depth),
            new Vector3(width,  height, depth),
            new Vector3(0,      height, depth)
        };

      
        int[] triangles = new int[]
        {
            // Back face
            2, 1, 0,
            0, 3, 2,

            // Front face
            4, 5, 6,
            6, 7, 4,

            // Left face
            3, 0, 4,
            4, 7, 3,

            // Right face
            1, 2, 6,
            6, 5, 1,

            // Top face
            2, 3, 7,
            7, 6, 2,

            // Bottom face
            1, 0, 4,
            4, 5, 1
        };
        
        Vector2[] uv = new Vector2[8]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
    
    public static void ScaleMeshToDimensions(Mesh mesh, Vector3 targetScale)
    {
        if (mesh == null)
        {
            Debug.LogError("Mesh bulunamadı veya null referans.");
            return;
        }

        // Mevcut mesh'in yerel (local) bounds bilgisini al
        // bounds.center = merkez (pivot'a göre), bounds.size = (en, boy, derinlik)
        Bounds bounds = mesh.bounds;

        // Mevcut boyutları al
        Vector3 currentSize = bounds.size;
        float currentWidth = currentSize.x;
        float currentHeight = currentSize.y;
        float currentDepth = currentSize.z;

        // Eğer mevcut boyut sıfır ise (örneğin düz bir yüzey veya tek eksende sıfır genişlik) 
        // bölme hatasını engellemek için kontrol edelim
        if (Mathf.Approximately(currentWidth, 0f) ||
            Mathf.Approximately(currentHeight, 0f) ||
            Mathf.Approximately(currentDepth, 0f))
        {
            Debug.LogWarning("Mesh boyutlarından biri sıfır, ölçeklendirme doğru çalışmayabilir!");
            return;
        }

        // Her eksen için ölçek katsayısını hesapla
        float scaleX = targetScale.x / currentWidth;
        float scaleY = targetScale.y / currentHeight;
        float scaleZ = targetScale.z / currentDepth;

        // Mevcut merkezin (pivot'a göre) konumu
        Vector3 center = bounds.center;

        // Vertex dizisini alıp yeniden ölçekleme ve merkez ayarlamasını yapalım
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            // 1) Mevcut vertex'leri önce pivot = (0,0,0) kabul edilecek şekilde merkeze göre kaydır
            //    (center'ı sıfıra çekmek için, vertex'ten center'ı çıkarıyoruz)
            Vector3 v = vertices[i] - center;

            // 2) İstenen boyutlara göre ölçekle
            v.x *= scaleX;
            v.y *= scaleY;
            v.z *= scaleZ;

            // 3) Tekrar vertices dizisine ata
            vertices[i] = v;
        }

        // Yeni vertices dizisini mesh'e geri yaz
        mesh.vertices = vertices;

        // Normalleri ve bounds'ı tekrar hesapla
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    
    public static float GetMeshXSize(Mesh mesh)
    {
        if (mesh == null)
        {
            Debug.LogError("Mesh is null!");
            return 0f;
        }

        Vector3[] vertices = mesh.vertices;

        if (vertices.Length == 0)
        {
            Debug.LogError("Mesh has no vertices!");
            return 0f;
        }

        float minX = float.MaxValue;
        float maxX = float.MinValue;

        foreach (Vector3 vertex in vertices)
        {
            if (vertex.x < minX) minX = vertex.x;
            if (vertex.x > maxX) maxX = vertex.x;
        }

        return maxX - minX;
    }
}