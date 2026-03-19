using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LocalDataUpTool
{
    /// <summary>
    /// Txt文件转Json文件的转换器类
    /// </summary>
    public class TxtToJsonConverter
    {
        /// <summary>
        /// 将data文件夹中的所有txt文件转换为json文件
        /// </summary>
        /// <returns>转换结果消息</returns>
        public static string ConvertTxtToJson()
        {
            try
            {
                // 查找data目录路径
                string dataPath = FileUtils.FindDataDirectory();
                
                // 检查data目录是否存在
                if (!Directory.Exists(dataPath))
                {
                    return "data目录不存在";
                }
                
                // 检查data目录中是否有txt文件
                string[] txtFiles = Directory.GetFiles(dataPath, "*.txt");
                if (txtFiles.Length == 0)
                {
                    return "data目录下没有txt文件";
                }
                
                // 执行转换
                return ConvertTxtToJsonAtPath(dataPath);
            }
            catch (Exception ex)
            {
                return $"转换失败: {ex.Message}";
            }
        }
        
        /// <summary>
        /// 在指定路径执行txt转json的转换
        /// </summary>
        /// <param name="dataFolderPath">data文件夹路径</param>
        /// <returns>转换结果消息</returns>
        private static string ConvertTxtToJsonAtPath(string dataFolderPath)
        {
            try
            {
                // 获取data文件夹中的所有txt文件
                string[] txtFiles = Directory.GetFiles(dataFolderPath, "*.txt");

                if (txtFiles.Length == 0)
                {
                    return "data目录下没有txt文件";
                }

                int convertedCount = 0;

                // 遍历每个txt文件进行转换,txtFile变量会自带完整的文件路径。
                foreach (string txtFile in txtFiles)
                {
                    // 读取txt文件内容
                    string[] lines = File.ReadAllLines(txtFile);

                    if (lines.Length < 4) // 至少需要空白行、列名、单位和一行数据
                    {
                        continue;
                    }

                    // 提取列名（第二行）
                    string[] columnNames = lines[1].Split('\t').Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();

                    // 提取单位（第三行）
                    string[] units = lines[2].Split('\t').Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();

                    // 存储所有数据行
                    List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

                    // 从第四行开始处理数据
                    for (int i = 3; i < lines.Length; i++)
                    {
                        string line = lines[i];

                        // 跳过空行和列名行（文件中可能有重复的列名行）
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("Time"))
                        {
                            continue;
                        }

                        // 分割数据
                        string[] values = line.Split('\t').Where(v => !string.IsNullOrWhiteSpace(v)).ToArray();

                        if (values.Length >= columnNames.Length)
                        {
                            Dictionary<string, object> dataItem = new Dictionary<string, object>();

                            for (int j = 0; j < columnNames.Length; j++)
                            {
                                string columnName = columnNames[j];
                                string value = values[j];

                                // 处理时间格式(去除时间开始前的单引号)
                                if (j == 0 && value.StartsWith("'"))
                                {
                                    dataItem[columnName] = value.TrimStart('\'');
                                }
                                // 尝试转换为数值类型
                                else if (double.TryParse(value, out double numericValue))
                                {
                                    dataItem[columnName] = numericValue;
                                }
                                // 保留字符串类型
                                else
                                {
                                    dataItem[columnName] = value;
                                }
                            }
                            dataList.Add(dataItem);
                        }
                    }

                    // 生成json文件路径
                    string jsonFilePath = Path.ChangeExtension(txtFile, ".json");

                    // 将数据转换为json并保存
                    string jsonContent = JsonSerializer.Serialize(dataList, new JsonSerializerOptions
                    {
                        WriteIndented = true, // 美化格式
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });

                    File.WriteAllText(jsonFilePath, jsonContent);
                    convertedCount++;
                }

                return $"成功转换 {convertedCount} 个文件";
            }
            catch (Exception ex)
            {
                return $"转换失败: {ex.Message}";
            }
        }
    }
}