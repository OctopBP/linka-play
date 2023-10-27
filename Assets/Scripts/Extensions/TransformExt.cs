using UnityEngine;

namespace Extensions
{
    public static class TransformExt
    {
        public static void SetPositionX(this Transform self, float x)
        {
            var position = self.position;
            position = new(x, position.y, position.z);
            self.position = position;
        }
        
        public static void SetPositionY(this Transform self, float y)
        {
            var position = self.position;
            position = new(position.x, y, position.z);
            self.position = position;
        }
        
        public static void SetPositionZ(this Transform self, float z)
        {
            var position = self.position;
            position = new(position.x, position.y, z);
            self.position = position;
        }
        
        public static void SetLocalPositionX(this Transform self, float x)
        {
            var localPosition = self.localPosition;
            localPosition = new(x, localPosition.y, localPosition.z);
            self.localPosition = localPosition;
        }
        
        public static void SetLocalPositionY(this Transform self, float y)
        {
            var localPosition = self.localPosition;
            localPosition = new(localPosition.x, y, localPosition.z);
            self.localPosition = localPosition;
        }
        
        public static void SetLocalPositionZ(this Transform self, float z)
        {
            var localPosition = self.localPosition;
            localPosition = new(localPosition.x, localPosition.y, z);
            self.localPosition = localPosition;
        }
    }
}