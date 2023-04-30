using UnityEngine;

namespace Code.Runtime
{
    public static class VectorExtensions
    {
        /// <param name="self">this <see cref="Vector3"/></param>
        /// <returns>the horizontal <i>(<see cref="Vector3.x"/> and <see cref="Vector3.z"/>)</i> components of this <see cref="Vector3"/></returns>
        public static Vector2 Horizontal(this Vector3 self) => new(self.x, self.z);

        /// <param name="self">this <see cref="Vector3"/></param>
        /// <returns>the vertical (<see cref="Vector3.y"/>) component of this <see cref="Vector3"/></returns>
        public static float Vertical(this Vector3 self) => self.y;

        public static Vector3 OffsetTo(this Vector3 self, Vector3 other) => self - other;
        public static Vector3 OffsetFrom(this Vector3 self, Vector3 other) => other - self;

        /// <param name="self">this <see cref="Vector3"/></param>
        /// <param name="horizontal">the new <see cref="Vector3.x"/> and <see cref="Vector3.z"/></param>
        /// <returns>a new <see cref="Vector3"/> with modified horizontal <i>(<see cref="Vector3.x"/> and <see cref="Vector3.z"/>)</i> components</returns>
        public static Vector3 WithHorizontal(this Vector3 self, Vector2 horizontal) =>
            new Vector3(horizontal.x, self.y, horizontal.y);

        /// <param name="self">this <see cref="Vector3"/></param>
        /// <param name="vertical">the new <see cref="Vector3.y"/></param>
        /// <returns>a new <see cref="Vector3"/> with a modified vertical <i>(<see cref="Vector3.y"/>)</i> component</returns>
        public static Vector3 WithVertical(this Vector3 self, float vertical) => new Vector3(self.x, vertical, self.z);

        /// <param name="self">this <see cref="Vector3"/></param>
        /// <param name="axis">which <see cref="Axis3D"/> to modify</param>
        /// <param name="value">the new value of that <paramref name="axis"/></param>
        /// <returns>a new <see cref="Vector3"/> with one <see cref="Axis3D"/> modified</returns>
        public static Vector3 With(in this Vector3 self, Axis3D axis, float value)
        {
            var vNew = self;
            vNew[(int)axis] = value;
            return vNew;
        }

        /// <param name="self">this <see cref="Vector2"/></param>
        /// <param name="axis">which <see cref="Axis2D"/> to modify</param>
        /// <param name="value">the new value of that <paramref name="axis"/></param>
        /// <returns>a new <see cref="Vector2"/> with one <see cref="Axis2D"/> modified</returns>
        public static Vector2 With(in this Vector2 self, Axis2D axis, float value)
        {
            var vNew = self;
            vNew[(int)axis] = value;
            return vNew;
        }

        public static float Get(this Vector3 self, Axis3D axis) => self[(int)axis];
        public static float Get(this Vector2 self, Axis2D axis) => self[(int)axis];

        public static Vector3 With(this Vector3 self, (float? x, float? y, float? z) updates) =>
            new(updates.x ?? self.x, updates.y ?? self.y, updates.z ?? self.z);

        public static Vector2 With(this Vector2 self, (float? x, float? y) updates) =>
            new(updates.x ?? self.x, updates.y ?? self.y);
    }
}