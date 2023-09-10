#if NET6_0_OR_GREATER

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace EasyTool.Extension
{

    public static class HttpClientExtension
    {
        #region 带有标准返回的Http请求

        public static async Task<Result> GetResultAsync(this HttpClient client, string requestUri, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await client.GetFromJsonAsync<Result>(requestUri, options, cancellationToken);
                if (result == null)
                    return new Result("网络异常", false);

                return result;
            }
            catch (Exception ex)
            {
                return new Result(ex.Message, false);
            }
        }

        public static async Task<Result<TRsp>> GetResultAsync<TRsp>(this HttpClient client, string requestUri, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await client.GetFromJsonAsync<Result<TRsp>>(requestUri, options, cancellationToken);
                if (result == null)
                    return new Result<TRsp>("网络异常", false);

                return result;
            }
            catch (Exception ex)
            {
                return new Result<TRsp>(ex.Message, false);
            }
        }

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

        public static async Task<Result> DeleteResultAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.DeleteAsync(requestUri, cancellationToken);
            Result rsp = await httpResponse.Content.ReadFromJsonAsync<Result>() ?? new Result("网络异常", false);
            return rsp;
        }

        #endregion


        #region 标准的Http请求扩展

        public static async Task<TRsp> PostFromJsonAsync<TRsp>(this HttpClient client, string requestUri, object value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
            return rsp;
        }

        public static async Task<TRsp> PutFromJsonAsync<TRsp>(this HttpClient client, string requestUri, object value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.PutAsJsonAsync(requestUri, value, options, cancellationToken);
            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
            return rsp;
        }

        public static async Task<TRsp> DeleteFromJsonAsync<TRsp>(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.DeleteAsync(requestUri, cancellationToken);
            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
            return rsp;
        }

        #endregion
    }


}

#endif