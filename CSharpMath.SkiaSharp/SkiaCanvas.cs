﻿using CSharpMath.Rendering;
using CSharpMath.Structures;
using SkiaSharp;
namespace CSharpMath.SkiaSharp
{
  public class SkiaCanvas : ICanvas {
    public SkiaCanvas(SKCanvas canvas) => Canvas = canvas;
    public SKCanvas Canvas { get; }
    public SKStrokeCap StrokeCap { get; set; }
    public Color DefaultColor { get; set; }
    public Color? CurrentColor { get; set; }
    public PaintStyle CurrentStyle { get; set; }

    private SKPaint _paint = new SKPaint { IsAntialias = true };
    private SKPath _path;

    private SKPaint StyledPaint(PaintStyle style, float strokeWidth = 0) {
      _paint.Color = Color;
      _paint.Style = (SKPaintStyle)style;
      _paint.StrokeWidth = strokeWidth;
      _paint.StrokeCap = StrokeCap;
      return _paint;
    }
    private SKPaint Paint => StyledPaint(CurrentStyle);
    private SKColor Color => CurrentColor.ToNative() ?? DefaultColor.ToNative();

    public void BeginRead(int contourCount) => _path = new SKPath();
    public void EndRead() => Canvas.DrawPath(_path, Paint);
    public void CloseContour() => _path.Close();
    public void Curve3(float x1, float y1, float x2, float y2) => _path.QuadTo(x1, y1, x2, y2);
    public void Curve4(float x1, float y1, float x2, float y2, float x3, float y3) => _path.CubicTo(x1, y1, x2, y2, x3, y3);
    public void LineTo(float x1, float y1) => _path.LineTo(x1, y1);
    public void MoveTo(float x0, float y0) { _path.Close(); _path.MoveTo(x0, y0); }

    public void StrokeRect(float left, float top, float width, float height) => Canvas.DrawRect(SKRect.Create(left, top, width, height), StyledPaint(PaintStyle.Stroke));
    public void FillColor()  => Canvas.DrawColor(Color);
    public void AddLine(float x1, float y1, float x2, float y2, float lineThickness) => Canvas.DrawLine(x1, y1, x2, y2, StyledPaint(PaintStyle.Stroke, lineThickness));
    public void Save() => Canvas.Save();
    public void Translate(float dx, float dy) => Canvas.Translate(dx, dy);
    public void Scale(float sx, float sy) => Canvas.Scale(sx, sy);
    public void Restore() => Canvas.Restore();
    public void FillText(string text, float x, float y, float pointSize) => Canvas.DrawText(text, x, y, new SKPaint { Color = Color, TextSize = pointSize, IsAntialias = true });
  }
}