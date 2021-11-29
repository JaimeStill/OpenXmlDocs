using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ImageMagick;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace OpenXmlDocs.Extensions
{
    public static class Imager
    {
        public static Drawing InitImage(this MainDocumentPart main, string path, int maxSize = 128)
        {
            if (File.Exists(path))
            {
                var file = new FileInfo(path);
                var imagePart = LoadImageIntoDoc(main, file);
                var imageSize = CalcImageSize(file);

                var image = EmbedImage(
                    main.GetIdOfPart(imagePart),
                    file.Name,
                    imageSize.width,
                    imageSize.height
                );

                return image;
            }
            else throw new FileNotFoundException($"{path} was not found");
        }

        static ImagePart LoadImageIntoDoc(MainDocumentPart main, FileInfo file)
        {
            var imagePart = main.AddImagePart(GetImagePartType(file.Extension));
            using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            imagePart.FeedData(stream);
            
            return imagePart;
        }

        static ImagePartType GetImagePartType(string ext) => ext.ToLower() switch
        {
            ".bmp"  => ImagePartType.Bmp,
            ".emf"  => ImagePartType.Emf,
            ".ico"  => ImagePartType.Icon,
            ".jpg"  => ImagePartType.Jpeg,
            ".jpeg" => ImagePartType.Jpeg,
            ".pcx"  => ImagePartType.Pcx,
            ".png"  => ImagePartType.Png,
            ".svg"  => ImagePartType.Svg,
            ".tiff" => ImagePartType.Tiff,
            ".wmf"  => ImagePartType.Wmf,
            _ => throw new NotSupportedException($"{ext} is not supported")
        };

        static (long width, long height) CalcImageSize(FileInfo file, int maxSize = 128)
        {
            const int emuRatio = 9525;

            var info = new MagickImageInfo(file.FullName);

            var scaled = ScaleImageDimensions(
                (float)info.Width,
                (float)info.Height,
                maxSize
            );

            scaled.width *= emuRatio;
            scaled.height *= emuRatio;

            return ((int)scaled.width, (int)scaled.height);
        }

        static (float width, float height) ScaleImageDimensions(float width, float height, int maxSize)
        {
            if (width > maxSize || height > maxSize)
            {
                if (width > height)
                {
                    var ratio = height / width;
                    width = maxSize;
                    height = maxSize * ratio;
                }
                else
                {
                    var ratio = width / height;
                    height = maxSize;
                    width = maxSize * ratio;
                }
            }

            return (width, height);
        }

        static Drawing EmbedImage(string embed, string name, long width, long height)
        {
            var element = new Drawing(
                new DW.Inline(
                    new DW.Extent() { Cx = width, Cy = height },
                    new DW.EffectExtent()
                    {
                        LeftEdge = 0L,
                        TopEdge = 0L,
                        RightEdge = 0L,
                        BottomEdge = 0L
                    },
                    new DW.DocProperties()
                    {
                        Id = (UInt32Value)1U,
                        Name = name
                    },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                        new A.GraphicFrameLocks() { NoChangeAspect = true }
                    ),
                    new A.Graphic(
                        new A.GraphicData(
                            new PIC.Picture(
                                new PIC.NonVisualPictureProperties(
                                    new PIC.NonVisualDrawingProperties()
                                    {
                                        Id = (UInt32Value)0U,
                                        Name = name
                                    },
                                    new PIC.NonVisualPictureDrawingProperties()
                                ),
                                new PIC.BlipFill(
                                    new A.Blip(
                                        new A.BlipExtensionList(
                                            new A.BlipExtension()
                                            {
                                                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                            }
                                        )
                                    )
                                    {
                                        Embed = embed,
                                        CompressionState = A.BlipCompressionValues.None
                                    },
                                    new A.Stretch(new A.FillRectangle())
                                ),
                                new PIC.ShapeProperties(
                                    new A.Transform2D(
                                        new A.Offset() { X = 0L, Y = 0L },
                                        new A.Extents() { Cx = width, Cy = height }
                                    ),
                                    new A.PresetGeometry(
                                        new A.AdjustValueList()
                                    )
                                    {
                                        Preset = A.ShapeTypeValues.Rectangle
                                    }
                                )
                            )
                        )
                        {
                            Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                        }
                    )
                )
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U,
                    EditId = "50D07946",

                }
            );

            return element;
        }
    }
}