using System;
using Unity.Collections;
using UnityEngine;

public static class Maths
{
    public static void NormalizeVec2(ref Vector2 vec)
    {
        float len_sq = vec.x * vec.x + vec.y * vec.y;

        if (len_sq < 1e-12f)
            return;

        float inv_len = 1f / (float)Math.Sqrt(len_sq);

        vec.x *= inv_len;
        vec.y *= inv_len;
    }

    public static void NormalizeVec3(ref Vector3 vec)
    {
        float len_sq = vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;

        if (len_sq < 1e-12f)
            return;

        float inv_len = 1f / (float)Math.Sqrt(len_sq);

        vec.x *= inv_len;
        vec.y *= inv_len;
        vec.z *= inv_len;
    }
}
