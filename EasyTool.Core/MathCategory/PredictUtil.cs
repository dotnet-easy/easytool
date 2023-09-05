using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 预测数据类
    /// </summary>
    public class PredictUtil
    {
        /// <summary>
        /// 线性回归预测
        /// </summary>
        /// <param name="inputData">输入数据</param>
        /// <param name="outputData">输出数据</param>
        /// <returns>预测结果</returns>
        public static double LinearRegressionPredict(double[] inputData, double[] outputData)
        {
            // 获取输入数据的数量
            int n = inputData.Length;

            // 计算平均数
            double inputMean = inputData.Sum() / n;
            double outputMean = outputData.Sum() / n;

            // 计算分子和分母
            double numerator = 0;
            double denominator = 0;
            for (int i = 0; i < n; i++)
            {
                numerator += (inputData[i] - inputMean) * (outputData[i] - outputMean);
                denominator += Math.Pow(inputData[i] - inputMean, 2);
            }

            // 计算斜率
            double slope = numerator / denominator;

            // 计算截距
            double intercept = outputMean - slope * inputMean;

            // 返回预测结果
            return slope * inputData.Last() + intercept;
        }

        /// <summary>
        /// 构建多项式矩阵
        /// </summary>
        /// <param name="inputData">输入数据</param>
        /// <param name="degree">多项式的次数</param>
        /// <returns>多项式矩阵</returns>
        private static double[][] BuildMatrix(double[] inputData, int degree)
        {
            int n = inputData.Length;
            double[][] matrix = new double[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = new double[degree + 1];
                for (int j = 0; j < degree + 1; j++)
                {
                    matrix[i][j] = Math.Pow(inputData[i], j);
                }
            }

            return matrix;
        }

        /// <summary>
        /// k近邻预测
        /// </summary>
        /// <param name="inputData">输入数据</param>
        /// <param name="outputData">输出数据</param>
        /// <param name="k">邻居数量</param>
        /// <returns>预测结果</returns>
        public static double KNNPredict(double[] inputData, double[] outputData, int k)
        {
            // 计算距离数组
            double[] distances = new double[inputData.Length];
            for (int i = 0; i < inputData.Length; i++)
            {
                distances[i] = Math.Abs(inputData[i] - inputData.Last());
            }

            // 排序距离数组
            int[] sortedIndexes = Enumerable.Range(0, distances.Length)
                .OrderBy(i => distances[i])
                .ToArray();

            // 计算预测结果
            double result = 0;
            for (int i = 0; i < k; i++)
            {
                result += outputData[sortedIndexes[i]];
            }

            return result / k;
        }

        ///// <summary>
        ///// 多项式回归预测
        ///// </summary>
        ///// <param name="inputData">输入数据</param>
        ///// <param name="outputData">输出数据</param>
        ///// <param name="degree">多项式的次数</param>
        ///// <returns>预测结果</returns>
        //public static double PolynomialRegressionPredict(double[] inputData, double[] outputData, int degree)
        //{
        //    // 创建多项式矩阵
        //    Matrix<double> X = DenseMatrix.OfArray(BuildMatrix(inputData, degree));
        //    Matrix<double> Y = DenseMatrix.OfColumnVectors(new[]
        //    {
        //        DenseVector.OfArray(outputData)
        //    });

        //    // 计算系数
        //    Matrix<double> coefficients = (X.Transpose() * X).Inverse() * X.Transpose() * Y;

        //    // 计算预测结果
        //    double result = 0;
        //    for (int i = 0; i < degree + 1; i++)
        //    {
        //        result += coefficients[i, 0] * Math.Pow(inputData.Last(), i);
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 决策树回归预测
        ///// </summary>
        ///// <param name="inputData">输入数据</param>
        ///// <param name="outputData">输出数据</param>
        ///// <returns>预测结果</returns>
        //public static double DecisionTreeRegressionPredict(double[] inputData, double[] outputData)
        //{
        //    var tree = new DecisionTreeRegression(inputData, outputData);
        //    return tree.Compute(inputData.Last());
        //}

        ///// <summary>
        ///// 随机森林回归预测
        ///// </summary>
        ///// <param name="inputData">输入数据</param>
        ///// <param name="outputData">输出数据</param>
        ///// <param name="trees">决策树数量</param>
        ///// <returns>预测结果</returns>
        //public static double RandomForestRegressionPredict(double[] inputData, double[] outputData, int trees)
        //{
        //    var forest = new RandomForestRegression(inputData, outputData, trees);
        //    return forest.Compute(inputData.Last());
        //}

        ///// <summary>
        ///// 梯度提升树回归预测
        ///// </summary>
        ///// <param name="inputData">输入数据</param>
        ///// <param name="outputData">输出数据</param>
        ///// <returns>预测结果</returns>
        //public static double GradientBoostingRegressionPredict(double[] inputData, double[] outputData)
        //{
        //    var boosting = new GradientBoostingRegression(inputData, outputData);
        //    return boosting.Compute(inputData.Last());
        //}
    }
}
