using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 对象
    /// </summary>
    public class DanceObject : IDisposable, IDanceJsonObject
    {
        // ===================================================================================================
        // **** Constructor ****
        // ===================================================================================================

        /// <summary>
        /// 析构
        /// </summary>
        ~DanceObject()
        {
            this.Destroy(false);
        }

        // ===================================================================================================
        // **** Static Field ****
        // ===================================================================================================

        /// <summary>
        /// 日志
        /// </summary>
        protected readonly static ILog log = LogManager.GetLogger(typeof(DanceObject));

        /// <summary>
        /// Json对象转化器
        /// </summary>
        private readonly static DanceJsonObjectConverter JsonObjectConverter = new();

        /// <summary>
        /// 调度检测是否可用
        /// </summary>
        public static Func<bool>? DispatcherCheckAccess { get; set; }

        /// <summary>
        /// 调度执行
        /// </summary>
        public static Action<Action>? DispatcherInvoke { get; set; }

        /// <summary>
        /// 日志执行
        /// </summary>
        public static Action<string>? RecordInvoke { get; set; }

        // ===================================================================================================
        // **** Static Function ****
        // ===================================================================================================

        /// <summary>
        /// 从json字符串创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象</returns>
        public static T? FromJsonString<T>(string json) where T : DanceObject
        {
            return JsonConvert.DeserializeObject<T>(json, JsonObjectConverter);
        }

        /// <summary>
        /// 从json文件创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码默认为<see cref="Encoding.UTF8"/></param>
        /// <returns>对象</returns>
        public static T? FromJsonFile<T>(string path, Encoding? encoding = null) where T : DanceObject
        {
            if (!File.Exists(path))
                return default;

            using StreamReader sr = new(path, encoding ?? Encoding.UTF8);
            string json = sr.ReadToEnd();

            return FromJsonString<T>(json);
        }

        // ===================================================================================================
        // **** Prioerty ****
        // ===================================================================================================

        /// <summary>
        /// 对象类型
        /// </summary>
        public string DanceObjectType => this.GetType().AssemblyQualifiedName ?? string.Empty;

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 是否已经释放
        /// </summary>
        private bool IsDisposed = false;

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 转化为Json字符串
        /// </summary>
        /// <param name="formatting"><inheritdoc cref="Formatting"/></param>
        /// <returns>Json字符串</returns>
        public virtual string ToJsonString(Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// 保存至文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="formatting"><inheritdoc cref="Formatting"/></param>
        /// <param name="encoding">编码默认为<see cref="Encoding.UTF8"/></param>
        public void ToJsonFile(string path, Formatting formatting = Formatting.Indented, Encoding? encoding = null)
        {
            using StreamWriter sw = new(path, false, encoding ?? Encoding.UTF8);
            string json = this.ToJsonString(formatting);
            sw.Write(json);
            sw.Flush();
            sw.Dispose();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Destroy(true);
            GC.SuppressFinalize(this);
        }

        // ===================================================================================================
        // **** Protected Function ****
        // ===================================================================================================

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing">是否执行销毁</param>
        protected void Destroy(bool disposing)
        {
            if (this.IsDisposed)
                return;

            if (disposing)
            {
                try
                {
                    this.Destroy();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }

            this.IsDisposed = true;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected virtual void Destroy()
        {

        }
    }
}
