#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Code.Runtime
{
    public class HandManager : MonoBehaviour
    {
        private static HandManager? _current;

        // ReSharper disable once ReplaceConditionalExpressionWithNullCoalescing
        public static HandManager Current => _current is { } c
            ? c
            : throw new NullReferenceException("The global HandManager was never created!");

        public readonly Plane GroundPlane = new(Vector3.up, 0);
        public Pickup? Holding;

        /// <summary>
        /// The size, in world space, of both sides of a cell (because a cell is always a square). 
        /// </summary>
        public float CellDiameter = 1;

        #region Snap points

        /// <summary>
        /// Gets the cell that contains a point <paramref name="offsetFromOrigin"/> distance away from <see cref="GridOrigin"/>.
        /// </summary>
        /// <param name="offsetFromOrigin">the distance, in world units, </param>
        /// <returns>the index of the cell that contains that point</returns>
        [Pure]
        private static int GetContainingCell(float offsetFromOrigin)
        {
            return Mathf.FloorToInt(offsetFromOrigin);
        }

        [Pure]
        private static int GetClosestSnapPoint(float offsetFromOrigin,
            MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return (int)Math.Round(offsetFromOrigin, rounding);
        }

        [Pure]
        private Vector2Int GetContainingCell(Vector2 worldHorizontal)
        {
            var cellX = GetContainingCell(worldHorizontal.x);
            var cellY = GetContainingCell(worldHorizontal.y);
            return new Vector2Int(cellX, cellY);
        }

        [Pure]
        private static Vector2Int GetClosestSnapPoint(Vector2 worldHorizontal)
        {
            var snapX = GetClosestSnapPoint(worldHorizontal.x);
            var snapY = GetClosestSnapPoint(worldHorizontal.y);
            return new Vector2Int(snapX, snapY);
        }

        [Pure]
        private Vector2 SnapToGrid(Vector2 worldHorizontal)
        {
            var snapPoint = GetClosestSnapPoint(worldHorizontal);
            var snapOffset = new Vector2(snapPoint.x * CellDiameter, snapPoint.y * CellDiameter);
            return snapOffset;
        }

        [Pure]
        private Vector3 SnapToGrid(Vector3 worldPos)
        {
            var snapped = SnapToGrid(worldPos.Horizontal());
            return worldPos.WithHorizontal(snapped);
        }

        [SuppressMessage("ReSharper", "Unity.NoNullCoalescing")]
        private readonly Lazy<Camera> MyCamera =
            new(static () => Camera.main ?? throw new NullReferenceException("No main camera set!"));

        private Camera GetCamera()
        {
            return MyCamera.Value;
        }

        private bool TryGetHandPosition(out Vector3 handPosition)
        {
            if (Input.mousePresent == false)
            {
                handPosition = default;
                return false;
            }

            if (Input.mousePosition is not { x: >= 0, y: >= 0 } mouse)
            {
                handPosition = default;
                return false;
            }

            var handRay = GetCamera().ScreenPointToRay(mouse + Vector3.forward);
            var gp = GroundPlane;
            if (gp.Raycast(handRay, out var distance))
            {
                handPosition = handRay.GetPoint(distance);
                return true;
            }

            handPosition = default;
            return false;
        }

        #endregion

        private void Awake()
        {
            if (_current is null)
            {
                _current = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void Update()
        {
            MoveHeldObject();
        }

        private void MoveHeldObject()
        {
            if (Holding is null || !TryGetHandPosition(out var handPosition))
            {
                return;
            }

            var snapPoint = SnapToGrid(handPosition);

            if (Input.GetMouseButton(0))
            {
                Holding.SnapAbove(snapPoint);
            }
            else
            {
                Holding.SnapTo(snapPoint);
                Holding = null;
            }
        }
    }
}