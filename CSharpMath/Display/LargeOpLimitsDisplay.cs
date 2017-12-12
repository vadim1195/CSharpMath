﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CSharpMath.Atoms;

namespace CSharpMath.Display {
  public class LargeOpLimitsDisplay<TGlyph> : IDisplay<TGlyph> {
    // A display representing the numerator of the fraction. Its position is relative
    // to the parent and it is not treated as a sub-display.
    public MathListDisplay<TGlyph> UpperLimit { get; private set; }
    // A display representing the numerator of the fraction. Its position is relative
    // to the parent and it is not treated as a sub-display.
    public MathListDisplay<TGlyph> LowerLimit { get; private set; }

    public RectangleF DisplayBounds => throw new NotImplementedException();

    public float Ascent => throw new NotImplementedException();

    public float Descent => throw new NotImplementedException();

    public float Width => throw new NotImplementedException();

    public Range Range => throw new NotImplementedException();

    public PointF Position => throw new NotImplementedException();
    public bool HasScript { get; set; }
    public void Draw(IGraphicsContext<TGlyph> context) => throw new NotImplementedException();
  }
}