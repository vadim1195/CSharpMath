using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpMath.Editor {
  using Atoms;
  public readonly struct MathListRange {
    public MathListRange(int start) => (Start, Length) = (MathListIndex.Level0Index(start), 1);
    public MathListRange(MathListIndex start) => (Start, Length) = (start, 1);
    public MathListRange(MathListIndex start, int length) => (Start, Length) = (start, length);
    public MathListRange(Range range) => (Start, Length) = (MathListIndex.Level0Index(range.Location), range.Length);

    public MathListIndex Start { get; }
    public int Length { get; }
    public MathListRange? SubIndexRange => Start.SubIndexType != MathListSubIndexType.None ? new MathListRange(Start.SubIndex, Length) : new MathListRange?();
    public Range FinalRange => new Range(Start.FinalIndex, Length);

    public override string ToString() => $"({Start}, {Length})";

    public static MathListRange operator +(MathListRange left, MathListRange right) {
      if (left.Start.AtSameLevel(right.Start)) throw new InvalidOperationException($"Cannot union ranges at different levels: {left}, {right}");
      var leftRange = left.FinalRange;
      var rightRange = right.FinalRange;
      var unionRange = leftRange + rightRange;
      var start = unionRange.Location == leftRange.Location ? left.Start : right.Start;
      return new MathListRange(start, unionRange.Length);
    }

    public static MathListRange Combine(IEnumerable<MathListRange> ranges) => ranges.Aggregate((acc, curr) => acc + curr);
  }
}
