//
//  ArrowSliderStyleKit.cs
//  Fcaico.Controls.ArrowSlider
//
//  Created by Frank Caico on 11/16/14.
//  Copyright (c) 2014 YuFit. All rights reserved.
//
//  Generated by PaintCode (www.paintcodeapp.com)
//



using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace Fcaico.Controls.ArrowSlider
{
    [Register ("ArrowSliderStyleKit")]
    internal class ArrowSliderStyleKit : NSObject
    {

        //// Initialization

        static ArrowSliderStyleKit()
        {
        }

        //// Drawing Methods

        public static void DrawArrowSlider(UIColor fillColor, UIColor outlineColor, float percentFull, PointF position, SizeF overallSize)
        {
            //// General Declarations
            var context = UIGraphics.GetCurrentContext();


            //// Variable Declarations
            var sizeOfFill = percentFull * overallSize.Width < 35.0f ? new SizeF(35.0f, overallSize.Height) : new SizeF(percentFull * overallSize.Width, overallSize.Height);

            //// Outline Drawing
            RectangleF outlineRect = new RectangleF(position.X, position.Y, overallSize.Width, overallSize.Height);
            context.SaveState();
            context.ClipToRect(outlineRect);
            //context.Translate(context, outlineRect.X, outlineRect.Y);
			context.TranslateCTM (outlineRect.X, outlineRect.Y);

            ArrowSliderStyleKit.DrawArrowOutline(new RectangleF(0.0f, 0.0f, outlineRect.Width, outlineRect.Height), outlineColor);
            context.RestoreState();


            //// Fill Drawing
            RectangleF fillRect = new RectangleF(position.X, position.Y, sizeOfFill.Width, sizeOfFill.Height);
            context.SaveState();
            context.ClipToRect(fillRect);
            //context.Translate(context, fillRect.X, fillRect.Y);
			context.TranslateCTM (fillRect.X, fillRect.Y);


            ArrowSliderStyleKit.DrawArrowFill(new RectangleF(0.0f, 0.0f, fillRect.Width, fillRect.Height), fillColor);
            context.RestoreState();
        }

        public static void DrawArrowFill(RectangleF frameFill, UIColor fillColor)
        {

            //// FillShape Drawing
            UIBezierPath fillShapePath = new UIBezierPath();
            fillShapePath.MoveTo(new PointF(frameFill.GetMinX() + 5.0f, frameFill.GetMaxY() - 5.0f));
            fillShapePath.AddLineTo(new PointF(frameFill.GetMaxX() - 24.77f, frameFill.GetMaxY() - 5.0f));
            fillShapePath.AddLineTo(new PointF(frameFill.GetMaxX() - 5.0f, frameFill.GetMinY() + 0.50000f * frameFill.Height));
            fillShapePath.AddLineTo(new PointF(frameFill.GetMaxX() - 24.77f, frameFill.GetMinY() + 5.0f));
            fillShapePath.AddLineTo(new PointF(frameFill.GetMinX() + 5.0f, frameFill.GetMinY() + 5.0f));
            fillShapePath.AddLineTo(new PointF(frameFill.GetMinX() + 5.0f, frameFill.GetMaxY() - 5.0f));
            fillShapePath.ClosePath();
            fillColor.SetFill();
            fillShapePath.Fill();
        }

        public static void DrawArrowOutline(RectangleF frameOutline, UIColor outlineColor)
        {

            //// OutlineShape Drawing
            UIBezierPath outlineShapePath = new UIBezierPath();
            outlineShapePath.MoveTo(new PointF(frameOutline.GetMinX() + 5.0f, frameOutline.GetMaxY() - 5.0f));
            outlineShapePath.AddLineTo(new PointF(frameOutline.GetMaxX() - 24.77f, frameOutline.GetMaxY() - 5.0f));
            outlineShapePath.AddLineTo(new PointF(frameOutline.GetMaxX() - 5.0f, frameOutline.GetMinY() + 0.50000f * frameOutline.Height));
            outlineShapePath.AddLineTo(new PointF(frameOutline.GetMaxX() - 24.77f, frameOutline.GetMinY() + 5.0f));
            outlineShapePath.AddLineTo(new PointF(frameOutline.GetMinX() + 5.0f, frameOutline.GetMinY() + 5.0f));
            outlineShapePath.AddLineTo(new PointF(frameOutline.GetMinX() + 5.0f, frameOutline.GetMaxY() - 5.0f));
            outlineShapePath.ClosePath();
            outlineColor.SetStroke();
            outlineShapePath.LineWidth = 1.0f;
            outlineShapePath.Stroke();
        }

    }
}
