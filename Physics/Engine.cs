using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SharpGL.SceneGraph.Primitives;

namespace EnderEngine2D.Physics
{
    internal class Engine
    {
        public readonly Dictionary<int, StaticBody> StaticBodies = new Dictionary<int, StaticBody>();
        public readonly Dictionary<int, RigidBody> RigidBodies = new Dictionary<int, RigidBody>();
        public float Gravitition { get; set; }
        public Vector2 GravititionDirection { get; set; }
        public Engine(float g)
        {
            Gravitition = g;
            GravititionDirection = new Vector2(0, -1);
        }
        public Engine(Vector2 gravititionDirection)
        {
            GravititionDirection = Vector2.Normalize(gravititionDirection);
            Gravitition = gravititionDirection.X / GravititionDirection.X;
        }
        public void Update()
        {
            var rigidBodyPhysicsCalcTasks = new List<Task>();
            foreach (var rigid in RigidBodies.Values)
            {
                if (!rigid.IsPhysicsal)
                {
                    continue;
                }
                Task.Run(() =>
                {
                    var intersectingBodies = new HashSet<StaticBody>();
                    var newRigid = new RigidBody(new PointF(rigid.X + GravititionDirection.X * Gravitition + rigid.Force.X, rigid.Y - GravititionDirection.Y * Gravitition - rigid.Force.Y), rigid.Size);
                    foreach (var @static in StaticBodies.Values)
                    {
                        if (!@static.HasCollision)
                        {
                            continue;
                        }
                        if (newRigid.Bounds.IntersectsWith(@static.Bounds))
                        {
                            intersectingBodies.Add(@static);
                        }
                    }
                    if (intersectingBodies.Count == 1)
                    {
                        rigid.IsGrounded = true;
                        var ib = intersectingBodies.First();
                        MathUtils.RectangleOverlapCalculator.CalculateOverlapAreas(newRigid.Bounds, new[] { ib.Bounds }, out var topArea, out var bottomArea, out var leftArea, out var rightArea);
                        var areas = new[]
                            {
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                topArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Top),
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                bottomArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Bottom),
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                leftArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Left),
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                rightArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Right), };
                        Array.Sort(areas, (a, b) => a.Area.CompareTo(b.Area));
                        var area = areas.Last();
                        switch (area.Direction)
                        {
                            case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Top:
                                rigid.Y = ib.Y + ib.Height + 0.00002f;
                                break;
                            case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Bottom:
                                rigid.Y = ib.Y - rigid.Height - 0.00002f;
                                break;
                            case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Left:
                                rigid.X = ib.X + ib.Width + 0.00002f;
                                break;
                            case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Right:
                                rigid.X = ib.X - rigid.Width - 0.00002f;
                                break;
                        }
                        rigid.Position = new PointF(rigid.X + GravititionDirection.X * Gravitition + rigid.Force.X, rigid.Y - GravititionDirection.Y * Gravitition - rigid.Force.Y);
                        rigid.Force /= new Vector2(rigid.InertiaAttenuation, rigid.InertiaAttenuation);
                        if (MathUtils.InTolerance(rigid.Force, Vector2.Zero, 0.00002f))
                            rigid.Force = Vector2.Zero;
                    }
                    else if (intersectingBodies.Count > 0)
                    {
                        rigid.IsGrounded = true;
                        newRigid = rigid;
                        var task = new Task(() =>
                        {
                            MathUtils.RectangleOverlapCalculator.CalculateOverlapAreas(newRigid.Bounds, intersectingBodies.Select(a => a.Bounds), out var topArea, out var bottomArea, out var leftArea, out var rightArea);
                            var areas = new[]
                            {
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                topArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Top),
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                bottomArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Bottom),
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                leftArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Left),
                            new MathUtils.RectangleOverlapCalculator.AreaWithDirection(
                                rightArea, MathUtils.RectangleOverlapCalculator.DirectionOfArea.Right), };
                            Array.Sort(areas, (a, b) => a.Area.CompareTo(b.Area));
                            var nonZeroLamdba = new Func<MathUtils.RectangleOverlapCalculator.AreaWithDirection, bool>(c => c.Area != 0);
                            var nonZeroCount = areas.Count(nonZeroLamdba);
                            if (nonZeroCount == 1)
                            {
                                var nonZeroValue = areas.FirstOrDefault(nonZeroLamdba);
                                switch (nonZeroValue.Direction)
                                {
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Top:
                                        newRigid.Y += nonZeroValue.Area / rigid.Height;
                                        break;
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Bottom:
                                        newRigid.Y -= nonZeroValue.Area / rigid.Height;
                                        break;
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Left:
                                        newRigid.X += nonZeroValue.Area / rigid.Width;
                                        break;
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Right:
                                        newRigid.X -= nonZeroValue.Area / rigid.Width;
                                        break;
                                }
                                goto Next;
                            }
                            for (var i = areas.Length - 1; i > 0; i--)
                            {
                                switch (areas[i].Direction)
                                {
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Top:
                                        newRigid.X += areas[i].Area / newRigid.Width;
                                        break;
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Bottom:
                                        newRigid.X -= areas[i].Area / newRigid.Width;
                                        break;
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Left:
                                        newRigid.Y += areas[i].Area / newRigid.Height;
                                        break;
                                    case MathUtils.RectangleOverlapCalculator.DirectionOfArea.Right:
                                        newRigid.Y -= areas[i].Area / newRigid.Height;
                                        break;
                                }
                            }
                        Next:
                            rigid.Position = newRigid.Position;
                            rigid.Position = new PointF(rigid.X + rigid.Force.X, rigid.Y - rigid.Force.Y);
                            rigid.Force /= new Vector2(rigid.InertiaAttenuation, rigid.InertiaAttenuation);
                            if (MathUtils.InTolerance(rigid.Force, Vector2.Zero, 0.00002f))
                                rigid.Force = Vector2.Zero;
                        });
                        rigidBodyPhysicsCalcTasks.Add(task);
                        task.Start();
                    }
                    else
                    {
                        rigid.IsGrounded = false;
                        rigid.Position = new PointF(rigid.X + GravititionDirection.X * Gravitition + rigid.Force.X, rigid.Y - GravititionDirection.Y * Gravitition - rigid.Force.Y);
                        rigid.Force /= new Vector2(rigid.InertiaAttenuation, rigid.InertiaAttenuation);
                        if (MathUtils.InTolerance(rigid.Force, Vector2.Zero, 0.00002f))
                            rigid.Force = Vector2.Zero;
                    }
                });
            }
            //Task.WaitAll(rigidBodyPhysicsCalcTasks.ToArray());
        }

        private static class MathUtils
        {
            /// <summary>
            /// 检测一个向量a是否等于另一个向量b（向量b带误差）。
            /// </summary>
            /// <param name="source">向量b</param>
            /// <param name="target">向量a</param>
            /// <param name="tolerance">误差</param>
            /// <returns></returns>
            public static bool InTolerance(Vector2 source, Vector2 target, float tolerance)
            {
                return Vector2.Distance(source, target) < tolerance;
            }

            public static class RectangleOverlapCalculator
            {
                public enum DirectionOfArea
                {
                    Top,
                    Bottom,
                    Left,
                    Right,
                }

                public struct AreaWithDirection
                {
                    public float Area;
                    public DirectionOfArea Direction;
                    public AreaWithDirection(float area, DirectionOfArea direction)
                    {
                        Area = area;
                        Direction = direction;
                    }
                }

                public static void CalculateOverlapAreas(RectangleF a, IEnumerable<RectangleF> bRects,
                    out float topArea, out float bottomArea, out float leftArea, out float rightArea)
                {
                    // 定义矩形a的四个部分
                    var topPart = new RectangleF(a.X, a.Y, a.Width, a.Height / 2);
                    var bottomPart = new RectangleF(a.X, a.Y + a.Height / 2, a.Width, a.Height / 2);
                    var leftPart = new RectangleF(a.X, a.Y, a.Width / 2, a.Height);
                    var rightPart = new RectangleF(a.X + a.Width / 2, a.Y, a.Width / 2, a.Height);

                    // 计算每个部分的重叠面积
                    topArea = CalculateUnionAreaForPart(topPart, bRects);
                    bottomArea = CalculateUnionAreaForPart(bottomPart, bRects);
                    leftArea = CalculateUnionAreaForPart(leftPart, bRects);
                    rightArea = CalculateUnionAreaForPart(rightPart, bRects);
                }

                private static float CalculateUnionAreaForPart(RectangleF part, IEnumerable<RectangleF> bRects)
                {
                    // 计算所有b矩形与当前部分的交集
                    List<RectangleF> intersections = new List<RectangleF>();
                    foreach (var b in bRects)
                    {
                        if (Intersect(part, b, out RectangleF intersection))
                        {
                            intersections.Add(intersection);
                        }
                    }

                    // 使用扫描线算法计算并集面积
                    return CalculateUnionArea(intersections);
                }

                private static bool Intersect(RectangleF a, RectangleF b, out RectangleF result)
                {
                    var x1 = Math.Max(a.X, b.X);
                    var y1 = Math.Max(a.Y, b.Y);
                    var x2 = Math.Min(a.X + a.Width, b.X + b.Width);
                    var y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

                    if (x2 > x1 && y2 > y1)
                    {
                        result = new RectangleF(x1, y1, x2 - x1, y2 - y1);
                        return true;
                    }

                    result = new Rectangle();
                    return false;
                }

                // 使用扫描线算法计算矩形并集面积
                private static float CalculateUnionArea(List<RectangleF> rects)
                {
                    if (rects == null || rects.Count == 0)
                        return 0;

                    // 创建事件列表：每个矩形有左边界（入）和右边界（出）事件
                    List<Event> events = new List<Event>();
                    foreach (var rect in rects)
                    {
                        events.Add(new Event(rect.X, 1, rect));    // 左边界（入）
                        events.Add(new Event(rect.X + rect.Width, -1, rect)); // 右边界（出）
                    }

                    // 按x坐标排序事件，x相同时入事件优先
                    events.Sort((e1, e2) =>
                    {
                        int cmp = e1.X.CompareTo(e2.X);
                        if (cmp != 0) return cmp;
                        return e2.Type.CompareTo(e1.Type); // 入事件(type=1)排在出事件(type=-1)前面
                    });

                    List<RectangleF> activeRects = new List<RectangleF>();
                    float totalArea = 0;
                    var lastX = events[0].X;

                    for (int i = 0; i < events.Count; i++)
                    {
                        var current = events[i];
                        var currentX = current.X;

                        // 如果当前x坐标有变化，计算上一个区间到当前区间的面积
                        if (currentX > lastX)
                        {
                            var dx = currentX - lastX;
                            var totalY = GetCoveredLength(activeRects);
                            totalArea += dx * totalY;
                        }

                        // 处理当前事件
                        if (current.Type == 1) // 入事件
                        {
                            activeRects.Add(current.Rect);
                        }
                        else // 出事件
                        {
                            // 移除对应的矩形
                            for (int j = 0; j < activeRects.Count; j++)
                            {
                                if (AreRectsEqual(activeRects[j], current.Rect))
                                {
                                    activeRects.RemoveAt(j);
                                    break;
                                }
                            }
                        }

                        lastX = currentX;
                    }

                    return totalArea;
                }

                // 计算y轴上的覆盖总长度
                private static float GetCoveredLength(List<RectangleF> rects)
                {
                    if (rects.Count == 0)
                        return 0;

                    // 提取所有y区间
                    List<(float start, float end)> intervals = new List<(float, float)>();
                    foreach (var rect in rects)
                    {
                        intervals.Add((rect.Y, rect.Y + rect.Height));
                    }

                    // 合并重叠区间
                    intervals.Sort((a, b) => a.start.CompareTo(b.start));
                    List<(float start, float end)> merged = new List<(float, float)>();
                    var currentStart = intervals[0].start;
                    var currentEnd = intervals[0].end;

                    for (int i = 1; i < intervals.Count; i++)
                    {
                        if (intervals[i].start <= currentEnd)
                        {
                            currentEnd = Math.Max(currentEnd, intervals[i].end);
                        }
                        else
                        {
                            merged.Add((currentStart, currentEnd));
                            currentStart = intervals[i].start;
                            currentEnd = intervals[i].end;
                        }
                    }
                    merged.Add((currentStart, currentEnd));

                    // 计算总长度
                    var length = 0f;
                    foreach (var interval in merged)
                    {
                        length += interval.end - interval.start;
                    }
                    return length;
                }

                private static bool AreRectsEqual(RectangleF r1, RectangleF r2)
                {
                    return r1.X == r2.X &&
                           r1.Y == r2.Y &&
                           r1.Width == r2.Width &&
                           r1.Height == r2.Height;
                }

                // 事件结构
                private struct Event
                {
                    public float X { get; }
                    public int Type { get; } // 1: 入事件, -1: 出事件
                    public RectangleF Rect { get; }

                    public Event(float x, int type, RectangleF rect)
                    {
                        X = x;
                        Type = type;
                        Rect = rect;
                    }
                }
            }
        }
    }
}
