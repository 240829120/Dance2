using DevExpress.Utils.Svg;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Internal;
using DevExpress.Xpf.Core.Native;
using DevExpress.XtraCharts.Native;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dance.Framework
{
    /// <summary>
    /// 缓存管理器扩展
    /// </summary>
    public static class DanceCacheManagerExpansion
    {
        /// <summary>
        /// 图片缓存池键
        /// </summary>
        public static readonly object IMAGE_CACHE_POOL_KEY = new();

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="manager">缓存管理器</param>
        /// <param name="uri">图片地址</param>
        /// <returns>图片</returns>
        public static ImageSource? GetImage(this IDanceCacheManager manager, Uri uri)
        {
            DanceCachePool pool = manager.GetCachePool(IMAGE_CACHE_POOL_KEY);

            if (pool.GetCache(uri) is ImageSource cache)
                return cache;

            string str = uri.ToString();
            ImageSource? source;
            if (str.EndsWith(".svg", StringComparison.InvariantCulture))
            {
                source = GetImageSourceSvg(uri);
            }
            else
            {
                source = GetImageSource(uri);
            }
            pool.SetCache(DanceCacheType.Strong, uri, source);

            return source;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="manager">缓存管理器</param>
        /// <param name="uri">地址</param>
        /// <returns>图片</returns>
        public static ImageSource? GetImage(this IDanceCacheManager manager, string uri)
        {
            return GetImage(manager, new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// 获取普通图片
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>图片</returns>
        private static BitmapImage? GetImageSource(Uri uri)
        {
            return new BitmapImage(uri);
        }

        /// <summary>
        /// 获取SVG图片
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>图片</returns>
        private static ImageSource? GetImageSourceSvg(Uri uri)
        {
            SvgImage orCreate = SvgImageHelper.GetOrCreate(uri, SvgImageHelper.CreateImage);
            return WpfSvgRenderer.CreateImageSource(orCreate, null, null, null, null, true);
        }
    }
}