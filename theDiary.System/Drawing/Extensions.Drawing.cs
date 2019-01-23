using System.IO;

namespace System.Drawing
{
    /// <summary>
    /// Extensions containing methods used for image conversions.
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// Creates an <see cref="Image"/> from a Base64 <see cref="String"/>.
        /// </summary>
        /// <param name="imageBase">The <see cref="String"/> instance used to create the <see cref="Image"/>.</param>
        /// <returns>An instance of <see cref="Image"/>.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="imageBase"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static Image FromBase64(this string imageBase)
        {
            if (imageBase.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException(nameof(imageBase));

            using (MemoryStream stream = new MemoryStream())
            {
                byte[] data = Convert.FromBase64String(imageBase);
                stream.Write(data, 0, data.Length - 1);
                stream.Flush();
                return Image.FromStream(stream);
            }                
        }

        /// <summary>
        /// Converts an <see cref="Image"/> to a Base64 <see cref="String"/>.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> instance to convert.</param>
        /// <returns>A Base64 <see cref="String"/> instance of the image.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="image"/> is <c>Null</c>.</exception>
        public static string ToBase64(this Image image)
        {
            if (image.IsNull())
                throw new ArgumentNullException(nameof(image));

            return image.ToBase64(new System.Drawing.Imaging.ImageFormat(image.RawFormat.Guid));
        }

        /// <summary>
        /// Converts an <see cref="Image"/> to a Base64 <see cref="String"/>.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> instance to convert.</param>
        /// <param name="format">A value of <see cref="System.Drawing.Imaging.ImageFormat"/> indicating the format of the <paramref name="image"/>.</param>
        /// <returns>A Base64 <see cref="String"/> instance of the image.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="image"/> is <c>Null</c>.</exception>
        public static string ToBase64(this Image image, System.Drawing.Imaging.ImageFormat format)
        {
            if (image.IsNull())
                throw new ArgumentNullException(nameof(image));

            using (MemoryStream stream = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(stream, format);
                byte[] imageBytes = stream.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// Converts an <see cref="Image"/> to a basic SVG format.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> instance to convert.</param>
        /// <returns>A <see cref="String"/> containing formatted SVG image.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="image"/> is <c>Null</c>.</exception>
        public static string ToSVG(this Image image)
        {
            if (image.IsNull())
                throw new ArgumentNullException(nameof(image));

            string base64 = image.ToBase64();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            result.AppendLine($@"<svg version=""1.1"" id=""Layer_1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" x=""0px"" y=""0px"" viewBox=""0 0 {image.Width} {image.Height}"" style=""enable-background:new 0 0 {image.Width} {image.Height};"" xml:space=""preserve"">");
            result.AppendLine($@"<image style=""overflow:visible;"" width=""{image.Width}"" height=""{image.Height}"" xlink:href=""data:image/png;base64,{base64}"" ></image>");
            result.AppendLine("</svg>");

            return result.ToString();
        }

        /// <summary>
        /// Converts an <see cref="Image"/> to a basic SVG format.
        /// </summary>
        /// <param name="image">To <see cref="Image"/> instance to convert.</param>
        /// <param name="stream">The <see cref="Stream"/> to write the content to.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="image"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        public static void ToSVG(this Image image, Stream stream)
        {
            if (stream.IsNull())
                throw new ArgumentNullException(nameof(stream));

            try
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string svgContent = image.ToSVG();
                    writer.Write(svgContent);
                    writer.Flush();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
