using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EasyTool.Extension
{
    /// <summary>
    /// 扩展HttpClient中一些缺少的请求方式
    /// </summary>
    public static class HttpClientExtension
    {
        private const string NetErrorMessage = "网络异常";

        #region 标准的Http请求扩展

        /// <summary>
        /// Post请求，并将返回Json结果反序列化对象
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="value">请求内容</param>
        /// <param name="options">请求参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<TRsp?> PostFromJsonAsync<TRsp>(this HttpClient client, string requestUri, object value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
            TRsp? rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
            return rsp;
        }

        /// <summary>
        /// Put请求，并将返回Json结果反序列化对象
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="value">请求内容</param>
        /// <param name="options">请求参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<TRsp?> PutFromJsonAsync<TRsp>(this HttpClient client, string requestUri, object value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.PutAsJsonAsync(requestUri, value, options, cancellationToken);
            TRsp? rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
            return rsp;
        }

        /// <summary>
        /// Delete请求，并将返回Json结果反序列化对象
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<TRsp?> DeleteFromJsonAsync<TRsp>(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.DeleteAsync(requestUri, cancellationToken);
            TRsp? rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
            return rsp;
        }

        #endregion

        #region 带约定Result的Http请求

        /// <summary>
        /// Get请求，并转换成Result结构
        /// </summary>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="options">请求参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<Result> GetResultAsync(this HttpClient client, string requestUri, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await client.GetFromJsonAsync<Result>(requestUri, options, cancellationToken);
                if (result == null)
                    return new Result(NetErrorMessage, false);

                return result;
            }
            catch (Exception ex)
            {
                return new Result(ex.Message, false);
            }
        }

        /// <summary>
        /// Get请求，并转换成Result<TRsp>结构
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="options">请求参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<Result<TRsp>> GetResultAsync<TRsp>(this HttpClient client, string requestUri, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await client.GetFromJsonAsync<Result<TRsp>>(requestUri, options, cancellationToken);
                if (result == null)
                    return new Result<TRsp>(NetErrorMessage, false);

                return result;
            }
            catch (Exception ex)
            {
                return new Result<TRsp>(ex.Message, false);
            }
        }

        /// <summary>
        /// Post请求，并转换成Result结构
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="value">请求内容</param>
        /// <param name="options">请求参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<Result> PostResultAsync(this HttpClient client, string requestUri, object value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {

                var httpResponse = await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
                if (httpResponse.IsSuccessStatusCode)
                {
                    Result result = await httpResponse.Content.ReadFromJsonAsync<Result>() ?? new Result("数据异常", false);
                    return result;
                }
                else
                {
                    return new Result($"{(int)httpResponse.StatusCode} {httpResponse.ReasonPhrase}", false);
                }
            }
            catch (Exception ex)
            {
                return new Result(ex.Message, false);
            }
        }

        /// <summary>
        /// Post请求，并转换成Result<TRsp>结构
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="value">请求内容</param>
        /// <param name="options">请求参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<Result<TRsp>> PostResultAsync<TRsp>(this HttpClient client, string requestUri, object value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpResponse = await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);

                if (httpResponse.IsSuccessStatusCode)
                {
                    Result<TRsp> result = await httpResponse.Content.ReadFromJsonAsync<Result<TRsp>>() ?? new Result<TRsp>("数据异常", false);
                    return result;
                }
                else
                {
                    return new Result<TRsp>($"{(int)httpResponse.StatusCode} {httpResponse.ReasonPhrase}", false);
                }
            }
            catch (Exception ex)
            {
                return new Result<TRsp>(ex.Message, false);
            }
        }

        /// <summary>
        /// Delete请求，并转换成Result结构
        /// </summary>
        /// <param name="client">HttpClient对象</param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task<Result> DeleteResultAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.DeleteAsync(requestUri, cancellationToken);
            Result rsp = await httpResponse.Content.ReadFromJsonAsync<Result>() ?? new Result(NetErrorMessage, false);
            return rsp;
        }

        #endregion


    }


}

