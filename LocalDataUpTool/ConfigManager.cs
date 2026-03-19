using System;
using System.IO;
using System.Text.Json;

namespace LocalDataUpTool
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 配置模型
        /// </summary>
         public class ConfigModel
         {
            public BackendConfig Backend { get; set; }
            public InfoConfig Info { get; set; }
         }

         /// <summary>
         /// 信息配置
         /// </summary>
         public class InfoConfig
         {
             public string tdUserId { get; set; }
             public string tdDeviceId { get; set; }
             public string tdChannelId { get; set; }
             public string uniqueIdentification { get; set; }
         }

        /// <summary>
        /// 后端配置
        /// </summary>
        public class BackendConfig
        {
            public string BaseUrl { get; set; }
            public string ApiPath { get; set; }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>配置对象</returns>
        public static ConfigModel GetConfig()
        {
            try
            {
                // 查找appsettings.json文件
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string configPath = Path.Combine(baseDir, "appsettings.json");

                // 如果在当前目录找不到，向上遍历查找
                if (!File.Exists(configPath))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string parentDir = Directory.GetParent(baseDir)?.FullName;
                        if (string.IsNullOrEmpty(parentDir))
                        {
                            break;
                        }
                        configPath = Path.Combine(parentDir, "appsettings.json");
                        if (File.Exists(configPath))
                        {
                            break;
                        }
                        baseDir = parentDir;
                    }
                }

                // 读取配置文件
                if (File.Exists(configPath))
                {
                    string configContent = File.ReadAllText(configPath);
                    return JsonSerializer.Deserialize<ConfigModel>(configContent);
                }

                // 返回默认配置
                return new ConfigModel
                {
                    Backend = new BackendConfig
                    {
                        BaseUrl = "http://localhost:8080",
                        ApiPath = "/data/upload"
                    },
                    Info = new InfoConfig
                    {
                        tdUserId = "NULL",
                        tdDeviceId = "NULL",
                        tdChannelId = "NULL"
                    }
                };
            }
            catch (Exception)
            {
                // 出错时返回默认配置
                return new ConfigModel
                {
                    Backend = new BackendConfig
                    {
                        BaseUrl = "http://localhost:8080",
                        ApiPath = "/data/upload"
                    },
                    Info = new InfoConfig
                    {
                        tdUserId = "NULL",
                        tdDeviceId = "NULL",
                        tdChannelId = "NULL"
                    }
                };
            }
        }
    }
}