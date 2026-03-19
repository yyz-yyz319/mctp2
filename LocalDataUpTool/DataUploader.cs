using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalDataUpTool
{
    /// <summary>
    /// 数据上传类
    /// </summary>
    public class DataUploader
    {
        /// <summary>
        /// 上传数据到后端服务器
        /// </summary>
        /// <returns>上传结果</returns>
        public static async Task<string> UploadData()
        {
            try
            {
                // 从配置文件中读取参数
                var config = ConfigManager.GetConfig();
                string tdUserId = config.Info.tdUserId;
                string tdDeviceId = config.Info.tdDeviceId;
                string tdChannelId = config.Info.tdChannelId;

                // 验证参数
                if (string.IsNullOrEmpty(tdUserId) || string.IsNullOrEmpty(tdDeviceId) || string.IsNullOrEmpty(tdChannelId))
                {
                    return "配置文件中缺少必要参数";
                }

                // 查找data目录
                string dataPath = FileUtils.FindDataDirectory();
                if (string.IsNullOrEmpty(dataPath))
                {
                    return "data目录不存在";
                }

                // 获取data目录下的所有json文件
                string[] jsonFiles = Directory.GetFiles(dataPath, "*.json");
                if (jsonFiles.Length == 0)
                {
                    return "data目录下没有json文件";
                }

                // 读取第一个json文件的内容
                string jsonContent = File.ReadAllText(jsonFiles[0]);
                var jsonData = JsonSerializer.Deserialize<object>(jsonContent) ?? new { };

                // 构建新的json数据
                var requestData = new Dictionary<string, object>
                {
                    { "tdUserId", tdUserId },
                    { "tdDeviceId", tdDeviceId },
                    { "tdChannelId", tdChannelId },
                    { "data", jsonData }
                };

                // 转换为json字符串
                string requestJson = JsonSerializer.Serialize(requestData, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                // 获取后端URL
                string backendUrl = config.Backend.BaseUrl + config.Backend.ApiPath;

                // 发送post请求
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(backendUrl, content);

                    // 处理响应
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return $"上传成功: {responseContent}";
                    }
                    else
                    {
                        return $"上传失败: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"上传失败: {ex.Message}";
            }
        }
    }
}