using UnityEngine;
struct PreciseVector3
{
    public double x, y, z;
    public float magnitude {
        get { return Mathf.Sqrt((float)(x * x + y * y + z * z));}
    }

    public PreciseVector3(Vector3 vector3)
    {
        this.x = vector3.x; this.y = vector3.y; this.z = vector3.z;
    }
    public PreciseVector3(double x, double y, double z)
    {
        this.x = x; this.y = y; this.z = z;
    }
    public static implicit operator PreciseVector3(Vector3 input)
    {
        return new PreciseVector3(input);
    }
    public static PreciseVector3 operator *(PreciseVector3 vector, double scalar)
    {
        return new PreciseVector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
    }
    
    public PreciseVector3 Dot(PreciseVector3 target)
    {
        return new PreciseVector3(this.x * target.x, this.y * target.y, this.z * target.z);
    }
}
