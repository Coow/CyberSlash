using UnityEngine;

// <summary>
// If you want know more about this check this link.
// Click <see href = "http://wiki.unity3d.com/index.php?title=IsVisibleFrom">here</see>
// </summary>

public static class RendererExtensions
{
    
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
