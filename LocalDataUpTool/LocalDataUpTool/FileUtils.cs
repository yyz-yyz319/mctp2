using System;
using System.IO;

namespace LocalDataUpTool
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// 查找data目录
        /// </summary>
        /// <returns>data目录路径，如果不存在返回空字符串</returns>
        public static string FindDataDirectory()
        {
            // 从应用程序基目录向上遍历，找到LocalDataUpTool目录
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectDir = baseDir;
            for (int i = 0; i < 10; i++)
            {
                if (Path.GetFileName(projectDir) == "LocalDataUpTool")
                {
                    break;
                }
                string parentDir = Directory.GetParent(projectDir)?.FullName ?? string.Empty;
                if (string.IsNullOrEmpty(parentDir))
                {
                    break;
                }
                projectDir = parentDir;
            }

            // 构建data目录路径
            string dataPath = Path.Combine(projectDir, "data");

            // 检查data目录是否存在
            if (Directory.Exists(dataPath))
            {
                return dataPath;
            }
            return string.Empty;
        }
    }
}