using Docnet.Core;
using Docnet.Core.Models;
using Docnet.Core.Converters;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace ManagementSystem.Common.Helper
{
    public class PdfHelper
    {
        /// <summary>
        /// 将PDF转换为图片
        /// </summary>
        /// <param name="inputPath">输入地址</param>
        /// <param name="outputFolder">输出地址</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool PdfToImage(string inputPath, string outputPath)
        {
            bool result = false;
            try
            {
                using (var docReader = DocLib.Instance.GetDocReader(inputPath, new PageDimensions(1080, 1920)))
                {
                    int num = docReader.GetPageCount();
                    for (int pageNumber = 0; pageNumber < num; pageNumber++)
                    {
                        using (var pageReader = docReader.GetPageReader(pageNumber))
                        {
                            var rawBytes = pageReader.GetImage(new NaiveTransparencyRemover(255, 255, 255));
                            var width = pageReader.GetPageWidth();
                            var height = pageReader.GetPageHeight();
                            // 加载图像数据
                            using (var image = Image.LoadPixelData<Rgba32>(rawBytes, width, height))
                            {
                                // 保存图像
                                image.Save(outputPath);
                            }
                        }
                    }
                };
                result = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
