using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CSharpMath.Atoms;
using CSharpMath.Display.Text;
using CSharpMath.FrontEnd;
using Color = CSharpMath.Structures.Color;

namespace CSharpMath.Display {
  public class GlyphConstructionDisplay<TFont, TGlyph> : IDownshiftableDisplay<TFont, TGlyph> where TFont : IFont<TGlyph> {
    private readonly IEnumerable<TGlyph> _glyphs;
    private readonly IEnumerable<PointF> _glyphPositions;
    private readonly TFont _mathFont;

    public float ShiftDown { get; set; }
    public RectangleF DisplayBounds => this.ComputeDisplayBounds();

    public float Ascent { get; set; }

    public float Descent { get; set; }

    public float Width { get; set; }

    public Range Range { get; set; }

    public PointF Position { get; set; }

    public void SetPosition(PointF position) {
      Position = position;
    }

    public bool HasScript { get; set; }

    public GlyphConstructionDisplay(IEnumerable<TGlyph> glyphs, IEnumerable<float> offsets, TFont font) {
      _glyphs = glyphs;
      _glyphPositions = offsets.Select(x => new PointF(0, x));
      _mathFont = font;
    }

    public void Draw(IGraphicsContext<TFont, TGlyph> context){
      context.SaveState();
      PointF delta = new PointF(Position.X, Position.Y - ShiftDown);
      context.Translate(delta);
      context.SetTextPosition(new PointF());
      context.DrawGlyphsAtPoints(_glyphs.AsForEach(), _mathFont, _glyphPositions.AsForEach(), TextColor);
      context.RestoreState();
    }
    public Color? TextColor { get; set; }

    public void SetTextColorRecursive(Color? textColor) {
      TextColor = TextColor ?? textColor;
    }
  }
}
